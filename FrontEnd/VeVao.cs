public class VeVao
{
    public List<VeBuffet> DanhSachVe { get; private set; } = new List<VeBuffet>();

    public List<VeBuffet> GetDanhSachVe()
    {
        return DanhSachVe;
    }

    public void DocTuFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Không tìm thấy file vé vào.");
            return;
        }

        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = line.Split('|');
            if (parts.Length == 2)
            {
                DanhSachVe.Add(new VeBuffet
                {
                    GiaVe = int.Parse(parts[0].Trim()),
                    KieuVe = parts[1].Trim(),
                });
            }
        }
    }

    public List<string> LayDanhSachVe()
    {
        return DanhSachVe.Select(m => m.KieuVe).Distinct().ToList();
    }
}