using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;

namespace JoyMoe.AspNetCore.Authentication.Baidu
{
    public class BaiduOptions : OAuthOptions
    {
        public BaiduOptions()
        {
            ClaimsIssuer = BaiduDefaults.Issuer;
            CallbackPath = new PathString(BaiduDefaults.CallbackPath);

            AuthorizationEndpoint = BaiduDefaults.AuthorizationEndpoint;
            TokenEndpoint = BaiduDefaults.TokenEndpoint;           
            UserInformationEndpoint = BaiduDefaults.UserInformationEndpoint;

            ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "uid");
            ClaimActions.MapJsonKey(ClaimTypes.Name, "uname");
            ClaimActions.MapJsonKey("urn:baidu:portrait", "portrait");
        }
    }
}
