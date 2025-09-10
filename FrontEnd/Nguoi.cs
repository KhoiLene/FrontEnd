namespace FrontEnd
{
    public class Nguoi
    {
        public string? Ten { get; set; }
        public string? GioiTinh { get; set; }
        public string? DienThoai { get; set; }

        public virtual string ThongTin()
        {
            return $"{Ten} - {GioiTinh} - {DienThoai}";
        }
    }
}
