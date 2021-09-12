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

        [Route("blog/{year:int}/{month:int}/{key}")]
        public IActionResult Post(int year, int month, string key)//int id = -1)
        {//the url will show the 'id' on the page
           // if(id == null)
             //   return new ContentResult { Content = "null" };
           // else
            return new ContentResult {
                Content = string.Format("Year: {0}; Month: {1}; Key: {2}", year, month, key)
                          //id.ToString()
            };
            //model binding
        }
    }
}
