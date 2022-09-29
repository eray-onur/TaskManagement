namespace TaskManagement.Application.Models.Requests.Auth
{
    public class AuthenticateRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public AuthenticateRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}