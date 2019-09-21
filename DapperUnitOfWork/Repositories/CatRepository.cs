using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DapperUnitOfWork.Entities;
using DapperUnitOfWork.Repositories.Base;

namespace DapperUnitOfWork.Repositories
{
    internal class CatRepository : RepositoryBase, ICatRepository
    {
        public CatRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public IEnumerable<Cat> GetAll() => Connection.Query<Cat>("SELECT * FROM Cat", transaction: Transaction);

        public Task<IEnumerable<Cat>> GetAllASync() => Connection.QueryAsync<Cat>("SELECT * FROM Cat", transaction: Transaction);

        public Cat FindById(int id)
        {
            var parameters = new { CatId = id };

            var query = @"SELECT * FROM Cat WHERE CatId = @CatId";

            return Connection.Query<Cat>(query, parameters, Transaction).FirstOrDefault();
        }

        public void Add(Cat entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var parameters = new
            {
                entity.BreedId,
                entity.Name,
                entity.Age
            };

            var query = @"  INSERT INTO Cat(BreedId, Name, Age)
                            VALUES(@BreedId, @Name, @Age);
                            SELECT SCOPE_IDENTITY()";

            entity.CatId = Connection.ExecuteScalar<int>(query, parameters, Transaction);
        }

        public void Update(Cat entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            var parameters = new
            {
                entity.CatId,
                entity.BreedId,
                entity.Name,
                entity.Age
            };

            var query = @"UPDATE Cat SET BreedId = @BreedId, Name = @Name, Age = @Age WHERE CatId = @CatId";

            Connection.Execute(query, parameters, Transaction);
        }

        public void Remove(Cat entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            Remove(entity.CatId);
        }

        public void Remove(int id)
        {
            var parameters = new { CatId = id };

            var query = @"DELETE FROM Cat WHERE CatId = @CatId";

            Connection.Execute(query, parameters, Transaction);
        }

        public IEnumerable<Cat> FindByBreedId(int breedId) =>
            Connection.Query<Cat>("SELECT * FROM Cat WHERE BreedId = @BreedId", new { BreedId = breedId }, Transaction);
    }
}