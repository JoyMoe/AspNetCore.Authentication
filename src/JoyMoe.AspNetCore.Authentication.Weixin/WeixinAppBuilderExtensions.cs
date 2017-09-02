using System;
using Microsoft.Extensions.Options;
using JoyMoe.AspNetCore.Authentication.Weixin;

namespace Microsoft.AspNetCore.Builder
{
    public static class WeixinAppBuilderExtensions
    {
        /// <summary>
        ///  Adds the <see cref="WeixinMiddleware"/> middleware to the specified <see cref="IApplicationBuilder"/>,
        ///  which enables Weibo authentication capabilities.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseWeixinAuthentication(this IApplicationBuilder app, WeixinOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            return app.UseMiddleware<WeixinMiddleware>(Options.Create(options));
        }

        /// <summary>
        /// Adds the <see cref="WeixinMiddleware"/> middleware to the specified <see cref="IApplicationBuilder"/>,
        /// which enables Weibo authentication capabilities.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseWeixinAuthentication(this IApplicationBuilder app, Action<WeixinOptions> configuration)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var options = new WeixinOptions();
            configuration(options);

            return app.UseMiddleware<WeixinMiddleware>(Options.Create(options));
        }
    }
}
