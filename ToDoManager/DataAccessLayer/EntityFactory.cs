namespace DataAccessLayer
{
    public interface IEntityFactory<out TEntity, out TEntityList>
    {
        TEntity GetEntity();
        TEntityList GetEntityList();
    }
}