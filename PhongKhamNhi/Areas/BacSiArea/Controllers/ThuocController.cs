using PhongKhamNhi.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhongKhamNhi.Areas.BacSiArea.Controllers
{
    public class ThuocController : Controller
    {
        // GET: BacSiArea/Thuoc
        public ActionResult Index(string ten, string lt, int pageNum = 1, int pageSize = 9)
        {
            if (lt == null)
                lt = "0";
            ViewBag.ten = ten;
            ViewBag.lt = int.Parse(lt);
            ViewBag.ListLoaiThuoc = new ThuocDAO().GetListLoaiThuoc();
            return View(new ThuocDAO().lstThuoc(ten, int.Parse(lt), pageNum, pageSize));
        }
    }
}