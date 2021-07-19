using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Verification.Models;

namespace Verification.Data.IRepos
{
    public interface IUserRepo
    {
        Task<User> CreateUser(User user);
    }
}