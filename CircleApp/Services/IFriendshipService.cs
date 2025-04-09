namespace CircleApp.Services
{
    public interface IFriendshipService
    {
        Task<(bool Success, List<string> Errors)>  CreateFriendrequest(int senderId, int receiverid);
        Task<(bool Success, List<string> Errors)> AcceptRequest(int requestId);
        Task<(bool Success, List<string> Errors)> RejectRequest(int requestId);
        Task<(bool Success, List<string> Errors)> CancelRequest(int requestId);

    }
}
