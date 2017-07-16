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
        public void UP_nedo_bd()
        {
            StreamReader reader = new StreamReader(@"C:\Users\zsuz\Desktop\волгту\парсерPYквартирыirr\data.json");
            //чтение файла и разбивка по объектам+ добавление
            string next_str = "";
            int id_tmp = 1;
            try
            {
                int i = 0;
                while(true)
                {
                    char[] b = new char[10];
                    reader.Read(b, i * 0, 10);
                    string str = next_str + string.Concat(b);
                   
                    b = new char[10];
                    ++i;
                    while (str.IndexOf('}') == -1)
                    {
                        reader.Read(b, i * 0, 10);
                        str += string.Concat(b);
                        b = new char[10];
                        ++i;
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
                    Entry new_temp = new Entry();
                    new_temp = JsonConvert.DeserializeObject<Entry>(arr[0]);
                    new_temp.Id = id_tmp;
                    ++id_tmp;
                    main_arr.Add(new_temp);

                }
                    
                    }
            catch
            {

            }
            
            /*
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
                Entry new_temp = new Entry();
                 new_temp = JsonConvert.DeserializeObject<Entry>(arr[0]);
                new_temp.Id = id_tmp;
                ++id_tmp;
                main_arr.Add(new_temp);

            }
            */
        }
        public ActionResult Index()
        {
            UP_nedo_bd();
          
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

        public ActionResult list_ad_ajax_1(string type = "all", string type2 = "all-type", int pg = 1,Search srch=null)
        {
            //, type=Model.Type, type2=Model.Type2,pg=Model.Current_page
            //, string type = "all", string type2 = "all-type", int pg = 1
            // string type = res.Type;
            //string type2 = res.Type2;
            //int pg = res.Current_page;
            /*
             List<Entry> res = new List<Entry>();
                    if (town != "Вся Россия")
                        res= main_arr.Where(x1 => (x1.Place.IndexOf(town) != -1) && x1.search_str(str)).ToList();
                    else
                    {
                        res = main_arr.Where(x1 => x1.search_str(str)).ToList();
                    } 
              
              */




            list_ad_View res = new list_ad_View() { Type=type, Type2= type2,Current_page=pg  , srch =srch};
            if (true)
            {
                int tmp = pg  - 2;
                tmp = tmp > 1 ? tmp : 1;
               for (int i=0;i<5;++i)
                {
                    res.str[i] = tmp;
                    ++tmp;
                }
            }

                //int tmp = pg - i - 1;
               // res.str[i] = tmp > 1 ? tmp : 1;
            
            

            //
            if (type != "all")
            {
                type = type == "sale" ? "Продажа" : "Аренда";
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
            UP_nedo_bd();



           

            if(srch != null)
            res.list = main_arr.
                Where(x1 => type == "all" ? true : x1.Type_ad == type ? true : false).
                Where(x2 => type == "all-type" ? true : x2.Type_of_apartment == type2 ? true : false).
                Where(x3=> (srch.town== "Вся Россия"?true: x3.Place.IndexOf(srch.town) != -1)&&(x3.search_str(srch.str))).
                Skip((pg > 0 ? pg - 1 : pg) * res.Count_ad_on_page).
                Take(res.Count_ad_on_page).
                ToList();
            else
                res.list = main_arr.
               Where(x1 => type == "all" ? true : x1.Type_ad == type ? true : false).
               Where(x2 => type == "all-type" ? true : x2.Type_of_apartment == type2 ? true : false).
               Skip((pg > 0 ? pg - 1 : pg) * res.Count_ad_on_page).
               Take(res.Count_ad_on_page).
               ToList();

            //
            res.str[5] = res.list.Count/10+1;
            res.Count_page = res.str[5];

            return PartialView(res);
        }
//END-PARTIAL BLOCK--------------------------------------------------------------------------------------------------------------------//
       

        //-POST/FORM BLOCK--------------------------------------------------------------------------------------------------------------------//
        //отправка формы с поиском
        [HttpPost]
        public ActionResult Search(string str, string category, string town)
        {
            UP_nedo_bd();
            //квартиры  list_ad(string type="all", string type2 = "all-type", int pg=1)
            Search srch = new Models.Search();
            srch.str = str;
            srch.category = category;
            srch.town = town;
            switch (category)
                {
                //"Все типы", "квартиры", "телефоны", "животные", "машины"
                case "Все типы":
                    return View();
                    break;
                case "Квартиры":
                    int a = 0;
                    
                    list_ad_View res = new list_ad_View() { Current_page = 1, Type = "all", Type2 = "all-type" };
                    res.srch = srch;
                    return View("list_ad",res);
                    //int b = 0;
                    break;
                case "Телефоны":
                    return View();
                    break;
                case "Животные":
                    return View();
                    break;
                case "Машины":
                    return View();
                    break;
                default:
                    return View();
                    break;
            }
           // int c = 0;
           
        }
        //END-POST/FORM BLOCK--------------------------------------------------------------------------------------------------------------------//


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

        //отображение списка с объявлениями
        //type= тип объявления type2 тип квартиры

        public ActionResult list_ad(string type="all", string type2 = "all-type", int pg=1,Search srch=null)//string Count_of_room, string , string, string, string,
        {
            // type2   gn kn zn
            //type sale lease

            //UP_nedo_bd();
            list_ad_View res = new list_ad_View() {Current_page=pg, Type= type, Type2 = type2 };
            res.srch = srch;

            // Type_of_apartment   Type_ad
            return View(res);
            }


        
public ActionResult Real_estate()//string Count_of_room, string , string, string, string,
        {
            Real_estate_View res = new Real_estate_View();
            //Url.Content("~/App_Data/gn.PNG")  Url.Content("~/App_Data/kn.PNG")  Url.Content("~/App_Data/zn.PNG")
            res.list.Add(new Real_estate_block() {Name= "Жилая недвижимость",img= Url.Content("~/Content/img/gn.PNG"),
                Type2="gn",
                Sale =new Real_estate_block_lvl_2("Продажа", "sale", new string[4] { "1 комнатные", "2 комнатные", "3 комнатные", "4 комнатные" }) ,
                Lease =new Real_estate_block_lvl_2("аренда", "lease", new string[4] { "1 комнатные", "2 комнатные", "3 комнатные", "4 комнатные" })
            }); 



            res.list.Add(new Real_estate_block()
            {
                Name = "Коммерческая недвижимость", img = Url.Content("~/Content/img/kn.PNG"),
                Type2 = "kn",
                Sale = new Real_estate_block_lvl_2("Продажа", "sale", new string[4] { "1 комнатные", "2 комнатные", "3 комнатные", "4 комнатные" }),
                Lease = new Real_estate_block_lvl_2("аренда", "lease", new string[4] { "1 комнатные", "2 комнатные", "3 комнатные", "4 комнатные" })
            });
            
                 res.list.Add(new Real_estate_block()
                 {
                     Name = "Загородная недвижимость", img = Url.Content("~/Content/img/zn.PNG"),
                     Type2 = "zn",
                     Sale = new Real_estate_block_lvl_2("Продажа", "sale", new string[4] { "1 комнатные", "2 комнатные", "3 комнатные", "4 комнатные" }),
                     Lease = new Real_estate_block_lvl_2("аренда", "lease", new string[4] { "1 комнатные", "2 комнатные", "3 комнатные", "4 комнатные" })
                 });

            return View(res);
        }
        public ActionResult Show_one_ad(int id=1)//хз мб вопрос лишний
        {
            UP_nedo_bd();
            Entry res = main_arr.First(x1 => x1.Id == id);

            return View(res);// в модель отдельную
        }
        }
}