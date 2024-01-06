using ECommerce.Core.Entities;

namespace ECommerce.Core.DTO
{
    public class UserDTO
    {
        public string Message { get; set; } = default!;
        public bool IsAuthanticated { get; set; }
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public AddressDTO Address { get; set; }
        public string Password { get; set; } = default!;
        public string Token { get; set; } = default!;
        public DateTime ExpiresOn { get; set; }
    }
}
