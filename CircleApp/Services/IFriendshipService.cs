using CircleApp.Data.Models;
using CircleApp.Dtos;

namespace CircleApp.Services
{
    public interface IFriendshipService
    {
        Task<(bool Success, List<string> Errors)>  CreateFriendrequest(int senderId, int receiverid);
        Task<(bool Success, List<string> Errors)> AcceptRequest(int requestId);
        Task<(bool Success, List<string> Errors)> RejectRequest(int requestId);
        Task<(bool Success, List<string> Errors)> CancelRequest(int requestId);
        Task<List<SuggestedFriendsWithNumberOfFriendDto>> GetSuggestedFriends(int userId);
        Task<List<Friendrequest>> GetSentFriendRequestAsync(int userId);
        Task<List<Friendrequest>> GetReceivedFriendRequestAsync(int userId);
        Task<(bool Success,List<User> Friends)> GetFriendsAsync(int userId);

    }
}
