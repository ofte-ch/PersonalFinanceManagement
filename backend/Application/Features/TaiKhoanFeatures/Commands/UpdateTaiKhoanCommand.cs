using Application.Interface;
using Domain.Entities;
using MediatR;

namespace Application.Features.TaiKhoanFeatures.Commands
{
    public class UpdateTaiKhoanCommand : IRequest<IResponse>
    {
        public int Id { get; set; }
        public String TenTaiKhoan { get; set; }
        public int LoaiTaiKhoan { get; set; }
        public Double SoDu { get; set; }

        public class UpdateTaiKhoanCommandHandler : IRequestHandler<UpdateTaiKhoanCommand, IResponse>
        {
            private readonly IApplicationDbContext _context;
            public UpdateTaiKhoanCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IResponse> Handle(UpdateTaiKhoanCommand command, CancellationToken cancellationToken)
            {
                var TaiKhoan = _context.TaiKhoan.Where(x => x.Id == command.Id).FirstOrDefault();
                if (TaiKhoan == null) return new NotFoundResponse("Không tìm thấy tài khoản!");
                else
                {
                    var loaiTaiKhoan = _context.LoaiTaiKhoan.Where(x => x.Id == command.LoaiTaiKhoan).FirstOrDefault();
                    TaiKhoan.TenTaiKhoan = command.TenTaiKhoan;
                    TaiKhoan.LoaiTaiKhoan = loaiTaiKhoan;
                    TaiKhoan.SoDu = command.SoDu;
                    await _context.SaveChangesAsync();
                    return new SuccessResponse(TaiKhoan.Id);
                }
            }
        }
    }
}
