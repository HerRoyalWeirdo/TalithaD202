using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.Controllers
{
    public class HomeController// : Controller
    {
        public string Index()//IActionResult Index()
        {
            return "hello testing ASP.NET Core MVC";
                //View();
        }
    }
}
