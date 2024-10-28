using Application.Interface;
using Domain.Entities;
using MediatR;

namespace Application.Features.GiaoDichFeatures.Commands
{
    public class UpdateGiaoDichCommand : IRequest<int>
    {
        public int Id { get; set; }
        public String TenGiaoDich { get; set; }
        public DateTime NgayGiaoDich { get; set; }
        public String LoaiGiaoDich { get; set; }
        public ChiTietGiaoDich ChiTietGiaoDich { get; set; }
        public Double TongTien { get; set; }
        public String GhiChu { get; set; }
        public class UpdateGiaoDichCommandHandler : IRequestHandler<UpdateGiaoDichCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public UpdateGiaoDichCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateGiaoDichCommand command, CancellationToken cancellationToken)
            {
                var GiaoDich = _context.GiaoDich.Where(x => x.Id == command.Id).FirstOrDefault();
                if (GiaoDich == null) return default;
                else
                {
                    GiaoDich.TenGiaoDich = command.TenGiaoDich;
                    GiaoDich.NgayGiaoDich = command.NgayGiaoDich;
                    GiaoDich.LoaiGiaoDich = command.LoaiGiaoDich;
                    GiaoDich.ChiTietGiaoDich = command.ChiTietGiaoDich;
                    GiaoDich.TongTien = command.TongTien;
                    GiaoDich.GhiChu = command.GhiChu;
                    await _context.SaveChangesAsync();
                    return GiaoDich.Id;
                }
            }
        }
    }
}
