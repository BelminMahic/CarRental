using System.Threading.Tasks;

namespace Acme.CarRentalService.DAL
{
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> 
        where TEntity : class
    {
        Task Delete(TEntity entityToDelete);

        Task Delete(object id);

        Task Insert(TEntity entity);

        Task Update(TEntity entityToUpdate);
    }
}
