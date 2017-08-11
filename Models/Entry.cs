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
        public string Type { get; set; }
        // public string Url_page { get; set; }
        public bool VIP { get; set; }
        public string Map { get; set; }
        public string Header { get; set; }
        //public string Under_header { get; set; }
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
        public int? Images_id { get; set; }
        public List<string> Images { get; set; }
        public List<byte[]> Images_byte { get; set; }
        public int? Info1_id { get; set; }
        public List<string> Info1 { get; set; }
        public int? Info2_id { get; set; }
        public List<string> Info2 { get; set; }
        public int? Info3_id { get; set; }
        public List<string> Info3 { get; set; }
        public int? Info4_id { get; set; }
        public List<string> Info4 { get; set; }
        //public string Images;
        // public string Info1;
        // public string Info2;
        //public string Info3;
        public Entry()
        {
            Images_id = null;
            Info1_id = null;
            Info2_id = null;
            Info3_id = null;
            Info4_id = null;
            VIP = false;
            Type_of_apartment = "";
            Type_ad = "";
            Type = "Квартиры";
            Map = "";
            Header = "";
            //Under_header = "";
            Price = 0;
            Phone_number = "";
            Name = "";
            Place = "";
            Link = "";
            Description = "";
            Info1 = new List<string>();
            Info2 = new List<string>();
           Info3 = new List<string>();
            Info4 = new List<string>();
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
            if (Type.IndexOf(str) != -1)
                return true;
            if (Header.IndexOf(str) != -1)
                return true;
            //if (Under_header.IndexOf(str) != -1)
                //return true;
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
            foreach (var i in Info4)
                if (i.IndexOf(str) != -1)
                    return true;




            return false;
        }

    }


    public class Entry_img
    {
        public int Id { get; set; }
        public string s_1 { get; set; } // название картинки
        public string s_2 { get; set; }
        public string s_3 { get; set; }
        public string s_4 { get; set; }
        public string s_5 { get; set; }
        public string s_6 { get; set; }
        public string s_7 { get; set; }
        public string s_8 { get; set; }
        public string s_9 { get; set; }
        public string s_10 { get; set; }
        public byte[] b_1 { get; set; }
        public byte[] b_2 { get; set; }
        public byte[] b_3 { get; set; }
        public byte[] b_4 { get; set; }
        public byte[] b_5 { get; set; }
        public byte[] b_6 { get; set; }
        public byte[] b_7 { get; set; }
        public byte[] b_8 { get; set; }
        public byte[] b_9 { get; set; }
        public byte[] b_10 { get; set; }
    }
    public class Entry_info
    {
        public int Id { get; set; }
        public string info_1 { get; set; } // название картинки
        public string info_2 { get; set; }
        public string info_3 { get; set; }
        public string info_4 { get; set; }
        public string info_5 { get; set; }
        public string info_6 { get; set; }
        public string info_7 { get; set; }
        public string info_8 { get; set; }
        public string info_9 { get; set; }
        public string info_10 { get; set; }
    }
    }