using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Verification.Data.IRepos;
using Verification.Models;

namespace Verification.Data.Repos
{
    public class UserRepo : CommonRepo<User>, IUserRepo
    {
        public UserRepo(MyContext context) : base(context)
        {
        }

        public async Task<bool> IsExisted(string email)
        {
            return await DbSet.AnyAsync(x => x.Email == email);
        }
    }
}