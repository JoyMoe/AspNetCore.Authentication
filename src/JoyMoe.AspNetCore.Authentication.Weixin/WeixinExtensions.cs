using System;
using Microsoft.AspNetCore.Authentication;
using JoyMoe.AspNetCore.Authentication.Weixin;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class WeixinExtensions
    {
        public static AuthenticationBuilder AddWeixin(this AuthenticationBuilder builder)
            => builder.AddWeixin(WeixinDefaults.AuthenticationScheme, options => { });

        public static AuthenticationBuilder AddWeixin(
            this AuthenticationBuilder builder,
            Action<WeixinOptions> configuration)
            => builder.AddWeixin(WeixinDefaults.AuthenticationScheme, configuration);

        public static AuthenticationBuilder AddWeixin(
            this AuthenticationBuilder builder, string scheme,
            Action<WeixinOptions> configuration)
            => builder.AddWeixin(scheme, WeixinDefaults.DisplayName, configuration);

        public static AuthenticationBuilder AddWeixin(
            this AuthenticationBuilder builder,
            string scheme, string name,
            Action<WeixinOptions> configuration)
            => builder.AddOAuth<WeixinOptions, WeixinHandler>(scheme, name, configuration);
    }
}
