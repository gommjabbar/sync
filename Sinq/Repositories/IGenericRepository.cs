using System;
using System.Collections.Generic;
namespace Sinq.Repositories
{
    public interface IGenericRepository<TEntity>
     where TEntity : class
    {
        bool Delete(object id);
        void Delete(TEntity entityToDelete);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null, Func<System.Linq.IQueryable<TEntity>, System.Linq.IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        TEntity GetByID(object id);
        void Insert(TEntity entity);

       // void Insert(object id,TEntity entity); //l-am facut pt foldersApiCOntroller
       
        void Update(TEntity entityToUpdate);
    }
}
