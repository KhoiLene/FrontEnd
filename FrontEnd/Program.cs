using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;
namespace FrontEnd
{
    class Program
    {
        static void Main()
        {
            List<HoaDon> danhSachHoaDon = new List<HoaDon>();
            while (true)
            {
                Console.WriteLine("\nHOÁ ĐƠN NHÀ HÀNG BUFFET(NHẬP 'hết một ngày' để xuất hoá đơn, nhập enter để nhập)\n");
                string end = Console.ReadLine()?.Trim().ToLower();
                if (end == "hết một ngày") break;
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                List<KhachHang> danhSachKhach = new List<KhachHang>();

                int dem = 0;

                while (true)
                {
                    Console.WriteLine("\n Nhập thông tin khách hàng (nhập 'bắt đầu' để nhập khách hàng hoặc enter để chọn vé vào): ");
                    string en = Console.ReadLine()?.Trim().ToLower();
                    if (en != "bắt đầu") break;
                    Console.Write("Tên khách hàng: ");
                    string ten = Console.ReadLine()?.Trim().ToLower();
                    dem++;
                    string ma = $"HCMUTE{DateTime.Now:yyyy:MM:dd:HH:mm:ss:}{dem}";


                    Console.Write("Giới tính: ");
                    string gioiTinh = Console.ReadLine();

                    string dienThoai;
                    while (true)
                    {
                        Console.Write("Số điện thoại (10 số): ");
                        dienThoai = Console.ReadLine();

                        if (!string.IsNullOrEmpty(dienThoai) &&
                            dienThoai.Length == 10 &&
                            long.TryParse(dienThoai, out _)) // kiểm tra toàn số
                        {
                            break; // hợp lệ, thoát vòng lặp
                        }

                        Console.WriteLine("Số điện thoại không hợp lệ, vui lòng nhập lại!");
                    }
                    Console.WriteLine("Số nhà: ");
                    string soNha = Console.ReadLine();
                    Console.WriteLine("Tên đường: ");
                    string tenDuong = Console.ReadLine();
                    Console.WriteLine("Tên quận: ");
                    string tenQuan = Console.ReadLine();
                    Console.WriteLine("Thành phố: ");
                    string thanhPho = Console.ReadLine();
                    DiaChi dc = new DiaChi(soNha, tenDuong, tenQuan, thanhPho);
                    KhachHang kh = new KhachHang
                    {
                        MaKhachHang = ma,
                        Ten = ten,
                        GioiTinh = gioiTinh,
                        DienThoai = dienThoai,
                        DiaChi = dc
                    };

                    danhSachKhach.Add(kh);
                    dem++;

                    Console.WriteLine($"\n Đã thêm khách hàng thứ {dem-1}!");
                }



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
                }


                ChonNuocLau chonNuocLau = new ChonNuocLau();
                chonNuocLau.DocTuFile("NuocLau.txt");

                var nuocLau = chonNuocLau.LayDanhSachNuocLau();
                if (nuocLau.Count == 0)
                {
                    Console.WriteLine("Không có loại nước lẩu nào.");
                    return;
                }
                List<NuocLau> hoaDonNuocLau = new List<NuocLau>();
                while (true)
                {
                    Console.WriteLine("Các loại nước lẩu:");
                    for (int i = 0; i < nuocLau.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {nuocLau[i]}");
                    }

                    Console.Write("Nhập số nước lẩu muốn chọn (hoặc gõ 'xong' để kết thúc): ");
                    string nhapLau = Console.ReadLine()?.Trim().ToLower();
                    if (nhapLau == "xong") break;

                    if (!int.TryParse(nhapLau, out int lauIndex) || lauIndex < 1 || lauIndex > nuocLau.Count)
                    {
                        Console.WriteLine("Số loại vé không hợp lệ.");
                        continue;
                    }

                    string kieuNuocLau = nuocLau[lauIndex - 1];
                    var danhSachNuocLauTheoLoai = chonNuocLau.DanhSachNuocLau.FindAll(v => v.TenNuocLau.Equals(kieuNuocLau));

                    if (danhSachNuocLauTheoLoai.Count == 0)
                    {
                        Console.WriteLine("Không tìm thấy vé phù hợp.");
                        continue;
                    }

                    var lauChon = danhSachNuocLauTheoLoai[0]; // lấy mẫu vé để lấy giá

                    Console.Write("Nhập số lượng nước lẩu muốn mua: ");
                    string nhapSoLuong = Console.ReadLine()?.Trim();
                    if (!int.TryParse(nhapSoLuong, out int soLuongNuocLau) || soLuongNuocLau < 1)
                    {
                        Console.WriteLine("Số lượng không hợp lệ. Mặc định là 1.");
                        soLuongNuocLau = 1;
                    }

                    hoaDonNuocLau.Add(new NuocLau
                    {
                        TenNuocLau = lauChon.TenNuocLau,
                        Gia = lauChon.Gia,
                        SoLuongNuocLau = soLuongNuocLau
                    });
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
                // Sau khi đã có hoaDonVeVao và hoaDonMonAn
                while (true)
                {
                    Console.WriteLine("\nGIỎ HÀNG HIỆN TẠI");

                    if (hoaDonVeVao.Count == 0 && hoaDonMonAn.Count == 0)
                    {
                        Console.WriteLine("Giỏ hàng trống.");
                    }
                    else
                    {
                        foreach (var ve in hoaDonVeVao)
                            Console.WriteLine($"Vé {ve.KieuVe} x{ve.SoLuongVe} - {ve.GiaVe:N0} VND mỗi vé");

                        foreach (var mon in hoaDonMonAn)
                            Console.WriteLine($"{mon.TenMon} ({mon.Nhom}) x{mon.SoLuongMon} - {mon.Gia:N0} VND mỗi món");
                    }

                    Console.WriteLine("\nNhập lệnh:");
                    Console.WriteLine("1 - vé vào");
                    Console.WriteLine("2 - nuớc lẩu");
                    Console.WriteLine("3 - thực đơn");
                    Console.WriteLine("xong - Xuất hóa đơn");
                    string lenh = Console.ReadLine()?.Trim().ToLower();

                    if (lenh == "xong") break;

                    if (lenh == "1") // sửa giỏ vé
                    {
                        Console.WriteLine("Các vé trong giỏ:");
                        for (int i = 0; i < hoaDonVeVao.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {hoaDonVeVao[i].KieuVe} x{hoaDonVeVao[i].SoLuongVe}");
                        }

                        Console.Write("Chọn số thứ tự vé muốn sửa: ");
                        string chonVe = Console.ReadLine()?.Trim();
                        if (!int.TryParse(chonVe, out int veIndex) || veIndex < 1 || veIndex > hoaDonVeVao.Count)
                        {
                            Console.WriteLine("Không hợp lệ.");
                            continue;
                        }

                        var ve = hoaDonVeVao[veIndex - 1];

                        Console.WriteLine("Nhập 'giảm', 'tăng' hoặc 'xoá': ");
                        string thaoTac = Console.ReadLine()?.Trim().ToLower();

                        if (thaoTac == "xoá")
                        {
                            hoaDonVeVao.Remove(ve);
                            Console.WriteLine("Đã xóa vé.");
                            continue;
                        }

                        Console.Write("Nhập số lượng: ");
                        if (!int.TryParse(Console.ReadLine(), out int sl) || sl < 1)
                        {
                            Console.WriteLine("Số lượng không hợp lệ.");
                            continue;
                        }

                        if (thaoTac == "tăng") ve.SoLuongVe += sl;
                        if (thaoTac == "giảm")
                        {
                            ve.SoLuongVe -= sl;
                            if (ve.SoLuongVe <= 0) hoaDonVeVao.Remove(ve);
                        }
                    }
                    else if (lenh == "2") // sửa giỏ nước lẩu
                    {
                        Console.WriteLine("Các nước lẩu trong giỏ:");
                        for (int i = 0; i < hoaDonNuocLau.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {hoaDonNuocLau[i].TenNuocLau} x{hoaDonNuocLau[i].SoLuongNuocLau}");
                        }

                        Console.Write("Chọn số thứ tự nước lẩu muốn sửa: ");
                        string chonVe = Console.ReadLine()?.Trim();
                        if (!int.TryParse(chonVe, out int veIndex) || veIndex < 1 || veIndex > hoaDonNuocLau.Count)
                        {
                            Console.WriteLine("Không hợp lệ.");
                            continue;
                        }

                        var ve = hoaDonNuocLau[veIndex - 1];

                        Console.WriteLine("Nhập 'giảm', 'tăng' hoặc 'xoá': ");
                        string thaoTac = Console.ReadLine()?.Trim().ToLower();

                        if (thaoTac == "xoá")
                        {
                            hoaDonNuocLau.Remove(ve);
                            Console.WriteLine("Đã xóa nước lẩu.");
                            continue;
                        }

                        Console.Write("Nhập số lượng: ");
                        if (!int.TryParse(Console.ReadLine(), out int sl) || sl < 1)
                        {
                            Console.WriteLine("Số lượng không hợp lệ.");
                            continue;
                        }

                        if (thaoTac == "tăng") ve.SoLuongNuocLau += sl;
                        if (thaoTac == "giảm")
                        {
                            ve.SoLuongNuocLau -= sl;
                            if (ve.SoLuongNuocLau <= 0) hoaDonNuocLau.Remove(ve);
                        }
                    }
                    else if (lenh == "3") // sửa giỏ món ăn
                    {
                        if (hoaDonMonAn.Count == 0)
                        {
                            Console.WriteLine("Giỏ món ăn trống.");
                            continue;
                        }

                        Console.WriteLine("Các món trong giỏ:");
                        for (int i = 0; i < hoaDonMonAn.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {hoaDonMonAn[i].TenMon} x{hoaDonMonAn[i].SoLuongMon}");
                        }

                        Console.Write("Chọn số thứ tự món muốn sửa: ");
                        string chonMon = Console.ReadLine()?.Trim();
                        if (!int.TryParse(chonMon, out int monIndex) || monIndex < 1 || monIndex > hoaDonMonAn.Count)
                        {
                            Console.WriteLine("Không hợp lệ.");
                            continue;
                        }

                        var mon = hoaDonMonAn[monIndex - 1];

                        Console.WriteLine("Nhập 'giảm', 'tăng' hoặc 'xoá': ");
                        string thaoTac = Console.ReadLine()?.Trim().ToLower();

                        if (thaoTac == "xoá")
                        {
                            hoaDonMonAn.Remove(mon);
                            Console.WriteLine("Đã xóa món.");
                            continue;
                        }

                        Console.Write("Nhập số lượng: ");
                        if (!int.TryParse(Console.ReadLine(), out int sl) || sl < 1)
                        {
                            Console.WriteLine("Số lượng không hợp lệ.");
                            continue;
                        }

                        if (thaoTac == "tăng") mon.SoLuongMon += sl;
                        if (thaoTac == "giảm")
                        {
                            mon.SoLuongMon -= sl;
                            if (mon.SoLuongMon <= 0) hoaDonMonAn.Remove(mon);
                        }
                    }
                }
                HoaDon hoaDon = new HoaDon
                {
                    KhachHang = danhSachKhach,
                    VeVao = hoaDonVeVao,
                    ChonNuocLau= hoaDonNuocLau,
                    MonAn = hoaDonMonAn,
                    ThoiGian = DateTime.Now
                };
                danhSachHoaDon.Add(hoaDon);
                for (int i = 0; i < danhSachHoaDon.Count; i++)
                {
                    var hd = danhSachHoaDon[i];
                    Console.WriteLine($"\nHóa đơn {i + 1} - {hd.ThoiGian}");

                    foreach (var kh in hd.KhachHang)
                    {
                        Console.WriteLine($"Khách hàng: {kh.Ten} (Mã: {kh.MaKhachHang})");
                    }

                    foreach (var ve in hd.VeVao)
                    {
                        Console.WriteLine($"Vé {ve.KieuVe} x{ve.SoLuongVe} - {ve.GiaVe:N0} VND");
                    }

                    foreach (var nl in hd.ChonNuocLau)
                    {
                        Console.WriteLine($"Nước lẩu {nl.TenNuocLau} x{nl.SoLuongNuocLau} - {nl.Gia:N0} VND");
                    }

                    foreach (var mon in hd.MonAn)
                    {
                        Console.WriteLine($"{mon.TenMon} ({mon.Nhom}) x{mon.SoLuongMon} - {mon.Gia:N0} VND");
                    }
                    Console.WriteLine($"\nTổng tiền thanh toán: {hoaDon.TongTien():N0} VND");
                    Console.WriteLine("Cảm ơn quý khách! Hẹn gặp lại!");
                }
            }
            Console.WriteLine("\n--- TỔNG KẾT DOANH THU TRONG NGÀY ---");

                for (int i = 0; i < danhSachHoaDon.Count; i++)
                {
                    var hd = danhSachHoaDon[i];
                    Console.WriteLine($"\nHóa đơn {i + 1} - {hd.ThoiGian}");

                    foreach (var kh in hd.KhachHang)
                    {
                        Console.WriteLine($"Khách hàng: {kh.Ten} (Mã: {kh.MaKhachHang})");
                    }

                    foreach (var ve in hd.VeVao)
                    {
                        Console.WriteLine($"Vé {ve.KieuVe} x{ve.SoLuongVe} - {ve.GiaVe:N0} VND");
                    }
                    foreach (var nl in hd.ChonNuocLau)
                    {
                        Console.WriteLine($"Nước lẩu {nl.TenNuocLau} x{nl.SoLuongNuocLau} - {nl.Gia:N0} VND");
                    }
                    foreach (var mon in hd.MonAn)
                    {
                        Console.WriteLine($"{mon.TenMon} ({mon.Nhom}) x{mon.SoLuongMon} - {mon.Gia:N0} VND");
                    }
            }
        }
    }
}