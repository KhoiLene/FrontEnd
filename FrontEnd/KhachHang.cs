namespace FrontEnd
{
    // Lớp con kế thừa từ Nguoi
    public class KhachHang : Nguoi
    {
        public string? MaKhachHang { get; set; }
        public DiaChi? DiaChi { get; set; }

        // Ghi đè phương thức ThongTin()
        public override string ThongTin()
        {
            return $"[HCMUTE:{MaKhachHang}] {Ten} - giới tính {GioiTinh} -\n Số điện thoại: {DienThoai} - \n Địa chỉ: {DiaChi} ";
        }
    }
}
