using DataAccessLayer;

namespace Entities
{
    public interface IToDoList
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