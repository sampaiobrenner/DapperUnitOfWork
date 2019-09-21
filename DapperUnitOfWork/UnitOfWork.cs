using DapperUnitOfWork.Repositories;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DapperUnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IBreedRepository _breedRepository;
        private ICatRepository _catRepository;
        private bool _disposed;

        public UnitOfWork()
        {
            _connection = new SqlConnection(@"Server=MATHEUS\SQLEXPRESS;Database=LosGatos;User Id=sa; Password=hiper;");
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IBreedRepository BreedRepository => _breedRepository ?? (_breedRepository = new BreedRepository(_transaction));

        public ICatRepository CatRepository => _catRepository ?? (_catRepository = new CatRepository(_transaction));

        public void Commit()
        {
            try
            {
                _transaction?.Commit();
            }
            catch
            {
                _transaction?.Rollback();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        private void ResetRepositories()
        {
            _breedRepository = null;
            _catRepository = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }
            }
            _disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}