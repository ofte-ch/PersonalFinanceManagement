using Application.Interface;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TheLoaiFeatures.Queries
{
    public class GetTheLoaiByIdQuery : IRequest<TheLoai>
    {
        public int Id { get; set; }
        public class GetTheLoaiByIdQueryHandler : IRequestHandler<GetTheLoaiByIdQuery, TheLoai>
        {
            private readonly IApplicationDbContext _context;
            public GetTheLoaiByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<TheLoai> Handle(GetTheLoaiByIdQuery query, CancellationToken cancellationToken)
            {
                var theLoai = await _context.TheLoai.Where(a => a.Id == query.Id).FirstOrDefaultAsync();
                if (theLoai == null)
                {
                    return null;
                }
                return theLoai;
            }
        }
    }
}
