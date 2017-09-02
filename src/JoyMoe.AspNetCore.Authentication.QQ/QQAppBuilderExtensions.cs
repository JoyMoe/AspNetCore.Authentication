using System;
using Microsoft.Extensions.Options;
using JoyMoe.AspNetCore.Authentication.QQ;

namespace Microsoft.AspNetCore.Builder
{
    public static class QQAppBuilderExtensions
    {
        /// <summary>
        ///  Adds the <see cref="QQMiddleware"/> middleware to the specified <see cref="IApplicationBuilder"/>,
        ///  which enables QQ authentication capabilities.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseQQAuthentication(this IApplicationBuilder app, QQOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            return app.UseMiddleware<QQMiddleware>(Options.Create(options));
        }

        /// <summary>
        /// Adds the <see cref="QQMiddleware"/> middleware to the specified <see cref="IApplicationBuilder"/>,
        /// which enables QQ authentication capabilities.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseQQAuthentication(this IApplicationBuilder app, Action<QQOptions> configuration)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var options = new QQOptions();
            configuration(options);

            return app.UseMiddleware<QQMiddleware>(Options.Create(options));
        }
    }
}
