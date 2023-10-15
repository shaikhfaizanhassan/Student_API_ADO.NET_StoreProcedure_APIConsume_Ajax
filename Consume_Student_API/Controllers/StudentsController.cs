using Microsoft.AspNetCore.Mvc;

namespace Consume_Student_API.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Details()
        {
            return View();
        }


    }
}
