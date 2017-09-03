/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/JoyMoe.AspNetCore.Authentication.OpenID.Providers
 * for more information concerning the license and the contributors participating to this project.
 */

using System;
using JoyMoe.AspNetCore.Authentication.OpenID.Steam;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Exposes convenient extensions that can be used to add an instance
    /// of the Steam authentication middleware in an ASP.NET 5 pipeline.
    /// </summary>
    public static class SteamAppBuilderExtensions
    {
        /// <summary>
        /// UseSteamAuthentication is obsolete. Configure Steam authentication with AddAuthentication().AddSteam in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
        /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
        [Obsolete("UseSteamAuthentication is obsolete. Configure Steam authentication with AddAuthentication().AddSteam in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.", error: true)]
		public static IApplicationBuilder UseSteamAuthentication(this IApplicationBuilder app)
        {
            throw new NotSupportedException("This method is no longer supported, see https://go.microsoft.com/fwlink/?linkid=845470");
        }

        /// <summary>
        /// UseSteamAuthentication is obsolete. Configure Steam authentication with AddAuthentication().AddSteam in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
        /// <param name="options">The <see cref="SteamOptions"/> used to configure the Steam options.</param>
        /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
        [Obsolete("UseSteamAuthentication is obsolete. Configure Steam authentication with AddAuthentication().AddSteam in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.", error: true)]
		public static IApplicationBuilder UseSteamAuthentication(
            this IApplicationBuilder app,
            SteamOptions options)
        {
            throw new NotSupportedException("This method is no longer supported, see https://go.microsoft.com/fwlink/?linkid=845470");
        }

        /// <summary>
        /// UseSteamAuthentication is obsolete. Configure Steam authentication with AddAuthentication().AddSteam in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
        /// <param name="configuration">The delegate used to configure the Steam options.</param>
        /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
        [Obsolete("UseSteamAuthentication is obsolete. Configure Steam authentication with AddAuthentication().AddSteam in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.", error: true)]
		public static IApplicationBuilder UseSteamAuthentication(
            this IApplicationBuilder app,
            Action<SteamOptions> configuration)
        {
            throw new NotSupportedException("This method is no longer supported, see https://go.microsoft.com/fwlink/?linkid=845470");
        }
    }
}
