using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
namespace SCR.Repository
{
    internal class DapperUnitOfWork : IUnitOfWork<IDbConnection>,IDisposable
    {

        private IDbTransaction _transaction { get; set; }

        private IDbConnection con;

        internal DapperUnitOfWork(IDbConnection connection) {
            con = connection;

        }
        public void Begin()
        {
            _transaction = con.BeginTransaction();
        }

        public void Commit()
        {
            if (this._transaction != null)
            {
                this._transaction.Commit();
            }

            Dispose();

        }

        public void Dispose()
        {
            if (this._transaction != null) {
                this._transaction.Dispose();
                this._transaction = null;
            }

            //这里不能把connection 释放，因为这是代表工作单元的释放，并不意味着connection 需要释放
        }

        public void Rollback()
        {
            if (this._transaction != null)
            {
                this._transaction.Rollback();
            }

            Dispose();
        }

        public IDbConnection GetWorker()
        {
            return con;
        }
    }
}
