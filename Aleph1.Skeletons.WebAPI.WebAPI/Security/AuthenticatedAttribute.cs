using Aleph1.Security.Contracts;
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
        //will be injected at run time
        public static ICipher _chiperService = null;
        public static ICipher ChiperService
        {
            get
            {
                return _chiperService ?? throw new NullReferenceException("ChiperService was not injected to the Authenticated Attribute");
            }
        }

        public bool AllowAnonymous { get; set; }

        /// <summary>Authenticates the request.</summary>
        /// <param name="context">The authentication context.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task that will perform authentication.</returns>
        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            try
            {
                //Client IP
                string userUniqueID = HttpContext.Current.Request.UserHostAddress;

                //Client logon Name (when using Windows Authentication)
                //string userUniqueID = context.Principal.Identity.Name;


                AuthenticationInfo authInfo = context.Request.Headers.GetAuthenticationInfo(ChiperService, userUniqueID);

                //TODO: Check ticket validity
                if (!AllowAnonymous && (authInfo == default(AuthenticationInfo) || !authInfo.IsManager))
                    throw new UnauthorizedAccessException("Only Managers can perform this operation - maybe you forgot to send a ticket?");

                //insert a newly created Authentication info to the header (reset his life span)
                if (authInfo != default(AuthenticationInfo))
                    context.Request.Headers.AddAuthenticationInfo(ChiperService, userUniqueID, authInfo);
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
            string AuthenticationValue = actionExecutedContext.Request.Headers.GetAuthenticationInfoValue();
            if (!String.IsNullOrWhiteSpace(AuthenticationValue) && actionExecutedContext.Response != null)
            {
                actionExecutedContext.Response.Headers.AddAuthenticationInfoValue(AuthenticationValue);
            }
        }
    }
}