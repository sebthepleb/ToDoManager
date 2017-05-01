using System.Web.Mvc;
using ToDoManager.Models;

namespace ToDoManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            var model = new ContactModel
            {
                Name = "Sebastian Brookfield",
                Email = "todomanager@sbrookfield.co.uk",
            };

            return View(model);
        }
    }
}