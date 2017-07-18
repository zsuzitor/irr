using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace irr.Models
{
    public class Search
    {
        public string str="";
        public string category="all";
        public string town= "Вся Россия";
        public int Count_ad_on_page=10;
        public string type = "all";
        public string type2 = "all-type";
        public int pg = 1;
        

        public Search copy()
        {
            Search res = new Search() { str = this.str, category = this.category, town = this.town, Count_ad_on_page = this.Count_ad_on_page,
                type = this.type,type2 = this.type2,pg = this.pg
            };
            return res;
        }
    }
}