using Application.Interface;
using Application.Response;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TheLoaiFeatures;
public class TheLoaiFeatures { 

    // Queries

    public class GetOne : BaseQuery<TheLoai, GetOne>
    {
        public int Id { get; set; }
        public class Handler : BaseHandler<GetOne>
        { 
            public Handler(IApplicationDbContext context) : base(context) { }

            public async override Task<TheLoai> Handle(GetOne query, CancellationToken cancellationToken)
            {
                var theLoai = await _context.TheLoai.Where(a => a.Id == query.Id).FirstOrDefaultAsync();
                return theLoai;
            }
        }
    }

    public class GetAll : BaseQuery<IEnumerable<TheLoai>, GetAll>
    {
        public class Handler : BaseHandler<GetAll>
        {
            public Handler(IApplicationDbContext context) : base(context) { }

            public async override Task<IEnumerable<TheLoai>> Handle(GetAll query, CancellationToken cancellationToken)
            {
                var TheLoaiList = await _context.TheLoai.ToListAsync();
                if (TheLoaiList != null)
                    return TheLoaiList.AsReadOnly();
                return null;
            }
        }
    }

    // Commands
    public class Create : BaseFeature
    {
        public String TenTheLoai { get; set; }
        public String MoTa { get; set; }
        public String PhanLoai { get; set; }

        public class Handler : BaseHandler<Create>
        {
            public Handler(IApplicationDbContext context) : base(context) { }

            public async override Task<IResponse> Handle(Create command, CancellationToken cancellationToken)
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
    public class Update : BaseFeature
    {
        public int Id { get; set; }
        public String TenTheLoai { get; set; }
        public String MoTa { get; set; }
        public String PhanLoai { get; set; }

        public class Handler : BaseHandler<Update>
        {
            public Handler(IApplicationDbContext context) : base(context) { }
            public async override Task<IResponse> Handle(Update command, CancellationToken cancellationToken)
            {
                var theLoai = _context.TheLoai.Where(x => x.Id == command.Id).FirstOrDefault();
                if (theLoai == null) return new NotFoundResponse("Không tìm thấy thể loại");
                else
                {
                    theLoai.TenTheLoai = command.TenTheLoai;
                    theLoai.MoTa = command.MoTa;
                    theLoai.PhanLoai = command.PhanLoai;
                    await _context.SaveChangesAsync();
                    return new SuccessResponse(theLoai.Id);
                }
            }
        }
    }

    public class Delete : BaseFeature
    {
        public int Id { get; set; }
        public class Handler : BaseHandler<Delete>
        {
            public Handler(IApplicationDbContext context) : base(context) { }
            public async override Task<IResponse> Handle(Delete request, CancellationToken cancellationToken)
            {
                var theLoai = _context.TheLoai.Where(x => x.Id == request.Id).FirstOrDefault();
                if (theLoai == null) return new NotFoundResponse("Không tìm thấy thể loại");
                else
                {
                    _context.TheLoai.Remove(theLoai);
                    await _context.SaveChangesAsync();
                    return new SuccessResponse(theLoai.Id);
                }
            }
        }
    }
}