namespace Spring25.BlCapstone.BE.APIs.RequestModels.Auth
{
    public class SignUpForm
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime? DOB { get; set; }
        public string? Phone { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
