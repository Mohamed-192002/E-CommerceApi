namespace ECommerce.Infrastructure.Data.Config
{
    public class IdentitySeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    FiratName = "Mohamed",
                    LastName = "abdelaziz",
                    Email = "Mohamed@gmail.com",
                    UserName = "Mo_Zizo",
                    Address = new Address
                    {
                        City = "Giza",
                        State = "haram",
                        ZipCode = "123"
                    }
                };
                await userManager.CreateAsync(user, "Mo@123456");
            }
        }
    }
}
