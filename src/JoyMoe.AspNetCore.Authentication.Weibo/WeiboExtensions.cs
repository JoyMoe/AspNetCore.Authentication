using System;
using Microsoft.AspNetCore.Authentication;
using JoyMoe.AspNetCore.Authentication.Weibo;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class WeiboExtensions
    {
        public static AuthenticationBuilder AddWeibo(this AuthenticationBuilder builder)
            => builder.AddWeibo(WeiboDefaults.AuthenticationScheme, options => { });

        public static AuthenticationBuilder AddWeibo(
            this AuthenticationBuilder builder,
            Action<WeiboOptions> configuration)
            => builder.AddWeibo(WeiboDefaults.AuthenticationScheme, configuration);

        public static AuthenticationBuilder AddWeibo(
            this AuthenticationBuilder builder, string scheme,
            Action<WeiboOptions> configuration)
            => builder.AddWeibo(scheme, WeiboDefaults.DisplayName, configuration);

        public static AuthenticationBuilder AddWeibo(
            this AuthenticationBuilder builder,
            string scheme, string name,
            Action<WeiboOptions> configuration)
            => builder.AddOAuth<WeiboOptions, WeiboHandler>(scheme, name, configuration);
    }
}
