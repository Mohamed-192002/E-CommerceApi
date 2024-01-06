using AutoMapper;
using ECommerce.Core.DTO;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ECommerce.Infrastructure.Repositories
{
    public class TokenServices(UserManager<AppUser> _userManager, IOptions<JWT> jwt, IMapper mapper) : ITokenServices
    {
        private readonly UserManager<AppUser> userManager = _userManager;
        private readonly IMapper _mapper = mapper;
        private readonly JWT _jwt = jwt.Value;



        // register
        public async Task<UserDTO> registerAsync(RegisterDTO model)
        {
            if (await userManager.FindByEmailAsync(model.Email) != null)
                return new UserDTO { Message = "Email is already registed!" };
            if (await userManager.FindByNameAsync(model.UserName) != null)
                return new UserDTO { Message = "UserName is already registed!" };
            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,    
                FiratName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var item in result.Errors)
                {
                    errors += $"{item.Description},";
                }
                return new UserDTO { Message = errors };
            }

            var JwtSecurityToken = await CreateJwtToken(user);
            return new UserDTO
            {
                Email = user.Email,
                ExpiresOn = JwtSecurityToken.ValidTo,
                IsAuthanticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken),
                UserName = user.UserName,
                Address = _mapper.Map<Address, AddressDTO>(user.Address)

            };
        }

        // Login
        public async Task<UserDTO> GetTokenAsync(LoginDTO model)
        {
            var authModel = new UserDTO();
            var user = await userManager.Users.Include(x => x.Address).FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user is null || !await userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            authModel.IsAuthanticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;

            authModel.Address = _mapper.Map<AddressDTO>(user.Address);

            return authModel;
        }

        // Current User
        public async Task<UserDTO> GetCurrentUser(string email)
        {
            var authModel = new UserDTO();
            var user = await userManager.Users.Include(x => x.Address).FirstOrDefaultAsync(x => x.Email == email);

            var jwtSecurityToken = await CreateJwtToken(user);

            authModel.IsAuthanticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Address = _mapper.Map<AddressDTO>(user.Address);

            return authModel;
        }

        // Create JWT Token
        private async Task<JwtSecurityToken> CreateJwtToken(AppUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
