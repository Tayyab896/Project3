using System.Collections.Generic;
using System.Web.Mvc;

namespace EstateAgency.Controllers
{
    public class HomeController : Controller
    {
        // This action will show the home page
        public ActionResult Index()
        {
            // Group Member Data - Can be replaced with database or dynamic data
            var groupMembers = new List<dynamic>
            {
                new { Name = "Tayyab Ali", ID = "20006949" },
                new { Name = "Ghulam Mustafa", ID = "20008440" },
                new { Name = "Muzaffar Ali", ID = "20008770" },
                new { Name = "Aftab Ur Rehman", ID = "20008424" },
                new { Name = "Abdullah Khan", ID = "20004972" }
            };

            // Pass data to the view
            ViewBag.GroupMembers = groupMembers;

            return View();
        }

        // About Us Page
        public ActionResult About()
        {
            return View();
        }

        // Contact Us Page
        public ActionResult Contact()
        {
            return View();
        }
    }
}
