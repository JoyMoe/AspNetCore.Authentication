using System;
using Microsoft.Extensions.Options;
using JoyMoe.AspNetCore.Authentication.Weixin;

namespace Microsoft.AspNetCore.Builder
{
    public static class WeixinAppBuilderExtensions
    {
        /// <summary>
        ///  UseWeixinAuthentication is obsolete. Configure Weixin authentication with AddAuthentication().AddWeixin in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
		[Obsolete("UseWeixinAuthentication is obsolete. Configure Weixin authentication with AddAuthentication().AddWeixin in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.", error: true)]
		public static IApplicationBuilder UseWeixinAuthentication(this IApplicationBuilder app, WeixinOptions options)
        {
            throw new NotSupportedException("This method is no longer supported, see https://go.microsoft.com/fwlink/?linkid=845470");
        }

        /// <summary>
        /// UseWeixinAuthentication is obsolete. Configure Weixin authentication with AddAuthentication().AddWeixin in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
		[Obsolete("UseWeixinAuthentication is obsolete. Configure Weixin authentication with AddAuthentication().AddWeixin in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.", error: true)]
		public static IApplicationBuilder UseWeixinAuthentication(this IApplicationBuilder app, Action<WeixinOptions> configuration)
        {
            throw new NotSupportedException("This method is no longer supported, see https://go.microsoft.com/fwlink/?linkid=845470");
        }
    }
}
