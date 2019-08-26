namespace NPost.Modules.Users.Services
{
    internal interface IPasswordService
    {
        string Hash(string password);
        bool IsValid(string hash, string password);
    }
}