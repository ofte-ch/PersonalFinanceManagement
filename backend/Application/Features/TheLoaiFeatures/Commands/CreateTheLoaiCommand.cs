using Application.Interface;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TheLoaiFeatures.Commands
{
    public class CreateTheLoaiCommand : IRequest<IResponse>
    {
        public String TenTheLoai { get; set; }
        public String MoTa { get; set; }
        public String PhanLoai { get; set; }

        public class CreateTheLoaiCommandHandler : IRequestHandler<CreateTheLoaiCommand, IResponse>
        {
            private readonly IApplicationDbContext _context;
            public CreateTheLoaiCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IResponse> Handle(CreateTheLoaiCommand command, CancellationToken cancellationToken)
            {
                var theLoai = new TheLoai
                {
                    TenTheLoai = command.TenTheLoai,
                    MoTa = command.MoTa,
                    PhanLoai = command.PhanLoai
                };
                _context.TheLoai.Add(theLoai);
                await _context.SaveChangesAsync();
                return new CreatedResponse(theLoai.Id);
            }
        }
    }
}
