using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() //string Index()
        {
            //return new ContentResult {Content = "hello testing ASP.NET Core MVC" };
            //return "hello testing ASP.NET Core MVC";    
            return View();
        }
    }
}
