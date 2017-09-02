using Microsoft.AspNetCore.Http;
using JoyMoe.AspNetCore.Authentication.QQ;

namespace Microsoft.AspNetCore.Builder
{
    public class QQOptions : OAuthOptions
    {
        public QQOptions()
        {
            AuthenticationScheme = QQDefaults.AuthenticationScheme;
            DisplayName = AuthenticationScheme;
            ClaimsIssuer = QQDefaults.Issuer;
            CallbackPath = new PathString(QQDefaults.CallbackPath);

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
