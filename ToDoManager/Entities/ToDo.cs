using DataAccessLayer;
using DataAccessLayer.CustomAttributes;
using Shared.Enums;

namespace Entities
{
    public interface IToDo : IEntity
    {
        string Title { get; set; }
        string Detail { get; set; }
    }

    [DalTable(ConnectionStrings.ToDoManagerDatabase, "tblToDo", "intToDoId")]
    public class ToDo : BaseEntity<ToDo>, IToDo
    {
        [DalColumn("vchTitle")]
        public string Title
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        [DalColumn("vchDetail")]
        public string Detail
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public ToDo()
        {
        }

        public ToDo(int id) : base(id)
        {
        }
    }
}