/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/JoyMoe.AspNetCore.Authentication.OpenID.Providers
 * for more information concerning the license and the contributors participating to this project.
 */

using System;
using JoyMoe.AspNetCore.Authentication.OpenID;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Exposes convenient extensions that can be used to add an instance
    /// of the OpenID authentication middleware in an ASP.NET Core pipeline.
    /// </summary>
    public static class OpenIdExtensions
    {
        /// <summary>
        /// UseOpenIdAuthentication is obsolete. Configure OpenId authentication with AddAuthentication().AddOpenId in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
        /// <param name="options">The <see cref="OpenIdOptions"/> used to configure the OpenID 2.0 options.</param>
        /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
        [Obsolete("UseOpenIdAuthentication is obsolete. Configure OpenId authentication with AddAuthentication().AddOpenId in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.", error: true)]
		public static IApplicationBuilder UseOpenIdAuthentication(
            this IApplicationBuilder app,
            OpenIdOptions options)
        {
            throw new NotSupportedException("This method is no longer supported, see https://go.microsoft.com/fwlink/?linkid=845470");
        }

        /// <summary>
        /// UseOpenIdAuthentication is obsolete. Configure OpenId authentication with AddAuthentication().AddOpenId in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
        /// <param name="configuration">The delegate used to configure the OpenID 2.0 options.</param>
        /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
        [Obsolete("UseOpenIdAuthentication is obsolete. Configure OpenId authentication with AddAuthentication().AddOpenId in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.", error: true)]
		public static IApplicationBuilder UseOpenIdAuthentication(
            this IApplicationBuilder app,
            Action<OpenIdOptions> configuration)
        {
            throw new NotSupportedException("This method is no longer supported, see https://go.microsoft.com/fwlink/?linkid=845470");
        }
    }
}
