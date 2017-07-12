using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace irr.Models
{
    public class Real_estate_View
    {
        
        public List<Real_estate_block> list;

        public Real_estate_View()
        {
            
            list = new List<Real_estate_block>();
        }
    }

    public class Real_estate_block
    {
        public string Name;
        public string Type2;
        public string img;
        public Real_estate_block_lvl_2 Sale;
        public Real_estate_block_lvl_2 Lease;

        public Real_estate_block()
        {
            Name = "";
            Type2 = "";
            img = "";
            Sale = new Real_estate_block_lvl_2();
            Lease = new Real_estate_block_lvl_2();
        }
    }
    public class Real_estate_block_lvl_2
    {
        public string Name;
        public string Type;
        public List<string> list;

        public Real_estate_block_lvl_2()//(string a1,params string[] a)
        {
            Name = "";
            Type = "";
            list = new List<string>();
        }
        public Real_estate_block_lvl_2(string a1,string type,params string[] a)
        {
            Name = a1;
            Type = type;
            list = a.ToList();
        }
    }
    }