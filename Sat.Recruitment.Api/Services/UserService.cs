using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services
{
    public class UserService : IUserService
    {
        private List<User> GetUsers()
        {
            List<User> users = new List<User>();
            var reader = ReadUsersFromFile();

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result.Split(",");
                var user = new User
                {
                    Name = line[0].ToString(),
                    Email = line[1].ToString(),
                    Phone = line[2].ToString(),
                    Address = line[3].ToString(),
                    UserType = line[4].ToString(),
                    Money = decimal.Parse(line[5].ToString()),
                };
                users.Add(user);
            }

            reader.Close();
            return users;
        }

        private StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
            FileStream fileStream = new FileStream(path, FileMode.Open);
            return new StreamReader(fileStream);
        }

        public bool IsUserDuplicated(User user)
        {
            var users = GetUsers();
            return users.Any(u => u.Email == user.Email || u.Phone == user.Phone || (u.Name == user.Name && u.Address == user.Address));
        }

        public async Task SaveUserToFile(User user)
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
            using var writer = new StreamWriter(path, append: true);
            await writer.WriteLineAsync();
            await writer.WriteAsync($"{user.Name},{user.Email},{user.Phone},{user.Address},{user.UserType},{user.Money}");
        }
    }
}
