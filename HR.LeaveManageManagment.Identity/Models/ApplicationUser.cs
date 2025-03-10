﻿using Microsoft.AspNetCore.Identity;
using System.Security.Principal;

namespace HR.LeaveManagement.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
