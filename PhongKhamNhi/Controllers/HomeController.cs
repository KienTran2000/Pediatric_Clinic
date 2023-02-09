using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhongKhamNhi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //List<ChiNhanh> lst = new ChiNhanhDAO().ListChiNhanh();
            //ViewBag.ListChiNhanh = lst;
            //ViewBag.ListBacSi = new BacSiDAO().GetListBacSiByMaCn(lst[0].MaChiNhanh);
            //ViewBag.tgHenMin = DateTime.Now.AddDays(1).ToString("yyyy-MM-ddTHH:mm");
            ViewBag.lstBs = new BacSiDAO().List5Bs();
            ViewBag.lstDv = new DichVuDAO().GetList3Dv();
            return View();
        }
        [HttpPost]
        public ActionResult Index(string ten, string cn, DateTime ns, string bs, string sdt, DateTime tgHen, string message)
        {
            PhieuDangKyKham p = new PhieuDangKyKham();
            p.ThoiGianDKK = DateTime.Now;
            p.HoTen = ten;
            p.MaChiNhanh = int.Parse(cn);
            p.NgaySinh = ns;
            if(bs != "")
                p.MaBS = int.Parse(bs);
            p.Sdt = sdt;
            p.ThoiGianHen = tgHen;
            p.LoiNhan = message;
            p.TrangThai = false;
            PhieuDangKyKhamDAO dao = new PhieuDangKyKhamDAO();
            dao.Insert(p);
            List<ChiNhanh> lst = new ChiNhanhDAO().ListChiNhanh();
            ViewBag.ListChiNhanh = lst;
            ViewBag.ListBacSi = new BacSiDAO().GetListBacSiByMaCn(lst[0].MaChiNhanh);
            return View();
        }

        public ActionResult LoadBacSi(int cn)
        {
            List<BacSi> lst = new BacSiDAO().GetListBacSiByMaCn(cn);
            return View(lst);
        }

        public ActionResult Doctors()
        {
            List<ChiNhanh> lst = new ChiNhanhDAO().ListChiNhanh();
            ViewBag.ListChiNhanh = lst;
            ViewBag.ListBacSi = new BacSiDAO().GetListBacSiByMaCn(lst[0].MaChiNhanh);
            return View(new BacSiDAO().GetListBs(1, 8));
        }

        public ActionResult News()
        {
            return View();
        }
        public ActionResult Service()
        {
            return View(new DichVuDAO().GetListDv());
        }
        public ActionResult ServiceDetail(int id)
        {
            return View(new DichVuDAO().FindByID(id));
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {

            return View(new ChiNhanhDAO().ListChiNhanh());
        }
    }
}