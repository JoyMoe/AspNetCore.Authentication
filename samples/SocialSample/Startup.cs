using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace SocialSample
{
	/* Note all servers must use the same address and port because these are pre-registered with the various providers. */
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; set; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(o => o.LoginPath = new PathString("/login"))
				.AddWeibo(o => {
					o.AppKey = "515625315";
					o.AppSecret = "5444909dca07dcd9164c7921cb95af1a";
				})
				.AddOpenId(options =>
				{
					options.AuthenticationScheme = "StackExchange";
					options.DisplayName = "StackExchange";
					options.Authority = new Uri("https://openid.stackexchange.com/");
					options.CallbackPath = "/signin-stackexchange";
				});
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseDeveloperExceptionPage();

			app.UseAuthentication();

			// Choose an authentication type
			app.Map("/login", signinApp =>
			{
				signinApp.Run(async context =>
				{
					var authType = context.Request.Query["authscheme"];
					if (!string.IsNullOrEmpty(authType))
					{
						// By default the client will be redirect back to the URL that issued the challenge (/login?authtype=foo),
						// send them to the home page instead (/).
						await context.ChallengeAsync(authType, new AuthenticationProperties() { RedirectUri = "/" });
						return;
					}

					var response = context.Response;
					response.ContentType = "text/html";
					await response.WriteAsync("<html><body>");
					await response.WriteAsync("Choose an authentication scheme: <br>");
					var schemeProvider = context.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();
					foreach (var provider in await schemeProvider.GetAllSchemesAsync())
					{
						await response.WriteAsync("<a href=\"?authscheme=" + provider.Name + "\">" + (provider.DisplayName ?? "(suppressed)") + "</a><br>");
					}
					await response.WriteAsync("</body></html>");
				});
			});

			// Sign-out to remove the user cookie.
			app.Map("/logout", signoutApp =>
			{
				signoutApp.Run(async context =>
				{
					var response = context.Response;
					response.ContentType = "text/html";
					await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
					await response.WriteAsync("<html><body>");
					await response.WriteAsync("You have been logged out. Goodbye " + context.User.Identity.Name + "<br>");
					await response.WriteAsync("<a href=\"/\">Home</a>");
					await response.WriteAsync("</body></html>");
				});
			});

			// Display the remote error
			app.Map("/error", errorApp =>
			{
				errorApp.Run(async context =>
				{
					var response = context.Response;
					response.ContentType = "text/html";
					await response.WriteAsync("<html><body>");
					await response.WriteAsync("An remote failure has occurred: " + context.Request.Query["FailureMessage"] + "<br>");
					await response.WriteAsync("<a href=\"/\">Home</a>");
					await response.WriteAsync("</body></html>");
				});
			});


			app.Run(async context =>
			{
				// Setting DefaultAuthenticateScheme causes User to be set
				var user = context.User;

				// This is what [Authorize] calls
				// var user = await context.AuthenticateAsync();

				// This is what [Authorize(ActiveAuthenticationSchemes = MicrosoftAccountDefaults.AuthenticationScheme)] calls
				// var user = await context.AuthenticateAsync(MicrosoftAccountDefaults.AuthenticationScheme);

				// Deny anonymous request beyond this point.
				if (user == null || !user.Identities.Any(identity => identity.IsAuthenticated))
				{
					// This is what [Authorize] calls
					// The cookie middleware will handle this and redirect to /login
					await context.ChallengeAsync();

					// This is what [Authorize(ActiveAuthenticationSchemes = MicrosoftAccountDefaults.AuthenticationScheme)] calls
					// await context.ChallengeAsync(MicrosoftAccountDefaults.AuthenticationScheme);

					return;
				}

				// Display user information
				var response = context.Response;
				response.ContentType = "text/html";
				await response.WriteAsync("<html><body>");
				await response.WriteAsync("Hello " + (context.User.Identity.Name ?? "anonymous") + "<br>");
				foreach (var claim in context.User.Claims)
				{
					await response.WriteAsync(claim.Type + ": " + claim.Value + "<br>");
				}

				await response.WriteAsync("Tokens:<br>");

				await response.WriteAsync("Access Token: " + await context.GetTokenAsync("access_token") + "<br>");
				await response.WriteAsync("Refresh Token: " + await context.GetTokenAsync("refresh_token") + "<br>");
				await response.WriteAsync("Token Type: " + await context.GetTokenAsync("token_type") + "<br>");
				await response.WriteAsync("expires_at: " + await context.GetTokenAsync("expires_at") + "<br>");
				await response.WriteAsync("<a href=\"/logout\">Logout</a><br>");
				await response.WriteAsync("<a href=\"/refresh_token\">Refresh Token</a><br>");
				await response.WriteAsync("</body></html>");
			});
		}
	}
}

