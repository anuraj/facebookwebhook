using System;
using System.ComponentModel;
using Microsoft.AspNetCore.WebHooks.Internal;

namespace Microsoft.Extensions.DependencyInjection
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class FacebookMvcBuilderExtensions
    {
        public static IMvcBuilder AddFacebookWebHooks(this IMvcBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            FacebookServiceCollectionSetup.AddFacebookServices(builder.Services);

            return builder.AddWebHooks();
        }
    }
}
