using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace JoyMoe.AspNetCore.Authentication.Weibo
{
    public class WeiboHandler : OAuthHandler<WeiboOptions>
    {
        public WeiboHandler(HttpClient client) : base(client) { }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            var queryString = new Dictionary<string, string>()
            {
                {"access_token",tokens.AccessToken },
                {"uid",tokens.Response.Value<string>("uid") }
            };
            var endpoint = QueryHelpers.AddQueryString(Options.UserInformationEndpoint, queryString);
            var response = await Backchannel.GetAsync(endpoint, Context.RequestAborted);
            if (!response.IsSuccessStatusCode)
            {
                Logger.LogError($"An error occurred while retrieving the user information: the remote server returned a " +
                    $"{response.StatusCode} response with the following payload: {await response.Content.ReadAsStringAsync()}.");

                throw new HttpRequestException("An error occurred when retrieving user information.");
            }
            var payload = JObject.Parse(await response.Content.ReadAsStringAsync());

            var id = WeiboHelper.GetId(payload);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, id, ClaimValueTypes.String, Options.ClaimsIssuer));

            var name = WeiboHelper.GetName(payload);
            if (!string.IsNullOrEmpty(name))
            {
                identity.AddClaim(new Claim(ClaimTypes.Name, name, ClaimValueTypes.String, Options.ClaimsIssuer));
            }
            var screenName = WeiboHelper.GetScreenName(payload);
            if (!string.IsNullOrEmpty(screenName))
            {
                identity.AddClaim(new Claim("urn:weibo:screen_name", screenName, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var profileImageUrl = WeiboHelper.GetProfileImageUrl(payload);
            if (!string.IsNullOrEmpty(profileImageUrl))
            {
                identity.AddClaim(new Claim("urn:weibo:profile_image_url", profileImageUrl, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var gender = WeiboHelper.GetGender(payload);
            if (!string.IsNullOrEmpty(gender))
            {
                identity.AddClaim(new Claim(ClaimTypes.Gender, gender, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var avatarLarge = WeiboHelper.GetAvatarLarge(payload);
            if (!string.IsNullOrEmpty(avatarLarge))
            {
                identity.AddClaim(new Claim("urn:weibo:avatar_large", avatarLarge, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var avatarHD = WeiboHelper.GetAvatarHD(payload);
            if (!string.IsNullOrEmpty(avatarHD))
            {
                identity.AddClaim(new Claim("urn:weibo:avatar_hd", avatarHD, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var coverImagePhone = WeiboHelper.GetCoverImagePhone(payload);
            if (!string.IsNullOrEmpty(coverImagePhone))
            {
                identity.AddClaim(new Claim("urn:weibo:cover_image_phone", coverImagePhone, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var location = WeiboHelper.GetLocation(payload);
            if (!string.IsNullOrEmpty(location))
            {
                identity.AddClaim(new Claim("urn:weibo:location", location, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, properties, Options.AuthenticationScheme);

            var context = new OAuthCreatingTicketContext(ticket, Context, Options, Backchannel, tokens);
            await Options.Events.CreatingTicket(context);

            return context.Ticket;
        }

        protected override string FormatScope()
        {
            return string.Join(",", Options.Scope);
        }
    }
}
