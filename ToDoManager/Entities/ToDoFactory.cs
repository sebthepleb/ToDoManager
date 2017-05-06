using DataAccessLayer;
using Shared.CustomAttributes;

namespace Entities
{
    [IocBulkLoad]
    public interface IToDoFactory : IEntityFactory<IToDo, IToDoList>
    {
    }

    public class ToDoFactory : IToDoFactory
    {
        public IToDo GetEntity()
        {
            return new ToDo();
        }

        public IToDo GetEntity(int id)
        {
            return new ToDo(id);
        }

        public IToDoList GetEntityList()
        {
            return new ToDoList();
        }
    }
}