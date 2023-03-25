using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhongKhamNhi.Areas.Admin.Controllers
{
    public class AdminBranchController : Controller
    {
        // GET: Admin/AdminBranch
        public ActionResult Index(int pageNum = 1, int pageSize = 9)
        {
            return View(new ChiNhanhDAO().ListCN(pageNum, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ChiNhanh cn, string tt)
        {
            if (ModelState.IsValid)
            {
                if (tt == "1")
                    cn.DangHoatDong = true;
                else
                    cn.DangHoatDong = false;
                    new ChiNhanhDAO().Insert(cn);
                    return RedirectToAction("Index", "AdminBranch");
                }
            return View(cn);
        }

        public ActionResult Edit(int id)
        {
            return View(new ChiNhanhDAO().FindByID(id));
        }
        [HttpPost]
        public ActionResult Edit(ChiNhanh cn, string tt)
        {
            if (ModelState.IsValid)
            {
                if (tt == "1")
                    cn.DangHoatDong = true;
                else
                    cn.DangHoatDong = false;
                new ChiNhanhDAO().Update(cn);
                return RedirectToAction("Index", "AdminBranch");
            }
            return View(cn);
        }
    }
}