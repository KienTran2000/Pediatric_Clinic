using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhongKhamNhi.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        // GET: Admin/AdminHome
        public ActionResult Index(int? cn, int? year, int? month)
        {
            if (cn == null)
                cn = 0;
            if (year == null)
                year = DateTime.Now.Year;
            if (month == null)
                month = DateTime.Now.Month;
            DoanhThuDAO dao = new DoanhThuDAO();
            List<StatisticsDTO> lst = dao.StatisticsByMonth(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(cn));
            List<int> ngay = new List<int>();
            List<double> doanhThu = new List<double>();
            foreach (var item in lst)
            {
                ngay.Add(item.Ngay);
                doanhThu.Add(item.DoanhThu);
            }
            ViewBag.cnSelected = cn;
            ViewBag.days = ngay;
            ViewBag.revenues = doanhThu;
            ViewBag.year = dao.GetYearOrder();
            ViewBag.yearSelected = year;
            ViewBag.month = month;
            ViewBag.total = dao.GetTotal(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(cn));
            return View(new ChiNhanhDAO().ListChiNhanh());
        }

        public ActionResult ThuocBan(int? cn, int? year, int? month)
        {
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
            ViewBag.month = month;
            ViewBag.lstCn = new ChiNhanhDAO().ListChiNhanh();
            ViewBag.total = dao.DoanhThuThuocBan(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(cn));
            return View(new ThuocDAO().ThongKeThuocBan(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(cn)));
        }
    }
}