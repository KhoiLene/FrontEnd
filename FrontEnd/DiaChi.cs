namespace FrontEnd
{
    public class DiaChi
    {
        private string soNha;
        private string tenDuong;
        private string tenQuan;
        private string thanhPho;

        public DiaChi(string soNha, string tenDuong, string tenQuan, string thanhPho)
        {
            this.soNha = soNha;
            this.tenDuong = tenDuong;
            this.tenQuan = tenQuan;
            this.thanhPho = thanhPho;
        }

        // Property cho SoNha
        public string SoNha
        {
            get => soNha;
            set => soNha = value;
        }

        // Property cho TenDuong
        public string TenDuong
        {
            get => tenDuong;
            set => tenDuong = value;
        }

        // Property cho TenQuan
        public string TenQuan
        {
            get => tenQuan;
            set => tenQuan = value;
        }

        // Property cho ThanhPho
        public string ThanhPho
        {
            get => thanhPho;
            set => thanhPho = value;
        }

        public override string ToString()
        {
            return $"So nha {soNha}, duong {tenDuong}, quan {tenQuan}, thanh pho {thanhPho}";
        }
    }
}
