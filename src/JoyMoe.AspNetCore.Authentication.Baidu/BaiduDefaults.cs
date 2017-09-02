namespace JoyMoe.AspNetCore.Authentication.Baidu
{
	public static class BaiduDefaults
	{
		public const string AuthenticationScheme = "Baidu";

		public static readonly string DisplayName = "Baidu";

		public static readonly string AuthorizationEndpoint = "http://openapi.baidu.com/oauth/2.0/authorize";

		public static readonly string TokenEndpoint = "https://openapi.baidu.com/oauth/2.0/token";

		public static readonly string UserInformationEndpoint = "https://openapi.baidu.com/rest/2.0/passport/users/getLoggedInUser";
	}
}
