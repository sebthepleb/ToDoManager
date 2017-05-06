using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Framework;
using BusinessLayer.ModelFactories;
using Entities;
using Models.ToDoManager;
using Shared.CustomAttributes;

namespace BusinessLayer.Managers
{
    [IocBulkLoad]
    public interface IToDoManager
    {
        List<ToDoModel> GetAllToDos();
        ToDoModel GetToDoById(int id);
        ToDoModel SaveToDo(ToDoModel model);
        void DeleteToDo(int id);
    }

    public class ToDoManager : IToDoManager
    {
        public List<ToDoModel> GetAllToDos()
        {
            return Ioc.Get<IToDoFactory>()
                .GetEntityList()
                .Select(td => td.ToModel())
                .ToList();
        }

        public ToDoModel GetToDoById(int id)
        {
            return Ioc.Get<IToDoFactory>()
                .GetEntity(id)
                .ToModel();
        }

        public ToDoModel SaveToDo(ToDoModel model)
        {
            var factory = Ioc.Get<IToDoFactory>();
            var todo = model.Id == null ? factory.GetEntity() : factory.GetEntity(model.Id.Value);

            todo.Title = model.Title;
            todo.Detail = model.Detail;
            todo.Save();

            return todo.ToModel();
        }

        public void DeleteToDo(int id)
        {
            Ioc.Get<IToDoFactory>().GetEntity(id).Delete();
        }
    }
}