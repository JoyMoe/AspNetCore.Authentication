/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/JoyMoe.AspNetCore.Authentication.OpenID.Providers
 * for more information concerning the license and the contributors participating to this project.
 */

using Microsoft.AspNetCore.Builder;

namespace JoyMoe.AspNetCore.Authentication.OpenID.Steam
{
    /// <summary>
    /// Contains various constants used as default values
    /// for the Steam authentication middleware.
    /// </summary>
    public static class SteamDefaults
    {
        /// <summary>
        /// Gets the default value associated with <see cref="AuthenticationOptions.AuthenticationScheme"/>.
        /// </summary>
        public const string AuthenticationScheme = "Steam";

        /// <summary>
        /// Gets the default value associated with <see cref="RemoteAuthenticationOptions.DisplayName"/>.
        /// </summary>
        public const string DisplayName = "Steam";

        /// <summary>
        /// Gets the default value associated with <see cref="OpenIdOptions.Authority"/>.
        /// </summary>
        public static readonly string Authority = "https://steamcommunity.com/openid";

        /// <summary>
        /// Gets the default value associated with <see cref="SteamOptions.UserInformationEndpoint"/>.
        /// </summary>
        public static readonly string UserInformationEndpoint = "https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/";
    }
}
