using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.Models
{
    public class FormattingService
    {
        public string AsReadDate(DateTime date)
        {
            return date.ToString("d");//or "D"
        }
    }
}
