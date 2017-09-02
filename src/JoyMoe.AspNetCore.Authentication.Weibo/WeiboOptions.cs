using Microsoft.AspNetCore.Http;
using JoyMoe.AspNetCore.Authentication.Weibo;

namespace Microsoft.AspNetCore.Builder
{
    public class WeiboOptions : OAuthOptions
    {
        public WeiboOptions()
        {
            AuthenticationScheme = WeiboDefaults.AuthenticationScheme;
            DisplayName = AuthenticationScheme;
            ClaimsIssuer = WeiboDefaults.Issuer;
            CallbackPath = new PathString(WeiboDefaults.CallbackPath);

            AuthorizationEndpoint = WeiboDefaults.AuthorizationEndpoint;
            TokenEndpoint = WeiboDefaults.TokenEndpoint;           
            UserInformationEndpoint = WeiboDefaults.UserInformationEndpoint;

            Scope.Add("email");
        }

        /// <summary>
        /// The App Key
        /// </summary>
        public string AppKey
        {
            get
            {
                return ClientId;
            }
            set
            {
                ClientId = value;
            }
        }

        /// <summary>
        /// The App Secret.
        /// </summary>
        public string AppSecret
        {
            get
            {
                return ClientSecret;
            }
            set
            {
                ClientSecret = value;
            }
        }
    }
}
