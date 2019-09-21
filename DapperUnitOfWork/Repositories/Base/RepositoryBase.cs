﻿using System.Data;

namespace DapperUnitOfWork.Repositories.Base
{
    internal abstract class RepositoryBase
    {
        protected IDbTransaction Transaction { get; }
        protected IDbConnection Connection => Transaction.Connection;

        protected RepositoryBase(IDbTransaction transaction) => Transaction = transaction;
    }
}