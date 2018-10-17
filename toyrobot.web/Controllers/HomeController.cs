
using System;
using System.Linq;
using System.Web.Mvc;
using toyrobot.Enums;
using toyrobot.web.Models;

namespace toyrobot.web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var model = new HomePageViewModel
            {
                Rows = 5,
                Columns = 5,
                BoxHeight = 100,
                BoxWidth = 100,
                Directions  = Enum.GetValues(typeof(Face)).Cast<Face>()
            };
            return View(model);
        }
    }
}