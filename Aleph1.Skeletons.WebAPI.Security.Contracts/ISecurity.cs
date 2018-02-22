namespace Aleph1.Skeletons.WebAPI.Security.Contracts
{
    public interface ISecurity
    {
        string GenerateTicket(string userUniqueID, bool isManager);
        string ReGenerateTicket(string ticketValue, string userUniqueID);

        bool IsAllowedForRegularContent(string ticketValue, string userUniqueID);
        bool IsAllowedForManagementContent(string ticketValue, string userUniqueID);
    }
}
