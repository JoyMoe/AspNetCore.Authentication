using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;

namespace JoyMoe.AspNetCore.Authentication.QQ
{
    public class QQOptions : OAuthOptions
    {
        public QQOptions()
        {
			CallbackPath = new PathString("/signin-qq");
			AuthorizationEndpoint = QQDefaults.AuthorizationEndpoint;
			TokenEndpoint = QQDefaults.TokenEndpoint;
            OpenIdEndpoint = QQDefaults.OpenIdEndpoint;
			UserInformationEndpoint = QQDefaults.UserInformationEndpoint;

            Scope.Add("get_user_info");
        }

        /// <summary>
        /// The APP ID.
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
        /// The app secret key for APP.
        /// </summary>
        public string AppKey
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

        /// <summary>
        /// The endpoint for received an openid.
        /// </summary>
        public string OpenIdEndpoint
        {
            get;
            set;
        }
    }
}
