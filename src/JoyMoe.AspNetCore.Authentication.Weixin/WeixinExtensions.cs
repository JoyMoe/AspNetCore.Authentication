using System;
using Microsoft.AspNetCore.Authentication;
using JoyMoe.AspNetCore.Authentication.Weixin;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class WeixinExtensions
	{
		public static AuthenticationBuilder AddWeixin(this AuthenticationBuilder builder)
			=> builder.AddWeixin(WeixinDefaults.AuthenticationScheme, _ => { });

		public static AuthenticationBuilder AddWeixin(this AuthenticationBuilder builder, Action<WeixinOptions> configureOptions)
			=> builder.AddWeixin(WeixinDefaults.AuthenticationScheme, configureOptions);

		public static AuthenticationBuilder AddWeixin(this AuthenticationBuilder builder, string authenticationScheme, Action<WeixinOptions> configureOptions)
			=> builder.AddWeixin(authenticationScheme, WeixinDefaults.DisplayName, configureOptions);

		public static AuthenticationBuilder AddWeixin(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<WeixinOptions> configureOptions)
			=> builder.AddOAuth<WeixinOptions, WeixinHandler>(authenticationScheme, displayName, configureOptions);
	}
}
