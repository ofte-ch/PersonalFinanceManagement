using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TaiKhoanFeatures.Commands
{
    public class DeleteTaiKhoanCommand : IRequest<IResponse>
    {
        public int Id { get; set; }

        public class DeleteTaiKhoanCommandHandler : IRequestHandler<DeleteTaiKhoanCommand, IResponse>
        {
            private readonly IApplicationDbContext _context;

            public DeleteTaiKhoanCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IResponse> Handle(DeleteTaiKhoanCommand request, CancellationToken cancellationToken)
            {
                var TaiKhoan = await _context.TaiKhoan.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
                if (TaiKhoan == null) return new NotFoundResponse("Không tìm thấy tài khoản!");
                _context.TaiKhoan.Remove(TaiKhoan);
                await _context.SaveChangesAsync();
                return new SuccessResponse(TaiKhoan.Id);
            }
        }
    }
}
