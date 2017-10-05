using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace JoyMoe.AspNetCore.Authentication.Weibo
{
    public static class WeiboDefaults
    {
        public const string AuthenticationScheme = "Weibo";

        public const string DisplayName = "Weibo";

        public const string CallbackPath = "/signin-weibo";

        public const string Issuer = "Weibo";

        public const string AuthorizationEndpoint = "https://api.weibo.com/oauth2/authorize";

        public const string TokenEndpoint = "https://api.weibo.com/oauth2/access_token";

        public const string UserInformationEndpoint = "https://api.weibo.com/2/users/show.json";

        public const string UserEmailsEndpoint = "https://api.weibo.com/2/account/profile/email.json";
    }
}
