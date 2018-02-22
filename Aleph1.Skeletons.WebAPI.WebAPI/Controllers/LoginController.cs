using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.Security.Contracts;
using Aleph1.Skeletons.WebAPI.WebAPI.Security;
using Aleph1.Skeletons.WebAPI.WebAPI.Security.Helpers;
using Aleph1.WebAPI.ExceptionHandler;
using System.Web;
using System.Web.Http;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Controllers
{
    /// <summary>handle login</summary>
    public class LoginController : ApiController
    {
        private readonly ISecurity SecurityService;

        /// <summary>Initializes a new instance of the <see cref="LoginController"/> class.</summary>
        [Logged(LogParameters = false)]
        public LoginController(ISecurity securityService)
        {
            this.SecurityService = securityService;
        }

        /// <summary>Logins to the app (specify if as manager).</summary>
        /// <param name="isManager">if set to <c>true</c> [is manager].</param>
        [Authenticated(AllowAnonymous = true), Logged, HttpPost, Route("api/Login"), FriendlyMessage("התרחשה שגיאה בעת ההתחברות")]
        public void Login(bool isManager)
        {
            //Client IP
            string userUniqueID = HttpContext.Current.Request.UserHostAddress;

            //Client logon Name (when using Windows Authentication)
            //string userUniqueID = HttpContext.Current.User.Identity.Name;

            Request.Headers.AddAuthenticationInfoValue(SecurityService.GenerateTicket(userUniqueID, isManager));
        }
    }
}
