using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        VeVao veVao = new VeVao();
        veVao.DocTuFile("ChonVeVao.txt");

        var cacVe = veVao.LayDanhSachVe();
        if (cacVe.Count == 0)
        {
            Console.WriteLine("Không có loại vé nào.");
            return;
        }
        List<VeBuffet> hoaDonVeVao = new List<VeBuffet>();
        while (true)
        {
            Console.WriteLine("Các loại vé vào:");
            for (int i = 0; i < cacVe.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {cacVe[i]}");
            }

            Console.Write("Nhập số loại vé muốn chọn (hoặc gõ 'xong' để kết thúc): ");
            string nhapLoai = Console.ReadLine()?.Trim().ToLower();
            if (nhapLoai == "xong") break;

            if (!int.TryParse(nhapLoai, out int veIndex) || veIndex < 1 || veIndex > cacVe.Count)
            {
                Console.WriteLine("Số loại vé không hợp lệ.");
                continue;
            }

            string kieuVeChon = cacVe[veIndex - 1];
            var danhSachVeTheoLoai = veVao.DanhSachVe.FindAll(v => v.KieuVe.Equals(kieuVeChon));

            if (danhSachVeTheoLoai.Count == 0)
            {
                Console.WriteLine("Không tìm thấy vé phù hợp.");
                continue;
            }

            var veChon = danhSachVeTheoLoai[0]; // lấy mẫu vé để lấy giá

            Console.Write("Nhập số lượng vé muốn mua: ");
            string nhapSoLuong = Console.ReadLine()?.Trim();
            if (!int.TryParse(nhapSoLuong, out int soLuongVe) || soLuongVe < 1)
            {
                Console.WriteLine("Số lượng không hợp lệ. Mặc định là 1.");
                soLuongVe = 1;
            }

            hoaDonVeVao.Add(new VeBuffet
            {
                KieuVe = veChon.KieuVe,
                GiaVe = veChon.GiaVe,
                SoLuongVe = soLuongVe
            });

            Console.WriteLine($"Đã thêm: {veChon.KieuVe} x{soLuongVe} - {veChon.GiaVe:N0} VND mỗi vé");
        }

        ThucDon thucDon = new ThucDon();
        thucDon.DocTuFile("data.txt");

        var cacNhom = thucDon.LayDanhSachNhom();
        if (cacNhom.Count == 0)
        {
            Console.WriteLine("Không có loại món nào.");
            return;
        }

        List<MonAn> hoaDonMonAn = new List<MonAn>();

        while (true)
        {
            Console.WriteLine("Các loại món ăn:");
            for (int i = 0; i < cacNhom.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {cacNhom[i]}");
            }

            Console.Write("Nhập số loại món muốn chọn (hoặc gõ 'xong' để kết thúc): ");
            string nhapLoai = Console.ReadLine()?.Trim().ToLower();
            if (nhapLoai == "xong") break;

            if (!int.TryParse(nhapLoai, out int nhomIndex) || nhomIndex < 1 || nhomIndex > cacNhom.Count)
            {
                Console.WriteLine("Số loại món không hợp lệ.");
                continue;
            }

            string nhomChon = cacNhom[nhomIndex - 1];
            var monTheoNhom = thucDon.LayMonTheoNhom(nhomChon);

            Console.WriteLine($"Món thuộc nhóm \"{nhomChon}\":");
            for (int i = 0; i < monTheoNhom.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {monTheoNhom[i].TenMon} - {monTheoNhom[i].Gia:N0} VND");
            }

            while (true)
            {
                Console.Write("Nhập số món muốn chọn trong nhóm này (hoặc gõ 'xong' để quay lại chọn nhóm khác): ");
                string nhapMon = Console.ReadLine()?.Trim().ToLower();
                if (nhapMon == "xong") break;

                if (!int.TryParse(nhapMon, out int monIndex) || monIndex < 1 || monIndex > monTheoNhom.Count)
                {
                    Console.WriteLine("Số món không hợp lệ.");
                    continue;
                }

                var monChon = monTheoNhom[monIndex - 1];

                Console.Write("Nhập số lượng món muốn mua: ");
                string nhapSoLuong = Console.ReadLine()?.Trim();
                if (!int.TryParse(nhapSoLuong, out int SoLuongMon) || SoLuongMon < 1)
                {
                    Console.WriteLine("Số lượng không hợp lệ. Mặc định là 1.");
                    SoLuongMon = 1;
                }

                hoaDonMonAn.Add(new MonAn
                {
                    TenMon = monChon.TenMon,
                    Nhom = monChon.Nhom,
                    Gia = monChon.Gia,
                    SoLuongMon = SoLuongMon
                });

                Console.WriteLine($"Đã thêm: {monChon.TenMon} x{SoLuongMon} - {monChon.Gia:N0} VND mỗi món");
            }
        }

        // Xuất hóa đơn
        Console.WriteLine("HÓA ĐƠN:");
        int tongTienMon = 0;
        int tongTienVe = 0;
        foreach (var mon in hoaDonMonAn)
        {
            Console.WriteLine($"- {mon.TenMon} ({mon.Nhom}): {mon.Gia:N0} VND");
            tongTienMon += mon.Gia * mon.SoLuongMon ;
        }
        foreach (var ve in hoaDonVeVao)
        {
            Console.WriteLine($"- Vé {ve.KieuVe} x{ve.SoLuongVe}: {ve.GiaVe:N0} VND mỗi vé");
            tongTienVe += ve.GiaVe * ve.SoLuongVe;
        }
        Console.WriteLine($"Tổng tiền: {tongTienMon+tongTienVe:N0} VND");
    }
}