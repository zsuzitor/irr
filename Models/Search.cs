using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace irr.Models
{
    public class Search
    {
        public string str { get; set; }
        public string category { get; set; }//
        public string town { get; set; }
        public int Count_ad_on_page { get; set; }
        public int Count_page { get; set; }
        public string type { get; set; }
        public string type2 { get; set; }
        public int pg { get; set; }
        public bool? price_bool { get; set; }
        public bool VIP { get; set; }

        public Search()
        {
            VIP = false;
            price_bool = null;
             str = "";
         category = "all";
         town = "Вся Россия";
        Count_ad_on_page = 10;
         type = "all";
         type2 = "all-type";
         pg = 1;
            Count_page = 1;
    }

        public Search(Search a)
        {
            VIP = a.VIP;
            str = a.str;
            category = a.category;
            town = a.town;
            Count_ad_on_page = a.Count_ad_on_page;
            type = a.type;
            type2 = a.type2;
            pg = a.pg;
            price_bool = a.price_bool;
        }
        public Search copy()
        {
            Search res = new Search() {
                VIP = this.VIP, price_bool = this.price_bool, str = this.str, category = this.category, town = this.town, Count_ad_on_page = this.Count_ad_on_page,
                type = this.type,type2 = this.type2,pg = this.pg
            };
            return res;
        }
    }
}