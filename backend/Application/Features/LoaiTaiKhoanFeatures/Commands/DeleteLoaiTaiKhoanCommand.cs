using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LoaiTaiKhoanFeatures.Commands
{
    public class DeleteLoaiTaiKhoanCommand : IRequest<IResponse>
    {
        public int Id { get; set; }

        public class DeleteLoaiTaiKhoanCommandHandler : IRequestHandler<DeleteLoaiTaiKhoanCommand, IResponse>
        {
            private readonly IApplicationDbContext _context;

            public DeleteLoaiTaiKhoanCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IResponse> Handle(DeleteLoaiTaiKhoanCommand request, CancellationToken cancellationToken)
            {
                var LoaiTaiKhoan = await _context.LoaiTaiKhoan.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
                if (LoaiTaiKhoan == null) return new NotFoundResponse("Không tìm thấy loại tài khoản cần xóa");
                _context.LoaiTaiKhoan.Remove(LoaiTaiKhoan);
                await _context.SaveChangesAsync();
                return new SuccessResponse(LoaiTaiKhoan.Id);
            }
        }
    }
}
