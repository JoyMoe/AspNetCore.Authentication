/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/JoyMoe.AspNetCore.Authentication.OpenID.Providers
 * for more information concerning the license and the contributors participating to this project.
 */

using System;
using Microsoft.AspNetCore.Http;

namespace JoyMoe.AspNetCore.Authentication.OpenID.Steam
{
    public class SteamOptions : OpenIdOptions
    {
        public SteamOptions()
        {
            AuthenticationScheme = SteamDefaults.AuthenticationScheme;
            DisplayName = SteamDefaults.DisplayName;
            Authority = new Uri(SteamDefaults.Authority);
            CallbackPath = new PathString("/signin-steam");
        }

        /// <summary>
        /// Gets or sets the application key used to retrive user details from Steam's API.
        /// </summary>
        public string ApplicationKey { get; set; }

        /// <summary>
        /// Gets or sets the endpoint used to retrieve user details.
        /// </summary>
        public string UserInformationEndpoint { get; set; } = SteamDefaults.UserInformationEndpoint;
    }
}
