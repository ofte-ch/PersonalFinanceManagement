using Application.Interface;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TaiKhoanFeatures.Commands
{
    public class CreateTaiKhoanCommand : IRequest<IResponse>
    {
        public String TenTaiKhoan { get; set; }
        public int LoaiTaiKhoan { get; set; }
        public Double SoDu { get; set; }

        public class CreateTaiKhoanCommandHandler : IRequestHandler<CreateTaiKhoanCommand, IResponse>
        {
            private readonly IApplicationDbContext _context;
            public CreateTaiKhoanCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IResponse> Handle(CreateTaiKhoanCommand command, CancellationToken cancellationToken)
            {
                var loaiTaiKhoan = _context.LoaiTaiKhoan.Where(x => x.Id == command.LoaiTaiKhoan).FirstOrDefault();
                if (loaiTaiKhoan == null) return new NotFoundResponse("Không tìm thấy loại tài khoản!");
                var TaiKhoan = new TaiKhoan
                {
                    TenTaiKhoan = command.TenTaiKhoan,
                    SoDu = command.SoDu,
                    LoaiTaiKhoan = loaiTaiKhoan
                };
                _context.TaiKhoan.Add(TaiKhoan);
                await _context.SaveChangesAsync();
                return new SuccessResponse(TaiKhoan.Id);
            }
        }
    }
}
