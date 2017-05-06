using DataAccessLayer;

namespace Entities
{
    public interface IToDoList : IEntityList<ToDo>
    {    
    }

    public class ToDoList : EntityList<ToDo>, IToDoList
    {
        public ToDoList()
        {
            Populate();
        }
    }
}