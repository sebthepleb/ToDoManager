namespace DataAccessLayer
{
    public interface IEntityFactory<out TEntity, out TEntityList>
    {
        TEntity GetEntity();
        TEntity GetEntity(int id);
        TEntityList GetEntityList();
    }
}