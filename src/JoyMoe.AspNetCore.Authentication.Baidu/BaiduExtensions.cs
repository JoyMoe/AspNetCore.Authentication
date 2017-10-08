using System;
using JoyMoe.AspNetCore.Authentication.Baidu;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BaiduExtensions
    {
        public static AuthenticationBuilder AddBaidu(this AuthenticationBuilder builder)
        {
            return builder.AddBaidu(BaiduDefaults.AuthenticationScheme, options => { });
        }

        public static AuthenticationBuilder AddBaidu(
            this AuthenticationBuilder builder,
            Action<BaiduOptions> configuration)
            => builder.AddBaidu(BaiduDefaults.AuthenticationScheme, configuration);

        public static AuthenticationBuilder AddBaidu(
            this AuthenticationBuilder builder, string scheme,
            Action<BaiduOptions> configuration)
            => builder.AddBaidu(scheme, BaiduDefaults.DisplayName, configuration);

        public static AuthenticationBuilder AddBaidu(
            this AuthenticationBuilder builder,
            string scheme, string name,
            Action<BaiduOptions> configuration)
            => builder.AddOAuth<BaiduOptions, BaiduHandler>(scheme, name, configuration);
    }
}
