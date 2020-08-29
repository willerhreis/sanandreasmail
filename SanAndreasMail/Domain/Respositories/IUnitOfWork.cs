using System.Threading.Tasks;

namespace SanAndreasMail.Domain.Respositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
