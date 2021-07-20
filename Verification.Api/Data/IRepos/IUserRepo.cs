using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Verification.Api.Models;

namespace Verification.Api.Data.IRepos
{
    public interface IUserRepo: ICommonRepo<User>
    {
        public Task<bool> IsExisted(string email);
    }
}