using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;

namespace JoyMoe.AspNetCore.Authentication.Weixin
{
    public class WeixinOptions : OAuthOptions
    {
        public WeixinOptions()
        {
            CallbackPath = new PathString("/signin-weixin");
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
