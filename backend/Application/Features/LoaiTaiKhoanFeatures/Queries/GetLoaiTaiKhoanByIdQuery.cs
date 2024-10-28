using Application.Interface;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LoaiTaiKhoanFeatures.Queries
{
    public class GetLoaiTaiKhoanByIdQuery : IRequest<LoaiTaiKhoan>
    {
        public int Id { get; set; }
        public class GetLoaiTaiKhoanByIdQueryHandler : IRequestHandler<GetLoaiTaiKhoanByIdQuery, LoaiTaiKhoan>
        {
            private readonly IApplicationDbContext _context;
            public GetLoaiTaiKhoanByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<LoaiTaiKhoan> Handle(GetLoaiTaiKhoanByIdQuery query, CancellationToken cancellationToken)
            {
                var LoaiTaiKhoan = await _context.LoaiTaiKhoan.Where(a => a.Id == query.Id).FirstOrDefaultAsync();
                if (LoaiTaiKhoan == null)
                {
                    return null;
                }
                return LoaiTaiKhoan;
            }
        }
    }
}
