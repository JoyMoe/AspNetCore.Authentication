using System;
using Microsoft.Extensions.Options;
using JoyMoe.AspNetCore.Authentication.Baidu;

namespace Microsoft.AspNetCore.Builder
{
    public static class BaiduAppBuilderExtensions
    {
        /// <summary>
        ///  Adds the <see cref="BaiduMiddleware"/> middleware to the specified <see cref="IApplicationBuilder"/>,
        ///  which enables Weibo authentication capabilities.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseBaiduAuthentication(this IApplicationBuilder app, BaiduOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            return app.UseMiddleware<BaiduMiddleware>(Options.Create(options));
        }

        /// <summary>
        /// Adds the <see cref="BaiduMiddleware"/> middleware to the specified <see cref="IApplicationBuilder"/>,
        /// which enables Weibo authentication capabilities.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseBaiduAuthentication(this IApplicationBuilder app, Action<BaiduOptions> configuration)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var options = new BaiduOptions();
            configuration(options);

            return app.UseMiddleware<BaiduMiddleware>(Options.Create(options));
        }
    }
}
