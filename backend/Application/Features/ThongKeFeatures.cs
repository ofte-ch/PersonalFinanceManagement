using Application.Interface;
using Application.Response;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ThongKeFeatures;
public class ThongKeFeatures
{
    public class GetThongKe : BaseQuery<IEnumerable<ThongKeTheLoaiResponse>, GetThongKe>
    {
        public int UserId { get; set; } // khi nào có đăng nhập thì sài tới cái này
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public class Handler : BaseHandler<GetThongKe>
        {
            public Handler(IApplicationDbContext context) : base(context) { }

            public async override Task<IEnumerable<ThongKeTheLoaiResponse>> Handle(GetThongKe query, CancellationToken cancellationToken)
            {
                var giaoDichList = await _context.GiaoDich.Where(a => a.NgayGiaoDich >= query.TuNgay && a.NgayGiaoDich <= query.DenNgay).ToListAsync();
                var theLoaiList = await _context.TheLoai.ToListAsync();
                var thongKeTheLoaiResponseList = new List<ThongKeTheLoaiResponse>();
                if (giaoDichList == null || theLoaiList == null)
                    return null;
                foreach (var theLoai in theLoaiList) // duyệt qua từng thể loại
                {
                    thongKeTheLoaiResponseList.Add(new ThongKeTheLoaiResponse
                    {
                        TheLoaiId = theLoai.Id,
                        TenTheLoai = theLoai.TenTheLoai,
                        TongThu = 0,
                        TongChi = 0
                    }); // tạo ra 1 list thống kê theo thể loại

                    var giaoDichListTrungTheLoai = giaoDichList.Where(x => x.ChiTietGiaoDich.TheLoai.Id == theLoai.Id).ToList(); // lọc ra các giao dịch trùng thể loại
                    foreach (var giaoDich in giaoDichListTrungTheLoai) // duyệt qua từng giao dịch trùng thể loại
                    {
                        if (giaoDich.ChiTietGiaoDich.TheLoai.PhanLoai == "Thu") // nếu là thu thì cộng vào tổng thu
                        {
                            thongKeTheLoaiResponseList.Where(x => x.TheLoaiId == theLoai.Id).FirstOrDefault().TongThu += giaoDich.TongTien;
                        }
                        else // nếu là chi thì cộng vào tổng chi
                        {
                            thongKeTheLoaiResponseList.Where(x => x.TheLoaiId == theLoai.Id).FirstOrDefault().TongChi += giaoDich.TongTien;
                        }
                    }
                }

                return thongKeTheLoaiResponseList;

            }
        }

        
    }
    public class ThongKeTheLoaiResponse
    {
        public int TheLoaiId { get; set; }
        public string TenTheLoai { get; set; }
        public double TongThu { get; set; }
        public double TongChi { get; set; }

    }
}