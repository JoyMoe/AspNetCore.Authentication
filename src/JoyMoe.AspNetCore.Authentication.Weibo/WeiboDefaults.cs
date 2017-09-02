namespace YJoyMoe.AspNetCore.AuthenticationWeibo
{
    public static class WeiboDefaults
    {
        public const string AuthenticationScheme = "Weibo";

        public const string CallbackPath = "/signin-weibo";

        public const string Issuer = "Weibo";

        public const string AuthorizationEndpoint = "https://api.weibo.com/oauth2/authorize";

        public const string TokenEndpoint = "https://api.weibo.com/oauth2/access_token";      

        public const string UserInformationEndpoint = "https://api.weibo.com/2/users/show.json";
    }
}
