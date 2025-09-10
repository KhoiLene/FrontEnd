using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        ThucDon thucDon = new ThucDon();
        thucDon.DocTuFile("data.txt");

        var cacNhom = thucDon.LayDanhSachNhom();
        if (cacNhom.Count == 0)
        {
            Console.WriteLine("Không có loại món nào.");
            return;
        }

        List<MonAn> hoaDon = new List<MonAn>();

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
                hoaDon.Add(monChon);
                Console.WriteLine($"Đã thêm: {monChon.TenMon} - {monChon.Gia:N0} VND");
            }
        }

        // Xuất hóa đơn
        Console.WriteLine("HÓA ĐƠN:");
        int tongTien = 0;
        foreach (var mon in hoaDon)
        {
            Console.WriteLine($"- {mon.TenMon} ({mon.Nhom}): {mon.Gia:N0} VND");
            tongTien += mon.Gia ?? 0;
        }
        Console.WriteLine($"Tổng tiền: {tongTien:N0} VND");
    }
}