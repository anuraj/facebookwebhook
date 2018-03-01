using System;
using Microsoft.AspNetCore.WebHooks.Internal;
using Microsoft.AspNetCore.WebHooks.Metadata;

namespace Microsoft.AspNetCore.WebHooks
{
    public class FacebookWebHookAttribute : WebHookAttribute, IWebHookBodyTypeMetadata, IWebHookEventSelectorMetadata
    {
        public FacebookWebHookAttribute()
            : base(FacebookConstants.ReceiverName)
        {
        }
        public WebHookBodyType BodyType => WebHookBodyType.Json;
        public string EventName { get; set; }
    }
}
