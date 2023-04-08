using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Test
{
    internal class MockUserService : IUserService
    {
        private List<User> _users = new List<User>() { new User() { Name = "John", Email = "john@example.com", Phone = "1234567890", Address = "123 Main St", UserType = "Normal", Money = decimal.Parse("50.00") } };

        public bool IsUserDuplicated(User user)
        {
            return _users.Any(u => u.Email == user.Email || u.Phone == user.Phone || (u.Name == user.Name && u.Address == user.Address));
        }

        public async Task SaveUserToFile(User user)
        {
            _users.Add(user);
        }
    }
}
