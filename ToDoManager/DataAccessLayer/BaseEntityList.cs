﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccessLayer
{
    public class BaseEntityList<TEntity> : List<TEntity> where TEntity : BaseEntity<TEntity>, IEntity
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