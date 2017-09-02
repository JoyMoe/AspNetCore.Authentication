using System;
using Microsoft.AspNetCore.Authentication;
using JoyMoe.AspNetCore.Authentication.Weibo;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class WeiboExtensions
	{
		public static AuthenticationBuilder AddWeibo(this AuthenticationBuilder builder)
			=> builder.AddWeibo(WeiboDefaults.AuthenticationScheme, _ => { });

		public static AuthenticationBuilder AddWeibo(this AuthenticationBuilder builder, Action<WeiboOptions> configureOptions)
			=> builder.AddWeibo(WeiboDefaults.AuthenticationScheme, configureOptions);

		public static AuthenticationBuilder AddWeibo(this AuthenticationBuilder builder, string authenticationScheme, Action<WeiboOptions> configureOptions)
			=> builder.AddWeibo(authenticationScheme, WeiboDefaults.DisplayName, configureOptions);

		public static AuthenticationBuilder AddWeibo(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<WeiboOptions> configureOptions)
			=> builder.AddOAuth<WeiboOptions, WeiboHandler>(authenticationScheme, displayName, configureOptions);
	}
}
