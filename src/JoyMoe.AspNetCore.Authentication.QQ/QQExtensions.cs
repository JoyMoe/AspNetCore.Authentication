using System;
using JoyMoe.AspNetCore.Authentication.QQ;
using Microsoft.AspNetCore.Authentication;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class QQExtensions
    {
        public static AuthenticationBuilder AddQQ(this AuthenticationBuilder builder)
            => builder.AddQQ(QQDefaults.AuthenticationScheme, options => { });

        public static AuthenticationBuilder AddQQ(
            this AuthenticationBuilder builder,
            Action<QQOptions> configuration)
            => builder.AddQQ(QQDefaults.AuthenticationScheme, configuration);

        public static AuthenticationBuilder AddQQ(
            this AuthenticationBuilder builder, string scheme,
            Action<QQOptions> configuration)
            => builder.AddQQ(scheme, QQDefaults.DisplayName, configuration);

        public static AuthenticationBuilder AddQQ(
            this AuthenticationBuilder builder,
            string scheme, string name,
            Action<QQOptions> configuration)
            => builder.AddOAuth<QQOptions, QQHandler>(scheme, name, configuration);
    }
}
