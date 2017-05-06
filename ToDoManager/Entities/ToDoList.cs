using DataAccessLayer;

namespace Entities
{
    public interface IToDoList
    {    
    }

    public class ToDoList : BaseEntityList<ToDo>, IToDoList
    {
        public ToDoList()
        {
            Populate();
        }
    }
}