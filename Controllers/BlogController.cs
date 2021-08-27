using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return new ContentResult {Content = "Blog posts" };
            //http://localhost:53934/blog/index
            //View();
        }

        public IActionResult Post(int id = -1)
        {//the url will show the 'id' on the page
           // if(id == null)
             //   return new ContentResult { Content = "null" };
           // else
            return new ContentResult {Content = id.ToString()};
            //model binding
        }
    }
}
