using Application.Interface;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TaiKhoanFeatures.Queries
{
    public class GetTaiKhoanByIdQuery : IRequest<TaiKhoan>
    {
        public int Id { get; set; }
        public class GetTaiKhoanByIdQueryHandler : IRequestHandler<GetTaiKhoanByIdQuery, TaiKhoan>
        {
            private readonly IApplicationDbContext _context;
            public GetTaiKhoanByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<TaiKhoan> Handle(GetTaiKhoanByIdQuery query, CancellationToken cancellationToken)
            {
                var taiKhoan = await _context.TaiKhoan.Where(a => a.Id == query.Id).FirstOrDefaultAsync();
                if (taiKhoan == null)
                {
                    return null;
                }
                return taiKhoan;
            }
        }
    }
}
