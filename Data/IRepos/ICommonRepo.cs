using System.Threading.Tasks;

namespace Verification.Data.IRepos
{
    public interface ICommonRepo<T>
    {
        Task<T> Create(T t);
    }
}