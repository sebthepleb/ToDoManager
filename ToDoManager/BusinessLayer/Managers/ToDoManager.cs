using System;
using System.Collections.Generic;
using Models.ToDoManager;
using Shared.CustomAttributes;

namespace BusinessLayer.Managers
{
    [IocBulkLoad]
    public interface IToDoManager
    {
        List<ToDoModel> GetAllToDos();
        ToDoModel GetToDoById(long id);
        ToDoModel SaveToDo(ToDoModel model);
    }

    public class ToDoManager : IToDoManager
    {
        public List<ToDoModel> GetAllToDos()
        {
            return new List<ToDoModel>
            {
                new ToDoModel
                {
                    Id = 1,
                    Title = "Test 1",
                    Detail = "I am the first test.",
                    DateCreated = DateTime.Parse("01/01/01")
                },
                new ToDoModel
                {
                    Id = 2,
                    Title = "Test 2",
                    Detail = "I am the second test.",
                    DateCreated = DateTime.Parse("02/02/02")
                }
            };
        }

        public ToDoModel GetToDoById(long id)
        {
            return new ToDoModel
            {
                Id = id,
                Title = $"Test {id}",
                Detail = $"I am test {id}.",
                DateCreated = DateTime.Now,
                UpdateUsername = "Hughbert.Cumberdale"
            };
        }

        public ToDoModel SaveToDo(ToDoModel model)
        {
            throw new NotImplementedException();
        }
    }
}