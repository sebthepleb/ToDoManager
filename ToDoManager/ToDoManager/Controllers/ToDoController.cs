using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using BusinessLayer.Framework;
using BusinessLayer.Managers;
using Models.ToDoManager;

namespace ToDoManager.Controllers
{
    [RoutePrefix("api/v1/ToDo/")]
    public class ToDoController : ApiController
    {
        [HttpGet]
        [Route]
        [ResponseType(typeof(List<ToDoModel>))]
        public IHttpActionResult GetAllToDos()
        {
            var toDos = Ioc.Get<IToDoManager>().GetAllToDos();
            return Ok(toDos);
        }

        [HttpGet]
        [Route("{id:long}")]
        [ResponseType(typeof(ToDoModel))]
        public IHttpActionResult GetToDo(long id)
        {
            var todo = Ioc.Get<IToDoManager>().GetToDoById(id);
            return Ok(todo);
        }

        [HttpPut]
        [HttpPost]
        [Route]
        [ResponseType(typeof(ToDoModel))]
        public IHttpActionResult SaveToDo(ToDoModel model)
        {
            var todo = Ioc.Get<IToDoManager>().SaveToDo(model);
            return Ok(todo);
        }
    }
}