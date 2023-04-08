﻿using Sat.Recruitment.Api.Models;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services
{
    public interface IUserService
    {
        bool IsUserDuplicated(User user);
        Task SaveUserToFile(User user);
    }
}
