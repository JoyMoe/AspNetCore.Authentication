using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace JoyMoe.AspNetCore.Authentication.QQ
{
    public class QQHandler : OAuthHandler<QQOptions>
    {
		public QQHandler(IOptionsMonitor<QQOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        { }

		protected override async Task<AuthenticationTicket> CreateTicketAsync(
			ClaimsIdentity identity,
			AuthenticationProperties properties,
			OAuthTokenResponse tokens)
		{
            var openid = await GetUserOpenIdAsync(tokens);
            var queryString = new Dictionary<string, string>()
            {
                {"oauth_consumer_key",Options.ClientId },
                {"access_token",tokens.AccessToken },
                {"openid",openid },
            };

            var endpoint = QueryHelpers.AddQueryString(Options.UserInformationEndpoint, queryString);
            var response = await Backchannel.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode)
            {
                Logger.LogError($"An error occurred while retrieving the user information: the remote server returned a " +
                    $"{response.StatusCode} response with the following payload: {await response.Content.ReadAsStringAsync()}.");

                throw new HttpRequestException("An error occurred when retrieving user information.");
            }

            var payload = JObject.Parse(await response.Content.ReadAsStringAsync());
            var ret = payload.Value<int>("ret");
            if (ret != 0)
            {
                var msg = payload.Value<string>("msg");
                throw new HttpRequestException($"An error occurred when retrieving user information. code: {ret}, msg: {msg}");
            }

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, openid, ClaimValueTypes.String, Options.ClaimsIssuer));

            identity.AddClaim(new Claim(ClaimTypes.Name, QQHelper.GetNickname(payload), ClaimValueTypes.String, Options.ClaimsIssuer));
            identity.AddClaim(new Claim(ClaimTypes.Gender, QQHelper.GetGender(payload), ClaimValueTypes.String, Options.ClaimsIssuer));

            var figureurl = QQHelper.GetFigureUrl(payload);
            if (!string.IsNullOrEmpty(figureurl))
            {
                identity.AddClaim(new Claim("urn:qq:figureurl", figureurl, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var figureurl_1 = QQHelper.GetFigureUrl50X50(payload);
            if (!string.IsNullOrEmpty(figureurl_1))
            {
                identity.AddClaim(new Claim("urn:qq:figureurl_1", figureurl_1, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var figureurl_2 = QQHelper.GetFigureUrl100X100(payload);
            if (!string.IsNullOrEmpty(figureurl_2))
            {
                identity.AddClaim(new Claim("urn:qq:figureurl_2", figureurl_2, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var figureurl_qq_1 = QQHelper.GetFigureQQUrl40X40(payload);
            if (!string.IsNullOrEmpty(figureurl_qq_1))
            {
                identity.AddClaim(new Claim("urn:qq:figureurl_qq_1", figureurl_qq_1, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var figureurl_qq_2 = QQHelper.GetFigureQQUrl100X100(payload);
            if (!string.IsNullOrEmpty(figureurl_qq_1))
            {
                identity.AddClaim(new Claim("urn:qq:figureurl_qq_2", figureurl_qq_2, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

			var context = new OAuthCreatingTicketContext(new ClaimsPrincipal(identity), properties, Context, Scheme, Options, Backchannel, tokens, payload);
			context.RunClaimActions();

			await Events.CreatingTicket(context);
			return new AuthenticationTicket(context.Principal, context.Properties, Scheme.Name);
        }

        protected override async Task<OAuthTokenResponse> ExchangeCodeAsync(string code, string redirectUri)
        {
            var queryString = new Dictionary<string, string>()
            {
                {"client_id",Options.ClientId},
                {"client_secret",Options.ClientSecret },
                {"redirect_uri",redirectUri },
                {"code",code },
                {"grant_type","authorization_code" },
            };
            var endpoint = QueryHelpers.AddQueryString(Options.TokenEndpoint, queryString);
            var response = await Backchannel.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode)
            {
                var error = "OAuth token endpoint failure: " + await response.Content.ReadAsStringAsync();
                return OAuthTokenResponse.Failed(new Exception(error));
            }
            var payload = JObject.FromObject(QueryHelpers.ParseQuery(await response.Content.ReadAsStringAsync()).ToDictionary(k => k.Key, k => k.Value.ToString()));
            return OAuthTokenResponse.Success(payload);
        }

        private async Task<string> GetUserOpenIdAsync(OAuthTokenResponse tokens)
        {
            var endpoint = QueryHelpers.AddQueryString(Options.OpenIdEndpoint, "access_token", tokens.AccessToken);
            var response = await Backchannel.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"An error occurred while retrieving the user openid.({response.StatusCode})");
            }
            var body = await response.Content.ReadAsStringAsync();
            var i = body.IndexOf("{");
            if (i > 0)
            {
                var j = body.LastIndexOf("}");
                body = body.Substring(i, j - i + 1);
            }
            var payload = JObject.Parse(body);
            var openid = payload.Value<string>("openid");
            if (string.IsNullOrEmpty(openid))
            {
                throw new HttpRequestException("An error occurred while retrieving the user openid. the openid is null.");
            }
            return openid;
        }

        protected override string FormatScope()
        {
            return string.Join(",", Options.Scope);
        }
    }
}
