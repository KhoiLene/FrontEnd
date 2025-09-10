namespace FrontEnd.Models
{
    // Lớp con kế thừa từ Nguoi
    public class KhachHang : Nguoi
    {
        public string? MaKhachHang { get; set; }
        public string? DiaChi { get; set; }
        public string? Email { get; set; }

        // Ghi đè phương thức ThongTin()
        public override string ThongTin()
        {
            return $"[HCMUTE:{MaKhachHang}] {Ten} - {GioiTinh} - {DienThoai} - {DiaChi} - {Email}";
        }
    }
}
