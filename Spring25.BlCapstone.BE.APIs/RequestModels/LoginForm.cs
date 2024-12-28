using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Spring25.BlCapstone.BE.APIs.RequestModels
{

    public class LoginForm 
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
