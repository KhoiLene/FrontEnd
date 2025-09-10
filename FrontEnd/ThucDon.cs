public class ThucDon
{
    public List<MonAn> DanhSachMon { get; private set; } = new List<MonAn>();

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
            if (parts.Length == 3)
            {
                DanhSachMon.Add(new MonAn
                {
                    TenMon = parts[0].Trim(),
                    Gia = int.Parse(parts[1].Trim()),
                    Nhom = parts[2].Trim(),
                });
            }
        }
    }

    public List<string> LayDanhSachNhom()
    {
        return DanhSachMon.Select(m => m.Nhom).Distinct().ToList();
    }

    public List<MonAn> LayMonTheoNhom(string nhom)
    {
        return DanhSachMon.Where(m => m.Nhom == nhom).ToList();
    }
}

