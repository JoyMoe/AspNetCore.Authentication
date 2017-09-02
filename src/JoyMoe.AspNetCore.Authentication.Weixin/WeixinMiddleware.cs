using System;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace JoyMoe.AspNetCore.Authentication.Weixin
{
    public class WeixinMiddleware : OAuthMiddleware<WeixinOptions>
    {
        public WeixinMiddleware(
        RequestDelegate next,
        IDataProtectionProvider dataProtectionProvider,
        ILoggerFactory loggerFactory,
        UrlEncoder encoder,
        IOptions<SharedAuthenticationOptions> sharedOptions,
        IOptions<WeixinOptions> options)
            : base(next, dataProtectionProvider, loggerFactory, encoder, sharedOptions, options)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (dataProtectionProvider == null)
            {
                throw new ArgumentNullException(nameof(dataProtectionProvider));
            }

            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            if (encoder == null)
            {
                throw new ArgumentNullException(nameof(encoder));
            }

            if (sharedOptions == null)
            {
                throw new ArgumentNullException(nameof(sharedOptions));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
        }

        protected override AuthenticationHandler<WeixinOptions> CreateHandler()
        {
            return new WeixinHandler(Backchannel);
        }
    }
}
