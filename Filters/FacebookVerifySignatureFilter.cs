using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Microsoft.AspNetCore.WebHooks.Filters
{
    public class FacebookVerifySignatureFilter : WebHookVerifySignatureFilter, IAsyncResourceFilter
    {
        public FacebookVerifySignatureFilter(IConfiguration configuration, IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory)
                    : base(configuration, hostingEnvironment, loggerFactory)
        {
        }

        public override string ReceiverName => FacebookConstants.ReceiverName;

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            var routeData = context.RouteData;
            var request = context.HttpContext.Request;
            if (routeData.TryGetWebHookReceiverName(out var receiverName) &&
                IsApplicable(receiverName) &&
                HttpMethods.IsPost(request.Method))
            {
                var errorResult = EnsureSecureConnection(ReceiverName, context.HttpContext.Request);
                if (errorResult != null)
                {
                    context.Result = errorResult;
                    return;
                }
            }

            var secretKey = GetSecretKey(
                ReceiverName,
                routeData,
                FacebookConstants.SecretKeyMinLength,
                FacebookConstants.SecretKeyMaxLength);
            if (secretKey == null)
            {
                context.Result = new NotFoundResult();
                return;
            }

            if (request.Query.TryGetValue(FacebookConstants.VerifyTokenParameterName, out StringValues verifytoken)
                && verifytoken.FirstOrDefault() != secretKey)
            {
                context.Result = new BadRequestResult();
                return;
            }

            if (request.Query.TryGetValue(FacebookConstants.ChallengeParameterName, out StringValues challenge))
            {
                context.Result = new OkObjectResult(challenge.FirstOrDefault());
                return;
            }

            await next();
        }
    }
}
