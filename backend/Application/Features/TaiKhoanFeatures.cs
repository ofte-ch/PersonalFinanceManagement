using Application.Interface;
using Application.Response;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.TaiKhoanFeatures;
public class TaiKhoanFeatures {
    // Queries

    public class GetOne : BaseQuery<TaiKhoanDTO, GetOne>
    {
        public int Id { get; set; }
        public class Handler : BaseHandler<GetOne>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            public Handler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context) {
                _httpContextAccessor = httpContextAccessor;
            }

            public async override Task<TaiKhoanDTO> Handle(GetOne query, CancellationToken cancellationToken)
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value;

                // Kiểm tra nếu UserId không có trong claim
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return null;
                }

                var taiKhoan = await _context.TaiKhoan
                .Where(t => t.Id == query.Id && t.User.Id == int.Parse(userIdClaim))
                .Select(t => new TaiKhoanDTO
                {
                    id = t.Id,
                    tenTaiKhoan = t.TenTaiKhoan,
                    loaiTaiKhoanId = t.LoaiTaiKhoanId,
                    soDu = t.SoDu
                })
                .FirstOrDefaultAsync(cancellationToken);
                if(taiKhoan == null)
                {
                    throw new KeyNotFoundException($"Tài khoản với id {query.Id}không tồn tại !");
                }
                return taiKhoan;

            }
        }
    }

    public class GetAll : BaseQuery<PagedResult<TaiKhoanDTO>, GetAll>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string? Keyword { get; set; }

        public class Handler : BaseHandler<GetAll>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context) {
                _httpContextAccessor = httpContextAccessor;
            }

            public async override Task<PagedResult<TaiKhoanDTO>> Handle(GetAll query, CancellationToken cancellationToken)
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value;

                // Kiểm tra nếu UserId không có trong claim
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return null;
                }

                // Lọc theo UserId
                var queryable = _context.TaiKhoan
                    .Where(t => t.User.Id == int.Parse(userIdClaim));

                // Lọc theo từ khóa nếu có
                if (!string.IsNullOrEmpty(query.Keyword))
                {
                    queryable = queryable.Where(t => t.TenTaiKhoan.Contains(query.Keyword) || t.LoaiTaiKhoan.Ten.Contains(query.Keyword));
                }
                var totalCount = await queryable.CountAsync(cancellationToken);

                var taiKhoanList = await queryable
               .Skip((query.Page - 1) * query.Size) // Bỏ qua các bản ghi trước đó
               .Take(query.Size) // Lấy số lượng bản ghi theo `Size`
               .Select(t => new TaiKhoanDTO
               {
                   id = t.Id,
                   tenTaiKhoan = t.TenTaiKhoan,
                   loaiTaiKhoanId = t.LoaiTaiKhoanId,
                   soDu = t.SoDu,
               })
               .ToListAsync(cancellationToken);

                return new PagedResult<TaiKhoanDTO>
                {
                    Data = taiKhoanList,
                    TotalCount = totalCount,
                    PageSize = query.Size,
                    CurrentPage = query.Page,
                    Keyword = query.Keyword
                };
            }
        }
    }

    // Commands
    public class Create : BaseFeature
    {
        public string TenTaiKhoan { get; set; }
        public int LoaiTaiKhoanId { get; set; }
        public double SoDu { get; set; }

        public class Handler : BaseHandler<Create>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            public Handler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context) {
                _httpContextAccessor = httpContextAccessor;
            }
            public async override Task<IResponse> Handle(Create command, CancellationToken cancellationToken)
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value;

                // Kiểm tra nếu UserId không có trong claim
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return null;
                }

                var loaiTaiKhoan = _context.LoaiTaiKhoan.Where(x => x.Id == command.LoaiTaiKhoanId).FirstOrDefault();
                if (loaiTaiKhoan == null) return new NotFoundResponse("Không tìm thấy loại tài khoản!");
               
                var TaiKhoan = new TaiKhoan
                {
                      TenTaiKhoan = command.TenTaiKhoan,
                      SoDu = command.SoDu,
                      LoaiTaiKhoan = loaiTaiKhoan,
                      User = _context.Users.Where(x=>x.Id == int.Parse(userIdClaim)).FirstOrDefault()
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
                return new SuccessResponse($"Thêm thành công tài khoản mới có id là: {TaiKhoan.Id}");
            }
        }

    }

    public class Update : BaseFeature
    {
        public int Id { get; set; }
        public string TenTaiKhoan { get; set; }
        public int LoaiTaiKhoanId { get; set; }
        public Double SoDu { get; set; }

        public class Handler : BaseHandler<Update>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            public Handler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
            {
                _httpContextAccessor = httpContextAccessor;
            }

            public async override Task<IResponse> Handle(Update command, CancellationToken cancellationToken)
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value;

                // Kiểm tra nếu UserId không có trong claim
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return null;
                }

                var TaiKhoan = _context.TaiKhoan.Where(x => x.Id == command.Id && x.User.Id == int.Parse(userIdClaim) ).FirstOrDefault();
                if (TaiKhoan == null) return new NotFoundResponse("Không tìm thấy tài khoản!");
                else
                {
                    var loaiTaiKhoan = _context.LoaiTaiKhoan.Where(x => x.Id == command.LoaiTaiKhoanId).FirstOrDefault();
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
                    return new SuccessResponse($"Cập nhật tài khoản thành công: {TaiKhoan.Id}");
                }
            }
        }
    }

    public class Delete : BaseFeature
    {
        public int Id { get; set; }

        public class Handler : BaseHandler<Delete>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            public Handler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
            {
                _httpContextAccessor = httpContextAccessor;
            }
            public async override Task<IResponse> Handle(Delete request, CancellationToken cancellationToken)
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value;

                // Kiểm tra nếu UserId không có trong claim
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return null;
                }

                var TaiKhoan = await _context.TaiKhoan.Where(a => a.Id == request.Id && a.User.Id == int.Parse(userIdClaim)).FirstOrDefaultAsync();
                if (TaiKhoan == null) return new NotFoundResponse("Không tìm thấy tài khoản!");
                _context.TaiKhoan.Remove(TaiKhoan);
                await _context.SaveChangesAsync();
                return new SuccessResponse($"Xóa tài khoản thành công: {TaiKhoan.Id}");
            }
        }
    }
}
