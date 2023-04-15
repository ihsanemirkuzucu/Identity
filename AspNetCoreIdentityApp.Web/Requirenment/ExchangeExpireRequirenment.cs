using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AspNetCoreIdentityApp.Web.Requirenment
{
    public class ExchangeExpireRequirenment : IAuthorizationRequirement
    {
    }
    public class ExchangeExpireRequirenmentHandler : AuthorizationHandler<ExchangeExpireRequirenment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ExchangeExpireRequirenment requirement)
        {

            if (!context.User.HasClaim(x => x.Type == "ExchangeExpireDate"))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            Claim exchangeExpireDate = context.User.FindFirst("ExchangeExpireDate")!;
            if (DateTime.Now > Convert.ToDateTime(exchangeExpireDate.Value))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
