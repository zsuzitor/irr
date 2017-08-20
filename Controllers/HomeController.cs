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
      
        EntryContext db = new EntryContext();
        //нигде не используется пока что
        List<string> main_town = new List<string>() { "Вся Россия", "Москва", "Петербург", "Пермь", "Воронеж", "Казань", "Сочи", "Саратов" };
        //db.Players.Add(player);
        //db.SaveChanges();
        //
        int ta = 0;
        //TODO список категорий и именно его закидывать уже куда надо List<string> category = new List<string>() { "Квартиры", "Телефоны", "Животные", "Машины" };

        //-SETTINGS/ADMIN BLOCK--------------------------------------------------------------------------------------------------------------------//
        
        public ActionResult Index()
        {
            db.Entries.RemoveRange(db.Entries.Where(x1=>x1.Header.IndexOf("test322")!=-1).ToList());
            db.Entries.RemoveRange(db.Entries.Where(x1 => x1.Id == 7378).ToList());
            db.SaveChanges();
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
            
            return PartialView(res);
        }
        
        public ActionResult Extended_search_ajax_3(string Search)
        {
            Search res = irr.Models.Search.FromString(Search);
            
            //partial для ajax
           
            return PartialView(res);
        }
        public ActionResult Extended_search_ajax_2(bool flag=false, string search=null)
        {
            Search res = irr.Models.Search.FromString(search); ;
           
            res.flag = flag;
            //partial для ajax
            
            return PartialView(res);
        }


        public ActionResult list_ad_ajax_1(string filter="",string search=null)
        {
            
            list_ad_View res = list_ad_ajax_1_function(filter, search);
           
            return PartialView(res);
        }

        [ChildActionOnly]
        public ActionResult vip_entry(Search srch)
        {
            srch.VIP = true;

            
            return PartialView(search_bd(srch));
        }
        
        public ActionResult Add_new_ad_ajax_1(string category,  string obg)
        {
            Entry res;
            if (obg != null)
                res = JsonConvert.DeserializeObject<Entry>(obg);
            else
                res = new Entry();
            ViewBag.category = category;
            res.Type = category;
            return PartialView(res);
        }

        //END-PARTIAL BLOCK--------------------------------------------------------------------------------------------------------------------//


        //-POST/FORM BLOCK--------------------------------------------------------------------------------------------------------------------//
        //отправка формы с поиском
        [HttpPost]
        public ActionResult Search(string str, string category, string town)
        {
            
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
        public ActionResult Ad_img_add_ad(HttpPostedFileBase[] uploadImage,string obg=null)
        {
            //TODO сейчас не работает и ничего не делает нужно в теории для обработки фоток которые загружаются

            //@Html.Hidden(Model.Images_byte,i)
            irr.Models.Entry res = new Entry();
            Entry_img img = new Entry_img();
            if (obg != null)
                res = JsonConvert.DeserializeObject<Entry>(obg);
            if ( uploadImage != null)//ModelState.IsValid &&
            {
                foreach(var i in uploadImage)
                {
                    try
                    {
                        byte[] imageData = null;
                        // считываем переданный файл в массив байтов
                        using (var binaryReader = new BinaryReader(i.InputStream))
                        {
                            imageData = binaryReader.ReadBytes(i.ContentLength);
                        }
                        // установка массива байтов
                        res.Images_byte.Add(imageData);



                        //return RedirectToAction("Add_new_ad", res);

                    }
                    catch
                    {

                    }


                }



                try
                {
                    img.b_1=res.Images_byte[0];
                    img.b_2 = res.Images_byte[1];
                    img.b_3 = res.Images_byte[2];
                    img.b_4 = res.Images_byte[3];
                    img.b_5 = res.Images_byte[4];
                    img.b_6 = res.Images_byte[5];
                    img.b_7 = res.Images_byte[6];
                    img.b_8 = res.Images_byte[7];
                    img.b_9 = res.Images_byte[8];
                    img.b_10 = res.Images_byte[9];


                }
                catch
                {

                }
                finally
                {
                    db.Images.Add(img);
                    db.SaveChanges();
                }
                res.Images_id = img.Id;



            }
            return View("Show_one_ad", res);
        }
        [HttpPost]
        public ActionResult list_ad_ajax_1_1(string page = "", string search = null)
        {
            if (page == null)
                page = "";
            list_ad_View res = list_ad_ajax_1_function("page="+page, search);
            
            return PartialView("list_ad_ajax_1", res);
        }
        [HttpPost]
        public ActionResult Extended_search_ajax_3_list_ad(Search srch)
        {
            

            //partial для ajax
            
            list_ad_View res = list_ad_ajax_1_function("", srch.ToString());
            
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
            bool error_flag = false;
            TempData["er_Type_of_apartment"] = null;
            TempData["er_Type_ad"] = null;
            TempData["er_Type"] = null;
            TempData["er_Price"] = null;
            TempData["er_Count_rooms"] = null;
            TempData["er_Phone_number"] = null;
            TempData["er_Place"] = null;
            //"Жилая недвижимость", "Коммерческая недвижимость", "Загородная недвижимость"
            if (a.Type_of_apartment != "Жилая недвижимость" && a.Type_of_apartment != "Коммерческая недвижимость" && a.Type_of_apartment != "Загородная недвижимость")
            {
                TempData["er_Type_of_apartment"] = "error Type of apartment";
                error_flag = true;
            }
            //"Продажа", "Аренда"
            if (a.Type_ad != "Продажа" && a.Type_ad != "Аренда")
            {
                TempData["er_Type_ad"] = "error Type ad";
                //ModelState.AddModelError("Type_ad", "error Type ad");
                error_flag = true;
            }
            if (a.Type != "Квартиры" )
            {
                TempData["er_Type"] = "error Type";
                //ModelState.AddModelError("Type", "error Type");
                error_flag = true;
            }
            if (a.Price == null|| a.Price ==0)
            {
                TempData["er_Price"] = "error Price";
                //ModelState.AddModelError("Price", "error Price");
                error_flag = true;
            }
            
                if (a.Count_rooms == null)
            {
                TempData["er_Count_rooms"] = "error Count rooms";
                //ModelState.AddModelError("Count_rooms", "error Count rooms");
                error_flag = true;
            }
            //Phone_number
            if (a.Phone_number == null)
            {
                TempData["er_Phone_number"] = "error Phone number";
               // ModelState.AddModelError("Phone_number", "error Phone number");
                error_flag = true;
            }
            else
            {
                if(a.Phone_number.Length>12|| a.Phone_number.Length < 10)
                {
                    TempData["er_Phone_number"] = "error Phone number";
                   // ModelState.AddModelError("Phone_number", "error Phone number");
                    error_flag = true;
                }
            }
            if (a.Place == null)
            {
                TempData["er_Place"] = "error Place";
                //ModelState.AddModelError("Place", "error Place");
                error_flag = true;
            }


            if (error_flag)
            {



                return View("Add_new_ad", a);
            }
            else
            {

            
            if (a.Type == "Квартиры")
            {
                // if(a.Info1.Count>0&&!string.IsNullOrEmpty( a.Info1[0]))//!string.IsNullOrEmpty
                {
                    //\r\n
                    string[] mas = a.Info1[0].Split(new char[] { '\r', '\n' });
                    a.Info1.Clear();
                    Entry_info info = new Entry_info();
                    try
                    {
                        info.info_1 = mas[0];
                        a.Info1.Add(mas[0]);
                        info.info_2 = mas[2];
                        a.Info1.Add(mas[2]);
                        info.info_3 = mas[4];
                        a.Info1.Add(mas[4]);
                        info.info_4 = mas[6];
                        a.Info1.Add(mas[6]);
                        info.info_5 = mas[8];
                        a.Info1.Add(mas[8]);
                        info.info_6 = mas[10];
                        a.Info1.Add(mas[10]);
                        info.info_7 = mas[12];
                        a.Info1.Add(mas[12]);
                        info.info_8 = mas[14];
                        a.Info1.Add(mas[14]);
                        info.info_9 = mas[16];
                        a.Info1.Add(mas[16]);
                        info.info_10 = mas[18];
                        a.Info1.Add(mas[18]);
                    }
                    catch
                    {

                    }
                    finally
                    {
                        db.Info.Add(info);
                        db.SaveChanges();
                        a.Info1_id = info.Id;
                    }

                }
                //if (a.Info2.Count > 0 && !string.IsNullOrEmpty(a.Info2[0]))//!string.IsNullOrEmpty
                {
                    //\r\n
                    string[] mas = a.Info2[0].Split(new char[] { '\r', '\n' });
                    a.Info2.Clear();
                    Entry_info info = new Entry_info();
                    try
                    {
                        info.info_1 = mas[0];
                        a.Info2.Add(mas[0]);
                        info.info_2 = mas[2];
                        a.Info2.Add(mas[2]);
                        info.info_3 = mas[4];
                        a.Info2.Add(mas[4]);
                        info.info_4 = mas[6];
                        a.Info2.Add(mas[6]);
                        info.info_5 = mas[8];
                        a.Info2.Add(mas[8]);
                        info.info_6 = mas[10];
                        a.Info2.Add(mas[10]);
                        info.info_7 = mas[12];
                        a.Info2.Add(mas[12]);
                        info.info_8 = mas[14];
                        a.Info2.Add(mas[14]);
                        info.info_9 = mas[16];
                        a.Info2.Add(mas[16]);
                        info.info_10 = mas[18];
                        a.Info2.Add(mas[18]);
                    }
                    catch
                    {

                    }
                    finally
                    {
                        db.Info.Add(info);
                        db.SaveChanges();
                        a.Info2_id = info.Id;
                    }

                }
                // if (a.Info3.Count > 0 && !string.IsNullOrEmpty(a.Info3[0]))//!string.IsNullOrEmpty
                {
                    //\r\n
                    string[] mas = a.Info3[0].Split(new char[] { '\r', '\n' });
                    a.Info3.Clear();
                    Entry_info info = new Entry_info();
                    try
                    {
                        info.info_1 = mas[0];
                        a.Info3.Add(mas[0]);
                        info.info_2 = mas[2];
                        a.Info3.Add(mas[2]);
                        info.info_3 = mas[4];
                        a.Info3.Add(mas[4]);
                        info.info_4 = mas[6];
                        a.Info3.Add(mas[6]);
                        info.info_5 = mas[8];
                        a.Info3.Add(mas[8]);
                        info.info_6 = mas[10];
                        a.Info3.Add(mas[10]);
                        info.info_7 = mas[12];
                        a.Info3.Add(mas[12]);
                        info.info_8 = mas[14];
                        a.Info3.Add(mas[14]);
                        info.info_9 = mas[16];
                        a.Info3.Add(mas[16]);
                        info.info_10 = mas[18];
                        a.Info3.Add(mas[18]);
                    }
                    catch
                    {

                    }
                    finally
                    {
                        db.Info.Add(info);
                        db.SaveChanges();
                        a.Info3_id = info.Id;
                    }

                }
                //if (a.Info4.Count > 0 && !string.IsNullOrEmpty(a.Info4[0]))//!string.IsNullOrEmpty
                {
                    //\r\n
                    string[] mas = a.Info4[0].Split(new char[] { '\r', '\n' });
                    a.Info4.Clear();
                    Entry_info info = new Entry_info();
                    try
                    {
                        info.info_1 = mas[0];
                        a.Info4.Add(mas[0]);
                        info.info_2 = mas[2];
                        a.Info4.Add(mas[2]);
                        info.info_3 = mas[4];
                        a.Info4.Add(mas[4]);
                        info.info_4 = mas[6];
                        a.Info4.Add(mas[6]);
                        info.info_5 = mas[8];
                        a.Info4.Add(mas[8]);
                        info.info_6 = mas[10];
                        a.Info4.Add(mas[10]);
                        info.info_7 = mas[12];
                        a.Info4.Add(mas[12]);
                        info.info_8 = mas[14];
                        a.Info4.Add(mas[14]);
                        info.info_9 = mas[16];
                        a.Info4.Add(mas[16]);
                        info.info_10 = mas[18];
                        a.Info4.Add(mas[18]);
                    }
                    catch
                    {

                    }
                    finally
                    {
                        db.Info.Add(info);
                        db.SaveChanges();
                        a.Info4_id = info.Id;
                    }

                }
                db.Entries.Add(a);
                db.SaveChanges();
            }





            return View("Add_new_ad_img_step", a);
        }
        }
        //END-POST/FORM BLOCK--------------------------------------------------------------------------------------------------------------------//


        public ActionResult Add_new_ad(Entry res=null,int rab=0)//rab что бы работало
        {
            if(res==null)
            
             res = new irr.Models.Entry();
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
            
            
            Entry res = db.Entries.First(x1 => x1.Id == id);
            Record(res);
            return View(res);
        }
        //Extended
        public ActionResult Extended_search()
        {
            Search res = new Models.Search();
           
            return View(res);

           
        }














        //-Function BLOCK--------------------------------------------------------------------------------------------------------------------//


        public List<Entry> search_bd(Search srch)
        {
            //TODO category нет поиска по категориям и бд без категорий
            List<Entry> res = new List<Entry>();
           

            if (srch.Id != null)
            {
                res = db.Entries.Where(x5 => x5.Id == srch.Id).ToList();
               
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



            var res_1 = db.Entries.
                Where(x1 => srch.type == "all" ? true : x1.Type_ad == srch.type ? true : false).
                Where(x2 => srch.type2 == "all-type" ? true : x2.Type_of_apartment == srch.type2 ? true : false).
                Where(x3 => (srch.town == "Вся Россия" ? true : x3.Place.IndexOf(srch.town) != -1));
                
    
                if (srch.VIP)
            {
                res_1 = res_1.Where(x5 => x5.VIP);
                
            }
                
            










            if (srch.Price_bot != null || srch.Price_top != null)//
            {
                    res_1 = res_1.Where(x5 => (x5.Price >= (srch.Price_bot == null ? 0 : srch.Price_bot)) && ((srch.Price_top == null ? true : x5.Price <= srch.Price_top)));//&& x5.Price >= srch.Price_top);
                    
                  

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
                if (!string.IsNullOrEmpty(srch.str))
                {
                    List<Entry> temp_res = new List<Entry>();
                    Entry[] tmp = null;
                    foreach (var i in res)
                    {
                        if(i.search_str(srch.str))
                        {
                            temp_res.Add(i);
                        }
                         

                    }
                    tmp =new Entry[temp_res.Count];
                     temp_res.CopyTo(tmp);
                    res = tmp.ToList();
                }
                if (srch.price_bool != null)
                {
                    res = res.OrderBy(x4 => x4.Price).ToList();
                    if (srch.price_bool == false)
                    {
                        //TO DO костыль ревеса хз не работает вроде +
                         res.Reverse();// res_1.Reverse();
                    }


                }
                if (srch.rooms_bool != null)
                {
                    res = res.OrderBy(x4 => x4.Count_rooms).ToList();
                    if (srch.rooms_bool == false)
                    {
                        res.Reverse();
                    }


                }
                
                srch.Count_page = res.Count / srch.Count_ad_on_page + 1;
                if (srch.pg > srch.Count_page)
                    srch.pg = srch.Count_page;
            int int_skip = (srch.pg > 0 ? srch.pg - 1 : srch.pg)*srch.Count_ad_on_page;
            res = res.Skip(int_skip< res.Count? int_skip: res.Count- srch.Count_ad_on_page).
                    Take(srch.Count_ad_on_page).
                    ToList();
            }

            for(int i=0;i<res.Count;++i)
            {
                Record(res[i]);
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
            


            list_ad_View res = new list_ad_View() { Count_ad_on_page = srch.Count_ad_on_page, Type = srch.type, Type2 = srch.type2, Current_page = srch.pg };



            res.list = search_bd(srch);
            res.srch = srch.copy();
            


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

        public Entry Record(Entry a)
        {
            Entry_info info= db.Info.First(x1 => x1.Id == a.Info1_id);
            Entry_img img = db.Images.First(x1 => x1.Id == a.Images_id);

            try
            {
                if (info.info_1 != null)
                    a.Info1.Add(info.info_1);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_2 != null)
                    a.Info1.Add(info.info_2);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_3 != null)
                    a.Info1.Add(info.info_3);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_4 != null)
                    a.Info1.Add(info.info_4);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_5 != null)
                    a.Info1.Add(info.info_5);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_6 != null)
                    a.Info1.Add(info.info_6);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_7 != null)
                    a.Info1.Add(info.info_7);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_8 != null)
                    a.Info1.Add(info.info_8);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_9 != null)
                    a.Info1.Add(info.info_9);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_10 != null)
                    a.Info1.Add(info.info_10);
                else
                    throw new IndexOutOfRangeException();
            }
            catch
            {

            }
            info = db.Info.First(x1 => x1.Id == a.Info2_id);

            try
            {
                if (info.info_1 != null)
                    a.Info2.Add(info.info_1);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_2 != null)
                    a.Info2.Add(info.info_2);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_3 != null)
                    a.Info2.Add(info.info_3);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_4 != null)
                    a.Info2.Add(info.info_4);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_5 != null)
                    a.Info2.Add(info.info_5);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_6 != null)
                    a.Info2.Add(info.info_6);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_7 != null)
                    a.Info2.Add(info.info_7);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_8 != null)
                    a.Info2.Add(info.info_8);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_9 != null)
                    a.Info2.Add(info.info_9);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_10 != null)
                    a.Info2.Add(info.info_10);
                else
                    throw new IndexOutOfRangeException();
            }
            catch
            {

            }
            info = db.Info.First(x1 => x1.Id == a.Info3_id);

            try
            {
                if (info.info_1 != null)
                    a.Info3.Add(info.info_1);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_2 != null)
                    a.Info3.Add(info.info_2);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_3 != null)
                    a.Info3.Add(info.info_3);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_4 != null)
                    a.Info3.Add(info.info_4);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_5 != null)
                    a.Info3.Add(info.info_5);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_6 != null)
                    a.Info3.Add(info.info_6);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_7 != null)
                    a.Info3.Add(info.info_7);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_8 != null)
                    a.Info3.Add(info.info_8);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_9 != null)
                    a.Info3.Add(info.info_9);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_10 != null)
                    a.Info3.Add(info.info_10);
                else
                    throw new IndexOutOfRangeException();
            }
            catch
            {

            }
            info = db.Info.First(x1 => x1.Id == a.Info4_id);

            try
            {
                if (info.info_1 != null)
                    a.Info4.Add(info.info_1);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_2 != null)
                    a.Info4.Add(info.info_2);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_3 != null)
                    a.Info4.Add(info.info_3);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_4 != null)
                    a.Info4.Add(info.info_4);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_5 != null)
                    a.Info4.Add(info.info_5);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_6 != null)
                    a.Info4.Add(info.info_6);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_7 != null)
                    a.Info4.Add(info.info_7);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_8 != null)
                    a.Info4.Add(info.info_8);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_9 != null)
                    a.Info4.Add(info.info_9);
                else
                    throw new IndexOutOfRangeException();
                if (info.info_10 != null)
                    a.Info4.Add(info.info_10);
                else
                    throw new IndexOutOfRangeException();
            }
            catch
            {

            }






            try
            {
                if (img.s_1 != null)
                    a.Images.Add(img.s_1);
                else
                    throw new IndexOutOfRangeException();
                if (img.s_2 != null)
                    a.Images.Add(img.s_2);
                else
                    throw new IndexOutOfRangeException();
                if (img.s_3 != null)
                    a.Images.Add(img.s_3);
                else
                    throw new IndexOutOfRangeException();
                if (img.s_4 != null)
                    a.Images.Add(img.s_4);
                else
                    throw new IndexOutOfRangeException();
                if (img.s_5 != null)
                    a.Images.Add(img.s_5);
                else
                    throw new IndexOutOfRangeException();
                if (img.s_6 != null)
                    a.Images.Add(img.s_6);
                else
                    throw new IndexOutOfRangeException();
                if (img.s_7 != null)
                    a.Images.Add(img.s_7);
                else
                    throw new IndexOutOfRangeException();
                if (img.s_8 != null)
                    a.Images.Add(img.s_8);
                else
                    throw new IndexOutOfRangeException();
                if (img.s_9 != null)
                    a.Images.Add(img.s_9);
                else
                    throw new IndexOutOfRangeException();
                if (img.s_10 != null)
                    a.Images.Add(img.s_10);
                else
                    throw new IndexOutOfRangeException();






            }
            catch
            {

            }
            try
            {
                if (img.b_1 != null)
                    a.Images_byte.Add(img.b_1);
                else
                    throw new IndexOutOfRangeException();
                if (img.b_2 != null)
                    a.Images_byte.Add(img.b_2);
                else
                    throw new IndexOutOfRangeException();
                if (img.b_3 != null)
                    a.Images_byte.Add(img.b_3);
                else
                    throw new IndexOutOfRangeException();
                if (img.b_4 != null)
                    a.Images_byte.Add(img.b_4);
                else
                    throw new IndexOutOfRangeException();
                if (img.b_5 != null)
                    a.Images_byte.Add(img.b_5);
                else
                    throw new IndexOutOfRangeException();
                if (img.b_6 != null)
                    a.Images_byte.Add(img.b_6);
                else
                    throw new IndexOutOfRangeException();
                if (img.b_7 != null)
                    a.Images_byte.Add(img.b_7);
                else
                    throw new IndexOutOfRangeException();
                if (img.b_8 != null)
                    a.Images_byte.Add(img.b_8);
                else
                    throw new IndexOutOfRangeException();
                if (img.b_9 != null)
                    a.Images_byte.Add(img.b_9);
                else
                    throw new IndexOutOfRangeException();
                if (img.b_10 != null)
                    a.Images_byte.Add(img.b_10);
                else
                    throw new IndexOutOfRangeException();







            }
            catch
            {

            }




            return a;
        }
    }


}










