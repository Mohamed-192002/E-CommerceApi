using ECommerce.Core.DTO;
using ECommerce.Core.Entities;
using System.Security.Claims;

namespace ECommerce.Core.Interfaces
{
    public interface ITokenServices
    {
        Task<UserDTO> registerAsync(RegisterDTO model);
        Task<UserDTO> GetTokenAsync(LoginDTO model);
        Task<UserDTO> GetCurrentUser(string email);
    }
}
