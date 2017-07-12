using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using irr.Models;
using System.IO;
using Newtonsoft.Json;
using System.Web.Mvc.Ajax;

namespace irr.Controllers
{
    public class HomeController : Controller
    {
        List<Entry> main_arr = new List<Entry>();


//-SETTINGS/ADMIN BLOCK--------------------------------------------------------------------------------------------------------------------//
        public ActionResult Index()
        {
            StreamReader reader = new StreamReader(@"C:\Users\zsuz\Desktop\волгту\парсерPYквартирыirr\data.json");
            //чтение файла и разбивка по объектам+ добавление
            string next_str = "";
            while (!reader.EndOfStream)
            {

                string str = next_str + reader.ReadLine();
                while (str.IndexOf('}') == 0)
                {
                    str += reader.ReadLine();
                }
                string[] arr = str.Split('}');
                try
                {
                    arr[0] += "}";
                    next_str = arr[1];
                }
                catch
                {


                }
                //сделать объект entry
                Entry new_temp = JsonConvert.DeserializeObject<Entry>(arr[0]);
                main_arr.Add(new_temp);

            }

            return View();
        }
        //END-SETTINGS/ADMIN BLOCK--------------------------------------------------------------------------------------------------------------------//



        //-PARTIAL BLOCK------------------------------------------------------------------------------------------------------------------------------//
        [ChildActionOnly]
        public ActionResult Main_header(string Search, string Category, string Town)
        {
            ViewBag.Search = Search;
            ViewBag.Category = Category;
            ViewBag.Town = Town;

            return PartialView();
        }

        public ActionResult list_ad_ajax_1(list_ad_View res)
        {
            //, type=Model.Type, type2=Model.Type2,pg=Model.Current_page
            //, string type = "all", string type2 = "all-type", int pg = 1
            string type = res.Type;
            string type2 = res.Type2;
            int pg = res.Current_page;
            //
            if (type != "all")
            {
                type = type == "sell" ? "Продажа" : "Аренда";
            }
            if (type2 != "all-type")
            {
                if (type2 == "gn")
                    type2 = "Жилая недвижимость";
                if (type2 == "kn")
                    type2 = "Коммерческая недвижимость";
                if (type2 == "zn")
                    type2 = "Загородная недвижимость";
            }
            res.list = main_arr.
                Where(x1 => type == "all" ? true : x1.Type_ad == type ? true : false).
                Where(x2 => type == "all-type" ? true : x2.Type_of_apartment == type2 ? true : false).
                Skip((pg > 0 ? pg - 1 : pg) * res.Count_ad_on_page).
                Take(res.Count_ad_on_page).
                ToList();
            //


            return PartialView(res);
        }
//END-PARTIAL BLOCK--------------------------------------------------------------------------------------------------------------------//
        //главная страница с разделами
        public ActionResult Categories()
        {
            Categories_View categories_View = new Categories_View();
            //блок с продажей
            categories_View.Sale.Name = "Продажа недвижимости";
            categories_View.Sale.list_cat.AddRange(new string[4] { "1 комнатные", "2 комнатные", "3 комнатные", "4 комнатные" });
            //блок с арендой

            categories_View.Lease.Name = "Аренда недвижимости";
            categories_View.Lease.list_cat.AddRange(new string[4] { "1 комнатные", "2 комнатные", "3 комнатные", "4 комнатные" });

            return View(categories_View);
        }

        //-POST/FORM BLOCK--------------------------------------------------------------------------------------------------------------------//
        //отправка формы с поиском
        [HttpPost]
        public ActionResult Search(string str, string category, string town)
        {
            return View();
        }
        //END-POST/FORM BLOCK--------------------------------------------------------------------------------------------------------------------//


        //отображение списка с объявлениями
        //type= тип объявления type2 тип квартиры

        public ActionResult list_ad(string type="all", string type2 = "all-type", int pg=1)//string Count_of_room, string , string, string, string,
        {
            // type2   gn kn zn
            //type sale lease

            list_ad_View res = new list_ad_View() {Current_page=pg, Count_ad_on_page=10 , Count_page = main_arr .Count/10+1,Type= type, Type2 = type2 };
            
            

            // Type_of_apartment   Type_ad
            return View(res);
            }


        
public ActionResult Real_estate()//string Count_of_room, string , string, string, string,
        {
            Real_estate_View res = new Real_estate_View();
            res.list.Add(new Real_estate_block() {Name= "Жилая недвижимость",img= @"C:\csharp\asp1\kniga\irr\irr\App_Data\gn.PNG",Type2="gn",
                Sale =new Real_estate_block_lvl_2("Продажа", "sale", new string[4] { "1 комнатные", "2 комнатные", "3 комнатные", "4 комнатные" }) ,
                Lease =new Real_estate_block_lvl_2("аренда", "lease", new string[4] { "1 комнатные", "2 комнатные", "3 комнатные", "4 комнатные" })
            }); 



            res.list.Add(new Real_estate_block()
            {
                Name = "Коммерческая недвижимость", img = @"C:\csharp\asp1\kniga\irr\irr\App_Data\kn.PNG",Type2 = "kn",
                Sale = new Real_estate_block_lvl_2("Продажа", "sale", new string[4] { "1 комнатные", "2 комнатные", "3 комнатные", "4 комнатные" }),
                Lease = new Real_estate_block_lvl_2("аренда", "lease", new string[4] { "1 комнатные", "2 комнатные", "3 комнатные", "4 комнатные" })
            });
            
                 res.list.Add(new Real_estate_block()
                 {
                     Name = "Загородная недвижимость", img = @"C:\csharp\asp1\kniga\irr\irr\App_Data\zn.PNG",Type2 = "zn",
                     Sale = new Real_estate_block_lvl_2("Продажа", "sale", new string[4] { "1 комнатные", "2 комнатные", "3 комнатные", "4 комнатные" }),
                     Lease = new Real_estate_block_lvl_2("аренда", "lease", new string[4] { "1 комнатные", "2 комнатные", "3 комнатные", "4 комнатные" })
                 });

            return View(res);
        }
    }
}