using CircleApp.Constants;
using CircleApp.Data;
using CircleApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CircleApp.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly ApplicationDbContext _context;
        public FriendshipService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, List<string> Errors)> AcceptRequest(int requestId)
        {
            var errors = new List<string>();
            try
            {
                var request = await _context.Friendrequests.FirstOrDefaultAsync(fr => fr.Id == requestId);
                if (request == null)
                {
                    errors.Add("Invalid request id");
                    return (false, errors);
                }

                var (success, createFriendError) = await CreateFriendShip(request.SenderId,request.ReceiverId);
                if (success)
                {
                    request.Status = FriendRequestStatus.Approved;
                    request.UpdatedAt = DateTime.UtcNow;
                    _context.Friendrequests.Update(request);
                    await _context.SaveChangesAsync();
                    return (true, errors);
                }
                else
                {
                    errors.AddRange(createFriendError);
                    return (false, errors);
                }
            }
            catch(Exception ex)
            {
                errors.Add("An error occured while accepting friend request");
                errors.Add(ex.Message);
                return (false, errors);
            }

        }

        
        public async Task<(bool Success, List<string> Errors)> CreateFriendShip(int senderId, int receiverId)
        {
            var errors = new List<string>();
            try
            {
                var friendship = new Friendship()
                {
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _context.Friendships.Add(friendship);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return (true, errors);
                }
                else
                {
                    errors.Add("Error while creating friend");
                    return (false, errors);

                }
            }
            catch(Exception ex)
            {
                errors.Add(ex.Message);
                return (false, errors);
            }
        }


        public async Task<(bool Success, List<string> Errors)> CancelRequest(int requestId)
        {
            var errors = new List<string>();
            try
            {
                var request =await _context.Friendrequests.FirstOrDefaultAsync(fr => fr.Id == requestId);
                if(request==null)
                {
                    errors.Add("Invalid request Id");
                    return (false, errors);
                }
                request.UpdatedAt = DateTime.UtcNow;
                request.Status = FriendRequestStatus.Cancled;
                _context.Friendrequests.Update(request);
                var result = await _context.SaveChangesAsync();
                if(result>0)
                {
                    return (true, errors);
                }
                else
                {
                    errors.Add("Request is not updated");
                    return (false, errors);
                }
            }
            catch(Exception ex)
            {
                errors.Add("An error occured while creating friend request");
                errors.Add(ex.Message);
                return (false, errors);
            }
        }

        public async Task<(bool Success, List<string> Errors)> CreateFriendrequest(int senderId, int receiverid)
        {
            var errors = new List<string>();
            if (senderId == receiverid)
            {
                errors.Add("ReceiverId is similar to senderid");
                return (false, errors);
            }
            var existFriendrequest = _context.Friendrequests.FirstOrDefault(fr => (fr.Status==FriendRequestStatus.Pending)&&((fr.SenderId == senderId && fr.ReceiverId == receiverid) || (fr.SenderId == receiverid && fr.ReceiverId == senderId)));
            if (existFriendrequest != null)
            {
                errors.Add("This friend request is already created");
                return (false, errors);
            }
            try
            {
                var friendRequest = new Friendrequest()
                {
                    SenderId = senderId,
                    ReceiverId = receiverid,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Status = FriendRequestStatus.Pending
                };
                _context.Friendrequests.Add(friendRequest);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return (true, errors);
                }
                else
                {
                    errors.Add("Friend request is not created");
                    return (false, errors);
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return (false, errors);
            }
        }


        public async Task<(bool Success, List<string> Errors)> RejectRequest(int requestId)
        {
            var errors = new List<string>();
            try
            {
                var request = await _context.Friendrequests.FirstOrDefaultAsync(fr => fr.Id == requestId);
                if (request == null)
                {
                    errors.Add("Invalid request Id");
                    return (false, errors);
                }
                request.UpdatedAt = DateTime.UtcNow;
                request.Status = FriendRequestStatus.Rejected;
                _context.Friendrequests.Update(request);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return (true, errors);
                }
                else
                {
                    errors.Add("Request is not updated");
                    return (false, errors);
                }
            }
            catch (Exception ex)
            {
                errors.Add("An error occured while creating friend request");
                errors.Add(ex.Message);
                return (false, errors);
            }

        }
        public async Task<(bool Success, List<string> Errors)> RemoveFriendAsync(int friendshipid)
        {
            var errors = new List<string>();
            try
            {
                var friendship = await _context.Friendships.FirstOrDefaultAsync(fr => fr.Id == friendshipid);
                if(friendship==null)
                {
                    errors.Add("Invalid friendship Id");
                    return (false, errors);
                }
                _context.Friendships.Remove(friendship);
                var result=await _context.SaveChangesAsync();
                if(result>0)
                {
                    return (true, errors);
                }
                else
                {
                    errors.Add("Unable to  remove friend");
                    return (false, errors);
                }
            }
            catch(Exception ex)
            {
                errors.Add(ex.Message);
                return (false, errors);
            }
        }
    }
}
