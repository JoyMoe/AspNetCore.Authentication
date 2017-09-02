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
            CallbackPath = new PathString("/signin-weibo");
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
