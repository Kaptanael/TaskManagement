using System.Threading;
using System.Threading.Tasks;
using TaskManagement.Model;

namespace TaskManagement.Data.Repository
{
    public interface IValueRepository: IRepository<Value>
    {
        Task<bool> IsDuplicateAsync(string name, CancellationToken cancellationToken = default(CancellationToken));
    }
}
