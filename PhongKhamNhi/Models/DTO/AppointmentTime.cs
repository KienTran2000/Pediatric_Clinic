using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhongKhamNhi.Models.DTO
{
    public class AppointmentTime
    {
        public List<string> lst;
        public AppointmentTime(int type)
        {
            lst = new List<string>();
            if(type == 0)
            {
                lst.Add("8h0");
                lst.Add("8h30");
                lst.Add("9h0");
                lst.Add("9h30");
                lst.Add("10h0");
                lst.Add("10h30");
                lst.Add("11h0");
                lst.Add("13h30");
                lst.Add("14h0");
                lst.Add("14h30");
                lst.Add("15h0");
                lst.Add("15h30");
                lst.Add("16h0");
                lst.Add("16h30");
            }
            else
            {
                lst.Add("8h0");
                lst.Add("10h0");
                lst.Add("13h30");
                lst.Add("15h30");
            }
        }
    }
}