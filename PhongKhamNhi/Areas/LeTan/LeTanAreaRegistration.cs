using System.Web.Mvc;

namespace PhongKhamNhi.Areas.LeTan
{
    public class LeTanAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LeTan";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LeTan_default",
                "LeTan/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}