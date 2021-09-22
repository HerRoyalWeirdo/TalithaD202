using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Authorization;

namespace ExploreCalifornia.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        //inject instance of BlogDataContext constructor
        private readonly BlogDataContext _db;

        public BlogController(BlogDataContext db)
        {
            _db = db;
        }//ctor tab+shift

        [Route("")]
        public IActionResult Index(int page=0)
        {
            //return new ContentResult {Content = "Blog posts" };
            //http://localhost:53934/blog/index
            //return View();

            //_db.Posts.ToArray() //will take posts from the database - to read out from saved ish

            //var posts = _db.Posts.OrderByDescending(x=>x.Posted).Take(5).ToArray(); //new[]

            //{
            //    new Post
            //    {
            //        Title = "My blog post",
            //        Posted = DateTime.Now,
            //        Author = "Jess Chadwick",
            //        Body = "This is a great blog post, don't you think?",
            //    },
            //    new Post
            //    {
            //        Title = "My second blog post",
            //        Posted = DateTime.Now,
            //        Author = "Jess Chadwick",
            //        Body = "This is ANOTHER great blog post, don't you think?",
            //    },
            //};
            var pageSize = 2;
            var totalPosts = _db.Posts.Count();
            var totalPages = totalPosts / pageSize;
            var previousPage = page - 1;
            var nextPage = page + 1;

            ViewBag.PreviousPage = previousPage;
            ViewBag.HasPreviousPage = previousPage >= 0;
            ViewBag.NextPage = nextPage;
            ViewBag.HasNextPage = nextPage < totalPages;

            var posts =
                _db.Posts
                    .OrderByDescending(x => x.Posted)
                    .Skip(pageSize * page)
                    .Take(pageSize)
                    .ToArray();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView(posts);

            return View(posts);
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

         //for db
            var post = _db.Posts.FirstOrDefault(x=>x.Key == key);//new Post
            //{

            //    Title = "My blah post",
            //    Posted = DateTime.Now,
            //    Author = "Talitha Mao-Adams",
            //    Body = "Don't you think this blah post is cool? ;-)"
            //};

            //ViewBag.Title = "My blog post";
            //ViewBag.Posted = DateTime.Now;
            //ViewBag.Author = "Talitha Mao-Adams";
            //ViewBag.Body = "This is a great blog post -_- can't you tell?";
            return View(post);
        }

        [Authorize]
        [HttpGet, Route("create")]
        public IActionResult Create()
        {
            return View();
        }
        //highlight Post and F12 to take to page
        [Authorize]
        [HttpPost, Route("create")]
        public IActionResult Create(Post post)//(CreatePostRequest post) //([Bind("Title", "Body")]Post post)
        {
            if (!ModelState.IsValid)
                return View();

            post.Author = User.Identity.Name;
            post.Posted = DateTime.Now;

            //connected to db up top
            _db.Posts.Add(post);
            _db.SaveChanges();
            //SQL Server Object Explorer < Database - Explorecalifornia-Tables- dbo.Posts

            return RedirectToAction("Post", "Blog", new
            {
                year = post.Posted.Year,
                month = post.Posted.Month,
                key = post.Key
            });//View();
        }

        //where the Post class is v
        //public class CreatePostRequest
        //{
        //    public string Title { get; set; }
        //    public string Body { get; set; }
        //}
    }
}
