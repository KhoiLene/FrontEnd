namespace FrontEnd
{
    class HoaDon
    {
        public List<KhachHang> KhachHang { get; set; } = new List<KhachHang>();
        public List<VeBuffet> VeVao { get; set; } = new List<VeBuffet>();
        public List<NuocLau> ChonNuocLau { get; set; } = new List<NuocLau>();
        public List<MonAn> MonAn { get; set; } = new List<MonAn>();
        public DateTime ThoiGian { get; set; } = DateTime.Now;

        public int TongTien()
        {
            int tong = 0;
            foreach (var ve in VeVao) tong += ve.GiaVe * ve.SoLuongVe;
            foreach (var nl in ChonNuocLau) tong += nl.Gia * nl.SoLuongNuocLau;
            foreach (var mon in MonAn) tong += mon.Gia * mon.SoLuongMon;
            return tong;
        }
    }
}

