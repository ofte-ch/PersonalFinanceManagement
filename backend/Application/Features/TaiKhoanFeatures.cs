using Application.Interface;
using Application.Response;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.TaiKhoanFeatures;
public class TaiKhoanFeatures {
    // Queries

    public class GetOne : BaseQuery<TaiKhoan, GetOne>
    {
        public int Id { get; set; }
        public class Handler : BaseHandler<GetOne>
        {
            public Handler(IApplicationDbContext context) : base(context) { }

            public async override Task<TaiKhoan> Handle(GetOne query, CancellationToken cancellationToken)
            {
                var taiKhoan = await _context.TaiKhoan.Where(a => a.Id == query.Id).FirstOrDefaultAsync();
                return taiKhoan;
            }
        }
    }

    public class GetAll : BaseQuery<IEnumerable<TaiKhoan>, GetAll>
    {
        public class Handler : BaseHandler<GetAll>
        {
            public Handler(IApplicationDbContext context) : base(context) { }

            public async override Task<IEnumerable<TaiKhoan>> Handle(GetAll query, CancellationToken cancellationToken)
            {
                var taiKhoanList = await _context.TaiKhoan.ToListAsync();
                if (taiKhoanList != null)
                    return taiKhoanList.AsReadOnly();
                return null;
            }
        }
    }

    // Commands

    public class Create : BaseFeature
    {
        public String TenTaiKhoan { get; set; }
        public int LoaiTaiKhoan { get; set; }
        public Double SoDu { get; set; }

        public class Handler : BaseHandler<Create>
        {
            public Handler(IApplicationDbContext context) : base(context) { }
            public async override Task<IResponse> Handle(Create command, CancellationToken cancellationToken)
            {
                

                var loaiTaiKhoan = _context.LoaiTaiKhoan.Where(x => x.Id == command.LoaiTaiKhoan).FirstOrDefault();
                if (loaiTaiKhoan == null) return new NotFoundResponse("Không tìm thấy loại tài khoản!");
               
                var TaiKhoan = new TaiKhoan
                {
                      TenTaiKhoan = command.TenTaiKhoan,
                      SoDu = command.SoDu,
                      LoaiTaiKhoan = loaiTaiKhoan
                };
                
                // Kiểm tra validation của đối tượng TaiKhoan
                var validationContext = new ValidationContext(TaiKhoan, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(TaiKhoan, validationContext, validationResults, validateAllProperties: true);

                // Nếu có lỗi validation, trả về thông báo lỗi
                if (!isValid)
                {
                    var errorMessages = string.Join("\n", validationResults.Select(vr => vr.ErrorMessage));
                    // Trả về tất cả các lỗi validation dưới dạng Response
                    return new ValidationFailResponse(errorMessages);
                }

                _context.TaiKhoan.Add(TaiKhoan);
                await _context.SaveChangesAsync();
                return new SuccessResponse(TaiKhoan.Id);
            }
        }

    }

    public class Update : BaseFeature
    {
        public int Id { get; set; }
        public String TenTaiKhoan { get; set; }
        public int LoaiTaiKhoan { get; set; }
        public Double SoDu { get; set; }

        public class Handler : BaseHandler<Update>
        {
            public Handler(IApplicationDbContext context) : base(context) { }

            public async override Task<IResponse> Handle(Update command, CancellationToken cancellationToken)
            {
                var TaiKhoan = _context.TaiKhoan.Where(x => x.Id == command.Id).FirstOrDefault();
                if (TaiKhoan == null) return new NotFoundResponse("Không tìm thấy tài khoản!");
                else
                {
                    var loaiTaiKhoan = _context.LoaiTaiKhoan.Where(x => x.Id == command.LoaiTaiKhoan).FirstOrDefault();
                    if (loaiTaiKhoan == null) return new NotFoundResponse("Không tìm thấy loại tài khoản!");

                    TaiKhoan.TenTaiKhoan = command.TenTaiKhoan;
                    TaiKhoan.LoaiTaiKhoan = loaiTaiKhoan;
                    TaiKhoan.SoDu = command.SoDu;

                    // Kiểm tra validation của đối tượng TaiKhoan
                    var validationContext = new ValidationContext(TaiKhoan, serviceProvider: null, items: null);
                    var validationResults = new List<ValidationResult>();
                    bool isValid = Validator.TryValidateObject(TaiKhoan, validationContext, validationResults, validateAllProperties: true);

                    // Nếu có lỗi validation, trả về thông báo lỗi
                    if (!isValid)
                    {
                        var errorMessages = string.Join("\n", validationResults.Select(vr => vr.ErrorMessage));
                        // Trả về tất cả các lỗi validation dưới dạng Response
                        return new ValidationFailResponse(errorMessages);
                    }

                    await _context.SaveChangesAsync();
                    return new SuccessResponse(TaiKhoan.Id);
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
                var TaiKhoan = await _context.TaiKhoan.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
                if (TaiKhoan == null) return new NotFoundResponse("Không tìm thấy tài khoản!");
                _context.TaiKhoan.Remove(TaiKhoan);
                await _context.SaveChangesAsync();
                return new SuccessResponse(TaiKhoan.Id);
            }
        }
    }
}
