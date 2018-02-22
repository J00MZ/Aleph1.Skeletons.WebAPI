using Aleph1.Skeletons.WebAPI.Security.Contracts;

namespace Aleph1.Skeletons.WebAPI.Security.Moq
{
    internal class SecurityMoq : ISecurity
    {
        public string GenerateTicket(string userUniqueID, bool isManager)
        {
            return null;
        }

        public string ReGenerateTicket(string ticketValue, string userUniqueID)
        {
            return null;
        }

        public bool IsAllowedForManagementContent(string ticketValue, string userUniqueID)
        {
            return true;
        }

        public bool IsAllowedForRegularContent(string ticketValue, string userUniqueID)
        {
            return true;
        }

    }
}
