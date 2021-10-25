using SCR.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using 
namespace SCR.Repository.Repositorys
{
    class BaseRepository<T> : IBaseReposity<T>
    {
        private IUnitOfWork<IDbConnection> UOW;
        private IDbConnection db;
        public BaseRepository(IUnitOfWork<IDbConnection> unitOfWork) {
            UOW = unitOfWork;
            db = UOW.GetWorker();
        }
        public T FindById(object id)
        {
           return db.
        }

        public IQueryable FindList(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
