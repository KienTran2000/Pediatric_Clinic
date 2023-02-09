using System.Web.Mvc;

namespace PhongKhamNhi.Areas.BacSiArea
{
    public class BacSiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "BacSiArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "BacSiArea_default",
                "BacSiArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}