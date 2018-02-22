using Aleph1.Logging;
using Aleph1.Security.Contracts;
using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.Security.Contracts;

namespace Aleph1.Skeletons.WebAPI.Security.Implementation
{
    internal class SecurityService : ISecurity
    {
        private readonly ICipher CipherService = null;

        [Logged(LogParameters = false)]
        public SecurityService(ICipher cipherService)
        {
            this.CipherService = cipherService;
        }

        [Logged]
        public string GenerateTicket(string userUniqueID, bool isManager)
        {
            AuthenticationInfo authInfo = new AuthenticationInfo() { IsManager = isManager };
            return this.CipherService.Encrypt(SettingsManager.AppPrefix, userUniqueID, authInfo, SettingsManager.TicketDurationTimeSpan);
        }

        [Logged]
        public string ReGenerateTicket(string ticketValue, string userUniqueID)
        {
            AuthenticationInfo authInfo = this.CipherService.Decrypt<AuthenticationInfo>(SettingsManager.AppPrefix, userUniqueID, ticketValue);
             return this.CipherService.Encrypt(SettingsManager.AppPrefix, userUniqueID, authInfo, SettingsManager.TicketDurationTimeSpan);
        }

        [Logged(LogReturnValue = true)]
        public bool IsAllowedForManagementContent(string ticketValue, string userUniqueID)
        {
            AuthenticationInfo authInfo = this.CipherService.Decrypt<AuthenticationInfo>(SettingsManager.AppPrefix, userUniqueID, ticketValue);
            return authInfo != default(AuthenticationInfo) && authInfo.IsManager;
        }

        [Logged(LogReturnValue = true)]
        public bool IsAllowedForRegularContent(string ticketValue, string userUniqueID)
        {
            AuthenticationInfo authInfo = this.CipherService.Decrypt<AuthenticationInfo>(SettingsManager.AppPrefix, userUniqueID, ticketValue);
            return authInfo != default(AuthenticationInfo);
        }
    }
}
