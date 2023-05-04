using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhongKhamNhi.Areas.LeTan.Controllers
{
    public class BaiVietController : Controller
    {
        // GET: LeTan/BaiViet
        public ActionResult Index(string tieuDe, string tu, string den, int pageNum = 1, int pageSize = 9)
        {
            ViewBag.tieuDe = tieuDe;
            ViewBag.tu = tu;
            ViewBag.den = den;
            if (tu == null)
                tu = "2020-11-19 12:00:00";
            if (den == null)
                den = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return View(new BaiVietDAO().lstBaiViet(tieuDe, tu, den, pageNum, pageSize));
        }

        public ActionResult Create()
        {
            return View(new BaiViet());
        }
        [HttpPost]
        public ActionResult Create(BaiViet d, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                if (photo != null && photo.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Content/assets/img/blog/"), System.IO.Path.GetFileName(photo.FileName));
                    photo.SaveAs(path);
                    d.AnhDaiDien = photo.FileName;
                }
                d.ThoiGianTao = DateTime.Now;
                BaiVietDAO dao = new BaiVietDAO();
                dao.Insert(d);                
                return RedirectToAction("Index", "BaiViet");
            }
            return View(d);
        }

        public ActionResult Edit(int id)
        {
            return View(new BaiVietDAO().FindByID(id));
        }
        [HttpPost]
        public ActionResult Edit(BaiViet d, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                if (photo != null && photo.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Content/assets/img/blog/"), System.IO.Path.GetFileName(photo.FileName));
                    photo.SaveAs(path);
                    d.AnhDaiDien = photo.FileName;
                }
                BaiVietDAO dao = new BaiVietDAO();
                dao.Update(d);
                return RedirectToAction("Index", "BaiViet");
            }
            return View(d);
        }

        public ActionResult Delete(int id)
        {
            new BaiVietDAO().Delete(id);
            return RedirectToAction("Index", "BaiViet");
        }
    }
}