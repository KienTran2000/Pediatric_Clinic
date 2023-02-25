using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhongKhamNhi.Areas.NvXn.Controllers
{
    public class XetNghiemController : Controller
    {
        // GET: NvXn/XetNghiem
        public ActionResult Index(string ten, string dv, int pageNum = 1, int pageSize = 9)
        {
            if (dv == null)
                dv = "0";
            ViewBag.ten = ten;
            ViewBag.dv = int.Parse(dv);
            ViewBag.ListDichVu = new DichVuDAO().GetListDv();
            return View(new XetNghiemDAO().lstXn(ten, int.Parse(dv), pageNum, pageSize));
        }
        [HttpPost]
        public ActionResult Index(FormCollection data, int pageNum = 1, int pageSize = 9)
        {
            XetNghiemDAO dao = new XetNghiemDAO();
            if (data.Count > 0)
            {
                string[] ids = data["checkBoxId"].Split(new char[] { ',' });
                foreach (string id in ids)
                {
                    dao.Delete(int.Parse(id));
                }
            }
            ViewBag.ListDichVu = new DichVuDAO().GetListDv();
            return View(dao.lstXn("", 0, pageNum, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(XetNghiem xn)
        {
            if (ModelState.IsValid)
            {
                new XetNghiemDAO().Insert(xn);
                return RedirectToAction("Index", "AdminTest");
            }
            return View(xn);
        }

        public ActionResult Edit(int id)
        {
            return View(new XetNghiemDAO().FindByID(id));
        }
        [HttpPost]
        public ActionResult Edit(XetNghiem xn)
        {
            if (ModelState.IsValid)
            {
                new XetNghiemDAO().Update(xn);
                return RedirectToAction("Index", "AdminTest");
            }
            return View(xn);
        }

        public ActionResult Delete(int id)
        {
            new XetNghiemDAO().Delete(id);
            return RedirectToAction("Index", "AdminTest");
        }
    }
}