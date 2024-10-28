using Application.Interface;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LoaiTaiKhoanFeatures.Queries
{
    public class GetAllLoaiTaiKhoanQuery : IRequest<IEnumerable<LoaiTaiKhoan>>
    {
        public class GetAllLoaiTaiKhoanQueryHandler : IRequestHandler<GetAllLoaiTaiKhoanQuery, IEnumerable<LoaiTaiKhoan>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllLoaiTaiKhoanQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<LoaiTaiKhoan>> Handle(GetAllLoaiTaiKhoanQuery query, CancellationToken cancellationToken)
            {
                var LoaiTaiKhoanList = await _context.LoaiTaiKhoan.ToListAsync();
                if (LoaiTaiKhoanList == null)
                {
                    return null;
                }
                return LoaiTaiKhoanList.AsReadOnly();
            }
        }
    }
}
