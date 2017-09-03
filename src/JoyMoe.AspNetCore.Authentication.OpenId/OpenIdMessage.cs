using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Extensions.Primitives;

namespace JoyMoe.AspNetCore.Authentication.OpenID
{
    /// <summary>
    /// Represents an OpenID message.
    /// </summary>
    public class OpenIdMessage
    {
        /// <summary>
        /// Initializes a new OpenID message.
        /// </summary>
        public OpenIdMessage()
            : this(new Dictionary<string, string>())
        {
        }

        /// <summary>
        /// Initializes a new OpenID message.
        /// </summary>
        /// <param name="parameters">The parameters associated with the message.</param>
        public OpenIdMessage(IDictionary<string, string> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            Parameters = parameters;
        }

        /// <summary>
        /// Initializes a new OpenID message.
        /// </summary>
        /// <param name="parameters">The parameters associated with the message.</param>
        public OpenIdMessage(IEnumerable<KeyValuePair<string, StringValues>> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            Parameters = parameters.ToDictionary(
                parameter => parameter.Key,
                parameter => (string) parameter.Value);
        }

        /// <summary>
        /// Gets or sets the openid.claimed_id property.
        /// </summary>
        public string ClaimedIdentifier
        {
            get { return GetParameter(OpenIdConstants.Parameters.ClaimedId); }
            set { SetParameter(OpenIdConstants.Parameters.ClaimedId, value); }
        }

        /// <summary>
        /// Gets or sets the openid.identity property.
        /// </summary>
        public string Identity
        {
            get { return GetParameter(OpenIdConstants.Parameters.Identity); }
            set { SetParameter(OpenIdConstants.Parameters.Identity, value); }
        }

        /// <summary>
        /// Gets or sets the openid.error property.
        /// </summary>
        public string Error
        {
            get { return GetParameter(OpenIdConstants.Parameters.Error); }
            set { SetParameter(OpenIdConstants.Parameters.Error, value); }
        }

        /// <summary>
        /// Gets or sets the openid.mode property.
        /// </summary>
        public string Mode
        {
            get { return GetParameter(OpenIdConstants.Parameters.Mode); }
            set { SetParameter(OpenIdConstants.Parameters.Mode, value); }
        }

        /// <summary>
        /// Gets or sets the openid.ns property.
        /// </summary>
        public string Namespace
        {
            get { return GetParameter(OpenIdConstants.Parameters.Namespace); }
            set { SetParameter(OpenIdConstants.Parameters.Namespace, value); }
        }

        /// <summary>
        /// Gets or sets the openid.realm property.
        /// </summary>
        public string Realm
        {
            get { return GetParameter(OpenIdConstants.Parameters.Realm); }
            set { SetParameter(OpenIdConstants.Parameters.Realm, value); }
        }

        /// <summary>
        /// Gets or sets the openid.return_to property.
        /// </summary>
        public string ReturnTo
        {
            get { return GetParameter(OpenIdConstants.Parameters.ReturnTo); }
            set { SetParameter(OpenIdConstants.Parameters.ReturnTo, value); }
        }

        /// <summary>
        /// Gets the parameters associated with this OpenID message.
        /// </summary>
        public IDictionary<string, string> Parameters { get; }

        /// <summary>
        /// Gets the attributes returned by the identity provider, or an empty
        /// dictionary if the message doesn't expose an attribute exchange alias.
        /// </summary>
        /// <returns>The attributes contained in this message.</returns>
        public IDictionary<string, string> GetAttributes()
        {
            var attributes = new Dictionary<string, string>(StringComparer.Ordinal);

            string alias;
            var extensions = GetExtensions();
            // If the ax alias cannot be found, return an empty dictionary.
            if (!extensions.TryGetValue(OpenIdConstants.Namespaces.Ax, out alias))
            {
                return attributes;
            }

            foreach (var parameter in Parameters)
            {
                var prefix = $"{OpenIdConstants.Prefixes.OpenId}.{alias}.{OpenIdConstants.Suffixes.Type}.";

                // Exclude parameters that don't correspond to the attribute exchange alias.
                if (!parameter.Key.StartsWith(prefix, StringComparison.Ordinal))
                {
                    continue;
                }

                // Exclude attributes whose alias is malformed.
                var name = parameter.Key.Substring(prefix.Length);
                if (string.IsNullOrEmpty(name))
                {
                    continue;
                }

                // Exclude attributes whose type is missing.
                var type = parameter.Value;
                if (string.IsNullOrEmpty(type))
                {
                    continue;
                }

                // Exclude attributes whose value is missing.
                string value;
                if (!Parameters.TryGetValue($"{OpenIdConstants.Prefixes.OpenId}.{alias}." +
                                            $"{OpenIdConstants.Suffixes.Value}.{name}", out value))
                {
                    continue;
                }

                // Exclude attributes whose value is null or empty.
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }

                attributes.Add(type, value);
            }

            return attributes;
        }

        /// <summary>
        /// Gets the extensions and their corresponding alias.
        /// </summary>
        /// <returns>The extensions contained in this message.</returns>
        public IDictionary<string, string> GetExtensions()
        {
            var extensions = new Dictionary<string, string>(StringComparer.Ordinal);

            foreach (var parameter in Parameters)
            {
                var prefix = $"{OpenIdConstants.Prefixes.Namespace}.";

                if (parameter.Key.StartsWith(prefix, StringComparison.Ordinal))
                {
                    extensions.Add(parameter.Value, parameter.Key.Substring(prefix.Length));
                }
            }

            return extensions;
        }

        /// <summary>
        /// Gets the parameter corresponding to the requested name and the default
        /// prefix or <c>null</c> if no appropriate parameter can be found.
        /// </summary>
        /// <param name="name">The parameter to retrieve.</param>
        /// <returns>The value extracted from the parameter.</returns>
        public string GetParameter(string name)
        {
            return GetParameter(OpenIdConstants.Prefixes.OpenId, name);
        }

        /// <summary>
        /// Gets the parameter corresponding to the requested name and the given
        /// prefix or <c>null</c> if no appropriate parameter can be found.
        /// </summary>
        /// <param name="prefix">The prefix used to discriminate the parameter.</param>
        /// <param name="name">The parameter to retrieve.</param>
        /// <returns>The value extracted from the parameter.</returns>
        public string GetParameter(string prefix, string name)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            string value;
            if (Parameters.TryGetValue($"{prefix}.{name}", out value))
            {
                return value;
            }

            return null;
        }

        /// <summary>
        /// Adds, replaces or removes the parameter corresponding
        /// to the requested name and the default prefix.
        /// </summary>
        /// <param name="name">The parameter to store.</param>
        /// <param name="value">The value associated with the parameter.</param>
        public void SetParameter(string name, [CanBeNull] string value)
        {
            SetParameter(OpenIdConstants.Prefixes.OpenId, name, value);
        }

        /// <summary>
        /// Adds, replaces or removes the parameter corresponding
        /// to the requested name and the given prefix.
        /// </summary>
        /// <param name="prefix">The prefix used to discriminate the parameter.</param>
        /// <param name="name">The parameter to store.</param>
        /// <param name="value">The value associated with the parameter.</param>
        public void SetParameter(string prefix, string name, [CanBeNull] string value)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            // If the parameter value is null, remove
            // it from the parameters dictionary.
            if (string.IsNullOrEmpty(value))
            {
                Parameters.Remove($"{prefix}.{name}");

                return;
            }

            Parameters[$"{prefix}.{name}"] = value;
        }
    }
}
