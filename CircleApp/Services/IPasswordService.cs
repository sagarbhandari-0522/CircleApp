namespace CircleApp.Services
{
    public interface IPasswordService
    {
            Task<(bool Success, List<string> Errors)> UpdatePasswordAsync(string currentUserId,string currentPassword, string newPassword, string confirmPassword);

    }
}
