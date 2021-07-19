using Microsoft.EntityFrameworkCore;
using Verification.Models;

namespace Verification.Data
{
    public class MyContext: DbContext
    {
        public MyContext(DbContextOptions<MyContext> opt) : base(opt)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}