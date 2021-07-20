using System.Threading.Tasks;

namespace Verification.Api.Data.IRepos
{
    public interface ICommonRepo<T>
    {
        Task<T> Create(T t);
    }
}