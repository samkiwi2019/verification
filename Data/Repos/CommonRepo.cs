using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Verification.Data.IRepos;
using Verification.Models;

namespace Verification.Data.Repos
{
    public abstract class CommonRepo<T> : ICommonRepo<T> where T : BaseEntity
    {
        private readonly MyContext _ctx;
        private readonly DbSet<T> _dbSet;
        
        protected CommonRepo(MyContext context)
        {
            _ctx = context;
            _dbSet = context.Set<T>();
        }
        
        public virtual async Task<T> Create(T t)
        {
            if (t == null)
            {
                throw new ArgumentException(nameof(t));
            }
            var result = await _dbSet.AddAsync(t);
            await _ctx.SaveChangesAsync();
            return result.Entity;
        }
    }
}