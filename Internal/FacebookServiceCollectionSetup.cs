using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebHooks.Filters;
using Microsoft.AspNetCore.WebHooks.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.WebHooks.Internal
{
    public static class FacebookServiceCollectionSetup
    {
        public static void AddFacebookServices(IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<MvcOptions>, MvcOptionsSetup>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IWebHookMetadata, FacebookMetadata>());
            services.TryAddSingleton<FacebookVerifySignatureFilter>();
        }
        private class MvcOptionsSetup : IConfigureOptions<MvcOptions>
        {
            public void Configure(MvcOptions options)
            {
                if (options == null)
                {
                    throw new ArgumentNullException(nameof(options));
                }

                options.Filters.AddService<FacebookVerifySignatureFilter>(WebHookSecurityFilter.Order);
            }
        }
    }
}
