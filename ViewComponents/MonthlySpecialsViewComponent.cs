using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExploreCalifornia.Models;

namespace ExploreCalifornia.ViewComponents
{
    //[ViewComponent]
    public class MonthlySpecialsViewComponent : ViewComponent
        //needs to end with ViewComponent or [ViewComponent] or : ViewComponent
    {
        private readonly BlogDataContext db;

        public MonthlySpecialsViewComponent(BlogDataContext db)
        {
            this.db = db;
        }

        public IViewComponentResult Invoke()
        {
            var specials = db.MonthlySpecials.ToArray();
            return View(specials);
                   //"To Do: Show Monthly Specials. ";
        }
        //tab + shift after ctor
        
    }
}
