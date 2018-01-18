using Sonetwsv.common;
using Sonetwsv.Mobilews;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading;
using System.Web.Mvc;

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
                    //if (Username != null && Password != null && Username == "admin" && Password == "admin")
                    if (Username != null && Password != null && cls_USERS_MOBILE.Login(Username, Password, out TenNguoiDung))
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


        [HttpPost]
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
                        if (startDate != null && endDate != null && startDate.Value < endDate.Value)
                        {
                            dateStart = startDate.Value;
                            dateEnd = endDate.Value;
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

        [HttpPost]
        public ActionResult GrantProduct(string KEY_DIEU_HANG, int? NOI_GOI_HANG)
        {
            if (clsConnect.DB_OpenConnection(DBHost, DBName, DBUser, DBPassword))
            {
                int rs = cls_STORE_REPORT.mStore_PlaceCall(new Guid(KEY_DIEU_HANG), NOI_GOI_HANG.Value);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }


        [HttpPost]
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

        [HttpPost]
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

        [HttpPost]
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

        [HttpPost]
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
                        if(startDate != null && endDate != null && startDate.Value < endDate.Value)
                        {
                            dateStart = startDate.Value;
                            dateEnd = endDate.Value;
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


        [HttpPost]
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



        [HttpPost]
        public ActionResult SaveMatHang()
        {            
            return Json(new { success = false });
        }

    }
}
