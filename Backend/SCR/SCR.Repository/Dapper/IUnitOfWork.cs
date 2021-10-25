using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SCR.Repository
{
    /// <summary>
    /// 为什么设计成泛型，因为 实现单元模式的提交和回滚 的载体并不确认，Dapper 是IDbConnection,EF 是dbcontext,所以延迟到具体实现类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IUnitOfWork<T>
    {
        T GetWorker();
        /// <summary>
        /// 为什么我们需要开始方法，因为并不是所有属于载体的的方法都需要使用工作单元模式，比如查询
        /// </summary>
        void Begin();
        void Commit();
        void Rollback();
    }
}
