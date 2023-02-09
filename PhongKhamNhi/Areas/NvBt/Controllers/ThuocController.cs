using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhongKhamNhi.Areas.NvBt.Controllers
{
    public class ThuocController : Controller
    {
        // GET: NvBt/Thuoc
        public ActionResult Index(string ten, string lt, int pageNum = 1, int pageSize = 9)
        {
            if (lt == null)
                lt = "0";
            ViewBag.ten = ten;
            ViewBag.lt = int.Parse(lt);
            ViewBag.ListLoaiThuoc = new ThuocDAO().GetListLoaiThuoc();
            return View(new ThuocDAO().lstThuoc(ten, int.Parse(lt), pageNum, pageSize));
        }
        [HttpPost]
        public ActionResult Index(FormCollection data, int pageNum = 1, int pageSize = 9)
        {
            ThuocDAO dao = new ThuocDAO();
            if (data.Count > 0)
            {
                string[] ids = data["checkBoxId"].Split(new char[] { ',' });
                foreach (string id in ids)
                {
                    dao.Delete(int.Parse(id));
                }
            }
            NhanVien nv = (NhanVien)Session["user"];
            return View(new ThuocDAO().lstThuoc("", 0, pageNum, pageSize));
        }

        public ActionResult Create()
        {
            ViewBag.ListLoaiThuoc = new ThuocDAO().GetListLoaiThuoc();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Thuoc t, string lt)
        {
            t.MaLoaiThuoc = int.Parse(lt);
            new ThuocDAO().Insert(t);
            return RedirectToAction("Index", "Thuoc");
        }

        public ActionResult Edit(int id)
        {
            ThuocDAO dao = new ThuocDAO();
            ViewBag.ListLoaiThuoc = dao.GetListLoaiThuoc();
            return View(dao.FindByID(id));
        }
        [HttpPost]
        public ActionResult Edit(Thuoc t, string lt)
        {
            t.MaLoaiThuoc = int.Parse(lt);
            new ThuocDAO().Update(t);
            return RedirectToAction("Index", "Thuoc");
        }

        public ActionResult Delete(int id)
        {
            new ThuocDAO().Delete(id);
            return RedirectToAction("Index", "Thuoc");
        }
    }
}