using System.Web.Mvc;

namespace ToDoManager.Controllers
{
    public class ManageController : Controller
    {
        public ActionResult List()
        {
            return View();
        }
    }
}