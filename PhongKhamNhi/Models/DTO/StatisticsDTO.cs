namespace PhongKhamNhi.Models.DTO
{
    public class StatisticsDTO
    {
        public int Ngay { get; set; }
        public double DoanhThu { get; set; }
    }

    public class ThongKeDichVuDTO
    {
        public string TenDV { get; set; }

        public int Sl { get; set; }

        public double DonGia { get; set; }
        public double ThanhTien { get; set; }
    }
}