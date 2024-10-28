using Application.Interface;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TaiKhoanFeatures.Queries
{
    public class GetAllTaiKhoanQuery : IRequest<IEnumerable<TaiKhoan>>
    {
        public class GetAllTaiKhoanQueryHandler : IRequestHandler<GetAllTaiKhoanQuery, IEnumerable<TaiKhoan>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllTaiKhoanQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<TaiKhoan>> Handle(GetAllTaiKhoanQuery query, CancellationToken cancellationToken)
            {
                var TaiKhoanList = await _context.TaiKhoan.ToListAsync();
                if (TaiKhoanList == null)
                {
                    return null;
                }
                return TaiKhoanList.AsReadOnly();
            }
        }
    }
}
