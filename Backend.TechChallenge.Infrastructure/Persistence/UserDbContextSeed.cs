using Backend.TechChallenge.Domain;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.TechChallenge.Infrastructure.Persistence
{
    public static class UserDbContextSeed
    {
        public static async Task SeedAsync(UserDbContext context)
        {
            if (!context.Users!.Any())
            {
                await context.Users!.AddRangeAsync(GetPreconfiguredUsers());
                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<User> GetPreconfiguredUsers()
        {
            var users = new List<User>();
            var path = @"..\Backend.TechChallenge.Infrastructure\Data\Users.txt";
            var fileStream = new FileStream(path, FileMode.Open);
            var reader = new StreamReader(fileStream);

            try
            {
                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLineAsync().Result;
                    var user = new User
                    {
                        Name = line.Split(',')[0].ToString(),
                        Email = line.Split(',')[1].ToString(),
                        Phone = line.Split(',')[2].ToString(),
                        Address = line.Split(',')[3].ToString(),
                        UserType = EnumHelper<UserTypes>.Parse(line.Split(',')[4].ToString()),
                        Money = decimal.Parse(line.Split(',')[5].ToString()),
                    };
                    users.Add(user);
                }
            }
            finally { 
                fileStream.Dispose();
                reader.Close();
            }

            return users;
        }
    }
}
