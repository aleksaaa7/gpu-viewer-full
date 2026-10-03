namespace Gpuviewer.Models
{
    public class RegisterResponseModel
    {
        public string? username { get; set; }
        public string? email { get; set; }
        public bool success { get; set; }
        public string? message { get; set; }
    }
}