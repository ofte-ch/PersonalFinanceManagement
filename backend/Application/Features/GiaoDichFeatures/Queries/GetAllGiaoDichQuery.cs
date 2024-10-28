using Application.Interface;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GiaoDichFeatures.Queries
{
    public class GetAllGiaoDichQuery : IRequest<IEnumerable<GiaoDich>>
    {
        public class GetAllGiaoDichQueryHandler : IRequestHandler<GetAllGiaoDichQuery, IEnumerable<GiaoDich>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllGiaoDichQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<GiaoDich>> Handle(GetAllGiaoDichQuery query, CancellationToken cancellationToken)
            {
                var GiaoDichList = await _context.GiaoDich.ToListAsync();
                if (GiaoDichList == null)
                {
                    return null;
                }
                return GiaoDichList.AsReadOnly();
            }
        }
    }
}
