using Application.Interface;
using Application.Response;
using Domain.Entities;
using Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ThongKeFeatures;
public class ThongKeFeatures
{
    public class GetThongKeTheoTheLoai : BaseQuery<IEnumerable<ThongKeTheLoaiResponseDTO>, GetThongKeTheoTheLoai>
    {
        public int UserId { get; set; } // khi nào có đăng nhập thì sài tới cái này
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public class Handler : BaseHandler<GetThongKeTheoTheLoai>
        {
            public Handler(IApplicationDbContext context) : base(context) { }

            public async override Task<IEnumerable<ThongKeTheLoaiResponseDTO>> Handle(GetThongKeTheoTheLoai query, CancellationToken cancellationToken)
            {
                if(!CheckDate(query.TuNgay, query.DenNgay))
                {
                    return null;
                }
                var giaoDichList = await _context.GiaoDich.Where(a => a.NgayGiaoDich >= query.TuNgay && a.NgayGiaoDich <= query.DenNgay).ToListAsync();
                var theLoaiList = await _context.TheLoai.ToListAsync();
                var thongKeTheLoaiResponseList = new List<ThongKeTheLoaiResponseDTO>();
                if (giaoDichList == null || theLoaiList == null)
                    return null;
                foreach (var theLoai in theLoaiList) // duyệt qua từng thể loại
                {
                    thongKeTheLoaiResponseList.Add(new ThongKeTheLoaiResponseDTO
                    {
                        TheLoaiId = theLoai.Id,
                        TenTheLoai = theLoai.TenTheLoai,
                        TongThu = 0,
                        SoLuongGiaoDichThu = 0,
                        TongChi = 0,
                        SoLuongGiaoDichChi = 0
                    }); // tạo ra 1 list thống kê theo thể loại

                    var giaoDichListTrungTheLoai = giaoDichList.Where(x => x.ChiTietGiaoDich.TheLoai.Id == theLoai.Id).ToList(); // lọc ra các giao dịch trùng thể loại
                    foreach (var giaoDich in giaoDichListTrungTheLoai) // duyệt qua từng giao dịch trùng thể loại
                    {
                        if (giaoDich.ChiTietGiaoDich.TheLoai.PhanLoai == "Thu") // nếu là thu thì cộng vào tổng thu
                        {
                            thongKeTheLoaiResponseList.Where(x => x.TheLoaiId == theLoai.Id).FirstOrDefault().TongThu += giaoDich.TongTien;
                            thongKeTheLoaiResponseList.Where(x => x.TheLoaiId == theLoai.Id).FirstOrDefault().SoLuongGiaoDichThu += 1;
                        }
                        else // nếu là chi thì cộng vào tổng chi
                        {
                            thongKeTheLoaiResponseList.Where(x => x.TheLoaiId == theLoai.Id).FirstOrDefault().TongChi += giaoDich.TongTien;
                            thongKeTheLoaiResponseList.Where(x => x.TheLoaiId == theLoai.Id).FirstOrDefault().SoLuongGiaoDichChi += 1;
                        }
                    }
                }

                return thongKeTheLoaiResponseList;

            }

           

        }
        
    }

    public class GetThongKeTheoTaiKhoan : BaseQuery<IEnumerable<ThongKeTaiKhoanResponseDTO>, GetThongKeTheoTaiKhoan>
    {
        public int UserId { get; set; } // khi nào có đăng nhập thì sài tới cái này
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }

        public class Handler : BaseHandler<GetThongKeTheoTaiKhoan>
        {
            public Handler(IApplicationDbContext context) : base(context) { }

            public async override Task<IEnumerable<ThongKeTaiKhoanResponseDTO>> Handle(GetThongKeTheoTaiKhoan query, CancellationToken cancellationToken)
            {
                if (!CheckDate(query.TuNgay, query.DenNgay))
                {
                    return null;
                }
                var giaoDichList = await _context.GiaoDich.Where(a => a.NgayGiaoDich >= query.TuNgay && a.NgayGiaoDich <= query.DenNgay).ToListAsync();
                var taiKhoanList = await _context.TaiKhoan.ToListAsync();
                var thongKeTaiKhoanResponseList = new List<ThongKeTaiKhoanResponseDTO>();
                if (giaoDichList == null || taiKhoanList == null)
                    return null;
                foreach (var taiKhoan in taiKhoanList) // duyệt qua từng tai khoan
                {
                    thongKeTaiKhoanResponseList.Add(new ThongKeTaiKhoanResponseDTO
                    {
                        TaiKhoanId = taiKhoan.Id,
                        TenTaiKhoan = taiKhoan.TenTaiKhoan,
                        LoaiTaiKhoan = taiKhoan.LoaiTaiKhoan.Ten,
                        TongThu = 0,
                        SoLuongGiaoDichThu = 0,
                        TongChi = 0,
                        SoLuongGiaoDichChi = 0
                    }); // tạo ra 1 list thống kê theo tai khoan

                    var giaoDichListTrungTaiKhoan = giaoDichList.Where(x => x.ChiTietGiaoDich.TaiKhoanGiaoDich.Contains(taiKhoan)).ToList(); // lọc ra các giao dịch có chứa thể loại
                    foreach (var giaoDich in giaoDichListTrungTaiKhoan) // duyệt qua từng giao dịch trùng thể loại
                    {
                        if (giaoDich.ChiTietGiaoDich.TaiKhoanGiaoDich.Count() < 2) // nếu giao dịch chỉ trên 1 tài khoản
                        {
                            if (giaoDich.ChiTietGiaoDich.TheLoai.PhanLoai == "Thu") // nếu là thu thì cộng vào tổng thu
                            {
                                thongKeTaiKhoanResponseList.Where(x => x.TaiKhoanId == taiKhoan.Id).FirstOrDefault().TongThu += giaoDich.TongTien;
                                thongKeTaiKhoanResponseList.Where(x => x.TaiKhoanId == taiKhoan.Id).FirstOrDefault().SoLuongGiaoDichThu += 1;
                            }
                            else // nếu là chi thì cộng vào tổng chi
                            {
                                thongKeTaiKhoanResponseList.Where(x => x.TaiKhoanId == taiKhoan.Id).FirstOrDefault().TongChi += giaoDich.TongTien;
                                thongKeTaiKhoanResponseList.Where(x => x.TaiKhoanId == taiKhoan.Id).FirstOrDefault().SoLuongGiaoDichChi += 1;
                            }
                        }
                        
                    }
                }

                return thongKeTaiKhoanResponseList;

            }


        }
    }



    public static bool CheckDate(DateTime TuNgay, DateTime DenNgay)
    {
        DateTime dateValue;
        // kiểm tra ngày có hợp lệ không
        if (TuNgay == DateTime.MinValue || DenNgay == DateTime.MinValue) //minvalue = 1/1/0001
        {
            return false;
        }
        if (TuNgay > DateTime.Now || DenNgay > DateTime.Now)
        {
            return false;
        }
        // kiểm tra ngày bắt đầu và ngày kết thúc
        if (TuNgay > DenNgay)
        {
            return false;
        }

        return true;
    }
}