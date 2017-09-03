/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/JoyMoe.AspNetCore.Authentication.OpenID.Providers
 * for more information concerning the license and the contributors participating to this project.
 */

using Microsoft.AspNetCore.Builder;

namespace JoyMoe.AspNetCore.Authentication.OpenID
{
    /// <summary>
    /// Contains various constants used as default values
    /// for the OpenID authentication middleware.
    /// </summary>
    public static class OpenIdDefaults
    {
        /// <summary>
        /// Gets the default value associated with <see cref="AuthenticationOptions.AuthenticationScheme"/>.
        /// </summary>
        public const string AuthenticationScheme = "OpenId";

        /// <summary>
        /// Gets the default value associated with <see cref="RemoteAuthenticationOptions.DisplayName"/>.
        /// </summary>
        public static readonly string DisplayName = "OpenId";

        /// <summary>
        /// Gets the default value associated with <see cref="RemoteAuthenticationOptions.CallbackPath"/>.
        /// </summary>
        public static readonly string CallbackPath = "/signin-openid";
    }
}
