namespace JoyMoe.AspNetCore.Authentication.QQ
{
    public static class QQDefaults
    {
        public const string AuthenticationScheme = "QQ";

        public const string DisplayName = "QQ";

        public const string Issuer = "QQ";

        public const string CallbackPath = "/signin-qq";

        public const string AuthorizationEndpoint = "https://graph.qq.com/oauth2.0/authorize";

        public const string TokenEndpoint = "https://graph.qq.com/oauth2.0/token";

        public const string UserIdentificationEndpoint = "https://graph.qq.com/oauth2.0/me";

        public const string UserInformationEndpoint = "https://graph.qq.com/user/get_user_info";
    }
}
