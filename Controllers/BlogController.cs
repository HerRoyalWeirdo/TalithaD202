using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExploreCalifornia.Models;

namespace ExploreCalifornia.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            //return new ContentResult {Content = "Blog posts" };
            //http://localhost:53934/blog/index
            return View();
        }

        [Route("{year:min(2000)}/{month:range(1,12)}/{key}")]
        //[Route("{year:int}/{month:int}/{key}")]
        public IActionResult Post(int year, int month, string key)//int id = -1)
        {//the url will show the 'id' on the page
         // if(id == null)
         //   return new ContentResult { Content = "null" };
         // else
         // return new ContentResult {
         //    Content = string.Format("Year: {0}; Month: {1}; Key: {2}", year, month, key)
         //id.ToString()
         //};
         //http://localhost:53934/blog/2016/11/test
         //model binding
            var post = new Post
            {
                Title = "My blah post",
                Posted = DateTime.Now,
                Author = "Talitha Mao-Adams",
                Body = "Don't you think this blah post is cool? ;-)"
            };

            //ViewBag.Title = "My blog post";
            //ViewBag.Posted = DateTime.Now;
            //ViewBag.Author = "Talitha Mao-Adams";
            //ViewBag.Body = "This is a great blog post -_- can't you tell?";
            return View(post);
        }
    }
}
