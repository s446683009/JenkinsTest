using System;
using System.Collections.Generic;
using System.Text;
using
using System.Linq;
using System.Linq.Expressions;

namespace SCR.Repository.Repositorys
{
    public interface IBaseReposity<T>
    {
       T FindById(object id);
       IQueryable FindList(Expression<Func<T,bool>> expression);
        


    }
}
