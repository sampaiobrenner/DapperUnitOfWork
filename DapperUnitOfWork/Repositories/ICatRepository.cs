using System.Collections.Generic;
using System.Threading.Tasks;
using DapperUnitOfWork.Entities;

namespace DapperUnitOfWork.Repositories
{
    public interface ICatRepository
    {
        void Add(Cat entity);

        IEnumerable<Cat> GetAll();

        Task<IEnumerable<Cat>> GetAllASync();

        Cat FindById(int id);

        IEnumerable<Cat> FindByBreedId(int breedId);

        void Remove(int id);

        void Remove(Cat entity);

        void Update(Cat entity);
    }
}