﻿namespace HR.LeaveManagement.Application.Models.Identity
{
    public class JwtSetting
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public double DurationInMinutes { get; set; }
        public double RefreshTokenDurationInDays { get; set; }
    }
}
