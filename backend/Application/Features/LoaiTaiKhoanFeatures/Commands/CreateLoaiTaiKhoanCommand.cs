using Application.Interface;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LoaiTaiKhoanFeatures.Commands
{
    public class CreateLoaiTaiKhoanCommand : IRequest<IResponse>
    {
        public String Ten { get; set; }

        public class CreateLoaiTaiKhoanCommandHandler : IRequestHandler<CreateLoaiTaiKhoanCommand, IResponse>
        {
            private readonly IApplicationDbContext _context;
            public CreateLoaiTaiKhoanCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IResponse> Handle(CreateLoaiTaiKhoanCommand request, CancellationToken cancellationToken)
            {
                var loaiTaiKhoan = new LoaiTaiKhoan
                {
                    Ten = request.Ten
                };
                _context.LoaiTaiKhoan.Add(loaiTaiKhoan);
                await _context.SaveChangesAsync();
                return new SuccessResponse(loaiTaiKhoan.Id);
            }
        }
    }
}
