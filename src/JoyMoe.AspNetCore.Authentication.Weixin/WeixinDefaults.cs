using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace JoyMoe.AspNetCore.Authentication.Weixin
{
    public static class WeixinDefaults
    {
        public const string AuthenticationScheme = "Weixin";

        public const string DisplayName = "Weixin";

        public const string CallbackPath = "/signin-weixin";

        public const string Issuer = "Weixin";

        public const string AuthorizationEndpoint = "https://open.weixin.qq.com/connect/qrconnect";

        public const string TokenEndpoint = "https://api.weixin.qq.com/sns/oauth2/access_token";

        public const string UserInformationEndpoint = "https://api.weixin.qq.com/sns/userinfo";
    }
}
