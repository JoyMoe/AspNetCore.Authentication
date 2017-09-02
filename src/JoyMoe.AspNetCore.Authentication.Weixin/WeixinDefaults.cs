namespace YJoyMoe.AspNetCore.AuthenticationWWeixin
{
    public static class WeixinDefaults
    {
        public const string AuthenticationScheme = "Weixin";

        public const string CallbackPath = "/signin-Weixin";

        public const string Issuer = "Weixin";

        public const string AuthorizationEndpoint = "https://open.weixin.qq.com/connect/qrconnect";

        public const string TokenEndpoint = "https://api.weixin.qq.com/sns/oauth2/access_token";

        public const string UserInformationEndpoint = "https://api.weixin.qq.com/sns/userinfo";
    }
}
