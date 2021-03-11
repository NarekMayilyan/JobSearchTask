using JobSearch.DAL.Interfaces;
using System.Threading.Tasks;

namespace JobSearch.DAL
{
    public interface IUnitOfWork
    {
        IJobCategoryRepository JobCategoryRepository { get; }
        IJobRepository JobRepository { get; }

        void Save();
        Task SaveAsync();
    }
}
