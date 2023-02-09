using PhongKhamNhi.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhongKhamNhi.Areas.LeTan.Controllers
{
    public class DichVuController : Controller
    {
        // GET: LeTan/DichVu
        public ActionResult Index()
        {
            return View(new DichVuDAO().GetListDv());
        }
    }
}