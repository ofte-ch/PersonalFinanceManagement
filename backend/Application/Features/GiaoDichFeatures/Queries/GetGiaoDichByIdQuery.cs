using Application.Interface;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.GiaoDichFeatures.Queries
{
    public class GetGiaoDichByIdQuery : IRequest<GiaoDich>
    {
        public int Id { get; set; }
        public class GetGiaoDichByIdQueryHandler : IRequestHandler<GetGiaoDichByIdQuery, GiaoDich>
        {
            private readonly IApplicationDbContext _context;
            public GetGiaoDichByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<GiaoDich> Handle(GetGiaoDichByIdQuery query, CancellationToken cancellationToken)
            {
                var GiaoDich = await _context.GiaoDich.Where(a => a.Id == query.Id).FirstOrDefaultAsync();
                if (GiaoDich == null)
                {
                    return null;
                }
                return GiaoDich;
            }
        }
    }
}
