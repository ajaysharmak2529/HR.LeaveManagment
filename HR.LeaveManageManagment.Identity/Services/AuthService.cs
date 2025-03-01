﻿using HR.LeaveManagement.Application.Constants;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HR.LeaveManagement.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSetting _jwtSettings;

        public AuthService(UserManager<ApplicationUser> userManager,
            IOptionsMonitor<JwtSetting> jwtSettings,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.CurrentValue;
            _signInManager = signInManager;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new Exception($"User with {request.Email} not found.");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new Exception($"Credentials for '{request.Email} aren't valid'.");
            }

            var accessToken = await GenerateToken(user);
            var RefreshToken = GenerateRefreshToken(user);

            AuthResponse response = new AuthResponse
            {
                Id = user.Id,
                Email = user.Email!,
                UserName = user.UserName!,
                AccessToken = accessToken,
                RefreshToken = RefreshToken,
                ExpireAt = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes)
            };

            return response;
        }
        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
            {
                throw new Exception($"Username '{request.UserName}' already exists.");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true
            };

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail == null)
            {

                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    var accessToken = await GenerateToken(user);
                    var refreshToken = GenerateRefreshToken(user);
                    user.RefreshToken = refreshToken;

                    await _userManager.UpdateAsync(user);

                    await _userManager.AddToRoleAsync(user, "Employee");
                    return new RegistrationResponse()
                    {
                        UserId = user.Id,
                        AccessToken = accessToken,
                        RefreshToken = refreshToken
                    };
                }
                else
                {
                    throw new Exception($"{result.Errors}");
                }
            }
            else
            {
                throw new Exception($"Email {request.Email} already exists.");
            }
        }
        private async Task<string> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(CustomClaimTypes.Uid, user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;
        }
        private string GenerateRefreshToken(ApplicationUser user)
        {

            var roleClaims = new List<Claim>();

            var claims = new[]
            {
                new Claim(CustomClaimTypes.Uid, user.Id)
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenDurationInDays),
                signingCredentials: signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;
        }
        public async Task<RefreshTokenResponse> RefreshUserToken(string refreshToken)
        {
            var JwtTokenValidator = new JwtSecurityTokenHandler();
            var validationResult = await JwtTokenValidator.ValidateTokenAsync(refreshToken, new TokenValidationParameters { });
            var response = new RefreshTokenResponse();
            if (validationResult.IsValid)
            {
                var JwtSecurityToken = JwtTokenValidator.ReadJwtToken(refreshToken);

                var userId = JwtSecurityToken.Claims.FirstOrDefault(x => x.Type == CustomClaimTypes.Uid)?.Value;
                var user = await _userManager.FindByIdAsync(userId!);
                if (user != null)
                {
                    var accessToken = await GenerateToken(user!);
                    var newRefreshToken = GenerateRefreshToken(user!);

                    user.RefreshToken = newRefreshToken;
                    await _userManager.UpdateAsync(user);

                    response.AccessToken = accessToken;
                    response.RefreshToken = newRefreshToken;
                    response.Success = true;
                }
                else
                {
                    response.Message = "User not found.";
                }
            }
            else
            {
                response.Message = "Invalid token or token has been expired.";
            }
            return response;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
