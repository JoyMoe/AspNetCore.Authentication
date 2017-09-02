using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;

namespace JoyMoe.AspNetCore.Authentication.Baidu
{
	public class BaiduOptions : OAuthOptions
	{
		public BaiduOptions()
		{
			CallbackPath = new PathString("/signin-baidu");
			AuthorizationEndpoint = BaiduDefaults.AuthorizationEndpoint;
			TokenEndpoint = BaiduDefaults.TokenEndpoint;
			UserInformationEndpoint = BaiduDefaults.UserInformationEndpoint;
		}

		public string Appkey
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

		public string SecretKey
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
	}
}
