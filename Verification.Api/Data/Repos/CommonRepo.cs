using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Verification.Api.Data.IRepos;
using Verification.Api.Models;

namespace Verification.Api.Data.Repos
{
    public abstract class CommonRepo<T> : ICommonRepo<T> where T : BaseEntity
    {
        protected readonly MyContext Ctx;
        protected readonly DbSet<T> DbSet;
        
        protected CommonRepo(MyContext context)
        {
            Ctx = context;
            DbSet = context.Set<T>();
        }
        
        public virtual async Task<T> Create(T t)
        {
            if (t == null)
            {
                throw new ArgumentException(nameof(t));
            }
            var result = await DbSet.AddAsync(t);
            await Ctx.SaveChangesAsync();
            return result.Entity;
        }
    }
}