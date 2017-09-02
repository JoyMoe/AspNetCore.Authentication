using Microsoft.AspNetCore.Http;
using JoyMoe.AspNetCore.Authentication.Weixin;

namespace Microsoft.AspNetCore.Builder
{
    public class WeixinOptions : OAuthOptions
    {
        public WeixinOptions()
        {
            AuthenticationScheme = WeixinDefaults.AuthenticationScheme;
            DisplayName = AuthenticationScheme;
            ClaimsIssuer = WeixinDefaults.Issuer;
            CallbackPath = new PathString(WeixinDefaults.CallbackPath);

            AuthorizationEndpoint = WeixinDefaults.AuthorizationEndpoint;
            TokenEndpoint = WeixinDefaults.TokenEndpoint;
            UserInformationEndpoint = WeixinDefaults.UserInformationEndpoint;

            Scope.Add("snsapi_login");
            Scope.Add("snsapi_userinfo");
        }

        /// <summary>
        /// The App Id.
        /// </summary>
        public string AppId
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
