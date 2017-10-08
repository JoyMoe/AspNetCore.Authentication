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
            ClaimsIssuer = QQDefaults.Issuer;
            CallbackPath = new PathString(QQDefaults.CallbackPath);

            AuthorizationEndpoint = QQDefaults.AuthorizationEndpoint;
            TokenEndpoint = QQDefaults.TokenEndpoint;
            UserIdentificationEndpoint = QQDefaults.UserIdentificationEndpoint;
            UserInformationEndpoint = QQDefaults.UserInformationEndpoint;

            Scope.Add("get_user_info");

            ClaimActions.MapJsonKey(ClaimTypes.Name, "nickname");
            ClaimActions.MapJsonKey(ClaimTypes.Gender, "gender");
            ClaimActions.MapJsonKey("urn:qq:picture", "figureurl");
            ClaimActions.MapJsonKey("urn:qq:picture_medium", "figureurl_1");
            ClaimActions.MapJsonKey("urn:qq:picture_full", "figureurl_2");
            ClaimActions.MapJsonKey("urn:qq:avatar", "figureurl_qq_1");
            ClaimActions.MapJsonKey("urn:qq:avatar_full", "figureurl_qq_2");
        }

        public string UserIdentificationEndpoint { get; set; }
    }
}
