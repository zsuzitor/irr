using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace irr.Models
{
    public class Entry
    {
        public int Id;
        public string Type_of_apartment;
        public string Type_ad;
            public string Url_page;
           
            public string Map;
            public string Header;
        public string Under_header;
        public string Price;
            public string Phone_number;
            public string Name;
            public string Place;
            public string Link;
            public string Description;
         public List<string> Images;
        public List<string> Info1;
        public List<string> Info2;
        public List<string> Info3;
        //public string Images;
       // public string Info1;
       // public string Info2;
        //public string Info3;
        public Entry()
        {
            Type_of_apartment = "";
            Type_ad = "";
            Url_page = "";
            Map = "";
            Header = "";
            Under_header = "";
            Price = "";
            Phone_number = "";
            Name = "";
            Place = "";
            Link = "";
            Description = "";
            Info1 = new List<string>();
            Info2 = new List<string>();
           Info3 = new List<string>();
            Images = new List<string>();
           // Images = "";
            //Info1 = "";
            //Info2 = "";
            //Info3 = "";


        }

    }
}