using Application.Interface;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TheLoaiFeatures.Queries
{
    public class GetAllTheLoaiQuery : IRequest<IEnumerable<TheLoai>>
    {
        public class GetAllTheLoaiQueryHandler : IRequestHandler<GetAllTheLoaiQuery, IEnumerable<TheLoai>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllTheLoaiQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<TheLoai>> Handle(GetAllTheLoaiQuery query, CancellationToken cancellationToken)
            {
                var TheLoaiList = await _context.TheLoai.ToListAsync();
                if (TheLoaiList == null)
                {
                    return null;
                }
                return TheLoaiList.AsReadOnly();
            }
        }
    }
}
