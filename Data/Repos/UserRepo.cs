using System.Threading.Tasks;
using Verification.Data.IRepos;
using Verification.Models;

namespace Verification.Data.Repos
{
    public class UserRepo : CommonRepo<User>, IUserRepo
    {
        public UserRepo(MyContext context) : base(context)
        {
        }
    }
}