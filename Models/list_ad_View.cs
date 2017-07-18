using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace irr.Models
{
    public class list_ad_View
    {
        public int Current_page;
        public int Count_ad_on_page;
        public int Count_page;
        public string Type;
        public string Type2;
        public List<Entry> list;
        public int[] str;
       public Search srch;

        public list_ad_View()
        {
            Current_page = 0;
            Count_ad_on_page = 10;
            Type = "all";
            Type2 = "all-type";
            list = new List<Entry>();
            str = new int[6];
            srch = new Search();
        }
    }
}