using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhongKhamNhi.Models.DTO
{
    public class BacSiDTO
    {
        public BacSiDTO()
        {

        }
        public BacSiDTO(BacSi bacSi)
        {
            MaBS = bacSi.MaBS;
            HoTen = bacSi.HoTen;
            HocVi = bacSi.HocVi;
        }
        public int MaBS { get; set; }
        public string HoTen { get; set; }
        public string HocVi { get; set; }


        public int sldk { get; set; }
    }
}