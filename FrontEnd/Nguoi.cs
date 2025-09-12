namespace FrontEnd
{
    public abstract class Nguoi
    {
        public string Ten { get; set; } 
        public string GioiTinh { get; set; } 
        public string DienThoai { get; set; } 

        public abstract string ThongTin();
    }
}
