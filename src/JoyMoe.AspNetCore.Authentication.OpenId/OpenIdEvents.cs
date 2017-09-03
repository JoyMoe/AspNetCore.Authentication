/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/JoyMoe.AspNetCore.Authentication.OpenID.Providers
 * for more information concerning the license and the contributors participating to this project.
 */

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace JoyMoe.AspNetCore.Authentication.OpenID
{
    /// <summary>
    /// Specifies callback methods that the <see cref="OpenIdMiddleware{TOptions}"/>
    /// invokes to enable developer control over the OpenID2 authentication process.
    /// </summary>
    public class OpenIdEvents : RemoteAuthenticationEvents
    {
        /// <summary>
        /// Defines a notification invoked when the user is authenticated by the identity provider.
        /// </summary>
        public Func<OpenIdContext, Task> OnAuthenticated { get; set; } = context => Task.FromResult<object>(null);

        /// <summary>
        /// Gets or sets the delegate that is invoked when the RedirectToAuthorizationEndpoint method is invoked.
        /// </summary>
        public Func<RedirectContext<OpenIdOptions>, Task> OnRedirectToAuthorizationEndpoint { get; set; } = context =>
        {
            context.Response.Redirect(context.RedirectUri);
            return Task.CompletedTask;
        };

        /// <summary>
        /// Defines a notification invoked when the user is authenticated by the identity provider.
        /// </summary>
        /// <param name="context">The context of the event carries information in and results out.</param>
        /// <returns>Task to enable asynchronous execution</returns>
        public virtual Task Authenticated(OpenIdContext context) => OnAuthenticated(context);

        /// <summary>
        /// Called when a Challenge causes a redirect to authorize endpoint in the OAuth handler.
        /// </summary>
        /// <param name="context">Contains redirect URI and <see cref="AuthenticationProperties"/> of the challenge.</param>
        public virtual Task RedirectToAuthorizationEndpoint(RedirectContext<OpenIdOptions> context) => OnRedirectToAuthorizationEndpoint(context);
    }
}
