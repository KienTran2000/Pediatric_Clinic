using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.DTO;
using PhongKhamNhi.Models.Entities;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PhongKhamNhi.Areas.NvXn.Controllers
{
    public class NvXnHomeController : Controller
    {
        // GET: NvXn/NvXnHome
        public ActionResult Index(string bn, string tu, string den, string trangThai, int pageNum = 1, int pageSize = 9)
        {
            ViewBag.bn = bn;
            ViewBag.tu = tu;
            ViewBag.den = den;
            ViewBag.trangThai = trangThai;
            if (tu == null)
                tu = "2020-11-19 12:00:00";
            if (den == null)
                den = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            NhanVien nv = (NhanVien)Session["user"];
            return View(new PhieuDkXnDAO().lstPhieuDkXn2(nv.MaChiNhanh, bn, tu, den, trangThai, pageNum, pageSize));
        }

        public ActionResult Edit(int id)
        {
            PhieuDKXN x = new PhieuDkXnDAO().FindByID(id);
            PhieuKhamBenh p = new PhieuKhamBenhDAO().FindByID(x.MaPhieuKB);
            ViewBag.bn = p.BenhNhi.HoTen;
            ViewBag.id = id;
            return PartialView(new PhieuDkXnDAO().ListKqXn(id));
        }
        [HttpPost]
        public ActionResult Edit(FormCollection f)
        {
            int id = int.Parse(f["idP"]);
            string kq = f["kq"];
            string[] arr = kq.Split(',');
            List<KetQuaXN> lst = new KqXnDAO().ListKqXn(id);
            KqXnDAO dao = new KqXnDAO();
            for(int i=0; i<lst.Count; i++)
            {
                lst[i].KetQua = arr[i];
                dao.Update(lst[i]);
            }
            new PhieuDkXnDAO().UpdateNvXn(id);
            return RedirectToAction("Index", "NvXnHome");
        }

        public ActionResult Print(int id)
        {
            return new ActionAsPdf("In", new { id = id });
        }
        public ActionResult In(int id)
        {
            PhieuDkXnDAO dao = new PhieuDkXnDAO();
            List<KqXnDTO> lst = dao.ListKqXn(id);
            PhieuDKXN x = dao.FindByID(id);
            ViewBag.PhieuDkXn = x;
            ViewBag.ngay = x.ThoiGianLap.ToString("dd/MM/yyyy");
            PhieuKhamBenh p = new PhieuKhamBenhDAO().FindByID(x.MaPhieuKB);
            ViewBag.Pk = p.MaPhieuKB;
            ViewBag.cn = p.ChiNhanh.DiaChi;
            ViewBag.BenhNhi = p.BenhNhi;
            return View(lst);
        }

        public ActionResult SlPhieuDkXn()
        {
            NhanVien nv = (NhanVien)Session["user"];
            return PartialView(new PhieuDkXnDAO().SlPhieuDkXn(nv.MaChiNhanh));
        }
    }
}