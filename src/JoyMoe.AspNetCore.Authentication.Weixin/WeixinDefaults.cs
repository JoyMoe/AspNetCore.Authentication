namespace JoyMoe.AspNetCore.Authentication.Weixin
{
    public static class WeixinDefaults
    {

        public const string AuthenticationScheme = "Weixin";

		public static readonly string DisplayName = "Weixin";

		public static readonly string AuthorizationEndpoint = "https://open.weixin.qq.com/connect/qrconnect";

		public static readonly string TokenEndpoint = "https://api.weixin.qq.com/sns/oauth2/access_token";

		public static readonly string UserInformationEndpoint = "https://api.weixin.qq.com/sns/userinfo";
    }
}
