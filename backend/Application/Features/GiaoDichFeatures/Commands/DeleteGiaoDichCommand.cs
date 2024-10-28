using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.GiaoDichFeatures.Commands
{
    public class DeleteGiaoDichCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class DeleteGiaoDichCommandHandler : IRequestHandler<DeleteGiaoDichCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public DeleteGiaoDichCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteGiaoDichCommand request, CancellationToken cancellationToken)
            {
                var GiaoDich = await _context.GiaoDich.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
                if (GiaoDich == null) return default;
                _context.GiaoDich.Remove(GiaoDich);
                await _context.SaveChangesAsync();
                return GiaoDich.Id;
            }
        }
    }
}
