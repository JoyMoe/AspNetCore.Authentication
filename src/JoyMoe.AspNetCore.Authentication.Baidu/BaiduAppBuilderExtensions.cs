using System;
using JoyMoe.AspNetCore.Authentication.Baidu;

namespace Microsoft.AspNetCore.Builder
{
	public static class BaiduAppBuilderExtensions
	{
		/// <summary>
		///  UseBaiduAuthentication is obsolete. Configure Baidu authentication with AddAuthentication().AddBaidu in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="options"></param>
		/// <returns>A reference to this instance after the operation has completed.</returns>
		[Obsolete("UseBaiduAuthentication is obsolete. Configure Baidu authentication with AddAuthentication().AddBaidu in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.", error: true)]
		public static IApplicationBuilder UseBaiduAuthentication(this IApplicationBuilder app, BaiduOptions options)
		{
			throw new NotSupportedException("This method is no longer supported, see https://go.microsoft.com/fwlink/?linkid=845470");
		}

		/// <summary>
		/// UseBaiduAuthentication is obsolete. Configure Baidu authentication with AddAuthentication().AddBaidu in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="configuration"></param>
		/// <returns>A reference to this instance after the operation has completed.</returns>
		[Obsolete("UseBaiduAuthentication is obsolete. Configure Baidu authentication with AddAuthentication().AddBaidu in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.", error: true)]
		public static IApplicationBuilder UseBaiduAuthentication(this IApplicationBuilder app, Action<BaiduOptions> configuration)
		{
			throw new NotSupportedException("This method is no longer supported, see https://go.microsoft.com/fwlink/?linkid=845470");
		}
	}
}
