namespace Gpuviewer.Models
{
    public class LoginResponseModel
    {
        public string? username { get; set; }
        public string? AccessToken { get; set; }
        public int? expiresIn { get; set; }
        public List<string>? roles { get; set; }
    }
}