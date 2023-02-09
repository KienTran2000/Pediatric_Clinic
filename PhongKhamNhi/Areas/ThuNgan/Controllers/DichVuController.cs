using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhongKhamNhi.Areas.ThuNgan.Controllers
{
    public class DichVuController : Controller
    {
        // GET: ThuNgan/DichVu
        public ActionResult Index()
        {
            return View(new DichVuDAO().GetListDv());
        }

        public ActionResult Create()
        {
            return View(new DichVuKham());
        }
        [HttpPost]
        public ActionResult Create(DichVuKham d, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                if (photo != null && photo.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Content/assets/img/blog/"), System.IO.Path.GetFileName(photo.FileName));
                    photo.SaveAs(path);
                    d.Anh = photo.FileName;
                }
                DichVuDAO dao = new DichVuDAO();
                int maDv = dao.Insert(d);
                List<XetNghiem> lst = (List<XetNghiem>)Session["dkxnDv"];
                if (lst != null)
                {
                    for (int i = 0; i < lst.Count(); i++)
                    {
                        dao.InsertXn(maDv, lst[i].MaXN);
                    }
                }
                Session["dkxnDv"] = null;
                return RedirectToAction("Index", "DichVu");
            }
            return View(d);
        }

        public ActionResult Edit(int id)
        {
            List<XetNghiem> lst = new XetNghiemDAO().GetListXnByMaDv(id);
            Session["dkxnDv"] = lst;
            Session["suaXn"] = 0;
            ViewBag.ListXn = lst;
            return View(new DichVuDAO().FindByID(id));
        }
        [HttpPost]
        public ActionResult Edit(DichVuKham d, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                if (photo != null && photo.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Content/assets/img/blog/"), System.IO.Path.GetFileName(photo.FileName));
                    photo.SaveAs(path);
                    d.Anh = photo.FileName;
                }
                DichVuDAO dao = new DichVuDAO();
                dao.Update(d);
                List<XetNghiem> lst = (List<XetNghiem>)Session["dkxnDv"];
                if ((int)Session["suaXn"] == 1)
                {
                    dao.DeleteXn(d.MaDV);
                    for (int i = 0; i < lst.Count(); i++)
                    {
                        dao.InsertXn(d.MaDV, lst[i].MaXN);
                    }
                }
                Session["dkxnDv"] = null;
                Session["suaXn"] = 0;
                return RedirectToAction("Index", "DichVu");
            }
            return View(d);
        }

        public JsonResult ListXn(string key)
        {
            var data = new XetNghiemDAO().lstSearchXn(key);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddXn(int id)
        {
            bool b = false;
            List<XetNghiem> lst = (List<XetNghiem>)Session["dkxnDv"];
            if (lst == null)
                lst = new List<XetNghiem>();
            foreach (XetNghiem item in lst)
            {
                if (item.MaXN == id)
                {
                    b = true;
                    break;
                }
            }
            if (!b)
            {
                XetNghiem x = new XetNghiem();
                x.MaXN = id;
                lst.Add(x);
            }
            Session["dkxnDv"] = lst;
            Session["suaXn"] = 1;
            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteXn(string id)
        {
            int ma = int.Parse(id.Split('-')[1]);
            List<XetNghiem> lst = (List<XetNghiem>)Session["dkxnDv"];
            foreach (XetNghiem item in lst)
            {
                if (item.MaXN == ma)
                {
                    lst.Remove(item);
                    break;
                }
            }
            Session["dkxnDv"] = lst;
            Session["suaXn"] = 1;
            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}