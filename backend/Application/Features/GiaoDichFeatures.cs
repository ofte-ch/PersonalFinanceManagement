using Application.Interface;
using Application.Response;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Application.Features.GiaoDichFeatures;

// Commands

public class GiaoDichFeatures
{
    public class Create : BaseFeature
    {
        public String TenGiaoDich { get; set; }
        public DateTime NgayGiaoDich { get; set; }
        public String LoaiGiaoDich { get; set; }
        public Collection<int> TaiKhoan { get; set; }
        public int TheLoai { get; set; }
        public Double TongTien { get; set; }
        public String GhiChu { get; set; }

        public class Handler : BaseHandler<Create>
        {
            public Handler(IApplicationDbContext context) : base(context)
            {
            }
            public async override Task<IResponse> Handle(Create command, CancellationToken cancellationToken)
            {
                if (command.TaiKhoan.Count == 0 || command.TaiKhoan.Count > 2)
                {
                    return new BadRequestResponse("Số lượng tài khoản tối thiểu là 1, tối đa là 2");
                }
                var TaiKhoanGiaoDich = _context.TaiKhoan.Where(x => command.TaiKhoan.Contains(x.Id)).ToList();
                if (TaiKhoanGiaoDich.Count == 0)
                {
                    return new NotFoundResponse("Không tìm thấy tài khoản");
                }
                var TheLoai = _context.TheLoai.Where(x => x.Id == command.TheLoai).FirstOrDefault();
                if (TheLoai == null)
                {
                    return new NotFoundResponse("Thể loại không tồn tại");
                }
                var ChiTietGiaoDich = new ChiTietGiaoDich
                {
                    TaiKhoanGiaoDich = TaiKhoanGiaoDich,
                    TheLoai = TheLoai,
                };
                var GiaoDich = new GiaoDich
                {
                    TenGiaoDich = command.TenGiaoDich,
                    NgayGiaoDich = command.NgayGiaoDich,
                    LoaiGiaoDich = command.LoaiGiaoDich,
                    ChiTietGiaoDich = ChiTietGiaoDich,
                    TongTien = command.TongTien,
                    GhiChu = command.GhiChu
                };
                if (TaiKhoanGiaoDich.Count == 1)
                {
                    if (TheLoai.PhanLoai == "Thu") // dành cho giao dịch nếu dùng 1 tài khoản
                    {
                        TaiKhoanGiaoDich[0].CapNhatSoDu(command.TongTien); // nếu cộng thêm tiền thì điền số dương
                    }
                    else
                    {
                        TaiKhoanGiaoDich[0].CapNhatSoDu(-command.TongTien); // nếu trừ tiền thì điền số âm
                    }
                }
                else //-----------------------------------------------chổ này cần xử lý lại cho giao dịch nếu dùng 2 tài khoản-----------------------------------//
                {
                    TaiKhoanGiaoDich[0].CapNhatSoDu(command.TongTien);
                    TaiKhoanGiaoDich[1].CapNhatSoDu(-command.TongTien);
                }
                foreach (var item in TaiKhoanGiaoDich) // kiểm tra lại sau khi giao dịch có tài khoản nào bị âm số dư không
                {
                    if(item.SoDu < 0 && GiaoDich.LoaiGiaoDich=="CK") // nếu số dư âm và loại giao dịch là chuyển khoản thì trả lại số dư
                    {
                        return new BadRequestResponse("Số dư tài khoản không đủ để thực hiện giao dịch chuyển khoản");
                    };
                }
                _context.GiaoDich.Add(GiaoDich);
                await _context.SaveChangesAsync();
                return new SuccessResponse(GiaoDich.Id);
            }
        }
    }
    public class Update : BaseFeature
    {
        public int Id { get; set; }
        public String TenGiaoDich { get; set; }
        public DateTime NgayGiaoDich { get; set; }
        public String LoaiGiaoDich { get; set; }
        public Collection<int> TaiKhoan { get; set; }
        public int TheLoai { get; set; }
        public Double TongTien { get; set; }
        public String GhiChu { get; set; }

        public class Handler : BaseHandler<Update>
        {
            public Handler(IApplicationDbContext context) : base(context)
            {
            }
            public async override Task<IResponse> Handle(Update command, CancellationToken cancellationToken)
            {
                if (command.TaiKhoan.Count == 0 || command.TaiKhoan.Count > 2)
                {
                    return new BadRequestResponse("Số lượng tài khoản tối thiểu là 1, tối đa là 2");
                }
                var TaiKhoanGiaoDich = _context.TaiKhoan.Where(x => command.TaiKhoan.Contains(x.Id)).ToList();
                if (TaiKhoanGiaoDich.Count == 0)
                {
                    return new NotFoundResponse("Không tìm thấy tài khoản");
                }
                var TheLoai = _context.TheLoai.Where(x => x.Id == command.TheLoai).FirstOrDefault();
                if (TheLoai == null)
                {
                    return new NotFoundResponse("Thể loại không tồn tại");
                }
                var ChiTietGiaoDich = new ChiTietGiaoDich
                {
                    TaiKhoanGiaoDich = TaiKhoanGiaoDich,
                    TheLoai = TheLoai,
                };
                var GiaoDich = _context.GiaoDich.Where(x => x.Id == command.Id).FirstOrDefault();
                if (GiaoDich == null)
                {
                    return new NotFoundResponse("Không tìm thấy giao dịch");
                }
                else
                {
                    GiaoDich.TenGiaoDich = command.TenGiaoDich;
                    GiaoDich.NgayGiaoDich = command.NgayGiaoDich;
                    GiaoDich.LoaiGiaoDich = command.LoaiGiaoDich;
                    GiaoDich.ChiTietGiaoDich = ChiTietGiaoDich;
                    GiaoDich.TongTien = command.TongTien;
                    GiaoDich.GhiChu = command.GhiChu;
                    await _context.SaveChangesAsync();
                    return new SuccessResponse(GiaoDich.Id);
                }
            }
        }
    }
    public class Delete : BaseFeature
    {
        public int Id { get; set; }
        public class Handler : BaseHandler<Delete>
        {
            public Handler(IApplicationDbContext context) : base(context) { }
            public async override Task<IResponse> Handle(Delete request, CancellationToken cancellationToken)
            {
                var GiaoDich = _context.GiaoDich.Where(x => x.Id == request.Id).FirstOrDefault();
                if (GiaoDich == null) return new NotFoundResponse("Không tìm thấy giao dịch");
                else
                {
                    _context.GiaoDich.Remove(GiaoDich);
                    await _context.SaveChangesAsync();
                    return new SuccessResponse(GiaoDich.Id);
                }
            }
        }
    }

    // Queries
    public class GetOne : BaseQuery<GiaoDich, GetOne>
    {
        public int Id { get; set; }
        public class Handler : BaseHandler<GetOne>
        {
            public Handler(IApplicationDbContext context) : base(context) { }
            public async override Task<GiaoDich> Handle(GetOne query, CancellationToken cancellationToken)
            {
                return _context.GiaoDich.Where(x => x.Id == query.Id).FirstOrDefault();
            }
        }
    }

    public class GetAll : BaseQuery<IEnumerable<GiaoDich>, GetAll>
    {
        public class Handler : BaseHandler<GetAll>
        {
            public Handler(IApplicationDbContext context) : base(context) { }
            public async override Task<IEnumerable<GiaoDich>> Handle(GetAll query, CancellationToken cancellationToken)
            {
                var LoaiTaiKhoanList = await _context.GiaoDich.ToListAsync();
                if (LoaiTaiKhoanList == null)
                {
                    return null;
                }
                return LoaiTaiKhoanList.AsReadOnly();
            }
        }
    }

}