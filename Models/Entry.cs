using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace irr.Models
{
  
    public class Entry
    {
        public int Id { get; set; }
        public string Type_of_apartment { get; set; }
        public string Type_ad { get; set; }
        public string Url_page { get; set; }
        public bool VIP { get; set; }
        public string Map { get; set; }
        public string Header { get; set; }
        public string Under_header { get; set; }
        public int ?Price { get; set; }
        public int ?Count_rooms{ get; set; }
        public double ?Total_area { get; set; }
        public double? Residential_area { get; set; }
        
        public int? Floor { get; set; }
        public int? Count_floor { get; set; }
        public string Phone_number { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
        public List<byte[]> Images_byte { get; set; }
        public List<string> Info1 { get; set; }
        public List<string> Info2 { get; set; }
        public List<string> Info3 { get; set; }
        //public string Images;
        // public string Info1;
        // public string Info2;
        //public string Info3;
        public Entry()
        {
            VIP = false;
            Type_of_apartment = "";
            Type_ad = "";
            Url_page = "";
            Map = "";
            Header = "";
            Under_header = "";
            Price = 0;
            Phone_number = "";
            Name = "";
            Place = "";
            Link = "";
            Description = "";
            Info1 = new List<string>();
            Info2 = new List<string>();
           Info3 = new List<string>();
            Images = new List<string>();
            Images_byte=new List< byte[] > ();
            // Images = "";
            //Info1 = "";
            //Info2 = "";
            //Info3 = "";


        }
        public bool search_str(string str)
        {
            if (string.IsNullOrEmpty(str))
                return true;
            if (Type_of_apartment.IndexOf(str) != -1)
                return true;
            if (Type_ad.IndexOf(str) != -1)
                return true;
            if (Header.IndexOf(str) != -1)
                return true;
            if (Under_header.IndexOf(str) != -1)
                return true;
            if (Price.ToString().IndexOf(str) != -1)
                return true;
            if (Phone_number.IndexOf(str) != -1)
                return true;
            if (Name.IndexOf(str) != -1)
                return true;
            if (Place.IndexOf(str) != -1)
                return true;
            if (Description.IndexOf(str) != -1)
                return true;
            foreach(var i in Info1)
                if (i.IndexOf(str) != -1)
                    return true;
            
            foreach (var i in Info2)
                if (i.IndexOf(str) != -1)
                    return true;
            
            foreach (var i in Info3)
                if (i.IndexOf(str) != -1)
                    return true;
            



            return false;
        }

    }
}