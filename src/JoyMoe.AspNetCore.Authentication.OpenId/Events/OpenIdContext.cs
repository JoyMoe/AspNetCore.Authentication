/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/JoyMoe.AspNetCore.Authentication.OpenID.Providers
 * for more information concerning the license and the contributors participating to this project.
 */

using System.Collections.Generic;
using System.Security.Claims;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace JoyMoe.AspNetCore.Authentication.OpenID
{
    /// <summary>
    /// Exposes various information about the current OpenID authentication flow.
    /// </summary>
    public class OpenIdContext : BaseContext<OpenIdOptions>
    {
        public OpenIdContext(
            HttpContext context,
            AuthenticationScheme scheme,
            OpenIdOptions options,
            AuthenticationTicket ticket)
            : base(context, scheme, options)
        {
            Scheme = scheme;
            Options = options;
            Ticket = ticket;
        }

        /// <summary>
        /// Gets the options used by the OpenID authentication middleware.
        /// </summary>
        public new AuthenticationScheme Scheme { get; }

        /// <summary>
        /// Gets the options used by the OpenID authentication middleware.
        /// </summary>
        public new OpenIdOptions Options { get; }

        /// <summary>
        /// Gets or sets the authentication ticket.
        /// </summary>
        public AuthenticationTicket Ticket { get; set; }

        /// <summary>
        /// Gets the identity containing the claims associated with the current user.
        /// </summary>
        public ClaimsIdentity Identity => Ticket?.Principal?.Identity as ClaimsIdentity;

        /// <summary>
        /// Gets the identifier returned by the identity provider.
        /// </summary>
        public string Identifier => Ticket?.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        /// <summary>
        /// Gets the authentication properties associated with the ticket.
        /// </summary>
        public AuthenticationProperties Properties => Ticket?.Properties;

        /// <summary>
        /// Gets or sets the attributes associated with the current user.
        /// </summary>
        public IDictionary<string, string> Attributes { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Gets or sets the optional JSON payload extracted from the current request.
        /// This property is not set by the generic middleware but can be used by specialized middleware.
        /// </summary>
        public JObject User { get; set; } = new JObject();
    }
}
