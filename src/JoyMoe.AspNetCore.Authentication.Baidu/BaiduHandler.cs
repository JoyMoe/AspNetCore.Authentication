using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Authentication;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JoyMoe.AspNetCore.Authentication.Baidu
{
    public class BaiduHandler : OAuthHandler<BaiduOptions>
    {
        public BaiduHandler(HttpClient client) : base(client) { }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            var queryString = new Dictionary<string, string>()
            {
                {"access_token",tokens.AccessToken }               
            };

            var response = await Backchannel.PostAsync(Options.UserInformationEndpoint, new FormUrlEncodedContent(queryString));
            if (!response.IsSuccessStatusCode)
            {
                Logger.LogError($"An error occurred while retrieving the user information: the remote server returned a " +
                    $"{response.StatusCode} response with the following payload: {await response.Content.ReadAsStringAsync()}.");

                throw new HttpRequestException("An error occurred when retrieving user information.");
            }

            var payload = JObject.Parse(await response.Content.ReadAsStringAsync());

            var id = BaiduHelper.GetId(payload);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, id, ClaimValueTypes.String, Options.ClaimsIssuer));

            var name = BaiduHelper.GetName(payload);
            if (!string.IsNullOrEmpty(name))
            {
                identity.AddClaim(new Claim(ClaimTypes.Name, name, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var portrait = BaiduHelper.GetPortrait(payload);
            if (!string.IsNullOrEmpty(portrait))
            {
                identity.AddClaim(new Claim("urn:baidu:portrait", portrait, ClaimValueTypes.String, Options.ClaimsIssuer));
            }
         
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, properties, Options.AuthenticationScheme);

            var context = new OAuthCreatingTicketContext(ticket, Context, Options, Backchannel, tokens);
            await Options.Events.CreatingTicket(context);

            return context.Ticket;
        }
    }
}
