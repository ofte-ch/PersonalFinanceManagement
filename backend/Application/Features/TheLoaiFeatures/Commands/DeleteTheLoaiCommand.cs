using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TheLoaiFeatures.Commands
{
    public class DeleteTheLoaiCommand : IRequest<IResponse>
    {
        public int Id { get; set; }

        public class DeleteTheLoaiCommandHandler : IRequestHandler<DeleteTheLoaiCommand, IResponse>
        {
            private readonly IApplicationDbContext _context;

            public DeleteTheLoaiCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IResponse> Handle(DeleteTheLoaiCommand request, CancellationToken cancellationToken)
            {
                var theLoai = await _context.TheLoai.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
                if (theLoai == null) return new NotFoundResponse("Không tìm thấy thể loại");
                _context.TheLoai.Remove(theLoai);
                await _context.SaveChangesAsync();
                return new SuccessResponse(theLoai.Id);
            }
        }
    }
}
