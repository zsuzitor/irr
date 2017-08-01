using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

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
        public bool? rooms_bool { get; set; }
        public bool? flag { get; set; }
        public bool VIP { get; set; }
        public int? Id { get; set; }
        public int? Price_bot { get; set; }
        public int? Price_top { get; set; }
        public int? Count_rooms_bot { get; set; }
        public int? Count_rooms_top { get; set; }
        public double? Total_area_bot { get; set; }
        public double? Total_area_top { get; set; }
        public double? Residential_area_bot { get; set; }
        public double? Residential_area_top { get; set; }
        
        public int? Floor { get; set; }
        public int? Count_floor { get; set; }
        public string Place { get; set; }

        public Search()
        {
            Id = null;
            Price_bot = null;
            Price_top = null;
            Count_rooms_bot = null;
            Count_rooms_top = null;
            Total_area_bot = null;
            Total_area_top = null;
            Residential_area_bot = null;
            Residential_area_top = null;
            Floor = null;
            Count_floor = null;
            Place = "";
            VIP = false;
            price_bool = null;
            rooms_bool = null;
            flag = null;
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
            Id = a.Id;
            Price_bot = a.Price_bot;
            Price_top = a.Price_top;
            Count_rooms_bot = a.Count_rooms_bot;
            Count_rooms_top = a.Count_rooms_top;
            Total_area_bot = a.Total_area_bot;
            Total_area_top = a.Total_area_top;
            Residential_area_bot = a.Residential_area_bot;
            Residential_area_top = a.Residential_area_top;
            Floor = a.Floor;
            Count_floor = a.Count_floor;
            Place = a.Place;
            VIP = a.VIP;
            str = a.str;
            category = a.category;
            town = a.town;
            Count_ad_on_page = a.Count_ad_on_page;
            type = a.type;
            type2 = a.type2;
            pg = a.pg;
            price_bool = a.price_bool;
            rooms_bool = a.rooms_bool;
            flag = a.flag;
        }
        public Search copy()
        {
            Search res = new Search() {
                VIP = this.VIP, price_bool = this.price_bool, str = this.str, category = this.category, town = this.town, Count_ad_on_page = this.Count_ad_on_page,
                rooms_bool=this.rooms_bool,
                flag=this.flag,
                type = this.type,type2 = this.type2,pg = this.pg,
                Id = this.Id,
                Price_bot = this.Price_bot,
                Price_top = this.Price_top,
                Count_rooms_bot = this.Count_rooms_bot,
                Count_rooms_top = this.Count_rooms_top,
                Total_area_bot = this.Total_area_bot,
                Total_area_top = this.Total_area_top,
                Residential_area_bot = this.Residential_area_bot,
                Residential_area_top = this.Residential_area_top,
                Floor = this.Floor,
            Count_floor = this.Count_floor,
            Place = this.Place,
        };
            return res;
        }
        public override string ToString()
        {
            string serialized = JsonConvert.SerializeObject(this);
            return serialized;
        }
        public static Search FromString(string a)
        {
            Search res= JsonConvert.DeserializeObject<Search>(a);
            return res;
        }
            //new_temp = JsonConvert.DeserializeObject<Entry>(arr[0]);
        }
}