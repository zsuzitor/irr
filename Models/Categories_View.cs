using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace irr.Models
{
    public class Categories_View
    {
        public category Sale;
        public category Lease;
        
        public Categories_View()
        {
            Sale = new category();
            Lease = new category();
    }
    }

    public class category
    {
        public string Name;
        public List<string> list_cat;
        public category()
        {
            Name = "";
            list_cat = new List<string>();
        }
    }
}