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
        //
        public ActionResult Extended_search_ajax_1(string category)
        {
            Search res = new Models.Search();
            res.category = category;
            //partial для ajax
            // TO-DO   смотреть какой пункт выбран  и устанавливать флаг для типа расширенного поиска
            return PartialView(res);
        }

        public ActionResult list_ad_ajax_1(string str="", string category= "all", string town= "Вся Россия", int Count_ad_on_page=10, string type= "all", string type2= "all-type", int pg=1)
        {
            
               irr.Models.Search srch = new Models.Search() { str = str, category = category, town = town, Count_ad_on_page = Count_ad_on_page, type = type, type2 = type2, pg = pg };

            //string type = "all", string type2 = "all-type", int pg = 1,


            //, type=Model.Type, type2=Model.Type2,pg=Model.Current_page
            //, string type = "all", string type2 = "all-type", int pg = 1
            // string type = res.Type;
            //string type2 = res.Type2;
            //int pg = res.Current_page;





            list_ad_View res = new list_ad_View() { Count_ad_on_page=srch.Count_ad_on_page, Type = srch.type, Type2= srch.type2, Current_page= srch.pg};
            if (true)
            {
                int tmp = srch.pg - 2;
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
            
            UP_nedo_bd();
            res.list = search_bd(srch);
            res.srch = srch.copy();





            //
            res.str[5] = srch.Count_page;
            res.Count_page = res.str[5];

            return PartialView(res);
        }
//END-PARTIAL BLOCK--------------------------------------------------------------------------------------------------------------------//
       

//-POST/FORM BLOCK--------------------------------------------------------------------------------------------------------------------//
        //отправка формы с поиском
        [HttpPost]
        public ActionResult Search(string str, string category, string town)
        {
            //UP_nedo_bd();
            //квартиры  list_ad(string type="all", string type2 = "all-type", int pg=1)
            Search srch = new Models.Search();
            srch.str = str;
            srch.category = category;
            srch.town = town;
            switch (category)
                {
               
                case "Все типы":
                    return View();
                    
                case "Квартиры":
                    
                    
                    list_ad_View res = new list_ad_View() { Current_page = 1, Type = "all", Type2 = "all-type" };
                    res.srch = srch;
                    return View("list_ad",res);
                    //int b = 0;
                    
                case "Телефоны":
                    return View();
                    
                case "Животные":
                    return View();
                    
                case "Машины":
                    return View();
                    
                default:
                    return View();
                    
            }
           // int c = 0;
           
        }
        [HttpPost]
        public ActionResult Extended_search(Search a)
        {
            //тут принимает постом форма поиска и уже переход к конкретным листам с объявлениями
            //TO-DO что то очень похожее на метод Search
           
            switch (a.category)
            {

                case "Все типы":
                    return View();
                   
                case "Квартиры":


                    list_ad_View res = new list_ad_View() { Current_page = a.pg, Type = a.type, Type2 = a.type2};
                    res.srch = a.copy();
                    return View("list_ad", res);
                    //int b = 0;
                    
                case "Телефоны":
                    return View();
                    
                case "Животные":
                    return View();
                    
                case "Машины":
                    return View();
                    
                default:
                    return View();
                   
            }
            

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

        public ActionResult list_ad(string type="all", string type2 = "all-type", int pg=1)//,Search srch=null
        {
            // type2   gn kn zn
            //type sale lease

            
            list_ad_View res = new list_ad_View() {Current_page=pg, Type= type, Type2 = type2 };
            res.srch.pg =pg ;
            res.srch.type = type;
            res.srch.type2 = type2;


            return View(res);
            }


        
public ActionResult Real_estate()
        {
            Real_estate_View res = new Real_estate_View();
           
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
        public ActionResult Show_one_ad(int id=1)
        {
            UP_nedo_bd();
            Entry res = main_arr.First(x1 => x1.Id == id);

            return View(res);
        }
        //Extended
        public ActionResult Extended_search()
        {
            Search res = new Models.Search();
           // res.srch = srch;
            return View(res);

           
        }














        //-Function BLOCK--------------------------------------------------------------------------------------------------------------------//


        public List<Entry> search_bd(Search srch)
        {
            //TODO category нет поиска по категориям и бд без категорий
             List<Entry> res = new List<Entry>();
            


            if (srch.type != "all")
            {
                if(srch.type== "sale" || srch.type== "lease")
                     srch.type = srch.type == "sale" ? "Продажа" : "Аренда";
            }
            if (srch.type2 != "all-type")
            {
                if (srch.type2 == "gn")
                    srch.type2 = "Жилая недвижимость";
                if (srch.type2 == "kn")
                    srch.type2 = "Коммерческая недвижимость";
                if (srch.type2 == "zn")
                    srch.type2 = "Загородная недвижимость";
            }

            
                res = main_arr.
                    Where(x1 => srch.type == "all" ? true : x1.Type_ad == srch.type ? true : false).
                    Where(x2 => srch.type2 == "all-type" ? true : x2.Type_of_apartment == srch.type2 ? true : false).
                    Where(x3 => (srch.town == "Вся Россия" ? true : x3.Place.IndexOf(srch.town) != -1) && (x3.search_str(srch.str))).ToList();//&&(x3.search_str(srch.str))
            srch.Count_page = res.Count / srch.Count_ad_on_page + 1;
            res = res.Skip((srch.pg > 0 ? srch.pg - 1 : srch.pg) * srch.Count_ad_on_page).
                    Take(srch.Count_ad_on_page).
                    ToList();
            

            return res;
        }
    }


}










