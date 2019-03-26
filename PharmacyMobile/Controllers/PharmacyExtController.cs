using CommonLibs;
using LanguageLocale;
using PagedList;
using System.Linq;
using Sonetwsv;
using Sonetwsv.common;
using Sonetwsv.Mobileap;
using Sonetwsv.Mobilews;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace PharmacyMobile.Controllers
{
    public class PharmacyExtController : Controller
    {        
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Session["KeyUserLogin"] = new Guid("3ac568c7-8c86-49eb-bdfa-0004dfecad8b");
            //Session["CodeUserLogin"] = "0985000934";
        }
        
        protected static string DBHost = ConfigurationManager.AppSettings["DBHost"];
        protected static string DBUser = ConfigurationManager.AppSettings["DBUser"];
        protected static string DBPassword = ConfigurationManager.AppSettings["DBPassword"];
        protected static string DBName = ConfigurationManager.AppSettings["DBName"];
        
        public ActionResult Login(string Username, string Password)
        {
            try
            {
                if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
                {
                    string TenNguoiDung = "User Login";
                    //if (Username != null && Password != null && Username == "admin" && Password == "admin")
                    if (Username != null && Password != null && cls_USERS_MOBILE.Login(Username, Password, out TenNguoiDung))
                    {
                        return Json(new { success = true, TEN_NGUOI_DUNG = TenNguoiDung });
                    }
                }
                return Json(new { success = false });
            }
            catch (Exception ex)

            { return Json(new { success = false, messenge = ex.Message + " " + ex.StackTrace }); }
        }

        public ActionResult Sendsmslogin(string PhoneNumber)
        {
            try
            {
                if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
                {
                    bool phoneExist = false;
                    phoneExist = cls_2018_CLIENT.m2018_CheckClient(PhoneNumber);
                    if (phoneExist)
                    {
                        string SmsContentForLoginCode = ConfigurationManager.AppSettings["SmsContentForLoginCode"];
                        string code = CommonFunctions.CreateString(6);
                        if (!string.IsNullOrWhiteSpace(code))
                        {
                            bool sendSmsResult = WebCommonFunctions.RequestSendSms(PhoneNumber, string.Format(SmsContentForLoginCode, code));
                            if (sendSmsResult)
                            {
                                Session["SmsLoginCode"] = code;
                                Session["TimeStampSmsLoginCode"] = DateTime.Now;
                                return Json(new { success = true });
                            }
                        }
                    }
                    else return Json(new { success = false, messenge = Languge.PhoneNotExist });
                }
                return Json(new { success = false, messenge = Languge.ConnectNotSuccess });
            }
            catch (Exception ex)

            { return Json(new { success = false, messenge = ex.Message + " " + ex.StackTrace }); }
        }

        public ActionResult Checksmslogincode(string PhoneNumber, string UserLoginCode)
        {
            try
            {
                if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
                {
                    string SmsLoginCode = Session["SmsLoginCode"] as string;
                    if (!string.IsNullOrWhiteSpace(SmsLoginCode))
                    {
                        DateTime timestampSms = (DateTime)Session["TimeStampSmsLoginCode"];
                        if((DateTime.Now - timestampSms).Minutes > 5)
                        {
                            Session["SmsLoginCode"] = null;
                            Session["TimeStampSmsLoginCode"] = null;
                            return Json(new { success = false, messenge = Languge.OTP_CodeExpired });
                        }
                        if (UserLoginCode.CompareTo(SmsLoginCode) == 0)
                        {
                            CARD_CLIENT clientCard = CARD_CLIENT.ParseFromDataTable<CARD_CLIENT>(cls_2018_CLIENT.m2018_GetoneClient(PhoneNumber));
                            Session["SmsLoginCode"] = null;
                            Session["TimeStampSmsLoginCode"] = null;
                            Session["KeyUserLogin"] = clientCard.KEY_CARD_CLIENT;
                            Session["CodeUserLogin"] = clientCard.COD_CARD_CLIENT;
                            return Json(new { success = true, UserDisplayName = PhoneNumber, userData = clientCard });
                        }
                    }
                }
                return Json(new { success = false, messenge = Languge.ConnectNotSuccess });
            }
            catch (Exception ex)

            { return Json(new { success = false, messenge = ex.Message + " " + ex.StackTrace }); }
        }

        public ActionResult GetMyCart(int? page, int? pageSize, DateTime? dateStart, DateTime? dateEnd)
        {
            if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
            {
                int PageNumber = page ?? 1;
                int PageSizeVal = pageSize ?? 12;
                DateTime now = DateTime.Now.Date;
                List<ORDER_CLIENT> lstDataOrder = ORDER_CLIENT.ParseListFromDataTable<ORDER_CLIENT>(cls_2018_ORDERS.m2018_GetallOrders((Guid)Session["KeyUserLogin"]));
                if (dateStart != null) lstDataOrder = lstDataOrder.Where(a => a.DAY_ORDER_CLIENT >= dateStart.Value.Date).ToList();
                if (dateEnd != null)
                {
                    var dateEndVal = dateEnd.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                    lstDataOrder = lstDataOrder.Where(a => a.DAY_ORDER_CLIENT <= dateEndVal).ToList();
                }
                var dataResult = lstDataOrder.Select(a => new {
                    KEY_ORDER_CLIENT = a.KEY_ORDER_CLIENT,
                    KEY_CARD_CLIENT = a.KEY_CARD_CLIENT,
                    DAY_ORDER_CLIENT = a.DAY_ORDER_CLIENT.Value.ToString("dd-MM-yyyy HH:mm:ss"),
                    COD_ORDER_CLIENT = a.COD_ORDER_CLIENT,
                    TONG_TIEN_HANG = a.TONG_TIEN_HANG,
                    TONG_GIAM_GIA = a.TONG_GIAM_GIA,
                    TONG_TIEN_TOAN = a.TONG_TIEN_TOAN,
                    STA_ORDER_CLIENT = a.STA_ORDER_CLIENT
                }).ToPagedList(PageNumber, PageSizeVal);
                return Json(new {
                    success = true,
                    result = dataResult,
                    currentPage = dataResult.PageNumber,
                    totalPage = dataResult.PageCount,
                    firstItemOnPage = dataResult.FirstItemOnPage,
                    lastItemOnPage = dataResult.LastItemOnPage
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDetailCart(Guid id)
        {
            if(id != null)
            {
                string CodeUserLogin = Session["CodeUserLogin"] as string;
                CARD_CLIENT clientCard = CARD_CLIENT.ParseFromDataTable<CARD_CLIENT>(cls_2018_CLIENT.m2018_GetoneClient(CodeUserLogin));
                List<Sonetwsv.MAT_HANG> productMap = Sonetwsv.MAT_HANG.ParseListFromDataTable<Sonetwsv.MAT_HANG>(cls_2018_GOODS.m2018_FilterGoods(""));
                ORDER_CLIENT orderClient = ORDER_CLIENT.ParseFromDataTable<ORDER_CLIENT>(cls_2018_ORDERS.m2018_GetoneOrders(id));
                List<ORDER_GOODS> orderGoods = ORDER_GOODS.ParseListFromDataTable<ORDER_GOODS>(cls_2018_ORDERS.m2018_GetsallGoods(id));
                return Json(new { success = true, result = orderClient, orderGoods = orderGoods, productMap = productMap, CLIENT_INFO = clientCard }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetNewCartData()
        {
            CARD_CLIENT clientCard = null;
            if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
            {
                string CodeUserLogin = Session["CodeUserLogin"] as string;
                clientCard = CARD_CLIENT.ParseFromDataTable<CARD_CLIENT>(cls_2018_CLIENT.m2018_GetoneClient(CodeUserLogin));
            }
            return Json(new
            {
                COD_ORDER_CLIENT = DateTime.Now.ToString("ddMMyyyyHHmmss"),
                CLIENT_INFO = clientCard
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGoodByPage(string filter, int? page)
        {
            if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
            {
                int pageIndex = page ?? 1;
                if (string.IsNullOrEmpty(filter)) filter = "";
                DateTime now = DateTime.Now.Date;
                DateTime startDate = new DateTime(now.Year, now.Month, now.Day);
                DateTime endDate = startDate.AddHours(23).AddMinutes(59).AddSeconds(59);
                List<Sonetwsv.MAT_HANG> lstDataOrder = Sonetwsv.MAT_HANG.ParseListFromDataTable<Sonetwsv.MAT_HANG>(cls_2018_GOODS.m2018_FilterGoods(filter));
                var pagedData = lstDataOrder.ToPagedList(pageIndex, 20);
                var questionPagedList = new StaticPagedList<Sonetwsv.MAT_HANG>(pagedData, pagedData.GetMetaData());
                return Json(new { success = true, result = questionPagedList,
                    currentPage = pagedData.PageNumber,
                    totalPage = pagedData.PageCount,
                    firstItemOnPage = pagedData.FirstItemOnPage,
                    lastItemOnPage = pagedData.LastItemOnPage
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
                
        public ActionResult GetUserInfo()
        {
            if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
            {
                string CodeClient = Session["CodeUserLogin"] as string;
                CARD_CLIENT userInfo = CARD_CLIENT.ParseFromDataTable<CARD_CLIENT>(cls_2018_CLIENT.m2018_GetoneClient(CodeClient));
                return Json(new { success = true, result = userInfo }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFeedInfo()
        {
            try
            {
                string FeedUrl = ConfigurationManager.AppSettings["FeedUrl"];
                var dataFeed = CommonFunctions.GetFeedData(FeedUrl);
                return Json(new { success = true, result = dataFeed }, JsonRequestBehavior.AllowGet);
            }
            catch { }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGoodInfo(Guid goodkey)
        {
            if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
            {
                Sonetwsv.MAT_HANG goodInfo = Sonetwsv.MAT_HANG.ParseFromDataTable<Sonetwsv.MAT_HANG>(cls_2018_GOODS.m2018_GetoneGoods(goodkey));
                return Json(new { success = true, result = goodInfo }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddNewCart([FromBody] string jsonData)
        {
            ORDER_CLIENT orderClient = null; ORDER_GOODS[] listGoods = null;
            JObject objectData = JObject.Parse(jsonData);
            if (objectData["orderClient"] != null) orderClient = objectData["orderClient"].ToObject<ORDER_CLIENT>();
            if (objectData["listGoods"] != null) listGoods = objectData["listGoods"].ToObject<ORDER_GOODS[]>();

            if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
            {
                DateTime now = DateTime.Now.Date;
                orderClient.KEY_ORDER_CLIENT = Guid.NewGuid();
                orderClient.KEY_CARD_CLIENT = (Guid)Session["KeyUserLogin"];
                orderClient.DAY_ORDER_CLIENT = now;
                orderClient.STA_ORDER_CLIENT = false;

                if (listGoods != null && listGoods.Length > 0)
                {
                    foreach(var itemGoodCard in listGoods)
                    {
                        itemGoodCard.KEY_ORDER_GOODS = Guid.NewGuid();
                        itemGoodCard.KEY_ORDER_CLIENT = orderClient.KEY_ORDER_CLIENT;
                        itemGoodCard.TIEN_VIET_NAM = itemGoodCard.DON_GIA_HANG * itemGoodCard.SO_LUONG_HANG ?? 0;
                        orderClient.TONG_TIEN_HANG += itemGoodCard.TIEN_VIET_NAM;
                    }
                }
                
                orderClient.TONG_TIEN_TOAN = orderClient.TONG_TIEN_HANG - orderClient.TONG_GIAM_GIA;
                bool success = cls_2018_ORDERS.m2018_InsertNewOrder(orderClient, listGoods);
                                
                return Json(new { success = success }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

    }

    
    public class KHACH_HANG
    {
        public string code;
        public string hoten;
        public string gioitinh;
        public DateTime? ngaysinh;
        public string dienthoai;
        public string email;
        public string diachi;
        public string congty;

        public bool ParseValue(NameValueCollection FormData)
        {
            try
            {
                string code = FormData["code"];
                if (!string.IsNullOrEmpty(code)) this.code = code;
                string hoten = FormData["hoten"];
                if (!string.IsNullOrEmpty(hoten)) this.hoten = hoten;
                string gioitinh = FormData["gioitinh"];
                if (!string.IsNullOrEmpty(gioitinh)) this.gioitinh = gioitinh;
                string ngaysinh = FormData["ngaysinh"];
                string thangsinh = FormData["thangsinh"];
                string namsinh = FormData["namsinh"];
                if (!string.IsNullOrEmpty(ngaysinh) && !string.IsNullOrEmpty(thangsinh) && !string.IsNullOrEmpty(namsinh))
                {
                    this.ngaysinh = new DateTime(int.Parse(namsinh), int.Parse(thangsinh), int.Parse(ngaysinh));
                }
                else this.ngaysinh = null;
                string dienthoai = FormData["dienthoai"];
                if (!string.IsNullOrEmpty(dienthoai)) this.dienthoai = dienthoai;
                string email = FormData["email"];
                if (!string.IsNullOrEmpty(email)) this.email = email;
                string diachi = FormData["diachi"];
                if (!string.IsNullOrEmpty(diachi)) this.diachi = diachi;
                string congty = FormData["congty"];
                if (!string.IsNullOrEmpty(congty)) this.congty = congty;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<string> CheckValid()
        {
            List<string> lstError = new List<string>();
            if (code == null) lstError.Add("Mã code");
            if (hoten == null) lstError.Add("Họ tên khách hàng");
            //if (ngaysinh == null) lstError.Add("Ngày sinh");
            if (gioitinh == null) lstError.Add("Giới tính");
            if (dienthoai == null) lstError.Add("Số điệnt thoại");

            return lstError;
        }
    }

    public class MAT_HANG
    {
        public Guid? KEY_MAT_HANG;
        public Guid? loaihang;
        public Guid? kieuhang;
        public Guid? nhomhang;
        public Guid? xuatxu;
        public Guid? hanghang;
        public string mamathang;
        public string tenmathang;
        public string masanxuat;
        public string donvitinh;
        public string donvitinhlon;
        public decimal? soquydoi;
        public decimal? tilegiamgia;
        public decimal? hesothue;
        public decimal? tiletienlai;
        public bool? hangkedon;
        public bool? hangdacbiet;
        public bool? hangkigui;
        public decimal? dongiamua;
        public decimal? giabanle;
        public decimal? giabansi;
        public string ghichu;
        public string hamluong;
        public string hoatchat;
        public decimal? tontoithieu;
        public decimal? tilemogioi;
        public decimal? tileghidiem;
        public bool? cosudung;
        public DateTime? DATE_DONG_BO;
        public int? VERS_DONG_BO;
        public bool? FLAG_DONG_BO;

        public bool ParseValue(NameValueCollection FormData)
        {
            try
            {
                string loaihang = FormData["loaihang"];
                if (!string.IsNullOrEmpty(loaihang)) this.loaihang = new Guid(loaihang);
                string kieuhang = FormData["kieuhang"];
                if (!string.IsNullOrEmpty(kieuhang)) this.kieuhang = new Guid(kieuhang);
                string nhomhang = FormData["nhomhang"];
                if (!string.IsNullOrEmpty(nhomhang)) this.nhomhang = new Guid(nhomhang);
                string xuatxu = FormData["xuatxu"];
                if (!string.IsNullOrEmpty(xuatxu)) this.xuatxu = new Guid(xuatxu);
                string hanghang = FormData["hangsanxuat"];
                if (!string.IsNullOrEmpty(hanghang)) this.hanghang = new Guid(hanghang);
                string mamathang = FormData["mamathang"];
                if (!string.IsNullOrEmpty(mamathang)) this.mamathang = mamathang;
                string tenmathang = FormData["tenmathang"];
                if (!string.IsNullOrEmpty(tenmathang)) this.tenmathang = tenmathang;
                string masanxuat = FormData["masanxuat"];
                if (!string.IsNullOrEmpty(masanxuat)) this.masanxuat = masanxuat;
                string donvitinh = FormData["donvitinh"];
                if (!string.IsNullOrEmpty(donvitinh)) this.donvitinh = donvitinh;
                string donvitinhlon = FormData["donvitinhlon"];
                if (!string.IsNullOrEmpty(donvitinhlon)) this.donvitinhlon = donvitinhlon;
                string soquydoi = FormData["soquydoi"];
                if (!string.IsNullOrEmpty(soquydoi)) this.soquydoi = decimal.Parse(soquydoi);
                string tilegiamgia = FormData["tilegiamgia"];
                if (!string.IsNullOrEmpty(tilegiamgia)) this.tilegiamgia = decimal.Parse(tilegiamgia);
                string hesothue = FormData["hesothue"];
                if (!string.IsNullOrEmpty(hesothue)) this.hesothue = decimal.Parse(hesothue);
                string tiletienlai = FormData["tiletienlai"];
                if (!string.IsNullOrEmpty(tiletienlai)) this.tiletienlai = decimal.Parse(tiletienlai);
                string hangkedon = FormData["hangkedon"];
                if (!string.IsNullOrEmpty(hangkedon)) this.hangkedon = bool.Parse(hangkedon);
                string hangdacbiet = FormData["hangdacbiet"];
                if (!string.IsNullOrEmpty(hangdacbiet)) this.hangdacbiet = bool.Parse(hangdacbiet);
                else this.hangdacbiet = false;
                string hangkigui = FormData["hangkigui"];
                if (!string.IsNullOrEmpty(hangkigui)) this.hangkigui = bool.Parse(hangkigui);
                else this.hangkigui = false;
                string dongiamua = FormData["dongiamua"];
                if (!string.IsNullOrEmpty(dongiamua)) this.dongiamua = decimal.Parse(dongiamua);
                else this.hangdacbiet = false;
                string giabanle = FormData["giabanle"];
                if (!string.IsNullOrEmpty(giabanle)) this.giabanle = decimal.Parse(giabanle);
                string giabansi = FormData["giabansi"];
                if (!string.IsNullOrEmpty(giabansi)) this.giabansi = decimal.Parse(giabansi);
                string ghichu = FormData["ghichu"];
                if (!string.IsNullOrEmpty(ghichu)) this.ghichu = ghichu; else this.ghichu = "";
                string hamluong = FormData["hamluong"];
                if (!string.IsNullOrEmpty(hamluong)) this.hamluong = hamluong; else this.hamluong = "";
                string hoatchat = FormData["hoatchat"];
                if (!string.IsNullOrEmpty(hoatchat)) this.hoatchat = hoatchat; else this.hoatchat = "";
                string tontoithieu = FormData["tontoithieu"];
                if (!string.IsNullOrEmpty(tontoithieu)) this.tontoithieu = decimal.Parse(tontoithieu);
                string tilemogioi = FormData["tilemogioi"];
                if (!string.IsNullOrEmpty(tilemogioi)) this.tilemogioi = decimal.Parse(tilemogioi);
                string tileghidiem = FormData["tileghidiem"];
                if (!string.IsNullOrEmpty(tileghidiem)) this.tileghidiem = decimal.Parse(tileghidiem);
                string cosudung = FormData["cosudung"];
                if (!string.IsNullOrEmpty(cosudung)) this.cosudung = bool.Parse(cosudung);
                else this.cosudung = false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<string> CheckValid()
        {
            List<string> lstError = new List<string>();
            if(loaihang == null) lstError.Add("Loại mặt hàng");
            if (kieuhang == null) lstError.Add("Kiểu mặt hàng");
            if (nhomhang == null) lstError.Add("Nhóm mặt hàng");
            if (xuatxu == null) lstError.Add("Xuất xứ mặt hàng");
            if (hanghang == null) lstError.Add("Hãng sản xuất mặt hàng");
            if (mamathang == null) lstError.Add("Mã mặt hàng");
            if (tenmathang == null) lstError.Add("Tên mặt hàng");
            if (masanxuat == null) lstError.Add("Mã sản xuất");
            if (donvitinh == null) lstError.Add("Đơn vị tính");
            if (donvitinhlon == null) lstError.Add("Đơn vị tính lớn");
            if (soquydoi == null) lstError.Add("Số quy đổi");
            if (tilegiamgia == null) lstError.Add("Tỉ lệ giảm giá");
            if (hesothue == null) lstError.Add("Hệ số thuế");
            if (tiletienlai == null) lstError.Add("Tỉ lệ tiền lãi");

            if (dongiamua == null) lstError.Add("Đơn giá mua");
            if (giabanle == null) lstError.Add("Giá bán lẻ");
            if (giabansi == null) lstError.Add("Giá bán sĩ");
            if (tontoithieu == null) lstError.Add("Tồn tối thiểu");
            if (tilemogioi == null) lstError.Add("Tỉ lệ mô giới");
            if (tileghidiem == null) lstError.Add("Tỉ lệ ghi điểm");

            return lstError;
        }
    }
        
}