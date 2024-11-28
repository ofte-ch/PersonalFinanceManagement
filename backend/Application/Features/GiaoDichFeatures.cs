using Application.Interface;
using Application.Response;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.GiaoDichFeatures;

// Commands

public class GiaoDichFeatures
{
    public class Create : BaseFeature
    {
        public String TenGiaoDich { get; set; }
        public DateTime NgayGiaoDich { get; set; }
        public String LoaiGiaoDich { get; set; }
        public Collection<int> TaiKhoan { get; set; }
        public int TheLoai { get; set; }
        public Double TongTien { get; set; }
        public String GhiChu { get; set; }

        public class Handler : BaseHandler<Create>
        {
            public Handler(IApplicationDbContext context) : base(context)
            {
            }
            public async override Task<IResponse> Handle(Create command, CancellationToken cancellationToken)
            {
                if (command.TaiKhoan.Count == 0 || command.TaiKhoan.Count > 2)
                {
                    return new BadRequestResponse("Số lượng tài khoản tối thiểu là 1, tối đa là 2");
                }
                var TaiKhoanGiaoDich = _context.TaiKhoan.Where(x => command.TaiKhoan.Contains(x.Id)).ToList();
                if (TaiKhoanGiaoDich.Count == 0)
                {
                    return new NotFoundResponse("Không tìm thấy tài khoản");
                }
                var TheLoai = _context.TheLoai.Where(x => x.Id == command.TheLoai).FirstOrDefault();
                if (TheLoai == null)
                {
                    return new NotFoundResponse("Thể loại không tồn tại");
                }
                var ChiTietGiaoDich = new ChiTietGiaoDich
                {
                    TaiKhoanGiaoDich = TaiKhoanGiaoDich,
                    TheLoai = TheLoai,
                };
                var GiaoDich = new GiaoDich
                {
                    TenGiaoDich = command.TenGiaoDich,
                    NgayGiaoDich = command.NgayGiaoDich,
                    LoaiGiaoDich = command.LoaiGiaoDich,
                    ChiTietGiaoDich = ChiTietGiaoDich,
                    TongTien = command.TongTien,
                    GhiChu = command.GhiChu
                };

                // Kiểm tra validation của đối tượng GiaoDich
                var validationContext = new ValidationContext(GiaoDich, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(GiaoDich, validationContext, validationResults, validateAllProperties: true);

                // Nếu có lỗi validation, trả về thông báo lỗi
                if (!isValid)
                {
                    var errorMessages = string.Join("\n", validationResults.Select(vr => vr.ErrorMessage));
                    // Trả về tất cả các lỗi validation dưới dạng Response
                    return new ValidationFailResponse(errorMessages);
                }

                if (TaiKhoanGiaoDich.Count == 1)
                {
                    if (TheLoai.PhanLoai == "Thu") // dành cho giao dịch nếu dùng 1 tài khoản
                    {
                        TaiKhoanGiaoDich[0].CapNhatSoDu(command.TongTien); // nếu cộng thêm tiền thì điền số dương
                    }
                    else
                    {
                        TaiKhoanGiaoDich[0].CapNhatSoDu(-command.TongTien); // nếu trừ tiền thì điền số âm
                    }
                }
                else //-----------------------------------------------chổ này cần xử lý lại cho giao dịch nếu dùng 2 tài khoản-----------------------------------//
                {
                    TaiKhoanGiaoDich[0].CapNhatSoDu(command.TongTien);
                    TaiKhoanGiaoDich[1].CapNhatSoDu(-command.TongTien);
                }
                //foreach (var item in TaiKhoanGiaoDich) // kiểm tra lại sau khi giao dịch có tài khoản nào bị âm số dư không
                //{
                //    if(item.SoDu < 0 && GiaoDich.LoaiGiaoDich=="CK") // nếu số dư âm và loại giao dịch là chuyển khoản thì trả lại số dư
                //    {
                //        return new BadRequestResponse("Số dư tài khoản không đủ để thực hiện giao dịch chuyển khoản");
                //    };
                //}
                _context.GiaoDich.Add(GiaoDich);
                await _context.SaveChangesAsync();
                return new SuccessResponse($"Thêm giao dịch mới thành công với mã giao dịch:{GiaoDich.Id} ");
            }
        }
    }
    public class Update : BaseFeature
    {
        public int Id { get; set; }
        public String TenGiaoDich { get; set; }
        public DateTime NgayGiaoDich { get; set; }
        public String LoaiGiaoDich { get; set; }
        public Collection<int> TaiKhoan { get; set; }
        public int TheLoai { get; set; }
        public Double TongTien { get; set; }
        public String GhiChu { get; set; }

        public class Handler : BaseHandler<Update>
        {
            public Handler(IApplicationDbContext context) : base(context)
            {
            }
            public async override Task<IResponse> Handle(Update command, CancellationToken cancellationToken)
            {
                if (command.TaiKhoan.Count == 0 || command.TaiKhoan.Count > 2)
                {
                    return new BadRequestResponse("Số lượng tài khoản tối thiểu là 1, tối đa là 2");
                }
                var TaiKhoanGiaoDich = _context.TaiKhoan.Where(x => command.TaiKhoan.Contains(x.Id)).ToList();
                if (TaiKhoanGiaoDich.Count == 0)
                {
                    return new NotFoundResponse("Không tìm thấy tài khoản");
                }

                var TheLoai = _context.TheLoai.Where(x => x.Id == command.TheLoai).FirstOrDefault();
                if (TheLoai == null)
                {
                    return new NotFoundResponse("Thể loại không tồn tại");
                }
                

                var ChiTietGiaoDich = new ChiTietGiaoDich
                {
                    TaiKhoanGiaoDich = TaiKhoanGiaoDich,
                    TheLoai = TheLoai,
                };
                var GiaoDich = _context.GiaoDich.Where(x => x.Id == command.Id).FirstOrDefault();
                if (GiaoDich == null)
                {
                    return new NotFoundResponse("Không tìm thấy giao dịch");
                }
                else
                {
                    // Kiểm tra tài khoản có thay đổi không
                    var taiKhoanCu = GiaoDich.ChiTietGiaoDich.TaiKhoanGiaoDich.Select(x => x.Id).ToList();
                    bool taiKhoanThayDoi = !command.TaiKhoan.SequenceEqual(taiKhoanCu);

                    if (taiKhoanThayDoi)
                    {
                        // Hoàn trả số dư từ tài khoản cũ
                        double soTienHoanTra = TheLoai.PhanLoai == "Thu" ? -GiaoDich.TongTien : GiaoDich.TongTien;

                        // Nếu tài khoản cũ có 1 tài khoản thì chỉ hoàn trả cho tài khoản đó
                        TaiKhoanGiaoDich[0].CapNhatSoDu(soTienHoanTra);
                        if (taiKhoanCu.Count == 2) // Nếu tài khoản cũ có 2 tài khoản
                        {
                            TaiKhoanGiaoDich[1].CapNhatSoDu(-soTienHoanTra);
                        }

                        // Cập nhật số dư cho tài khoản mới
                        double soTienCapNhat = TheLoai.PhanLoai == "Thu" ? command.TongTien : -command.TongTien;

                        // Nếu tài khoản mới chỉ có 1 tài khoản thì chỉ cập nhật cho tài khoản đó
                        TaiKhoanGiaoDich[0].CapNhatSoDu(soTienCapNhat);
                        if (command.TaiKhoan.Count == 2) // Nếu tài khoản mới có 2 tài khoản
                        {
                            TaiKhoanGiaoDich[1].CapNhatSoDu(-soTienCapNhat);
                        }
                    }


                    // Kiểm tra loại giao dịch có thay đổi không
                    if (GiaoDich.ChiTietGiaoDich.TheLoai.PhanLoai != ChiTietGiaoDich.TheLoai.PhanLoai)
                    {
                        int heSo = GiaoDich.ChiTietGiaoDich.TheLoai.PhanLoai == "Thu" ? -2 : 2;
                        TaiKhoanGiaoDich[0].CapNhatSoDu(command.TongTien * heSo);
                        if (TaiKhoanGiaoDich.Count == 2) TaiKhoanGiaoDich[1].CapNhatSoDu(-command.TongTien * heSo);
                    }

                    // Kiểm tra số tiền có thay đổi không
                    if (GiaoDich.TongTien != command.TongTien)
                    {
                        double chenhlechTien = command.TongTien - GiaoDich.TongTien;
                        TaiKhoanGiaoDich[0].CapNhatSoDu(TheLoai.PhanLoai == "Thu" ? chenhlechTien : -chenhlechTien);
                        if (TaiKhoanGiaoDich.Count == 2) TaiKhoanGiaoDich[1].CapNhatSoDu(TheLoai.PhanLoai == "Thu" ? -chenhlechTien : chenhlechTien);
                    }

                    GiaoDich.TenGiaoDich = command.TenGiaoDich;
                    GiaoDich.NgayGiaoDich = command.NgayGiaoDich;
                    GiaoDich.LoaiGiaoDich = command.LoaiGiaoDich;
                    GiaoDich.ChiTietGiaoDich = ChiTietGiaoDich;
                    GiaoDich.TongTien = command.TongTien;
                    GiaoDich.GhiChu = command.GhiChu;

                    // Kiểm tra validation của đối tượng GiaoDich
                    var validationContext = new ValidationContext(GiaoDich, serviceProvider: null, items: null);
                    var validationResults = new List<ValidationResult>();
                    bool isValid = Validator.TryValidateObject(GiaoDich, validationContext, validationResults, validateAllProperties: true);

                    // Nếu có lỗi validation, trả về thông báo lỗi
                    if (!isValid)
                    {
                        var errorMessages = string.Join("\n", validationResults.Select(vr => vr.ErrorMessage));
                        // Trả về tất cả các lỗi validation dưới dạng Response
                        return new ValidationFailResponse(errorMessages);
                    }

                    

                    await _context.SaveChangesAsync();
                    return new SuccessResponse($"Cập nhật giao dịch thành công: {GiaoDich.Id}");
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
                var GiaoDich = _context.GiaoDich.Where(x => x.Id == request.Id).FirstOrDefault();
                if (GiaoDich == null) return new NotFoundResponse("Không tìm thấy giao dịch");
                else
                {
                    var taiKhoanGiaoDich = GiaoDich.ChiTietGiaoDich.TaiKhoanGiaoDich.ToList();

                    if (taiKhoanGiaoDich.Count == 1)
                    {
                        if (GiaoDich.ChiTietGiaoDich.TheLoai.PhanLoai == "Thu")
                        {
                            taiKhoanGiaoDich[0].CapNhatSoDu(-GiaoDich.TongTien); // Hoàn lại số tiền thu
                        }
                        else
                        {
                            taiKhoanGiaoDich[0].CapNhatSoDu(GiaoDich.TongTien); // Hoàn lại số tiền chi
                        }
                    }
                    else if (taiKhoanGiaoDich.Count == 2)
                    {
                        taiKhoanGiaoDich[0].CapNhatSoDu(-GiaoDich.TongTien); // Hoàn tiền tài khoản 1
                        taiKhoanGiaoDich[1].CapNhatSoDu(GiaoDich.TongTien);  // Trừ tiền tài khoản 2
                    }
                    _context.GiaoDich.Remove(GiaoDich);
                    await _context.SaveChangesAsync();
                    return new SuccessResponse($"Xóa giao dịch thành công, mã giao dịch: {GiaoDich.Id}");
                }
            }
        }
    }

    // Queries
    public class GetOne : BaseQuery<GiaoDich, GetOne>
    {
        public int Id { get; set; }

        public class Handler : BaseHandler<GetOne>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            // Constructor sửa lại tên tham số để không trùng với trường
            public Handler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
            {
                _httpContextAccessor = httpContextAccessor;
            }

            public async override Task<GiaoDich> Handle(GetOne query, CancellationToken cancellationToken)
            {
                // Lấy UserId từ claim
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value;

                // Kiểm tra nếu UserId không có trong claim
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return null;
                }

                // Tìm giao dịch theo Id và UserId
                return await _context.GiaoDich
                    .Where(x => x.Id == query.Id &&
                                x.ChiTietGiaoDich.TaiKhoanGiaoDich.Any(tk => tk.User.Id == int.Parse(userIdClaim)))
                    .FirstOrDefaultAsync(cancellationToken);
            }
        }
    }

    public class GetAll : BaseQuery<PagedResult<GiaoDichDTO>, GetAll>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string? Keyword { get; set; }

        public class Handler : BaseHandler<GetAll>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
            {
                _httpContextAccessor = httpContextAccessor;
            }

            public async override Task<PagedResult<GiaoDichDTO>> Handle(GetAll query, CancellationToken cancellationToken)
            {
                // Lấy UserId từ HttpContext
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return null;  // Trả về null nếu không có UserId
                }

                int userId = int.Parse(userIdClaim);

                // Tạo truy vấn lọc theo UserId
                var queryable = _context.GiaoDich
                    .Where(gd => gd.ChiTietGiaoDich.TaiKhoanGiaoDich.Any(tk => tk.User.Id == userId));

                // Lọc theo từ khóa nếu có
                if (!string.IsNullOrEmpty(query.Keyword))
                {
                    queryable = queryable.Where(gd => gd.TenGiaoDich.Contains(query.Keyword));
                }

                // Tính tổng số lượng bản ghi
                var totalCount = await queryable.CountAsync(cancellationToken);

                // Lấy danh sách giao dịch với phân trang
                var giaoDichList = await queryable
                    .Skip((query.Page - 1) * query.Size)
                    .Take(query.Size)
                    .Select(gd => new GiaoDichDTO
                    {
                        id = gd.Id,
                        TenGiaoDich = gd.TenGiaoDich,
                        NgayGiaoDich = gd.NgayGiaoDich,
                        LoaiGiaoDich = gd.LoaiGiaoDich,
                        TongTien = gd.TongTien,
                        GhiChu = gd.GhiChu,
                        TaiKhoanGiaoDich = gd.ChiTietGiaoDich.TaiKhoanGiaoDich.Select(tk => new TaiKhoanDTO
                        {
                            id = tk.Id,
                            tenTaiKhoan = tk.TenTaiKhoan,
                            loaiTaiKhoanId = tk.LoaiTaiKhoanId,
                            soDu = tk.SoDu
                        }).ToList()
                    })
                    .ToListAsync(cancellationToken);

                // Trả về kết quả dạng phân trang
                return new PagedResult<GiaoDichDTO>
                {
                    Data = giaoDichList,
                    TotalCount = totalCount,
                    PageSize = query.Size,
                    CurrentPage = query.Page,
                    Keyword = query.Keyword
                };
            }
        }
    }


}