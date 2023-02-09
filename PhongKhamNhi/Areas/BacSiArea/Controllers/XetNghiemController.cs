using PhongKhamNhi.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhongKhamNhi.Areas.BacSiArea.Controllers
{
    public class XetNghiemController : Controller
    {
        // GET: BacSiArea/XetNghiem
        public ActionResult Index(string ten, string dv, int pageNum = 1, int pageSize = 9)
        {
            if (dv == null)
                dv = "0";
            ViewBag.ten = ten;
            ViewBag.dv = int.Parse(dv);
            ViewBag.ListDichVu = new DichVuDAO().GetListDv();
            return View(new XetNghiemDAO().lstXn(ten, int.Parse(dv), pageNum, pageSize));
        }
    }
}