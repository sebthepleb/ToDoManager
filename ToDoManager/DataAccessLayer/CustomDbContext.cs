using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DataAccessLayer.CustomAttributes;
using Shared.Enums;

namespace DataAccessLayer
{
    public class CustomDbContext<TEntity> : DbContext where TEntity : BaseEntity<TEntity>, IEntity
    {
        public CustomDbContext() : base($"name={GetConnectionString()}")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<CustomDbContext<TEntity>>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new EntityConfiguration<TEntity>());
        }

        public DbSet<TEntity> Entities { get; set; }

        private static ConnectionStrings GetConnectionString()
        {
            var connectionString = typeof(TEntity).GetCustomAttribute<DalTable>()?.ConnectionString;
            
            if (connectionString == null)
                throw new Exception($"No connection string was found for entity {typeof(TEntity).Name}");

            return connectionString.Value;
        }
    }

    public class EntityConfiguration<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : BaseEntity<TEntity>, IEntity
    {
        public EntityConfiguration()
        {
            var entityType = typeof(TEntity);
            var tableAttribute = entityType.GetCustomAttribute<DalTable>();
            var mappableProperties = entityType.GetProperties().Where(p => p.GetCustomAttribute<DalColumn>() != null);

            // Set the table name for the entity.
            ToTable(tableAttribute.TableName);
            
            // Set the primary key column.
            HasKey(x => x.Id);

            // Map the base entity properties to their equivalent column names.
            Property(x => x.Id).HasColumnName(tableAttribute.IdentityColumnName);
            Property(x => x.DateCreated).HasColumnName("dteDateCreated");
            Property(x => x.DateUpdated).HasColumnName("dteDateUpdated");
            Property(x => x.UpdateUsername).HasColumnName("vchUpdateUsername");

            var expressionParameter = Expression.Parameter(entityType);
            var funcType = typeof(Func<,>);

            var generatedLambdaMethod = typeof(Expression)
                .GetMethods()
                .Single(mi => mi.ToString() == "System.Linq.Expressions.Expression`1[TDelegate] Lambda[TDelegate](System.Linq.Expressions.Expression, System.Linq.Expressions.ParameterExpression[])");

            var propertyMethods = GetType()
                    .GetMethods()
                    .Where(m => m.Name == "Property")
                    .ToArray();

            // Go through each property that has a column mapping, and apply it.
            // This goes into some pretty extreme generics, and after doing it all I honestly can't
            // remember why I didn't just use the [Column] attribute.
            foreach (var propertyInfo in mappableProperties)
            {
                var propertyName = propertyInfo.Name;
                var propertyType = propertyInfo.PropertyType;
                var columnName = propertyInfo.GetCustomAttribute<DalColumn>().ColumnName;

                var expressionProperty = Expression.Property(expressionParameter, propertyName);
                var func = funcType.MakeGenericType(entityType, propertyType);
                var genericLambda = generatedLambdaMethod.MakeGenericMethod(func);
                var genericLambdaInvoked = genericLambda.Invoke(null, new object[] { expressionProperty, new[] { expressionParameter } });
                var genericLambdaInvocationType = genericLambdaInvoked.GetType();
                var propertyMethod = propertyMethods.SingleOrDefault(m => m.GetParameters().Any(p => p.ParameterType == genericLambdaInvocationType));

                if (propertyMethod == null)
                {
                    var genericPropertyMethods = propertyMethods
                        .Where(m => m.IsGenericMethodDefinition)
                        .ToArray();

                    var underlyingType = Nullable.GetUnderlyingType(propertyType);
                    propertyMethod = genericPropertyMethods[underlyingType != null ? 1 : 0].MakeGenericMethod(underlyingType ?? propertyType);
                }

                var propertyMethodInvocation = propertyMethod.Invoke(this, new[] { genericLambdaInvoked });

                propertyMethodInvocation
                    .GetType()
                    .GetMethods()
                    .First(m => m.Name == "HasColumnName")
                    .Invoke(propertyMethodInvocation, new object[] {columnName});
            }
        }
    }
}