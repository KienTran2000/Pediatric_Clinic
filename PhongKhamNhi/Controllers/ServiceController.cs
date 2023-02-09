using PhongKhamNhi.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhongKhamNhi.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Index(int pageNum = 1, int pageSize = 12)
        {
            ViewBag.lstDv = new DichVuDAO().GetList3Dv();
            return View(new DichVuDAO().GetListDv(pageNum, pageSize));
        }
        public ActionResult Detail(int id)
        {
            ViewBag.lstDv = new DichVuDAO().GetList3Dv();
            return View(new DichVuDAO().FindByID(id));
        }
    }
}