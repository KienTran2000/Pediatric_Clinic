using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhongKhamNhi.Areas.LeTan.Controllers
{
    public class BenhNhiController : Controller
    {
        // GET: LeTan/BenhNhi
        public ActionResult Index(string ten, string ns, string sdt, string dc, int pageNum = 1, int pageSize = 9)
        {
            ViewBag.ten = ten;
            ViewBag.ns = ns;
            ViewBag.sdt = sdt;
            ViewBag.dc = dc;
            return View(new BenhNhiDAO().lstBn(ten, ns, sdt, dc, pageNum, pageSize));
        }
        [HttpPost]
        public ActionResult Index(FormCollection data, int pageNum = 1, int pageSize = 9)
        {
            BenhNhiDAO dao = new BenhNhiDAO();
            if (data.Count > 0)
            {
                string[] ids = data["checkBoxId"].Split(new char[] { ',' });
                foreach (string id in ids)
                {
                    dao.Delete(int.Parse(id));
                }
            }
            return View(dao.lstBn("", "", "", "", pageNum, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(BenhNhi b, string gt)
        {
            if (ModelState.IsValid)
            {
                b.GioiTinh = gt;
                new BenhNhiDAO().Insert(b);
                return RedirectToAction("Index", "BenhNhi");
            }
            return View(b);
        }

        public ActionResult Edit(int id)
        {
            BenhNhi b = new BenhNhiDAO().FindByID(id);
            return View(b);
        }
        [HttpPost]
        public ActionResult Edit(BenhNhi b, string gt)
        {
            if (ModelState.IsValid)
            {
                b.GioiTinh = gt;
                new BenhNhiDAO().Update(b);
                return RedirectToAction("Index", "BenhNhi");
            }
            return View(b);
        }

        public ActionResult History(int id)
        {
            ViewBag.ten = new BenhNhiDAO().FindByID(id).HoTen;
            return View(new PhieuKhamBenhDAO().lichSuKham(id, 1, 11));
        }

        public ActionResult Delete(int id)
        {
            new BenhNhiDAO().Delete(id);
            return RedirectToAction("Index", "BenhNhi");
        }
    }
}