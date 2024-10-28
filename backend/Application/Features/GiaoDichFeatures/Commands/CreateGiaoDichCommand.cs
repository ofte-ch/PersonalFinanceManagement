using Application.Interface;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GiaoDichFeatures.Commands
{
    public class CreateGiaoDichCommand : IRequest<IResponse>
    {
        public String TenGiaoDich { get; set; }
        public DateTime NgayGiaoDich { get; set; }
        public String LoaiGiaoDich { get; set; }
        public Collection<int> TaiKhoan { get; set; }
        public int TheLoai { get; set; }
        public Double TongTien { get; set; }
        public String GhiChu { get; set; }

        public class CreateGiaoDichCommandHandler : IRequestHandler<CreateGiaoDichCommand, IResponse>
        {
            private readonly IApplicationDbContext _context;
            public CreateGiaoDichCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IResponse> Handle(CreateGiaoDichCommand command, CancellationToken cancellationToken)
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
                if (TheLoai == null) {
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
                _context.GiaoDich.Add(GiaoDich);
                await _context.SaveChangesAsync();
                return new SuccessResponse(GiaoDich.Id);
            }
        }
    }
}
