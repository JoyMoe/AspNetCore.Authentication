using System;
using Microsoft.AspNetCore.Authentication;
using JoyMoe.AspNetCore.Authentication.OpenID.Steam;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Exposes convenient extensions that can be used to add an instance
    /// of the Steam authentication middleware in an ASP.NET 5 pipeline.
    /// </summary>
    public static class SteamExtensions
    {
        public static AuthenticationBuilder AddSteam(this AuthenticationBuilder builder)
			=> builder.AddSteam(SteamDefaults.AuthenticationScheme, _ => { });

		public static AuthenticationBuilder AddSteam(this AuthenticationBuilder builder, Action<SteamOptions> configuration)
			=> builder.AddSteam(SteamDefaults.AuthenticationScheme, configuration);

		public static AuthenticationBuilder AddSteam(this AuthenticationBuilder builder, string authenticationScheme, Action<SteamOptions> configuration)
			=> builder.AddSteam(authenticationScheme, SteamDefaults.DisplayName, configuration);

		public static AuthenticationBuilder AddSteam(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<SteamOptions> configuration)
		    => builder.AddScheme<SteamOptions, SteamHandler>(authenticationScheme, displayName, configuration);
    }
}
