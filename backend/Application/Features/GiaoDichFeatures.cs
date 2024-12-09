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
        //public String LoaiGiaoDich { get; set; }
        //public Collection<int> TaiKhoan { get; set; }

        public int TaiKhoanChuyen { get; set; }
        public int? TaiKhoanNhan { get; set; }
        public int TheLoai { get; set; }
        public Double TongTien { get; set; }
        public String? GhiChu { get; set; }

        public class Handler : BaseHandler<Create>
        {
            public Handler(IApplicationDbContext context) : base(context)
            {
            }
            public async override Task<IResponse> Handle(Create command, CancellationToken cancellationToken)
            {
                //if (command.TaiKhoan.Count == 0 || command.TaiKhoan.Count > 2)
                //{
                //    return new BadRequestResponse("Số lượng tài khoản tối thiểu là 1, tối đa là 2");
                //}
                //var TaiKhoanGiaoDich = _context.TaiKhoan.Where(x => command.TaiKhoan.Contains(x.Id)).ToList();
                var taiKhoanChuyen = _context.TaiKhoan.Where(x => x.Id == command.TaiKhoanChuyen).FirstOrDefault();
                var taiKhoanNhan = _context.TaiKhoan.Where(x => x.Id == command.TaiKhoanNhan).FirstOrDefault();
                //if (TaiKhoanGiaoDich.Count == 0)
                //{
                //    return new NotFoundResponse("Không tìm thấy tài khoản");
                //}
                if (taiKhoanChuyen == null && taiKhoanNhan == null)
                {
                    return new NotFoundResponse("Không tìm thấy tài khoản");
                }
                if(taiKhoanChuyen == null)
                {
                    return new NotFoundResponse("Không tìm thấy tài khoản chuyển");
                }

                var TheLoai = _context.TheLoai.Where(x => x.Id == command.TheLoai).FirstOrDefault();
                if (TheLoai == null)
                {
                    return new NotFoundResponse(". Thể loại: " + command.TheLoai + 
                        "Thể loại không tồn tại");
                }
                //var ChiTietGiaoDich = new ChiTietGiaoDich
                //{
                //    TaiKhoanGiaoDich = TaiKhoanGiaoDich,
                //    TheLoai = TheLoai,
                //};
                var GiaoDich = new GiaoDich
                {
                    TenGiaoDich = command.TenGiaoDich,
                    NgayGiaoDich = command.NgayGiaoDich,
                    TaiKhoanChuyen = taiKhoanChuyen,
                    TaiKhoanNhan = taiKhoanNhan,
                    TheLoai = TheLoai,
                    //LoaiGiaoDich = command.LoaiGiaoDich,
                    //ChiTietGiaoDich = ChiTietGiaoDich,
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

                if (taiKhoanNhan == null)
                {
                    if (TheLoai?.PhanLoai == "Thu") // dành cho giao dịch nếu dùng 1 tài khoản
                    {
                        taiKhoanChuyen.CapNhatSoDu(command.TongTien); // nếu cộng thêm tiền thì điền số dương
                    }
                    else
                    {
                        taiKhoanChuyen.CapNhatSoDu(-command.TongTien); // nếu trừ tiền thì điền số âm
                    }
                }
                else //-----------------------------------------------chổ này cần xử lý lại cho giao dịch nếu dùng 2 tài khoản-----------------------------------//
                {
					// Kiểm tra tài khoản chuyển có đủ tiền không 
					if (taiKhoanChuyen.SoDu < command.TongTien)
					{
						return new BadRequestResponse("Số dư tài khoản không đủ.");
					}
                    try { 
					    taiKhoanChuyen.CapNhatSoDu(-command.TongTien); //trừ tiền tài khoản chuyển
                        taiKhoanNhan.CapNhatSoDu(command.TongTien); // cộng tiền tài khoản nhận
                    }
                    catch (Exception ex) {
                        // Nếu xảy ra lỗi ==> rollback, cập nhật lại số dư tk
						taiKhoanChuyen.CapNhatSoDu(command.TongTien);
						taiKhoanNhan.CapNhatSoDu(-command.TongTien); 
                        return new BadRequestResponse(ex.Message);
					}
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
        //public Collection<int> TaiKhoan { get; set; }
        public int TaiKhoanChuyenId { get; set; }
        public int? TaiKhoanNhanId { get; set; }
        public int TheLoaiId { get; set; }
        public Double TongTien { get; set; }
        public String? GhiChu { get; set; }

        public class Handler : BaseHandler<Update>
        {
            public Handler(IApplicationDbContext context) : base(context)
            {
            }
            public async override Task<IResponse> Handle(Update command, CancellationToken cancellationToken)
            {
                //if (command.TaiKhoan.Count == 0 || command.TaiKhoan.Count > 2)
                //{
                //    return new BadRequestResponse("Số lượng tài khoản tối thiểu là 1, tối đa là 2");
                //}
                var taiKhoanChuyen = _context.TaiKhoan.Where(x => x.Id == command.TaiKhoanChuyenId).FirstOrDefault();
                var taiKhoanNhan = _context.TaiKhoan.Where(x => x.Id == command.TaiKhoanNhanId).FirstOrDefault();
                //if (TaiKhoanGiaoDich.Count == 0)
                //{
                //    return new NotFoundResponse("Không tìm thấy tài khoản");
                //}
                if (taiKhoanChuyen == null && taiKhoanNhan == null)
                {
                    return new NotFoundResponse("Không tìm thấy tài khoản");
                }
                if (taiKhoanChuyen == null)
                {
                    return new NotFoundResponse("Không tìm thấy tài khoản chuyển");
                }

                var TheLoai = _context.TheLoai.Where(x => x.Id == command.TheLoaiId).FirstOrDefault();
                if (TheLoai == null)
                {
                    return new NotFoundResponse("Thể loại không tồn tại");
                }
                

                //var ChiTietGiaoDich = new ChiTietGiaoDich
                //{
                //    TaiKhoanGiaoDich = TaiKhoanGiaoDich,
                //    TheLoai = TheLoai,
                //};
                var GiaoDich = _context.GiaoDich.Where(x => x.Id == command.Id).FirstOrDefault();
                if (GiaoDich == null)
                {
                    return new NotFoundResponse("Không tìm thấy giao dịch");
                }
                else
                {
                    // Kiểm tra tài khoản có thay đổi không
                    bool taiKhoanThayDoi = GiaoDich.TaiKhoanChuyen.Id != taiKhoanChuyen.Id ||
                       GiaoDich.TaiKhoanNhan?.Id != taiKhoanNhan?.Id;

                    // Kiểm tra nếu tài khoản thay đổi
                    if (taiKhoanThayDoi)
                    {
                        // Hoàn trả số dư từ tài khoản cũ
                        GiaoDich.TaiKhoanChuyen.CapNhatSoDu(GiaoDich.TongTien); // Hoàn lại tiền cho tài khoản chuyển cũ
                        if (GiaoDich.TaiKhoanNhan != null)
                        {
                            GiaoDich.TaiKhoanNhan.CapNhatSoDu(-GiaoDich.TongTien); // Trừ tiền từ tài khoản nhận cũ
                        }

                        // Cập nhật số dư cho tài khoản mới
                        taiKhoanChuyen.CapNhatSoDu(-command.TongTien); // Trừ tiền từ tài khoản chuyển mới
                        if (taiKhoanNhan != null)
                        {
                            taiKhoanNhan.CapNhatSoDu(command.TongTien); // Cộng tiền vào tài khoản nhận mới
                        }
                    }


                    // Kiểm tra loại giao dịch có thay đổi không
                    if (GiaoDich.TheLoai.PhanLoai != TheLoai.PhanLoai)
                    {
                        // Xác định hệ số dựa trên thay đổi loại giao dịch
                        int heSo = GiaoDich.TheLoai.PhanLoai == "Thu" ? -2 : 2;

                        // Cập nhật số dư cho tài khoản chuyển
                        GiaoDich.TaiKhoanChuyen.CapNhatSoDu(command.TongTien * heSo);

                        // Nếu có tài khoản nhận, cũng cần cập nhật
                        if (GiaoDich.TaiKhoanNhan != null)
                        {
                            GiaoDich.TaiKhoanNhan.CapNhatSoDu(-command.TongTien * heSo);
                        }
                    }

                    // Kiểm tra số tiền có thay đổi không
                    if (GiaoDich.TongTien != command.TongTien)
                    {
                        double chenhlechTien = command.TongTien - GiaoDich.TongTien;

                        // Cập nhật số dư tài khoản chuyển
                        GiaoDich.TaiKhoanChuyen.CapNhatSoDu(TheLoai.PhanLoai == "Thu" ? chenhlechTien : -chenhlechTien);

                        // Nếu có tài khoản nhận, cập nhật số dư tài khoản nhận
                        if (GiaoDich.TaiKhoanNhan != null)
                        {
                            GiaoDich.TaiKhoanNhan.CapNhatSoDu(TheLoai.PhanLoai == "Thu" ? -chenhlechTien : chenhlechTien);
                        }
                    }

                    GiaoDich.TenGiaoDich = command.TenGiaoDich;
                    GiaoDich.NgayGiaoDich = command.NgayGiaoDich;
                    GiaoDich.TaiKhoanChuyen = taiKhoanChuyen;
                    GiaoDich.TaiKhoanNhan = taiKhoanNhan;
                    GiaoDich.TheLoai = TheLoai;
                    //GiaoDich.LoaiGiaoDich = command.LoaiGiaoDich;
                    //GiaoDich.ChiTietGiaoDich = ChiTietGiaoDich;
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
                    //var taiKhoanGiaoDich = GiaoDich.ChiTietGiaoDich.TaiKhoanGiaoDich.ToList();
                    var taiKhoanChuyen = GiaoDich.TaiKhoanChuyen;
                    var taiKhoanNhan = GiaoDich.TaiKhoanNhan;

                    if (taiKhoanNhan == null)
                    {
                        if (GiaoDich.TheLoai.PhanLoai == "Thu")
                        {
                            taiKhoanChuyen.CapNhatSoDu(-GiaoDich.TongTien); // Hoàn lại số tiền thu
                        }
                        else
                        {
                            taiKhoanChuyen.CapNhatSoDu(GiaoDich.TongTien); // Hoàn lại số tiền chi
                        }
                    }
                    else
                    {
                        taiKhoanChuyen.CapNhatSoDu(-GiaoDich.TongTien); // Hoàn tiền tài khoản chuyển
                        taiKhoanNhan.CapNhatSoDu(GiaoDich.TongTien);  // Trừ tiền tài khoản nhận
                    }
                    _context.GiaoDich.Remove(GiaoDich);
                    await _context.SaveChangesAsync();
                    return new SuccessResponse($"Xóa giao dịch thành công, mã giao dịch: {GiaoDich.Id}");
                }
            }
        }
    }

    // Queries
    public class GetOne : BaseQuery<GiaoDichDTO, GetOne>
    {
        public int Id { get; set; }

        public class Handler : BaseHandler<GetOne>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
            {
                _httpContextAccessor = httpContextAccessor;
            }

            public async override Task<GiaoDichDTO> Handle(GetOne query, CancellationToken cancellationToken)
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
                                (x.TaiKhoanChuyen.User.Id == int.Parse(userIdClaim) ||
                                 (x.TaiKhoanNhan != null && x.TaiKhoanNhan.User.Id == int.Parse(userIdClaim))))
                    .Select(gd => new GiaoDichDTO
                    {
                        id = gd.Id,
                        TenGiaoDich = gd.TenGiaoDich,
                        NgayGiaoDich = gd.NgayGiaoDich,
                        //LoaiGiaoDich = gd.LoaiGiaoDich,
                        TaiKhoanChuyen = new TaiKhoanDTO
                        {
                            id = gd.TaiKhoanChuyen.Id,
                            tenTaiKhoan = gd.TaiKhoanChuyen.TenTaiKhoan,
                            loaiTaiKhoanId = gd.TaiKhoanChuyen.LoaiTaiKhoanId,
                            soDu = gd.TaiKhoanChuyen.SoDu
                        },
                        TaiKhoanNhan = gd.TaiKhoanNhan == null ? null : new TaiKhoanDTO
                        {
                            id = gd.TaiKhoanNhan.Id,
                            tenTaiKhoan = gd.TaiKhoanNhan.TenTaiKhoan,
                            loaiTaiKhoanId = gd.TaiKhoanNhan.LoaiTaiKhoanId,
                            soDu = gd.TaiKhoanNhan.SoDu
                        },
                        TheLoai = new TheLoaiDTO
                        {
                            id = gd.TheLoai.Id,
                            tenTheLoai = gd.TheLoai.TenTheLoai,
                            moTa = gd.TheLoai.MoTa,
                            phanLoai = gd.TheLoai.PhanLoai
                        },
                        TongTien = gd.TongTien,
                        GhiChu = gd.GhiChu,
                    })
                    .FirstOrDefaultAsync(cancellationToken);

            }
        }
    }

    public class GetAll : BaseQuery<PagedResult<GiaoDichDTO>, GetAll>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string? Keyword { get; set; }

        public int? MaTaiKhoan { get; set; }

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
                    .Where(gd => gd.TaiKhoanChuyen.User.Id == userId || gd.TaiKhoanNhan.User.Id == userId);

                // Lọc theo từ khóa nếu có
                if (!string.IsNullOrEmpty(query.Keyword))
                {
                    queryable = queryable.Where(gd => gd.TenGiaoDich.Contains(query.Keyword));
                }

                // Lọc theo tài khoản giao dịch nếu có
                if (query.MaTaiKhoan != null)
                {
                    queryable = queryable.Where(gd => gd.TaiKhoanChuyen.Id == query.MaTaiKhoan || gd.TaiKhoanNhan.Id == query.MaTaiKhoan);
                }

                // sắp xếp từ mới nhất tới cũ nhất
                queryable = queryable.OrderByDescending(a => a.NgayGiaoDich);


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
                        //LoaiGiaoDich = gd.LoaiGiaoDich,
                        TaiKhoanChuyen = new TaiKhoanDTO
                        {
                            id = gd.TaiKhoanChuyen.Id,
                            tenTaiKhoan = gd.TaiKhoanChuyen.TenTaiKhoan,
                            loaiTaiKhoanId = gd.TaiKhoanChuyen.LoaiTaiKhoanId,
                            soDu = gd.TaiKhoanChuyen.SoDu
                        },
                        TaiKhoanNhan = gd.TaiKhoanNhan == null ? null : new TaiKhoanDTO
                        {
                            id = gd.TaiKhoanNhan.Id,
                            tenTaiKhoan = gd.TaiKhoanNhan.TenTaiKhoan,
                            loaiTaiKhoanId = gd.TaiKhoanNhan.LoaiTaiKhoanId,
                            soDu = gd.TaiKhoanNhan.SoDu
                        },
                        TheLoai = new TheLoaiDTO
                        {
                            id = gd.TheLoai.Id,
                            tenTheLoai = gd.TheLoai.TenTheLoai,
                            moTa = gd.TheLoai.MoTa,
                            phanLoai = gd.TheLoai.PhanLoai
                        },
                        TongTien = gd.TongTien,
                        GhiChu = gd.GhiChu,
                        //TaiKhoanGiaoDich = gd.ChiTietGiaoDich.TaiKhoanGiaoDich.Select(tk => new TaiKhoanDTO
                        //{
                        //    id = tk.Id,
                        //    tenTaiKhoan = tk.TenTaiKhoan,
                        //    loaiTaiKhoanId = tk.LoaiTaiKhoanId,
                        //    soDu = tk.SoDu
                        //}).ToList()
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

        public class GetByDateRange : BaseQuery<PagedResult<GiaoDichDTO>, GetByDateRange>
        {
            public int Page { get; set; }
            public int Size { get; set; }

            public DateTime? TuNgay { get; set; }

            public DateTime? DenNgay { get; set; }
            public class Handler : BaseHandler<GetByDateRange>
            {
                private readonly IHttpContextAccessor _httpContextAccessor;

                public Handler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
                {
                    _httpContextAccessor = httpContextAccessor;
                }

                public async override Task<PagedResult<GiaoDichDTO>> Handle(GetByDateRange query, CancellationToken cancellationToken)
                {
                    if (!ThongKeFeatures.ThongKeFeatures.CheckDate(query.TuNgay, query.DenNgay))
                    {
                        return null;
                    }
                    // Lấy UserId từ HttpContext
                    var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value;
                    if (string.IsNullOrEmpty(userIdClaim))
                    {
                        return null;  // Trả về null nếu không có UserId
                    }

                    int userId = int.Parse(userIdClaim);

                    // Tạo truy vấn lọc theo UserId
                    var queryable = _context.GiaoDich
                        .Where(gd => gd.TaiKhoanChuyen.User.Id == userId || gd.TaiKhoanNhan.User.Id == userId)
                        .Where(a => a.NgayGiaoDich >= query.TuNgay && a.NgayGiaoDich <= query.DenNgay)
                        .OrderByDescending(a => a.NgayGiaoDich);

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
                            //LoaiGiaoDich = gd.LoaiGiaoDich,
                            TaiKhoanChuyen = new TaiKhoanDTO
                            {
                                id = gd.TaiKhoanChuyen.Id,
                                tenTaiKhoan = gd.TaiKhoanChuyen.TenTaiKhoan,
                                loaiTaiKhoanId = gd.TaiKhoanChuyen.LoaiTaiKhoanId,
                                soDu = gd.TaiKhoanChuyen.SoDu
                            },
                            TaiKhoanNhan = gd.TaiKhoanNhan == null ? null : new TaiKhoanDTO
                            {
                                id = gd.TaiKhoanNhan.Id,
                                tenTaiKhoan = gd.TaiKhoanNhan.TenTaiKhoan,
                                loaiTaiKhoanId = gd.TaiKhoanNhan.LoaiTaiKhoanId,
                                soDu = gd.TaiKhoanNhan.SoDu
                            },
                            TheLoai = new TheLoaiDTO
                            {
                                id = gd.TheLoai.Id,
                                tenTheLoai = gd.TheLoai.TenTheLoai,
                                moTa = gd.TheLoai.MoTa,
                                phanLoai = gd.TheLoai.PhanLoai
                            },
                            TongTien = gd.TongTien,
                            GhiChu = gd.GhiChu,
                            //TaiKhoanGiaoDich = gd.ChiTietGiaoDich.TaiKhoanGiaoDich.Select(tk => new TaiKhoanDTO
                            //{
                            //    id = tk.Id,
                            //    tenTaiKhoan = tk.TenTaiKhoan,
                            //    loaiTaiKhoanId = tk.LoaiTaiKhoanId,
                            //    soDu = tk.SoDu
                            //}).ToList()
                        })
                        .ToListAsync(cancellationToken);

                    // Trả về kết quả dạng phân trang
                    return new PagedResult<GiaoDichDTO>
                    {
                        Data = giaoDichList,
                        TotalCount = totalCount,
                        PageSize = query.Size,
                        CurrentPage = query.Page,
                    };
                }
            }
        }



}