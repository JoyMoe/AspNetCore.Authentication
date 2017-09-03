using System;
using Microsoft.AspNetCore.Authentication;
using JoyMoe.AspNetCore.Authentication.OpenID;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class OpenIdExtensions
	{
		public static AuthenticationBuilder AddOpenId(this AuthenticationBuilder builder)
			=> builder.AddOpenId(OpenIdDefaults.AuthenticationScheme, _ => { });

		public static AuthenticationBuilder AddOpenId(this AuthenticationBuilder builder, Action<OpenIdOptions> configuration)
		{
			var options = new OpenIdOptions();
            configuration(options);

			return builder.AddOpenId(options.AuthenticationScheme, configuration);
		}

		public static AuthenticationBuilder AddOpenId(this AuthenticationBuilder builder, string authenticationScheme, Action<OpenIdOptions> configuration)
		{
			var options = new OpenIdOptions();
            configuration(options);

			return builder.AddOpenId(authenticationScheme, options.DisplayName, configuration);
		}

		public static AuthenticationBuilder AddOpenId(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<OpenIdOptions> configuration)
		    => builder.AddScheme<OpenIdOptions, OpenIdHandler<OpenIdOptions>>(authenticationScheme, displayName, configuration);
	}
}
