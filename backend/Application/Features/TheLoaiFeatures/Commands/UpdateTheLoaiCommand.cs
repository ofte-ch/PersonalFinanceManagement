using Application.Interface;
using MediatR;

namespace Application.Features.TheLoaiFeatures.Commands
{
    public class UpdateTheLoaiCommand : IRequest<IResponse>
    {
        public int Id { get; set; }
        public String TenTheLoai { get; set; }
        public String MoTa { get; set; }
        public String PhanLoai { get; set; }

        public class UpdateTheLoaiCommandHandler : IRequestHandler<UpdateTheLoaiCommand, IResponse>
        {
            private readonly IApplicationDbContext _context;
            public UpdateTheLoaiCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IResponse> Handle(UpdateTheLoaiCommand command, CancellationToken cancellationToken)
            {
                var theLoai = _context.TheLoai.Where(x => x.Id == command.Id).FirstOrDefault();
                if (theLoai == null) return new NotFoundResponse("Không tìm thấy thể loại");
                else
                {
                    theLoai.TenTheLoai = command.TenTheLoai;
                    theLoai.MoTa = command.MoTa;
                    theLoai.PhanLoai = command.PhanLoai;
                    await _context.SaveChangesAsync();
                    return new SuccessResponse(theLoai.Id);
                }
            }
        }
    }
}
