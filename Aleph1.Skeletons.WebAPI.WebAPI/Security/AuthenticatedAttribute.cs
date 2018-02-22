using Aleph1.Skeletons.WebAPI.Security.Contracts;
using Aleph1.Skeletons.WebAPI.WebAPI.Security.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Security
{
    internal class AuthenticatedAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public bool AllowAnonymous { get; set; }
        public bool RequireManagerAccess { get; set; }

        #region Security Service
        //has to be injected at run time
        public static ISecurity _securityService = null;
        public static ISecurity SecurityService
        {
            get
            {
                return _securityService ?? throw new NullReferenceException("SecurityService was not injected to the Authenticated Attribute");
            }
        }
        #endregion Security Service

        /// <summary>Authenticates the request.</summary>
        /// <param name="context">The authentication context.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task that will perform authentication.</returns>
        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            try
            {
                if(!AllowAnonymous)
                {
                    string authValue = context.Request.Headers.GetAuthenticationInfoValue();

                    //Client IP
                    string userUniqueID = HttpContext.Current.Request.UserHostAddress;

                    //Client logon Name (when using Windows Authentication)
                    //string userUniqueID = context.Principal.Identity.Name;

                    //TODO: Check WTE you want using the SecurityService
                    bool canAccess = RequireManagerAccess ?
                        SecurityService.IsAllowedForManagementContent(authValue, userUniqueID) :
                        SecurityService.IsAllowedForRegularContent(authValue, userUniqueID);
                    if(!canAccess)
                        throw new UnauthorizedAccessException("You are not allowed to perform this operation with this ticket");

                    //Regenerating a ticket with the same data - to reset the ticket life span
                    context.Request.Headers.AddAuthenticationInfoValue(SecurityService.ReGenerateTicket(authValue, userUniqueID));
                }
            }
            catch (Exception ex)
            {
                context.ErrorResult = new AuthenticationFailureResult(ex.Message, context.Request);
            }

            return Task.FromResult(0);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        /// <summary>pass the AuthenticationInfo value from the request to the response - if present</summary>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            string authValue = actionExecutedContext.Request.Headers.GetAuthenticationInfoValue();
            if (actionExecutedContext.Response != null)
            {
                actionExecutedContext.Response.Headers.AddAuthenticationInfoValue(authValue);
            }
        }
    }
}