namespace FrontEnd
{
    class HoaDon
    {
        public List<KhachHang> KhachHang { get; set; } = new List<KhachHang>();
        public List<VeBuffet> VeVao { get; set; } = new List<VeBuffet>();
        public List<MonAn> MonAn { get; set; } = new List<MonAn>();
        public DateTime ThoiGian { get; set; } = DateTime.Now;

        public int TongTien()
        {
            int tong = 0;
            foreach (var ve in VeVao) tong += ve.GiaVe * ve.SoLuongVe;
            foreach (var mon in MonAn) tong += mon.Gia * mon.SoLuongMon;
            return tong;
        }
    }
}

