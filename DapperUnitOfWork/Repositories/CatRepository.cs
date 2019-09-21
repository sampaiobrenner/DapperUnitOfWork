using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DapperUnitOfWork.Entities;

namespace DapperUnitOfWork.Repositories
{
    internal class CatRepository : RepositoryBase, ICatRepository
    {
        public CatRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public IEnumerable<Cat> All() => Connection.Query<Cat>("SELECT * FROM Cat", null, Transaction);

        public Task<IEnumerable<Cat>> AllASync() => Connection.QueryAsync<Cat>("SELECT * FROM Cat", null, Transaction);

        public Cat Find(int id)
        {
            var parametros = new
            {
                CatId = id
            };

            var query = @"SELECT * FROM Cat WHERE CatId = @CatId";

            return Connection.Query<Cat>(query, parametros, Transaction).FirstOrDefault();
        }

        public void Add(Cat entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var parametros = new
            {
                entity.BreedId,
                entity.Name,
                entity.Age
            };

            var query = @"  INSERT INTO Cat(BreedId, Name, Age)
                            VALUES(@BreedId, @Name, @Age);
                            SELECT SCOPE_IDENTITY()";

            entity.CatId = Connection.ExecuteScalar<int>(query, parametros, Transaction);
        }

        public void Update(Cat entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            var parametros = new
            {
                entity.CatId,
                entity.BreedId,
                entity.Name,
                entity.Age
            };

            var query = @"UPDATE Cat SET BreedId = @BreedId, Name = @Name, Age = @Age WHERE CatId = @CatId";

            Connection.Execute(query, parametros, Transaction);
        }

        public void Remove(Cat entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            Remove(entity.CatId);
        }

        public void Remove(int id)
        {
            var parametros = new
            {
                CatId = id
            };

            var query = @"DELETE FROM Cat WHERE CatId = @CatId";

            Connection.Execute(query, parametros, Transaction);
        }

        public IEnumerable<Cat> FindByBreedId(int breedId) =>
            Connection.Query<Cat>("SELECT * FROM Cat WHERE BreedId = @BreedId", new { BreedId = breedId }, Transaction);
    }
}