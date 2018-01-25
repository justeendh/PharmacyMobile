using Newtonsoft.Json;
using Sonetwsv.common;
using Sonetwsv.Mobilews;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Threading;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PharmacyMobile.Controllers
{
    public class PharmacyController : Controller
    {
        private static string DBHost = ConfigurationManager.AppSettings["DBHost"];
        private static string DBUser = ConfigurationManager.AppSettings["DBUser"];
        private static string DBPassword = ConfigurationManager.AppSettings["DBPassword"];
        private static string DBName = ConfigurationManager.AppSettings["DBName"];

        public ActionResult Login(string Username, string Password)
        {
            try
            {
                if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
                {
                    string TenNguoiDung = "User Login";
                    if (Username != null && Password != null && Username == "admin" && Password == "admin")
                    //if (Username != null && Password != null && cls_USERS_MOBILE.Login(Username, Password, out TenNguoiDung))
                    {
                        return Json(new { success = true, TEN_NGUOI_DUNG = TenNguoiDung });
                    }
                }
                return Json(new { success = false });
            }
            catch (Exception ex)

            { return Json(new { success = false, messenge = ex.Message + " " + ex.StackTrace   }); }
        }

        public ActionResult LoadDataDoanhThu()
        {
            if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
            {
                DateTime now = DateTime.Now.Date;
                DateTime startDate = new DateTime(now.Year, now.Month, now.Day);
                DateTime endDate = startDate.AddHours(23).AddMinutes(59).AddSeconds(59);
                DataTable dt = cls_SALES_REPORT.mSales_SumStores(startDate, endDate);
                List<object> lstResult = new List<object>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        lstResult.Add(new {
                            PHARMACY_ID = (Guid)(item["KEY_CHI_NHANH"]),
                            PHARMACY_NAME = item["TEN_CHI_NHANH"],
                            DOANH_THU = item["TIEN_VIET_NAM"],
                            LOI_NHUAN = item["TIEN_VIET_LAI"]
                        });
                    }
                }
                return Json(new { success = true, result = lstResult }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }


        [System.Web.Mvc.HttpPost]
        public ActionResult LoadDataDoanhThuTime(string TypeView, DateTime? startDate, DateTime? endDate)
        {
            if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
            {
                DateTime now = DateTime.Now.Date;
                DateTime dateStart = DateTime.Now;
                DateTime dateEnd = DateTime.Now;
                string FilterStr = "Hôm nay";
                switch (TypeView)
                {
                    case "today":
                        dateStart = now;
                        dateEnd = now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                        FilterStr = "Hôm nay";
                        break;
                    case "thismonth":
                        dateStart = new DateTime(now.Year, now.Month, 1);
                        dateEnd = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month), 23, 59, 59);
                        FilterStr = "Tháng này";
                        break;
                    case "thisyear":
                        dateStart = new DateTime(now.Year, 1, 1);
                        dateEnd = new DateTime(now.Year, 12, DateTime.DaysInMonth(now.Year, 12), 23, 59, 59);
                        FilterStr = "Năm này";
                        break;
                    case "option":
                        if (startDate != null && endDate != null && startDate.Value <= endDate.Value)
                        {
                            dateStart = startDate.Value;
                            dateEnd = endDate.Value.AddHours(23).AddMinutes(59).AddSeconds(59);
                            FilterStr = "Tuỳ chọn";
                            break;
                        }
                        else return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    default:
                        return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }

                DataTable dt = cls_SALES_REPORT.mSales_SumStores(dateStart, dateEnd);
                List<object> lstResult = new List<object>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        lstResult.Add(new
                        {
                            PHARMACY_ID = (Guid)(item["KEY_CHI_NHANH"]),
                            PHARMACY_NAME = item["TEN_CHI_NHANH"],
                            DOANH_THU = item["TIEN_VIET_NAM"],
                            LOI_NHUAN = item["TIEN_VIET_LAI"]
                        });
                    }
                }
                return Json(new { success = true,
                    FILTER_STR = FilterStr,
                    result = lstResult }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult LoadDieuHangData()
        {
            if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
            {
                DateTime startDate = DateTime.Now.Date;
                DateTime endDate = DateTime.Now.Date;
                DataTable dt = cls_STORE_REPORT.mStore_SumStores(startDate, endDate);
                List<object> lstResult = new List<object>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        lstResult.Add(new { ID_MAT_HANG = item["MA_MAT_HANG"], TEN_MAT_HANG = item["TEN_MAT_HANG"], SO_LUONG = item["SO_LUONG_HANG"] });
                    }
                }
                return Json(new { success = true, result = lstResult }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            
           
        }

        public ActionResult LoadDieuHangDataPharmacy()
        {
            if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
            {
                DateTime startDate = DateTime.Now.Date;
                DateTime endDate = DateTime.Now.Date;
                DataTable dt = cls_STORE_REPORT.mStore_OneStores(startDate, endDate);
                List<object> lstResult = new List<object>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    Guid CurentChiNhanh = (Guid)dt.Rows[0]["KEY_CHI_NHANH"];
                    string TenChiNhanh = dt.Rows[0]["TEN_CHI_NHANH"] as string;
                    decimal DataSumChiNhanh = 0;
                    List<object> lstProduct = new List<object>();
                    for (int i =0; i <  dt.Rows.Count; i++)
                    {
                        DataRow item = dt.Rows[i];
                        if ((Guid)item["KEY_CHI_NHANH"] == CurentChiNhanh)
                        {
                            lstProduct.Add(new { KEY_DIEU_HANG = item["KEY_DIEU_HANG"], TEN_MAT_HANG = item["TEN_MAT_HANG"], SO_LUONG = item["SO_LUONG_HANG"] });
                            decimal ValAdd = 0;
                            bool rsParse = decimal.TryParse(item["SO_LUONG_HANG"].ToString(), out ValAdd);
                            if (rsParse) DataSumChiNhanh += ValAdd;

                            if (i == dt.Rows.Count - 1) lstResult.Add(new { PHARMACY_ID = CurentChiNhanh, PHARMACY_NAME = TenChiNhanh, PHARMACY_DATA = lstProduct, DATA_SUM = DataSumChiNhanh });
                        }
                        else
                        {
                            lstResult.Add(new { PHARMACY_ID = CurentChiNhanh, PHARMACY_NAME = TenChiNhanh, PHARMACY_DATA = lstProduct, DATA_SUM = DataSumChiNhanh });

                            lstProduct = new List<object>();
                            CurentChiNhanh = (Guid)item["KEY_CHI_NHANH"];
                            TenChiNhanh = item["TEN_CHI_NHANH"] as string;
                            DataSumChiNhanh = 0;

                            lstProduct.Add(new { KEY_DIEU_HANG = item["KEY_DIEU_HANG"], TEN_MAT_HANG = item["TEN_MAT_HANG"], SO_LUONG = item["SO_LUONG_HANG"] });
                            decimal ValAdd = 0;
                            bool rsParse = decimal.TryParse(item["SO_LUONG_HANG"].ToString(), out ValAdd);
                            if (rsParse) DataSumChiNhanh += ValAdd;

                            if(i == dt.Rows.Count-1) lstResult.Add(new { PHARMACY_ID = CurentChiNhanh, PHARMACY_NAME = TenChiNhanh, PHARMACY_DATA = lstProduct, DATA_SUM = DataSumChiNhanh });

                        }
                    }
                }
                return Json(new { success = true, result = lstResult }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult GrantProduct(string KEY_DIEU_HANG, int? NOI_GOI_HANG)
        {
            if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
            {
                int rs = cls_STORE_REPORT.mStore_PlaceCall(new Guid(KEY_DIEU_HANG), NOI_GOI_HANG.Value);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }


        [System.Web.Mvc.HttpPost]
        public ActionResult SubmitFilterDieuHang(string PharmacyID, string typeFilter, DateTime? txtpickDate, int? txtpickMonthYear_Month, int? txtpickMonthYear_Year, int? txtpickYear)
        {
            string filterStr = DateTime.Now.Date.ToString("dd-MM-yyyy");
            DateTime dateFilterStart = DateTime.Now;
            DateTime dateFilterEnd = DateTime.Now;
            DataTable dt = null;
            DataTable dtChart = null;
            if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
            {
                switch (typeFilter)
                {
                    //Lọc dữ liệu theo ngày
                    case "#txtpickDate":
                        DateTime dateFilter = DateTime.Now.Date;
                        filterStr = dateFilter.ToString("dd-MM-yyyy");
                        if (txtpickDate != null)
                        {
                            dateFilter = txtpickDate.Value;
                            filterStr = dateFilter.ToString("dd-MM-yyyy");

                            dt = cls_SALES_REPORT.mSales_OneStores(new Guid(PharmacyID), dateFilter, dateFilter.Date.AddHours(23).AddMinutes(59).AddSeconds(59));

                            DateTime dateStartChart = new DateTime(dateFilter.Year, 1, 1);
                            DateTime dateEndChart = new DateTime(dateFilter.Year, 12, 31);
                            dtChart = cls_SALES_REPORT.mSales_OneStoreChart(new Guid(PharmacyID), dateStartChart, dateEndChart.Date.AddHours(23).AddMinutes(59).AddSeconds(59));

                            break;
                        }
                        return Json(new { success = false });

                    //Lọc dữ liệu theo tháng
                    case "#txtpickMonthYear":
                        if (txtpickMonthYear_Month != null && txtpickMonthYear_Year != null)
                        {
                            filterStr = string.Format("{0:00}-{1}", txtpickMonthYear_Month.Value, txtpickMonthYear_Year.Value);
                            DateTime dateStart = new DateTime(txtpickMonthYear_Year.Value, txtpickMonthYear_Month.Value, 1);
                            DateTime dateEnd = new DateTime(txtpickMonthYear_Year.Value, txtpickMonthYear_Month.Value, DateTime.DaysInMonth(txtpickMonthYear_Year.Value, txtpickMonthYear_Month.Value));
                            dt = cls_SALES_REPORT.mSales_OneStores(new Guid(PharmacyID), dateStart, dateEnd.Date.AddHours(23).AddMinutes(59).AddSeconds(59));


                            DateTime dateStartChart = new DateTime(txtpickMonthYear_Year.Value, 1, 1);
                            DateTime dateEndChart = new DateTime(txtpickMonthYear_Year.Value, 12, 31);
                            dtChart = cls_SALES_REPORT.mSales_OneStoreChart(new Guid(PharmacyID), dateStartChart, dateEndChart.Date.AddHours(23).AddMinutes(59).AddSeconds(59));
                            
                            break;
                        }
                        return Json(new { success = false });

                    //Lọc dữ liệu theo năm
                    case "#txtpickYear":
                        if (txtpickYear != null)
                        {
                            filterStr = string.Format("{0}", txtpickYear.Value);
                            DateTime dateStart = new DateTime(txtpickYear.Value, 1, 1);
                            DateTime dateEnd = new DateTime(txtpickYear.Value, 12, DateTime.DaysInMonth(txtpickYear.Value, 12));
                            dt = cls_SALES_REPORT.mSales_OneStores(new Guid(PharmacyID), dateStart, dateEnd.Date.AddHours(23).AddMinutes(59).AddSeconds(59));

                            DateTime dateStartChart = new DateTime(txtpickYear.Value, 1, 1);
                            DateTime dateEndChart = new DateTime(txtpickYear.Value, 12, 31);
                            dtChart = cls_SALES_REPORT.mSales_OneStoreChart(new Guid(PharmacyID), dateStartChart, dateEndChart.Date.AddHours(23).AddMinutes(59).AddSeconds(59));
                            
                            break;
                        }
                        return Json(new { success = false });

                    default:
                        return Json(new { success = false });
                }


                if(dt != null)
                {
                    string[] lstLabel = new string[12];
                    decimal[] lstDataDoanhSo = new decimal[12];
                    decimal[] lstDataLoiNhuan = new decimal[12];
                    decimal[] lstDataSanLuongNHap = new decimal[12];
                    List<object> lstDataSet = new List<object>();

                    if (dtChart != null)
                    {
                        Dictionary<int, dynamic> dictData = new Dictionary<int, dynamic>();
                        foreach (DataRow item in dtChart.Rows)
                        {
                            dictData.Add(int.Parse(item["MON_SALE_VIEW"].ToString()),
                                new
                                {
                                    TIEN_VIET_BAN = decimal.Parse(item["TIEN_VIET_BAN"].ToString()),
                                    TIEN_VIET_TRA = decimal.Parse(item["TIEN_VIET_TRA"].ToString()),
                                    TIEN_VIET_MUA = decimal.Parse(item["TIEN_VIET_MUA"].ToString())
                                });
                        }

                        for (int i = 1; i <= 12; i++)
                        {
                            lstLabel[i - 1] = string.Format("Tháng {0:00}", i);
                            if (dictData.ContainsKey(i))
                            {
                                lstDataDoanhSo[i - 1] = dictData[i].TIEN_VIET_BAN;
                                lstDataLoiNhuan[i - 1] = dictData[i].TIEN_VIET_TRA;
                                lstDataSanLuongNHap[i - 1] = dictData[i].TIEN_VIET_MUA;
                            }
                            else
                            {
                                lstDataDoanhSo[i - 1] = 0;
                                lstDataLoiNhuan[i - 1] = 0;
                                lstDataSanLuongNHap[i - 1] = 0;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= 12; i++)
                        {
                            lstLabel[i - 1] = string.Format("Tháng {0:00}", i);
                            lstDataDoanhSo[i - 1] = 0;
                            lstDataLoiNhuan[i - 1] = 0;
                            lstDataSanLuongNHap[i - 1] = 0;
                        }
                    }

                    if (dt.Rows.Count > 0)
                    {
                        DataRow item = dt.Rows[0];
                        return Json(new
                        {
                            success = true,
                            result = new
                            {
                                filterStrDisplay = filterStr,
                                PHARMACY_NAME = "PHARMACY ",
                                DOANH_SO_BAN = item["TIEN_VIET_NAM"],
                                KHACH_TRA_LAI = item["TIEN_VIET_TRA"],
                                LOI_NHUAN_GOP = item["TIEN_VIET_LAI"],
                                SAN_LUONG_NHAP = item["TIEN_VIET_MUA"],
                                TIEN_TON_KHO = item["TIEN_VIET_KHO"],
                                CHART_LABEL = lstLabel,
                                CHART_DATA = new object[]
                                {
                                    new { DATA = lstDataSanLuongNHap, COLOR = "#fed1d1", LABEL = "SẢN LƯỢNG MUA" },
                                    new { DATA = lstDataDoanhSo, COLOR = "#5cb85c", LABEL = "DOANH SỐ BÁN" },
                                    new { DATA = lstDataLoiNhuan, COLOR = "#f00", LABEL = "KHÁCH TRẢ LẠI" }
                                }
                            }
                        });
                    }
                }
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadDataMatHang(string query)
        {
            if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
            {
                if (string.IsNullOrEmpty(query)) query = "";
                DataTable dt = cls_GOODS_QUERY.mGoods_FilterGoods(query);
                List<object> lstResult = new List<object>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        lstResult.Add(new { ID_MAT_HANG = (Guid)(item["KEY_MAT_HANG"]), TEN_MAT_HANG = item["TEN_MAT_HANG"], GIA_BAN_LE = item["GIA_BAN_LE"] });
                    }
                }
                return Json(new { success = true, result = lstResult }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult LoadDataMatHangTheoHeSo(decimal? hsoDuoi, decimal? hsoTren)
        {
            if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
            {
                if (hsoDuoi != null && hsoTren != null)
                {
                    DataTable dt = cls_GOODS_QUERY.mGoods_FilterGoods(hsoDuoi.Value, hsoTren.Value);
                    List<object> lstResult = new List<object>();
                    if (dt != null)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            lstResult.Add(new { ID_MAT_HANG = (Guid)(item["KEY_MAT_HANG"]), TEN_MAT_HANG = item["TEN_MAT_HANG"], GIA_BAN_LE = item["GIA_BAN_LE"] });
                        }
                    }
                    return Json(new { success = true, result = lstResult }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadDataMatHangAmLai()
        {
            if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
            {
                DataTable dt = cls_GOODS_QUERY.mGoods_FilterBelow();
                List<object> lstResult = new List<object>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        lstResult.Add(new { ID_MAT_HANG = (Guid)(item["KEY_MAT_HANG"]), TEN_MAT_HANG = item["TEN_MAT_HANG"], GIA_BAN_LE = item["GIA_BAN_LE"] });
                    }
                }
                return Json(new { success = true, result = lstResult }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadDataMatHangNoPrice()
        {
            if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
            {
                DataTable dt = cls_GOODS_QUERY.mGoods_FilterZeros();
                List<object> lstResult = new List<object>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        lstResult.Add(new { ID_MAT_HANG = (Guid)(item["KEY_MAT_HANG"]), TEN_MAT_HANG = item["TEN_MAT_HANG"], GIA_BAN_LE = item["GIA_BAN_LE"] });
                    }
                }
                return Json(new { success = true, result = lstResult }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadDataMatHangHoaVon()
        {
            if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
            {
                DataTable dt = cls_GOODS_QUERY.mGoods_FilterEqual();
                List<object> lstResult = new List<object>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        lstResult.Add(new { ID_MAT_HANG = (Guid)(item["KEY_MAT_HANG"]), TEN_MAT_HANG = item["TEN_MAT_HANG"], GIA_BAN_LE = item["GIA_BAN_LE"] });
                    }
                }
                return Json(new { success = true, result = lstResult }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadDataChiTietMatHang(string id)
        {
            if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
            {
                DataTable dt = cls_GOODS_QUERY.mGoods_GetoneGoods(new Guid(id));
                List<object> lstResult = new List<object>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow item = dt.Rows[0];
                    object result = new
                    {
                        ID_MAT_HANG = (Guid)(item["KEY_MAT_HANG"]),
                        TEN_MAT_HANG = item["TEN_MAT_HANG"],
                        DON_VI_TINH = item["DON_VI_TINH"],
                        GIA_BAN_LE = item["GIA_BAN_LE"],
                        DON_GIA_MUA = item["DON_GIA_MUA"],
                        TIEN_LAI_GOP = item["TIEN_LAI_GOP"],
                        GIAM_GIA_MUA_SI = item["TILE_GIAM_GIA"],
                        CHIET_KHAU_MO_GIOI = item["TILE_MOI_GIOI"],
                        HE_SO_TICH_DIEM = item["TILE_GHI_DIEM"]
                    };

                    return Json(new { success = true, result = result }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult LoadDataLichSuGia(string id)
        {
            if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
            {
                DataTable dt = cls_GOODS_QUERY.mGoods_HistoryGoods(new Guid(id));
                List<object> lstResult = new List<object>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        lstResult.Add(new {
                            ID_MAT_HANG = (Guid)(item["KEY_MAT_HANG"]),
                            NGAY_CHINH_GIA = ((DateTime)item["DAY_CHINH_GIA"]).ToString("dd-MM-yyyy"),
                            GIA_BAN_LE = item["GIA_BAN_LE"],
                            GIA_BAN_SI = item["GIA_BAN_SI"],
                            TEN_NGUOI_DUNG = item["TEN_NGUOI_DUNG"]
                        });
                    }
                }
                return Json(new { success = true, result = lstResult }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadDataMuaGanDay(string id)
        {
            if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
            {
                DataTable dt = cls_GOODS_QUERY.mGoods_PurchaseGoods(new Guid(id));
                List<object> lstResult = new List<object>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        lstResult.Add(new {
                            NGAY_MUA = ((DateTime)item["NGAY_NHAP_XUAT"]).ToString("dd-MM-yyyy"),
                            SO_LUONG = item["SO_LUONG_HANG"],
                            GIA_MUA = item["GIA_SAU_THUE"],
                            LOAI_NHAP_XUAT = item["LOAI_NHAP_XUAT"]
                        });
                    }
                }
                return Json(new { success = true, result = lstResult }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult CreateNewPrice(string id, decimal? NewPrice)
        {
            if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
            {
                if(NewPrice != null)
                {
                    int rs = cls_GOODS_QUERY.mGoods_UpdateGia(new Guid(id), NewPrice.Value);
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false } );
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult LoadDataThongKe(string TypeView, DateTime? startDate, DateTime? endDate)
        {
            if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
            {
                List<object> lstResult = new List<object>();
                DateTime now = DateTime.Now.Date;
                DateTime dateStart = DateTime.Now;
                DateTime dateEnd = DateTime.Now;
                string FilterStr = "Hôm nay";
                switch (TypeView)
                {
                    case "today":
                        dateStart = now.Date;
                        dateEnd = now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                        FilterStr = "Hôm nay";
                        break;
                    case "thismonth":
                        dateStart = new DateTime(now.Year, now.Month, 1);
                        dateEnd = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month), 23, 59, 59);
                        FilterStr = "Tháng này";
                        break;
                    case "thisyear":
                        dateStart = new DateTime(now.Year, 1, 1);
                        dateEnd = new DateTime(now.Year, 12, DateTime.DaysInMonth(now.Year, 12), 23, 59, 59);
                        FilterStr = "Năm này";
                        break;
                    case "option":
                        if(startDate != null && endDate != null && startDate.Value <= endDate.Value)
                        {
                            dateStart = startDate.Value;
                            dateEnd = endDate.Value.AddHours(23).AddMinutes(59).AddSeconds(59);
                            FilterStr = "Tuỳ chọn";
                            break;
                        }
                        else return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    default:
                        return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }

                DataTable dt = cls_SALES_REPORT.mSales_BestSales(dateStart, dateEnd);
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        lstResult.Add(new
                        {
                            ID_MAT_HANG = (Guid)item["KEY_MAT_HANG"],
                            TEN_MAT_HANG = item["TEN_MAT_HANG"],
                            SO_LUONG_HANG = item["SO_LUONG_HANG"],
                            DOANH_THU = item["TIEN_VIET_NAM"],
                            DATE_START = dateStart.ToString("dd-MM-yyyy"),
                            DATE_END = dateEnd.ToString("dd-MM-yyyy")
                        });
                    }
                }


                string TitleChart = "";
                if (dateStart.Date.CompareTo(dateEnd.Date) == 0) TitleChart = string.Format("Thống kê doanh thu ngày {0}", dateStart.ToString("dd-MM-yyyy"));
                else TitleChart = string.Format("Thống kê doanh thu từ ngày {0} đến {1}", dateStart.ToString("dd-MM-yyyy"), dateEnd.ToString("dd-MM-yyyy"));

                DataTable dtChart = cls_SALES_REPORT.mSales_StoreChart(dateStart, dateEnd);
                if (dtChart != null)
                {
                    string[] lstLabel = new string[dtChart.Rows.Count];
                    decimal[] lstDataBan = new decimal[dtChart.Rows.Count];
                    decimal[] lstDataTra = new decimal[dtChart.Rows.Count];
                    decimal[] lstDataMua = new decimal[dtChart.Rows.Count];

                    for (int i = 0; i < dtChart.Rows.Count; i++)
                    {
                        DataRow item = dtChart.Rows[i];
                        lstLabel[i] = item["MA_CHI_NHANH"].ToString();
                        lstDataBan[i] = (decimal)item["TIEN_VIET_BAN"];
                        lstDataTra[i] = (decimal)item["TIEN_VIET_TRA"];
                        lstDataMua[i] = (decimal)item["TIEN_VIET_MUA"];
                    }


                    return Json(new
                    {
                        success = true,
                        result = new
                        {
                            TITLE_CHART = TitleChart,
                            FILTER_STR = FilterStr,
                            CHART_LABEL = lstLabel,
                            CHART_DATASET = new object[]
                            {
                                new { DATA = lstDataMua, COLOR = "#fed1d1", LABEL = "SẢN LƯỢNG MUA" },
                                new { DATA = lstDataBan, COLOR = "#5cb85c", LABEL = "DOANH SỐ BÁN" },
                                new { DATA = lstDataTra, COLOR = "#f00", LABEL = "KHÁCH TRẢ LẠI" }
                            },
                            DOANH_THU_DATA = lstResult
                        }
                    }, JsonRequestBehavior.AllowGet);

                }

                return Json(new
                {
                    success = true,
                    result = new
                    {
                        TITLE_CHART = TitleChart,
                        FILTER_STR = FilterStr,
                        CHART_LABEL = new string[] { "" },
                        CHART_DATASET = new object[]
                            {
                                new { DATA = new decimal[]{ 0 }, COLOR = "#fed1d1", LABEL = "SẢN LƯỢNG MUA" },
                                new { DATA = new decimal[]{ 0 }, COLOR = "#5cb85c", LABEL = "DOANH SỐ BÁN" },
                                new { DATA = new decimal[]{ 0 }, COLOR = "#f00", LABEL = "KHÁCH TRẢ LẠI" }
                            },
                        DOANH_THU_DATA = lstResult
                    }
                }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }


        [System.Web.Mvc.HttpPost]
        public ActionResult LoadDataBestSale(string id, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                if (id == null) return Json(new { success = false, reason = "id" }, JsonRequestBehavior.AllowGet);
                if (startDate == null || startDate == null) return Json(new { success = false, reason = "date" }, JsonRequestBehavior.AllowGet);
                if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
                {
                    DataTable dt = cls_SALES_REPORT.mSales_BestGoods(new Guid(id), startDate.Value.Date, endDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59));
                    List<object> lstResult = new List<object>();
                    if (dt != null)
                    {
                        string TEN_MAT_HANG = "--------------";
                        decimal TONG_DOANH_THU = 0;
                        decimal SumAdd = 0;
                        foreach (DataRow item in dt.Rows)
                        {
                            TEN_MAT_HANG = item["TEN_MAT_HANG"] as string;
                            lstResult.Add(new
                            {
                                PHARMACY_NAME = item["TEN_CHI_NHANH"],
                                SO_LUONG = item["SO_LUONG_HANG"],
                                DOANH_THU = item["TIEN_VIET_NAM"]
                            });
                            SumAdd = 0;
                            bool rsParse = decimal.TryParse(item["TIEN_VIET_NAM"].ToString(), out SumAdd);
                            if (rsParse) TONG_DOANH_THU += SumAdd;
                        }

                        return Json(new { success = true, result = new { TEN_MAT_HANG = TEN_MAT_HANG, TONG_DOANH_THU = TONG_DOANH_THU, PHARMACY_DATA = lstResult } }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { success = false, reason = "connection" });
            }
            catch (Exception ex)
            {
                return Content(ex.Message + ex.StackTrace);
            }
        }

        private List<Dictionary<string, object>> DataTableToJSONWithJSONNet(DataTable table, string[] columname)
        {
            List<Dictionary<string, object>> lstResult = new List<Dictionary<string, object>>();
            if(table == null) return lstResult;
            foreach (DataRow item in table.Rows)
            {
                Dictionary<string, object> obj = new Dictionary<string, object>();
                for (int i = 0; i < columname.Length; i++)
                {
                    obj.Add(columname[i], item[columname[i]]);
                }
                lstResult.Add(obj);
            }
            return lstResult;
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Loaddataaddmathang()
        {
            try
            {
                DataTable dtHangHang = cls_GOODS_QUERY.HangHang_Lookup();
                DataTable dtLoaiHang = cls_GOODS_QUERY.LoaiHang_Lookup();
                DataTable dtKieuHang = cls_GOODS_QUERY.TypeHang_Lookup();
                DataTable dtNhomHang = cls_GOODS_QUERY.NhomHang_Lookup();
                DataTable dtXuatXu = cls_GOODS_QUERY.NhaNuoc_Lookup();

                List<Dictionary<string, object>> lstHangHang = DataTableToJSONWithJSONNet(dtHangHang, new string[] { "KEY_HANG_HANG", "TEN_HANG_HANG", "MA_HANG_HANG" });
                List<Dictionary<string, object>> lstLoaiHang = DataTableToJSONWithJSONNet(dtLoaiHang, new string[] { "KEY_LOAI_HANG", "TEN_LOAI_HANG", "MA_LOAI_HANG" });
                List<Dictionary<string, object>> lstKieuHang = DataTableToJSONWithJSONNet(dtKieuHang, new string[] { "KEY_TYPE_HANG", "TEN_TYPE_HANG", "MA_TYPE_HANG" });
                List<Dictionary<string, object>> lstNhomHang = DataTableToJSONWithJSONNet(dtNhomHang, new string[] { "KEY_NHOM_HANG", "TEN_NHOM_HANG", "MA_NHOM_HANG" });
                List<Dictionary<string, object>> lstXuatXu = DataTableToJSONWithJSONNet(dtXuatXu, new string[] { "KEY_NHA_NUOC", "TEN_NHA_NUOC", "MA_NHA_NUOC" });

                return Json(new { success = true, result = new {
                    lstHangHang = lstHangHang, lstLoaiHang = lstLoaiHang,
                    lstKieuHang = lstKieuHang, lstNhomHang = lstNhomHang,
                    lstXuatXu = lstXuatXu
                } }, JsonRequestBehavior.AllowGet);
            } catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message, st = ex.StackTrace, JsonRequestBehavior.AllowGet });
            }
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult SaveMatHang()
        {
            MAT_HANG item = new MAT_HANG();
            if(item.ParseValue(Request.Form))
            {
                List<string> Errors = item.CheckValid();
                if(Errors.Count == 0)
                {
                    int result = cls_GOODS_QUERY.mGoods_InsertNew(item.loaihang.Value, item.kieuhang.Value, item.nhomhang.Value, item.xuatxu.Value, item.hanghang.Value,
                                                    Guid.NewGuid(), item.mamathang, item.masanxuat, item.tenmathang, item.donvitinh, item.donvitinhlon,
                                                    item.soquydoi.Value, item.tilegiamgia.Value, item.hesothue.Value, item.tiletienlai.Value, item.hangkedon.Value, item.hangdacbiet.Value,
                                                    item.hangkigui.Value, item.dongiamua.Value, item.giabanle.Value, item.giabansi.Value, item.ghichu, item.hamluong,
                                                    item.hoatchat, item.tontoithieu.Value, item.tilemogioi.Value, item.tileghidiem.Value, item.cosudung.Value, DateTime.Today, 1, true);
                    if (result == 0) return Json(new { success = true });
                    else return Json(new { success = false, msg = "Đã có lỗi xảy ra (SAVE_FAIL). Vui lòng kiểm tra dữ liệu và thử lại." });
                }
                else
                {
                    return Json(new { success = false, errors = Errors });
                }
            }
            else return Json(new { success = false, msg = "Đã có lỗi xảy ra (DATA_FAIL). Vui lòng kiểm tra dữ liệu và thử lại." });
        }


        public ActionResult LoadDataKhachHang(string query)
        {
            if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
            {
                if (string.IsNullOrEmpty(query)) query = "";
                DataTable dt = cls_CARDS_CLIENT.mClient_FilterClient(query);
                List<object> lstResult = new List<object>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        lstResult.Add(new { COD_CARD_CLIENT = (item["COD_CARD_CLIENT"]), TEN_CARD_CLIENT = item["TEN_CARD_CLIENT"], TEL_CARD_CLIENT = item["TEL_CARD_CLIENT"]
                                            ,ADD_CARD_CLIENT = item["ADD_CARD_CLIENT"]
                        });
                    }
                }
                return Json(new { success = true, result = lstResult }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult SaveKhachHang()
        {

            KHACH_HANG item = new KHACH_HANG();
            if (item.ParseValue(Request.Form))
            {
                List<string> Errors = item.CheckValid();
                if (Errors.Count == 0)
                {
                    int result = cls_CARDS_CLIENT.mClient_InsertNew(Guid.NewGuid(), item.code, item.hoten, item.gioitinh, item.ngaysinh, item.dienthoai, item.email,
                                                                    item.diachi, item.congty, 0, 0, 0, DateTime.Now, 1, true);
                    if (result == 0) return Json(new { success = true });
                    else return Json(new { success = false, msg = "Đã có lỗi xảy ra (SAVE_FAIL). Vui lòng kiểm tra dữ liệu và thử lại." });
                }
                else
                {
                    return Json(new { success = false, errors = Errors });
                }
            }
            else return Json(new { success = false, msg = "Đã có lỗi xảy ra (DATA_FAIL). Vui lòng kiểm tra dữ liệu và thử lại." });
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
                if (!string.IsNullOrEmpty(ngaysinh)) this.ngaysinh = DateTime.Parse(ngaysinh);
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
            if (gioitinh == null) lstError.Add("Giới tính");
            if (dienthoai == null) lstError.Add("Số điệnt thoại");
            if (email == null) lstError.Add("Email");
            if (diachi == null) lstError.Add("Địa chỉ");
            if (congty == null) lstError.Add("Công ty");

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
