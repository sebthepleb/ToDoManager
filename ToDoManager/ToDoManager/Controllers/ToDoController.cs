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

        [HttpPost]
        [Route]
        [ResponseType(typeof(ToDoModel))]
        public IHttpActionResult CreateToDo(ToDoModel model)
        {
            var todo = Ioc.Get<IToDoManager>().SaveToDo(model);
            return Ok(todo);
        }

        [HttpPut]
        [Route("{id:long}")]
        [ResponseType(typeof(ToDoModel))]
        public IHttpActionResult SaveToDo(long id, ToDoModel model)
        {
            if (model == null)
                return BadRequest("The ToDo model was not provided.");

            // Treat the Id parameter as authoritative.
            model.Id = id;

            var todo = Ioc.Get<IToDoManager>().SaveToDo(model);
            return Ok(todo);
        }
    }
}