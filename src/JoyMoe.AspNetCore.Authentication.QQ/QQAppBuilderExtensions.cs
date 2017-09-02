using System;
using Microsoft.Extensions.Options;
using JoyMoe.AspNetCore.Authentication.QQ;

namespace Microsoft.AspNetCore.Builder
{
    public static class QQAppBuilderExtensions
    {
        /// <summary>
        ///  UseQQAuthentication is obsolete. Configure QQ authentication with AddAuthentication().AddQQ in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
		[Obsolete("UseQQAuthentication is obsolete. Configure QQ authentication with AddAuthentication().AddQQ in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.", error: true)]
		public static IApplicationBuilder UseQQAuthentication(this IApplicationBuilder app, QQOptions options)
        {
            throw new NotSupportedException("This method is no longer supported, see https://go.microsoft.com/fwlink/?linkid=845470");
        }

        /// <summary>
        /// UseQQAuthentication is obsolete. Configure QQ authentication with AddAuthentication().AddQQ in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
		[Obsolete("UseQQAuthentication is obsolete. Configure QQ authentication with AddAuthentication().AddQQ in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.", error: true)]
		public static IApplicationBuilder UseQQAuthentication(this IApplicationBuilder app, Action<QQOptions> configuration)
        {
            throw new NotSupportedException("This method is no longer supported, see https://go.microsoft.com/fwlink/?linkid=845470");
        }
    }
}
