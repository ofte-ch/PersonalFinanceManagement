using Application.Interface;
using Application.Response;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LoaiTaiKhoanFeatures;

public class LoaiTaiKhoanFeatures
{
    // Commands
    public class Create : BaseFeature
    {
        public string Ten { get; set; }
        public class Handler : BaseHandler<Create>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            public Handler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context) {
                _httpContextAccessor = httpContextAccessor;
            }

            public async override Task<IResponse> Handle(Create request, CancellationToken cancellationToken)
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value;

                // Kiểm tra nếu UserId không có trong claim
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return null;
                }

                var loaiTaiKhoan = new LoaiTaiKhoan
                {
                    Ten = request.Ten,
                    User = _context.Users.Where(x => x.Id == int.Parse(userIdClaim)).FirstOrDefault()
                };

                // Kiểm tra validation của đối tượng loaiTaiKhoan
                var validationContext = new ValidationContext(loaiTaiKhoan, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(loaiTaiKhoan, validationContext, validationResults, validateAllProperties: true);

                // Nếu có lỗi validation, trả về thông báo lỗi
                if (!isValid)
                {
                    var errorMessages = string.Join("\n", validationResults.Select(vr => vr.ErrorMessage));
                    // Trả về tất cả các lỗi validation dưới dạng Response
                    return new ValidationFailResponse(errorMessages);
                }

                _context.LoaiTaiKhoan.Add(loaiTaiKhoan);
                await _context.SaveChangesAsync();
                return new SuccessResponse(loaiTaiKhoan.Id);
            }
        }
    }
    public class Update : BaseFeature
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public class Handler : BaseHandler<Update>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            public Handler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
            {
                _httpContextAccessor = httpContextAccessor;
            }
            public async override Task<IResponse> Handle(Update request, CancellationToken cancellationToken)
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value;

                if(string.IsNullOrEmpty(userIdClaim))
                {
                    return null;
                }

                var loaiTaiKhoan = _context.LoaiTaiKhoan.Where(x => x.Id == request.Id && x.User.Id == int.Parse(userIdClaim)).FirstOrDefault();
                if (loaiTaiKhoan == null) return new NotFoundResponse("Không tìm thấy loại tài khoản cần cập nhật");
                else
                {
                    loaiTaiKhoan.Ten = request.Ten;

                    // Kiểm tra validation của đối tượng loaiTaiKhoan
                    var validationContext = new ValidationContext(loaiTaiKhoan, serviceProvider: null, items: null);
                    var validationResults = new List<ValidationResult>();
                    bool isValid = Validator.TryValidateObject(loaiTaiKhoan, validationContext, validationResults, validateAllProperties: true);

                    // Nếu có lỗi validation, trả về thông báo lỗi
                    if (!isValid)
                    {
                        var errorMessages = string.Join("\n", validationResults.Select(vr => vr.ErrorMessage));
                        // Trả về tất cả các lỗi validation dưới dạng Response
                        return new ValidationFailResponse(errorMessages);
                    }

                    await _context.SaveChangesAsync();
                    return new SuccessResponse(loaiTaiKhoan.Id);
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

                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return null;
                }

                var loaiTaiKhoan = _context.LoaiTaiKhoan.Where(x => x.Id == request.Id && x.User.Id==int.Parse(userIdClaim)).FirstOrDefault();
                if (loaiTaiKhoan == null) return new NotFoundResponse("Không tìm thấy loại tài khoản cần xóa");
                else
                {
                    _context.LoaiTaiKhoan.Remove(loaiTaiKhoan);
                    await _context.SaveChangesAsync();
                    return new SuccessResponse(loaiTaiKhoan.Id);
                }
            }
        }
    }

    // Queries
    public class GetOne : BaseQuery<LoaiTaiKhoanDTO, GetOne>
    {
        public int Id { get; set; }
        public class Handler : BaseHandler<GetOne>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            public Handler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
            {
                _httpContextAccessor = httpContextAccessor;
            }

            public async override Task<LoaiTaiKhoanDTO> Handle(GetOne query, CancellationToken cancellationToken)
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value;

                if(string.IsNullOrEmpty(userIdClaim))
                {
                    return null;
                }

                var loaiTaiKhoanDTO = await _context.LoaiTaiKhoan
                    .Where(a => a.Id == query.Id && a.User.Id == int.Parse(userIdClaim))
                    .Select(a => new LoaiTaiKhoanDTO(a.Id, a.Ten, a.User.Id))
                    .FirstOrDefaultAsync();

                if (loaiTaiKhoanDTO != null)
                    return loaiTaiKhoanDTO;
                return null;
            }
        }
    }

    public class GetAll : BaseQuery<IEnumerable<LoaiTaiKhoanDTO>, GetAll>
    {
        public class Handler : BaseHandler<GetAll>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            public Handler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
            {
                _httpContextAccessor = httpContextAccessor;
            }
            public async override Task<IEnumerable<LoaiTaiKhoanDTO>> Handle(GetAll query, CancellationToken cancellationToken)
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value;

                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return null;
                }

                var LoaiTaiKhoanList = await _context.LoaiTaiKhoan.Where(ltk => ltk.User.Id == int.Parse(userIdClaim))
                    .Select(ltk => new LoaiTaiKhoanDTO { id = ltk.Id, ten = ltk.Ten, userId = ltk.User.Id }).ToListAsync();

                if (LoaiTaiKhoanList == null)
                {
                    return null;
                }
                return LoaiTaiKhoanList.AsReadOnly();
            }
        }
    }

}