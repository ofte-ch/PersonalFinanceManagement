using Application.Interface;
using Application.Response;
using Domain.Entities;
using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Application.Features.ThongKeFeatures;
public class ThongKeFeatures
{
    public class GetThongKeTheoTheLoai : BaseQuery<IEnumerable<ThongKeTheLoaiResponseDTO>, GetThongKeTheoTheLoai>
    {
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }

        public class Handler : BaseHandler<GetThongKeTheoTheLoai>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
            {
                _httpContextAccessor = httpContextAccessor;
            }

            public async override Task<IEnumerable<ThongKeTheLoaiResponseDTO>> Handle(GetThongKeTheoTheLoai query, CancellationToken cancellationToken)
            {
                // Kiểm tra nếu ngày không hợp lệ
                if (!CheckDate(query.TuNgay, query.DenNgay))
                {
                    return null;
                }

                // Lấy UserId từ HttpContext, nếu không có thì return null
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    // Nếu chưa đăng nhập, trả về null
                    return null;
                }

                int userId = int.Parse(userIdClaim);

                // Lọc giao dịch theo ngày và UserId
                var giaoDichList = await _context.GiaoDich
                    .Where(a => a.NgayGiaoDich >= query.TuNgay && a.NgayGiaoDich <= query.DenNgay)
                    .Where(a => a.TaiKhoanGoc.User.Id == userId || a.TaiKhoanPhu.User.Id == userId) // Lọc theo UserId
                    .ToListAsync(cancellationToken);

                var theLoaiList = await _context.TheLoai.ToListAsync();
                var thongKeTheLoaiResponseList = new List<ThongKeTheLoaiResponseDTO>();

                if (giaoDichList == null || theLoaiList == null)
                    return null;

                foreach (var theLoai in theLoaiList) // Duyệt qua từng thể loại
                {
                    thongKeTheLoaiResponseList.Add(new ThongKeTheLoaiResponseDTO
                    {
                        TheLoaiId = theLoai.Id,
                        TenTheLoai = theLoai.TenTheLoai,
                        TongThu = 0,
                        SoLuongGiaoDichThu = 0,
                        TongChi = 0,
                        SoLuongGiaoDichChi = 0
                    });

                    // Lọc giao dịch trùng thể loại và theo UserId
                    var giaoDichListTrungTheLoai = giaoDichList
                        .Where(x => x.TheLoai.Id == theLoai.Id)
                        .ToList();

                    foreach (var giaoDich in giaoDichListTrungTheLoai) // Duyệt qua từng giao dịch trùng thể loại
                    {
                        if (giaoDich.TheLoai.PhanLoai == "Thu") // Nếu là thu thì cộng vào tổng thu
                        {
                            var thongKe = thongKeTheLoaiResponseList.FirstOrDefault(x => x.TheLoaiId == theLoai.Id);
                            if (thongKe != null)
                            {
                                thongKe.TongThu += giaoDich.TongTien;
                                thongKe.SoLuongGiaoDichThu += 1;
                            }
                        }
                        else // Nếu là chi thì cộng vào tổng chi
                        {
                            var thongKe = thongKeTheLoaiResponseList.FirstOrDefault(x => x.TheLoaiId == theLoai.Id);
                            if (thongKe != null)
                            {
                                thongKe.TongChi += giaoDich.TongTien;
                                thongKe.SoLuongGiaoDichChi += 1;
                            }
                        }
                    }
                }

                return thongKeTheLoaiResponseList;
            }
        }
    }


    public class GetThongKeTheoTaiKhoan : BaseQuery<IEnumerable<ThongKeTaiKhoanResponseDTO>, GetThongKeTheoTaiKhoan>
    {
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }

        public class Handler : BaseHandler<GetThongKeTheoTaiKhoan>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
            {
                _httpContextAccessor = httpContextAccessor;
            }

            public async override Task<IEnumerable<ThongKeTaiKhoanResponseDTO>> Handle(GetThongKeTheoTaiKhoan query, CancellationToken cancellationToken)
            {
                // Kiểm tra nếu ngày không hợp lệ
                if (!CheckDate(query.TuNgay, query.DenNgay))
                {
                    return null;
                }

                // Kiểm tra UserId trong HttpContext, nếu không có thì trả về null
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    // Nếu chưa đăng nhập, trả về null
                    return null;
                }

                // Lấy userId từ claim của người dùng
                int userId = int.Parse(userIdClaim);

                // Lọc giao dịch theo ngày và UserId, kiểm tra tài khoản gốc và tài khoản phụ
                var giaoDichList = await _context.GiaoDich
                    .Where(a => a.NgayGiaoDich >= query.TuNgay && a.NgayGiaoDich <= query.DenNgay)
                    // Kiểm tra nếu tài khoản gốc hoặc tài khoản phụ có UserId trùng với userId
                    .Where(a => a.TaiKhoanGoc.User.Id == userId || a.TaiKhoanPhu.User.Id == userId)
                    .ToListAsync(cancellationToken);


                var taiKhoanList = await _context.TaiKhoan.Where(tk=>tk.User.Id == userId).ToListAsync();
                var thongKeTaiKhoanResponseList = new List<ThongKeTaiKhoanResponseDTO>();

                if (giaoDichList == null || taiKhoanList == null)
                    return null;

                foreach (var taiKhoan in taiKhoanList) // Duyệt qua từng tài khoản
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
                    });

                    // Lọc giao dịch có chứa tài khoản này
                    var giaoDichListTrungTaiKhoan = giaoDichList
                            .Where(x => x.TaiKhoanGoc == taiKhoan || x.TaiKhoanPhu == taiKhoan)
                            .ToList();

                    foreach (var giaoDich in giaoDichListTrungTaiKhoan) // Duyệt qua từng giao dịch
                    {
                        // Nếu tài khoản phụ tồn tại tức là giao dịch giữa 2 tài khoản 1 chủ nhân nên k cần thống kê
                        if (giaoDich.TaiKhoanPhu != null)
                        {
                            continue;
                        }
                        // Nếu tài khoản này là tài khoản gốc (thêm vào tổng chi)
                        if (giaoDich.TaiKhoanGoc == taiKhoan)
                        {
                            
                            var thongKe = thongKeTaiKhoanResponseList.FirstOrDefault(x => x.TaiKhoanId == taiKhoan.Id);
                            if (giaoDich.TheLoai.PhanLoai == "Thu")
                            {
                                if (thongKe != null)
                                {
                                    thongKe.TongThu += giaoDich.TongTien; // Cộng vào tổng thu
                                    thongKe.SoLuongGiaoDichThu += 1; // Tăng số lượng giao dịch thu
                                }
                            }
                            else
                            {
                                if (thongKe != null)
                                {
                                    thongKe.TongChi += giaoDich.TongTien; // Cộng vào tổng chi
                                    thongKe.SoLuongGiaoDichChi += 1; // Tăng số lượng giao dịch chi
                                }
                            }
                        }
                    }
                }
                return thongKeTaiKhoanResponseList;
            }
        }
    }

    public static bool CheckDate(DateTime? TuNgay, DateTime? DenNgay)
    {
        if (TuNgay == null || DenNgay == null) return false;
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