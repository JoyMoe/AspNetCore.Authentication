using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;

namespace JoyMoe.AspNetCore.Authentication.Weibo
{
    public class WeiboOptions : OAuthOptions
    {
        public WeiboOptions()
        {
            ClaimsIssuer = WeiboDefaults.Issuer;
            CallbackPath = new PathString(WeiboDefaults.CallbackPath);

            AuthorizationEndpoint = WeiboDefaults.AuthorizationEndpoint;
            TokenEndpoint = WeiboDefaults.TokenEndpoint;           
            UserInformationEndpoint = WeiboDefaults.UserInformationEndpoint;

            Scope.Add("email");

            ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
            ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
            ClaimActions.MapJsonKey(ClaimTypes.Gender, "gender");
            ClaimActions.MapJsonKey("urn:weibo:screen_name", "screen_name");
            ClaimActions.MapJsonKey("urn:weibo:profile_image_url", "profile_image_url");
            ClaimActions.MapJsonKey("urn:weibo:avatar_large", "avatar_large");
            ClaimActions.MapJsonKey("urn:weibo:avatar_hd", "avatar_hd");
            ClaimActions.MapJsonKey("urn:weibo:cover_image_phone", "cover_image_phone");
            ClaimActions.MapJsonKey("urn:weibo:location", "location");
        }

        public string UserEmailsEndpoint { get; set; } = WeiboDefaults.UserEmailsEndpoint;
    }
}
