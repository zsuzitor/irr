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
        EntryContext db = new EntryContext();
        //db.Players.Add(player);
        //db.SaveChanges();
        //
        int ta = 0;
        //TODO список категорий и именно его закидывать уже куда надо List<string> category = new List<string>() { "Квартиры", "Телефоны", "Животные", "Машины" };

        //-SETTINGS/ADMIN BLOCK--------------------------------------------------------------------------------------------------------------------//
        public void UP_nedo_bd()
        {


            //List<Entry> tffr = db.Entrys;



            // StreamWriter writer = new StreamWriter(@"C:\csharp\asp1\kniga\irr\irr\Content\data.json", true);
            //StreamReader reader = new StreamReader(@"C:\Users\zsuz\Desktop\волгту\парсерPYквартирыirr\data.json");
            StreamReader reader = new StreamReader(@"C:\csharp\asp1\kniga\irr\irr\Content\data.json");
            //чтение файла и разбивка по объектам+ добавление
            string next_str = "";
            int id_tmp = 1;
            try
            {
                int i = 0;
                bool tr = true;
                while (tr)
                {
                    char[] b = new char[10];
                    reader.Read(b, i * 0, 10);
                    string str = next_str + string.Concat(b);

                    b = new char[10];
                    ++i;
                    while ((str.IndexOf('}') == -1)&&tr)
                    {
                        reader.Read(b, i * 0, 10);
                        str += string.Concat(b);
                        b = new char[10];
                        ++i;
                        if (str.IndexOf('\0') != -1)
                            tr = false;
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

                    /*
                    string a123 = arr[0].Substring(0, arr[0].IndexOf("Price")+9);
                    string a1233= arr[0].Substring(arr[0].IndexOf("Price") + 5, 30);
                    string a12333 = "";
                    var a12334 = a1233.ToCharArray();
                    for (int i44=0;i44< a12334.Length;++i44)
                    {
                        if (char.IsDigit(a12334[i44]))
                        {
                            a12333 += a12334[i44];
                        }
                    }
                    a123 += a12333+ arr[0].Substring(arr[0].IndexOf("Phone_number") -4);
                    arr[0] = a123;
                    a123 = arr[0].Substring(arr[0].IndexOf("\"Общая площадь\":")+18,20);
                    double a123_int = 0;
                    string tmp123 = "";
                    bool flag = true;
                    foreach(var i44 in a123.ToCharArray())
                    {
                        if(flag&&(i44=='.'||char.IsDigit(i44)))
                        {
                            tmp123 += i44;
                        }
                        else
                        {
                            flag = false;
                        }
                            
                    }
                    double.TryParse(tmp123, out a123_int);




                    a123 = arr[0].Substring(arr[0].IndexOf("\"Жилая площадь\": ") + 18, 20);
                    double a12344_int = 0;
                     tmp123 = "";
                     flag = true;
                    foreach (var i44 in a123.ToCharArray())
                    {
                        if (flag && (i44 == '.' || char.IsDigit(i44)))
                        {
                            tmp123 += i44;
                        }
                        else
                        {
                            flag = false;
                        }

                    }
                    double.TryParse(tmp123, out a12344_int);






                    flag = true;
                    tmp123 = "";
                    string tmp1234 = "";
                    a123 = arr[0].Substring(arr[0].IndexOf(" \"Этаж\":")+10, 20);
                    foreach (var i44 in a123.ToCharArray())
                    {
                        if (flag)
                        {
                            if (char.IsDigit(i44))
                            {
                                tmp123 += i44;
                                
                            }
                            else
                            flag = false;
                        }
                        else
                        {
                            if (char.IsDigit(i44))
                            {
                                tmp1234 += i44;
                                
                            }
                           
                        }

                    }


                    int a1234_int = 0;
                    int.TryParse(tmp123, out a1234_int);
                    int a12345_int = 0;
                    int.TryParse(tmp1234, out a12345_int);














    */
                    Entry new_temp = new Entry();
                    
                    new_temp = JsonConvert.DeserializeObject<Entry>(arr[0]);
                    /*
                    if (a123_int == 0)
                        new_temp.Total_area = null;
                    else
                        new_temp.Total_area = a123_int;
                    if (a1234_int == 0)
                        new_temp.Floor = null;
                    else
                        new_temp.Floor = a1234_int;

                    if (a12345_int == 0)
                        new_temp.Count_floor = null;
                    else
                        new_temp.Count_floor = a12345_int;

                    if (a12344_int == 0)
                        new_temp.Residential_area = null;
                    else
                        new_temp.Residential_area = a12344_int;
                     */

                    new_temp.Id = id_tmp;
                    ++id_tmp;
                    main_arr.Add(new_temp);

                    //string serialized = JsonConvert.SerializeObject(new_temp);

                    //writer.Write(serialized);
                    

                }
                    
                    }
            catch
            {
               
            }



           // writer.Close();
        }
        public ActionResult Index()
        {
            //представление тоже удалить

            UP_nedo_bd();
            // db.Entrys.Add(main_arr[0]);
            //db.SaveChanges();
            
            foreach(var i in main_arr)
            {
                Entry_img a_img = new Entry_img();
                Entry_info a_info_1 = new Entry_info();
                Entry_info a_info_2 = new Entry_info();
                Entry_info a_info_3 = new Entry_info();
                Entry_info a_info_4 = new Entry_info();
                try
                {
                    a_img.s_1 = i.Images[0];
                    a_img.s_2 = i.Images[1];
                    a_img.s_3 = i.Images[2];
                    a_img.s_4 = i.Images[3];
                    a_img.s_5 = i.Images[4];
                    a_img.s_6 = i.Images[5];
                    a_img.s_7 = i.Images[6];
                    a_img.s_8 = i.Images[7];
                    a_img.s_9 = i.Images[8];
                    a_img.s_10 = i.Images[9];
                }
                catch
                {

                }
                try
                {
                    a_info_1.info_1 = i.Info1[0];
                    a_info_1.info_2 = i.Info1[1];
                    a_info_1.info_3 = i.Info1[2];
                    a_info_1.info_4 = i.Info1[3];
                    a_info_1.info_5 = i.Info1[4];
                    a_info_1.info_6 = i.Info1[5];
                    a_info_1.info_7 = i.Info1[6];
                    a_info_1.info_8 = i.Info1[7];
                    a_info_1.info_9 = i.Info1[8];
                    a_info_1.info_10 = i.Info1[9];
                }
                catch
                {

                }
                try
                {
                    a_info_2.info_1 = i.Info2[0];
                    a_info_2.info_2 = i.Info2[1];
                    a_info_2.info_3 = i.Info2[2];
                    a_info_2.info_4 = i.Info2[3];
                    a_info_2.info_5 = i.Info2[4];
                    a_info_2.info_6 = i.Info2[5];
                    a_info_2.info_7 = i.Info2[6];
                    a_info_2.info_8 = i.Info2[7];
                    a_info_2.info_9 = i.Info2[8];
                    a_info_2.info_10 = i.Info2[9];
                }
                catch
                {

                }
                try
                {
                    a_info_3.info_1 = i.Info3[0];
                    a_info_3.info_2 = i.Info3[1];
                    a_info_3.info_3 = i.Info3[2];
                    a_info_3.info_4 = i.Info3[3];
                    a_info_3.info_5 = i.Info3[4];
                    a_info_3.info_6 = i.Info3[5];
                    a_info_3.info_7 = i.Info3[6];
                    a_info_3.info_8 = i.Info3[7];
                    a_info_3.info_9 = i.Info3[8];
                    a_info_3.info_10 = i.Info3[9];
                }
                catch
                {

                }
                try
                {
                    a_info_4.info_1 = i.Info4[0];
                    a_info_4.info_2 = i.Info4[1];
                    a_info_4.info_3 = i.Info4[2];
                    a_info_4.info_4 = i.Info4[3];
                    a_info_4.info_5 = i.Info4[4];
                    a_info_4.info_6 = i.Info4[5];
                    a_info_4.info_7 = i.Info4[6];
                    a_info_4.info_8 = i.Info4[7];
                    a_info_4.info_9 = i.Info4[8];
                    a_info_4.info_10 = i.Info4[9];
                }
                catch
                {

                }
                db.Info.Add(a_info_1);
                db.SaveChanges();
                i.Info1_id = a_info_1.Id;

                db.Info.Add(a_info_2);
                db.SaveChanges();
                i.Info2_id = a_info_2.Id;

                db.Info.Add(a_info_3);
                db.SaveChanges();
                i.Info3_id = a_info_3.Id;

                db.Info.Add(a_info_4);
                db.SaveChanges();
                i.Info4_id = a_info_4.Id;

                db.Images.Add(a_img);
                db.SaveChanges();
                i.Images_id = a_img.Id;


                db.Entrys.Add(i);
                db.SaveChanges();
               





            }
            //var ta = db.Entrys.ToList();
            //var ta = db.Entrys;
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
        
        public ActionResult Extended_search_ajax_3(string Search)
        {
            Search res = irr.Models.Search.FromString(Search);
            //res.category = category;
            //partial для ajax
            // TO-DO   смотреть какой пункт выбран  и устанавливать флаг для типа расширенного поиска
            return PartialView(res);
        }
        public ActionResult Extended_search_ajax_2(bool flag=false, string search=null)
        {
            Search res = irr.Models.Search.FromString(search); ;
            //TO DO хз мб флан убрать
            res.flag = flag;
            //partial для ajax
            
            return PartialView(res);
        }


        public ActionResult list_ad_ajax_1(string filter="",string search=null)
        {
            
            list_ad_View res = list_ad_ajax_1_function(filter, search);
            //if (res.list.Count == 1)
                //return new RedirectResult(string.Concat("/Home/Show_one_ad/?id=", res.list[0].Id.ToString()));
            //return View("Show_one_ad", res.list[0]);
            return PartialView(res);
        }

        [ChildActionOnly]
        public ActionResult vip_entry(Search srch)
        {
            srch.VIP = true;

            UP_nedo_bd();
            return PartialView(search_bd(srch));
        }
        
        public ActionResult Add_new_ad_ajax_1(string category)
        {
            //if (category == "Квартиры")
            //{
            //   irr.Models.Entry res = new irr.Models.Entry();
            //  return View(res);
            //}
            ViewBag.category = category;
            //
            return PartialView();
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
        public ActionResult Ad_img_add_ad(HttpPostedFileBase uploadImage,Entry res=null)
        {
            //TODO сейчас не работает и ничего не делает нужно в теории для обработки фоток которые загружаются

            //@Html.Hidden(Model.Images_byte,i)
            if (res == null)
                res = new Entry();
            if ( uploadImage != null)//ModelState.IsValid &&
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                // установка массива байтов
                res.Images_byte.Add( imageData);
                
                

                //return RedirectToAction("Add_new_ad", res);
            }
            return View("Add_new_ad",res);
        }
        [HttpPost]
        public ActionResult list_ad_ajax_1_1(string page = "", string search = null)
        {
            if (page == null)
                page = "";
            list_ad_View res = list_ad_ajax_1_function("page="+page, search);
            //if (res.list.Count == 1)
            //return new RedirectResult(string.Concat("/Home/Show_one_ad/?id=", res.list[0].Id.ToString()));
            //return View("Show_one_ad", res.list[0]);
            return PartialView("list_ad_ajax_1", res);
        }
        [HttpPost]
        public ActionResult Extended_search_ajax_3_list_ad(Search srch)
        {
            //Search res = new Models.Search();

            //partial для ajax
            // TO-DO   смотреть какой пункт выбран  и устанавливать флаг для типа расширенного поиска
            list_ad_View res = list_ad_ajax_1_function("", srch.ToString());
            //if (res.list.Count == 1)
            //return new RedirectResult(string.Concat("/Home/Show_one_ad/?id=",res.list[0].Id.ToString()));
            return PartialView("list_ad_ajax_1", res);
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
        [HttpPost]
        public ActionResult Add_new_ad(Entry a)
        {
            //irr.Models.Entry res = new irr.Models.Entry();
            //

            
string serialized = JsonConvert.SerializeObject(a);
            StreamWriter writer = new StreamWriter(@"C:\csharp\asp1\kniga\irr\irr\Content\data.json", true);
            writer.Write(serialized);
                 writer.Close();
            //UP_nedo_bd();
            //Entry res = main_arr.First(x1 => x1.Id == main_arr.Count+1);

            return View("Show_one_ad", a);
            //return View();
        }
        //END-POST/FORM BLOCK--------------------------------------------------------------------------------------------------------------------//
        public ActionResult Add_new_ad()
        {
            irr.Models.Entry res = new irr.Models.Entry();
            return View(res);
        }

        //главная страница с разделами
        public ActionResult Categories()
        {
            Categories_View categories_View = new Categories_View();
            //блок с продажей
            categories_View.Sale.Name = "Продажа недвижимости";
            categories_View.Sale.list_cat.AddRange(new string[5] { "1 комнатные", "2 комнатные", "3 комнатные", "4 комнатные", "5 и более" });
            //блок с арендой

            categories_View.Lease.Name = "Аренда недвижимости";
            categories_View.Lease.list_cat.AddRange(new string[5] { "1 комнатные", "2 комнатные", "3 комнатные", "4 комнатные", "5 и более" });

            return View(categories_View);
        }

        //отображение списка с объявлениями
        //type= тип объявления type2 тип квартиры

        public ActionResult list_ad(string type="all", string type2 = "all-type", int pg=1,int ?Count_rooms=null)//,Search srch=null
        {
            // type2   gn kn zn
            //type sale lease

            
            list_ad_View res = new list_ad_View() {Current_page=pg, Type= type, Type2 = type2 };
            res.srch.pg =pg ;
            res.srch.type = type;
            res.srch.type2 = type2;
            res.srch.Count_rooms_bot = Count_rooms;
            res.srch.category = "Квартиры";
            if (Count_rooms == 5)
                res.srch.Count_rooms_top = null;
            else
            res.srch.Count_rooms_top = Count_rooms;


            return View(res);
            }


        
public ActionResult Real_estate()
        {
            Real_estate_View res = new Real_estate_View();
           
            res.list.Add(new Real_estate_block() {Name= "Жилая недвижимость",img= Url.Content("~/Content/img/gn.PNG"),
                Type2="gn",
                Sale =new Real_estate_block_lvl_2("Продажа", "sale", new string[5] { "1 комнатные", "2 комнатные", "3 комнатные", "4 комнатные", "5 и более" }) ,
                Lease =new Real_estate_block_lvl_2("Аренда", "lease", new string[5] { "1 комнатные", "2 комнатные", "3 комнатные", "4 комнатные", "5 и более" })
            }); 



            res.list.Add(new Real_estate_block()
            {
                Name = "Коммерческая недвижимость", img = Url.Content("~/Content/img/kn.PNG"),
                Type2 = "kn",
                Sale = new Real_estate_block_lvl_2("Продажа", "sale", new string[5] { "1 комнатные", "2 комнатные", "3 комнатные", "4 комнатные", "5 и более" }),
                Lease = new Real_estate_block_lvl_2("Аренда", "lease", new string[5] { "1 комнатные", "2 комнатные", "3 комнатные", "4 комнатные", "5 и более" })
            });
            
                 res.list.Add(new Real_estate_block()
                 {
                     Name = "Загородная недвижимость", img = Url.Content("~/Content/img/zn.PNG"),
                     Type2 = "zn",
                     Sale = new Real_estate_block_lvl_2("Продажа", "sale", new string[5] { "1 комнатные", "2 комнатные", "3 комнатные", "4 комнатные", "5 и более" }),
                     Lease = new Real_estate_block_lvl_2("Аренда", "lease", new string[5] { "1 комнатные", "2 комнатные", "3 комнатные", "4 комнатные", "5 и более" })
                 });

            return View(res);
        }
        public ActionResult Show_one_ad(int id=1)
        {
            //TODO <div title="жилая/общая" не отображает при наведении в представлении
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
            //bool out_bool = true;

            if (srch.Id != null)
            {
                res = main_arr.Where(x5 => x5.Id == srch.Id).ToList();
                //out_bool = false;
            }
            else
            { 
            if (srch.type != "all")
            {
                if (srch.type == "sale" || srch.type == "lease")
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



            var res_1 = main_arr.
                Where(x1 => srch.type == "all" ? true : x1.Type_ad == srch.type ? true : false).
                Where(x2 => srch.type2 == "all-type" ? true : x2.Type_of_apartment == srch.type2 ? true : false).
                Where(x3 => (srch.town == "Вся Россия" ? true : x3.Place.IndexOf(srch.town) != -1) && (x3.search_str(srch.str)));//.OrderBy(x4=> int.TryParse(x4.Price))
            if (srch.VIP)
            {
                res_1 = res_1.Where(x5 => x5.VIP);
                //res = res_1.ToList();
                //return res;
            }

            if (srch.price_bool != null)
            {
                res_1 = res_1.OrderBy(x4 => x4.Price);
                if (srch.price_bool == false)
                {
                    res_1 = res_1.Reverse();
                }


            }
            if (srch.rooms_bool != null)
            {
                res_1 = res_1.OrderBy(x4 => x4.Count_rooms);
                if (srch.rooms_bool == false)
                {
                    res_1 = res_1.Reverse();
                }


            }










            if (srch.Price_bot != null || srch.Price_top != null)//
            {
                    res_1 = res_1.Where(x5 => (x5.Price >= (srch.Price_bot == null ? 0 : srch.Price_bot)) && ((srch.Price_top == null ? true : x5.Price <= srch.Price_top)));//&& x5.Price >= srch.Price_top);
                    
                   // res_1 = res_1.Where(x5 => (x5.Price >= (srch.Price_bot == null ? 0 : srch.Price_bot)));//&& x5.Price >= srch.Price_top);


                }
                if (srch.Count_rooms_bot != null || srch.Count_rooms_top != null)
            {

                res_1 = res_1.Where(x5 => (x5.Count_rooms >= (srch.Count_rooms_bot == null ? 0 : srch.Count_rooms_bot)) && ((srch.Count_rooms_top == null ? true : x5.Count_rooms <= srch.Count_rooms_top)));


            }
            if (srch.Total_area_bot != null || srch.Total_area_top != null)
            {
                res_1 = res_1.Where(x5 => (x5.Total_area >= (srch.Total_area_bot == null ? 0 : srch.Total_area_bot)) && ((srch.Total_area_top == null ? true : x5.Total_area <= srch.Total_area_top)));


            }
            if (srch.Residential_area_bot != null || srch.Residential_area_top != null)
            {
                res_1 = res_1.Where(x5 => (x5.Residential_area >= (srch.Residential_area_bot == null ? 0 : srch.Residential_area_bot)) && ((srch.Residential_area_top == null ? true : x5.Residential_area <= srch.Residential_area_top)));


            }
            if (srch.Floor != null)
            {
                res_1 = res_1.Where(x5 => x5.Floor == srch.Floor);


            }
            if (srch.Count_floor != null)
            {
                res_1 = res_1.Where(x5 => x5.Count_floor == srch.Count_floor);

            }
            if (srch.Place != null)
            {
                res_1 = res_1.Where(x5 => x5.Place.IndexOf(srch.Place) != -1);

            }


            res = res_1.ToList();
        

            srch.Count_page = res.Count / srch.Count_ad_on_page + 1;
                if (srch.pg > srch.Count_page)
                    srch.pg = srch.Count_page;
            int int_skip = (srch.pg > 0 ? srch.pg - 1 : srch.pg)*srch.Count_ad_on_page;
            res = res.Skip(int_skip< res.Count? int_skip: res.Count- srch.Count_ad_on_page).
                    Take(srch.Count_ad_on_page).
                    ToList();
            }

            return res;
        }


        public list_ad_View list_ad_ajax_1_function(string filter = "", string search = null)
        {
            irr.Models.Search srch = irr.Models.Search.FromString(search);
            if(!string.IsNullOrEmpty(filter))
            {
                if (filter != "cancel")
                {
                    if (filter == "price")
                        if (srch.price_bool != null)
                        {
                            srch.pg = 1;
                            srch.price_bool = !srch.price_bool;
                        }

                    if (filter == "rooms")
                        if (srch.rooms_bool != null)
                        {
                            srch.pg = 1;
                            srch.rooms_bool = !srch.rooms_bool;
                        }
                    if (filter.IndexOf("page=")!=-1)
                    {
                        try
                        {
                            string a = filter.Substring(5);//, filter.Length
                            int b = Convert.ToInt32(a);
                            if (b < 1)
                                throw new System.InvalidOperationException("er+-");
                            srch.pg = b;
                        }
                        catch
                        {

                        }
                        
                    }

                }
                else
                {
                    srch.pg = 1;
                    srch.price_bool = null;
                    srch.rooms_bool = null;
                }
            }
            




            //string type = "all", string type2 = "all-type", int pg = 1,


            //, type=Model.Type, type2=Model.Type2,pg=Model.Current_page
            //, string type = "all", string type2 = "all-type", int pg = 1
            // string type = res.Type;
            //string type2 = res.Type2;
            //int pg = res.Current_page;





            list_ad_View res = new list_ad_View() { Count_ad_on_page = srch.Count_ad_on_page, Type = srch.type, Type2 = srch.type2, Current_page = srch.pg };


            //int tmp = pg - i - 1;
            // res.str[i] = tmp > 1 ? tmp : 1;



            //

            UP_nedo_bd();
            res.list = search_bd(srch);
            res.srch = srch.copy();
            




            //


            if (true)
            {
                res.str.Add(1);
                int tmp = srch.pg - 2;
                tmp = tmp > 1 ? tmp : 2;

                for (int i = 1; i < 5 && tmp < srch.Count_page; ++i)
                {
                    res.str.Add(tmp);
                    ++tmp;
                }
            }
            if (srch.Count_page != 1)
            {
                res.str.Add(srch.Count_page);
            }

            res.Count_page = srch.Count_page;
            return res;
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }


}










