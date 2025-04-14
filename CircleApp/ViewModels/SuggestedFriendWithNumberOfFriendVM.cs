namespace CircleApp.ViewModels
{
    public class SuggestedFriendWithNumberOfFriendVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ProfilePictureUrl { get; set; }
        public int NumberOfFriend { get; set; }



        public string FollowerText 
        {
            get
            {
                if (NumberOfFriend == 0)
                {
                    return "No Follower";
                }
                else if (NumberOfFriend == 1)
                {
                    return "1 Follower";
                }
                else
                {
                    return $"{NumberOfFriend} Followers";
                }
            }
        }
    }
}
