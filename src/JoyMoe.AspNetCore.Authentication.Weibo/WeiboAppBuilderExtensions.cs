using System;
using Microsoft.Extensions.Options;
using JoyMoe.AspNetCore.Authentication.Weibo;

namespace Microsoft.AspNetCore.Builder
{
    public static class WeiboAppBuilderExtensions
    {
        /// <summary>
        ///  UseWeiboAuthentication is obsolete. Configure Weibo authentication with AddAuthentication().AddWeibo in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
		[Obsolete("UseWeiboAuthentication is obsolete. Configure Weibo authentication with AddAuthentication().AddWeibo in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.", error: true)]
		public static IApplicationBuilder UseWeiboAuthentication(this IApplicationBuilder app, WeiboOptions options)
        {
            throw new NotSupportedException("This method is no longer supported, see https://go.microsoft.com/fwlink/?linkid=845470");
        }

        /// <summary>
        /// UseWeiboAuthentication is obsolete. Configure Weibo authentication with AddAuthentication().AddWeibo in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
		[Obsolete("UseWeiboAuthentication is obsolete. Configure Weibo authentication with AddAuthentication().AddWeibo in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.", error: true)]
		public static IApplicationBuilder UseWeiboAuthentication(this IApplicationBuilder app, Action<WeiboOptions> configuration)
        {
            throw new NotSupportedException("This method is no longer supported, see https://go.microsoft.com/fwlink/?linkid=845470");
        }
    }
}
