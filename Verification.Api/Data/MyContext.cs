using Microsoft.EntityFrameworkCore;
using Verification.Api.Models;

namespace Verification.Api.Data
{
    public class MyContext: DbContext
    {
        public MyContext(DbContextOptions<MyContext> opt) : base(opt)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}