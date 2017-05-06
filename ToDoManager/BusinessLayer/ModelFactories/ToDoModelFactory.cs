using Entities;
using Models.ToDoManager;

namespace BusinessLayer.ModelFactories
{
    public static class ToDoModelFactory
    {
        public static ToDoModel ToModel(this IToDo entity)
        {
            return new ToDoModel
            {
                Id = entity.Id,
                Title = entity.Title,
                Detail = entity.Detail,
                DateCreated = entity.DateCreated,
                DateUpdated = entity.DateUpdated,
                UpdateUsername = entity.UpdateUsername
            };
        }
    }
}