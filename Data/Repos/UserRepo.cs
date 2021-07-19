using System.Threading.Tasks;
using Verification.Data.IRepos;
using Verification.Models;

namespace Verification.Data.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly MyContext _ctx;

        protected UserRepo(MyContext context)
        {
            _ctx = context;
        }

        public async Task<User> CreateUser(User user)
        {
            var result = await _ctx.AddAsync(user);
            await _ctx.SaveChangesAsync();
            return result.Entity;
        }
    }
}