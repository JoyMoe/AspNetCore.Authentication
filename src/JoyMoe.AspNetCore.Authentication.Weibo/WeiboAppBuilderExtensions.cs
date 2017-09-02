using System;
using Microsoft.Extensions.Options;
using JoyMoe.AspNetCore.Authentication.Weibo;

namespace Microsoft.AspNetCore.Builder
{
    public static class WeiboAppBuilderExtensions
    {
        /// <summary>
        ///  Adds the <see cref="WeiboMiddleware"/> middleware to the specified <see cref="IApplicationBuilder"/>,
        ///  which enables Weibo authentication capabilities.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseWeiboAuthentication(this IApplicationBuilder app, WeiboOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            return app.UseMiddleware<WeiboMiddleware>(Options.Create(options));
        }

        /// <summary>
        /// Adds the <see cref="WeiboMiddleware"/> middleware to the specified <see cref="IApplicationBuilder"/>,
        /// which enables Weibo authentication capabilities.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseWeiboAuthentication(this IApplicationBuilder app, Action<WeiboOptions> configuration)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var options = new WeiboOptions();
            configuration(options);

            return app.UseMiddleware<WeiboMiddleware>(Options.Create(options));
        }
    }
}
