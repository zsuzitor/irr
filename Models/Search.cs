using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace irr.Models
{
    public class Search
    {
        public string str;
        public string category;//
        public string town;
        public int Count_ad_on_page;
        public string type ;
        public string type2;
        public int pg ;

        public Search()
        {
             str = "";
         category = "all";
         town = "Вся Россия";
        Count_ad_on_page = 10;
         type = "all";
         type2 = "all-type";
         pg = 1;
    }

        public Search(Search a)
        {
            str = a.str;
            category = a.category;
            town = a.town;
            Count_ad_on_page = a.Count_ad_on_page;
            type = a.type;
            type2 = a.type2;
            pg = a.pg;
        }
        public Search copy()
        {
            Search res = new Search() { str = this.str, category = this.category, town = this.town, Count_ad_on_page = this.Count_ad_on_page,
                type = this.type,type2 = this.type2,pg = this.pg
            };
            return res;
        }
    }
}