using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccessLayer
{
    public interface IEntityList
    {
    }

    public class EntityList<TEntity> : List<TEntity>, IEntityList where TEntity : Entity<TEntity>, IEntity
    {
        protected void Populate(Expression<Func<TEntity, bool>> expression = null)
        {
            using (var context = new CustomDbContext<TEntity>())
            {
                var collection = expression == null
                    ? context.Entities
                    : context.Entities.Where(expression);

                foreach (var entity in collection)
                    Add(entity);
            }
        }
    }
}