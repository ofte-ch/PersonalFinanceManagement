using Application.Interface;
using MediatR;

namespace Application.Features.LoaiTaiKhoanFeatures.Commands
{
    public class UpdateLoaiTaiKhoanCommand : IRequest<IResponse>
    {
        public int Id { get; set; }
        public String Ten { get; set; }

        public class UpdateLoaiTaiKhoanCommandHandler : IRequestHandler<UpdateLoaiTaiKhoanCommand, IResponse>
        {
            private readonly IApplicationDbContext _context;
            public UpdateLoaiTaiKhoanCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IResponse> Handle(UpdateLoaiTaiKhoanCommand command, CancellationToken cancellationToken)
            {
                var loaiTaiKhoan = _context.LoaiTaiKhoan.Where(x => x.Id == command.Id).FirstOrDefault();
                if (loaiTaiKhoan == null) return new NotFoundResponse("Không tìm thấy loại tài khoản cần cập nhật");
                else
                {
                    loaiTaiKhoan.Ten = command.Ten;
                    await _context.SaveChangesAsync();
                    return new SuccessResponse(loaiTaiKhoan.Id);
                }
            }
        }
    }
}
