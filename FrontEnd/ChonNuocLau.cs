namespace FrontEnd
{
    public class ChonNuocLau
    {
        public List<NuocLau> DanhSachNuocLau { get; private set; } = new List<NuocLau>();

        public List<NuocLau> GetDanhSachNuoc()
        {
            return DanhSachNuocLau;
        }
        public void DocTuFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Không tìm thấy file thực đơn.");
                return;
            }

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split('|');
                if (parts.Length == 2)
                {
                    DanhSachNuocLau.Add(new NuocLau
                    {
                        TenNuocLau = parts[1].Trim(),
                        Gia = int.Parse(parts[0].Trim()),
                    });
                }
            }
        }

        public List<string> LayDanhSachNuocLau()
        {
            return DanhSachNuocLau.Select(m => m.TenNuocLau).Distinct().ToList();
        }
    }
}