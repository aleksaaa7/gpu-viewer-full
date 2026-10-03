using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Gpuviewer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Gpuviewer.Services
{
    public class JwtService : IJwtService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public JwtService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<LoginResponseModel?> AuthenticateAsync(LoginDto loginDto)
        {
          
            if (string.IsNullOrWhiteSpace(loginDto.username) || string.IsNullOrWhiteSpace(loginDto.password))
            {
                return null;
            }

            var user = await _userManager.FindByNameAsync(loginDto.username)
                       ?? await _userManager.FindByEmailAsync(loginDto.username);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.password))
            {
                return null; 
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            // Read jwt conf
            var jwtSettings = _configuration.GetSection("Jwtconfig");
            var keyString = jwtSettings["Key"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            int validityMins = int.Parse(jwtSettings["TokenValidityMins"] ?? "30");

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString!));
            var expirationTime = DateTime.UtcNow.AddMinutes(validityMins);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: expirationTime,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResponseModel
            {
                username = user.UserName,
                AccessToken = tokenString,
                expiresIn = validityMins * 60,
                roles = userRoles.ToList()
            };
        }
        public async Task<RegisterResponseModel?> RegisterAsync(RegisterDto registerDto)
        {
            if (string.IsNullOrWhiteSpace(registerDto.username) ||
                string.IsNullOrWhiteSpace(registerDto.email) ||
                string.IsNullOrWhiteSpace(registerDto.password))
            {
                return new RegisterResponseModel
                {
                    success = false,
                    message = "All fields are required."
                };
            }

            var existingUser = await _userManager.FindByNameAsync(registerDto.username)
                                ?? await _userManager.FindByEmailAsync(registerDto.email);

            if (existingUser != null)
            {
                return new RegisterResponseModel
                {
                    success = false,
                    message = "Username or email is already taken."
                };
            }

            var newUser = new IdentityUser
            {
                UserName = registerDto.username,
                Email = registerDto.email
            };

            var result = await _userManager.CreateAsync(newUser, registerDto.password);

            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                return new RegisterResponseModel
                {
                    success = false,
                    message = errors
                };
            }

            await _userManager.AddToRoleAsync(newUser, "Guest");

            return new RegisterResponseModel
            {
                username = newUser.UserName,
                email = newUser.Email,
                success = true,
                message = "Registration successful."
            };
        }
    }
}