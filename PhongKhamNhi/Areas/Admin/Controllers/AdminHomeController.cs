using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.DTO;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PhongKhamNhi.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        // GET: Admin/AdminHome
        public ActionResult Index(int? cn, int? year, int? month, int? loai)
        {
            if (cn == null)
                cn = 0;
            if (loai == null)
                loai = 0;
            if (year == null)
                year = DateTime.Now.Year;
            if (month == null)
                month = DateTime.Now.Month;
            DoanhThuDAO dao = new DoanhThuDAO();
            List<StatisticsDTO> lst = dao.StatisticsByMonth(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(cn), Convert.ToInt32(loai));
            List<int> ngay = new List<int>();
            List<double> doanhThu = new List<double>();
            foreach (var item in lst)
            {
                ngay.Add(item.Ngay);
                doanhThu.Add(item.DoanhThu);
            }
            ViewBag.cnSelected = cn;
            ViewBag.days = ngay;
            ViewBag.loai = loai;
            ViewBag.revenues = doanhThu;
            ViewBag.year = dao.GetYearOrder();
            ViewBag.yearSelected = year;
            ViewBag.month = month;
            ViewBag.total = dao.GetTotal(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(cn), Convert.ToInt32(loai));
            return View(new ChiNhanhDAO().ListAllChiNhanh());
        }

        public ActionResult ThuocBan(int? cn, int? year, int? month, int? loai)
        {
            if (loai == null)
                loai = 0;
            if (cn == null)
                cn = 0;
            if (year == null)
                year = DateTime.Now.Year;
            if (month == null)
                month = DateTime.Now.Month;
            DoanhThuDAO dao = new DoanhThuDAO();
            ViewBag.cnSelected = cn;
            ViewBag.year = dao.GetYearOrder();
            ViewBag.yearSelected = year;
            ViewBag.loai = loai;
            ViewBag.month = month;
            ViewBag.lstCn = new ChiNhanhDAO().ListAllChiNhanh();
            ViewBag.total = dao.DoanhThuThuocBan(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(cn), Convert.ToInt32(loai));
            return View(new ThuocDAO().ThongKeThuocBan(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(cn), Convert.ToInt32(loai)));
        }

        public ActionResult PrintMedicine(int? cn, int? year, int? month, int? loai)
        {
            return new ActionAsPdf("InMedicine", new { cn = cn, year = year, month = month, loai = loai });
        }
        public ActionResult InMedicine(int? cn, int? year, int? month, int? loai)
        {
            List<ThuocBanDTO> lst = new ThuocDAO().ThongKeThuocBan2(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(cn), Convert.ToInt32(loai));
            double tong = 0;
            foreach (ThuocBanDTO item in lst)
            {
                tong += item.ThanhTien;
            }
            ViewBag.ngay = DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.Nam = year;
            ViewBag.Thang = month;
            if (cn == 0)
                ViewBag.Cn = "Toàn bộ";
            else
                ViewBag.Cn = new ChiNhanhDAO().FindByID(cn.Value).TenChiNhanh;
            ViewBag.Tong = tong;
            return View(lst);
        }

        public ActionResult PrintDv(int? cn, int? year, int? month)
        {
            return new ActionAsPdf("InDv", new { cn = cn, year = year, month = month });
        }
        public ActionResult InDv(int? cn, int? year, int? month)
        {
            List<ThongKeDichVuDTO> lst = new DoanhThuDAO().ThongKeDichVu(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(cn));
            double tong = 0;
            foreach (ThongKeDichVuDTO item in lst)
            {
                tong += item.ThanhTien;
            }
            ViewBag.ngay = DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.Nam = year;
            ViewBag.Thang = month;
            if (cn == 0)
                ViewBag.Cn = "Toàn bộ";
            else
                ViewBag.Cn = new ChiNhanhDAO().FindByID(cn.Value).TenChiNhanh;
            ViewBag.Tong = tong;
            return View(lst);
        }
    }
}