using System;

namespace HR.LeaveManagement.Application.Models.Identity
{
    public class RegistrationResponse
    {
        public string UserId { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpireAt { get; set; }
    }
}
