using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExploreCalifornia.Models
{
    public class Post
    {
        public long Id { get; set; }
        private string _key;
        public string Key
        {
            get
            {
                if(_key == null)
                {
                    _key = Regex.Replace(Title.ToLower(), "[^a-z0-9]", "-");//lower n upper case atoz and numbers
                }
                return _key;
            }
            set { _key = value; }
        }
        //^ for BlogDataContext to Db

        [Display(Name ="Post Title")]
        [Required]
        [DataType(DataType.Text)]
        //[DataType(DataType.Password)] - shows the dots
        [StringLength(100, MinimumLength =5, ErrorMessage ="Title must be 5-100 character long.")]
        public string Title { get; set; }

        public string Author { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(100, ErrorMessage ="Blog posts must be more than 100 characters long.")]
        public string Body { get; set; }

        public DateTime Posted { get; set; }
    }
}
//http://localhost:53934/blog/2016/11/test