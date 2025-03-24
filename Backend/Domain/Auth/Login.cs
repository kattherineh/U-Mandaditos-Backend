namespace Domain.Auth
{
    public class Login
    {
        public string? Email;
        public string? Password;

        public Login(string email, string password) 
        {
            Email = email;  
            Password = password;
        }
    }
}
