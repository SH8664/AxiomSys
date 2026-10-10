using AutoMapper;
using AxiomSysBL.IServices.IAuthServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AxiomSysBL.Services.AuthServices
{
    public class AuthService : IAuthService
    {

        private readonly IConfiguration _configuration;

        UserManager<IdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        IMapper _mapper;
        public AuthService(IConfiguration configuration, IMapper mapper, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task RegisterAsync(UserRegisterDto dto)
        {
            try
            {

                var user = _mapper.Map<IdentityUser>(dto);
                var result = await _userManager.CreateAsync(user, dto.Password);
                await _roleManager.CreateAsync(new IdentityRole("User"));
                await _userManager.AddToRoleAsync(user, "User");
            }
            catch (Exception ex)
            {
                // Extract the deep database error (e.g., FK violation, string truncation, duplicate key)
                var realError = ex.InnerException?.Message ?? ex.Message;
                throw new Exception($"Error registering user: {realError}");
            }

            return;
        }

        public async Task<string?> LoginAsync(UserLoginDto dto)
        {
            try
            {

                var user = await _userManager.FindByEmailAsync(dto.Email);

                if (user == null)
                {
                    return "not found";
                }

                var isValid = await _userManager.CheckPasswordAsync(user, dto.Password);

                if (isValid)
                {
                    return generateToken(user.Id);
                }

                return "not a correct password";
            }
            catch (Exception ex)
            {
                // Extract the deep database error (e.g., FK violation, string truncation, duplicate key)
                var realError = ex.InnerException?.Message ?? ex.Message;
                throw new Exception($"Error registering user: {realError}");
            }
        }



        public string generateToken(string userId)
        {
            var expirationMinutes = int.Parse(_configuration["jwt:expiresInMin"] ?? "60");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
            };

            var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration["jwt:key"]!));

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["jwt:issuer"],
                audience: _configuration["jwt:audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(expirationMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
