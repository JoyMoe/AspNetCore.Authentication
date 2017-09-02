using System;
using Microsoft.AspNetCore.Authentication;
using JoyMoe.AspNetCore.Authentication.Baidu;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class BaiduExtensions
	{
		public static AuthenticationBuilder AddBaidu(this AuthenticationBuilder builder)
			=> builder.AddBaidu(BaiduDefaults.AuthenticationScheme, _ => { });

		public static AuthenticationBuilder AddBaidu(this AuthenticationBuilder builder, Action<BaiduOptions> configureOptions)
			=> builder.AddBaidu(BaiduDefaults.AuthenticationScheme, configureOptions);

		public static AuthenticationBuilder AddBaidu(this AuthenticationBuilder builder, string authenticationScheme, Action<BaiduOptions> configureOptions)
			=> builder.AddBaidu(authenticationScheme, BaiduDefaults.DisplayName, configureOptions);

		public static AuthenticationBuilder AddBaidu(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<BaiduOptions> configureOptions)
			=> builder.AddOAuth<BaiduOptions, BaiduHandler>(authenticationScheme, displayName, configureOptions);
	}
}
