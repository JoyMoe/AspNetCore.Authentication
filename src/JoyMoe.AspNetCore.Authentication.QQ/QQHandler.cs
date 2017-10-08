using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        protected override async Task<AuthenticationTicket> CreateTicketAsync( ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            var identifier = await GetUserIdentifierAsync(tokens);
            if (string.IsNullOrEmpty(identifier))
            {
                throw new HttpRequestException("An error occurred while retrieving the user identifier.");
            }

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, identifier, ClaimValueTypes.String, Options.ClaimsIssuer));

            var queryString = new Dictionary<string, string>()
            {
                {"oauth_consumer_key",Options.ClientId },
                {"access_token",tokens.AccessToken },
                {"openid",identifier },
            };

            var response = await Backchannel.PostAsync(Options.UserInformationEndpoint, new FormUrlEncodedContent(queryString));
            if (!response.IsSuccessStatusCode)
            {
                Logger.LogError($"An error occurred while retrieving the user information: the remote server returned a " +
                                $"{response.StatusCode} response with the following payload: {await response.Content.ReadAsStringAsync()}.");

                throw new HttpRequestException("An error occurred when retrieving user information.");
            }

            var payload = JObject.Parse(await response.Content.ReadAsStringAsync());

            var status = payload.Value<int>("ret");
            if (status != 0)
            {
                Logger.LogError($"An error occurred while retrieving the user information: the remote server returned a " +
                                $"{response.StatusCode} response with the following payload: {await response.Content.ReadAsStringAsync()}.");

                throw new HttpRequestException("An error occurred when retrieving user information.");
            }

            var principal = new ClaimsPrincipal(identity);
            var context = new OAuthCreatingTicketContext(principal, properties, Context, Scheme, Options, Backchannel, tokens, payload);
            context.RunClaimActions(payload);

            await Options.Events.CreatingTicket(context);

            return new AuthenticationTicket(context.Principal, context.Properties, Scheme.Name);
        }

        protected override async Task<OAuthTokenResponse> ExchangeCodeAsync(string code, string redirectUri)
        {
            var queryString = new Dictionary<string, string>()
            {
                {"client_id",Options.ClientId },
                {"client_secret",Options.ClientSecret },
                {"redirect_uri",redirectUri },
                {"code",code },
                {"grant_type","authorization_code" },
            };

            var response = await Backchannel.PostAsync(Options.TokenEndpoint, new FormUrlEncodedContent(queryString));
            if (!response.IsSuccessStatusCode)
            {
                Logger.LogError($"An error occurred while retrieving the user information: the remote server returned a " +
                                $"{response.StatusCode} response with the following payload: {await response.Content.ReadAsStringAsync()}.");

                throw new HttpRequestException("An error occurred when retrieving user information.");
            }

            var payload = JObject.FromObject(QueryHelpers.ParseQuery(await response.Content.ReadAsStringAsync())
                .ToDictionary(pair => pair.Key, k => k.Value.ToString()));

            return OAuthTokenResponse.Success(payload);
        }

        private async Task<string> GetUserIdentifierAsync(OAuthTokenResponse tokens)
        {
            var address = QueryHelpers.AddQueryString(Options.UserIdentificationEndpoint, "access_token", tokens.AccessToken);
            var request = new HttpRequestMessage(HttpMethod.Get, address);

            var response = await Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, Context.RequestAborted);
            if (!response.IsSuccessStatusCode)
            {
                Logger.LogError("An error occurred while retrieving the user identifier: the remote server " +
                                "returned a {Status} response with the following payload: {Headers} {Body}.",
                                /* Status: */ response.StatusCode,
                                /* Headers: */ response.Headers.ToString(),
                                /* Body: */ await response.Content.ReadAsStringAsync());

                throw new HttpRequestException("An error occurred while retrieving the user identifier.");
            }

            var body = await response.Content.ReadAsStringAsync();

            var index = body.IndexOf("{");
            if (index > 0)
            {
                body = body.Substring(index, body.LastIndexOf("}") - index + 1);
            }

            var payload = JObject.Parse(body);

            return payload.Value<string>("openid");
        }

        protected override string FormatScope() => string.Join(",", Options.Scope);
    }
}
