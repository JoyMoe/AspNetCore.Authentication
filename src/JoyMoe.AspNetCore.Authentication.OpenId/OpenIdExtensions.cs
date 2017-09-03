using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using JoyMoe.AspNetCore.Authentication.OpenID;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class OpenIdExtensions
	{
		public static AuthenticationBuilder AddOpenId(this AuthenticationBuilder builder)
			=> builder.AddOpenId<OpenIdOptions, OpenIdHandler<OpenIdOptions>>(OpenIdDefaults.AuthenticationScheme, _ => { });

		public static AuthenticationBuilder AddOpenId(this AuthenticationBuilder builder, Action<OpenIdOptions> configuration)
		{
			var options = new OpenIdOptions();
            configuration(options);

			return builder.AddOpenId<OpenIdOptions, OpenIdHandler<OpenIdOptions>>(options.AuthenticationScheme, configuration);
		}

		public static AuthenticationBuilder AddOpenId<TOptions, THandler>(this AuthenticationBuilder builder, string authenticationScheme, Action<TOptions> configuration)
			where TOptions : OpenIdOptions, new()
            where THandler : OpenIdHandler<TOptions>
		{
			var options = new TOptions();
            configuration(options);

			return builder.AddOpenId<TOptions, THandler>(authenticationScheme, options.DisplayName, configuration);
		}

		public static AuthenticationBuilder AddOpenId<TOptions, THandler>(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<TOptions> configuration)
			where TOptions : OpenIdOptions, new()
            where THandler : OpenIdHandler<TOptions>
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<TOptions>, OpenIdPostConfigureOptions<TOptions, THandler>>());
            return builder.AddRemoteScheme<TOptions, THandler>(authenticationScheme, displayName, configuration);
        }
	}
}
