using Application.Interface;
using Application.Response;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
                // Kiểm tra validation của đối tượng theLoai
                var validationContext = new ValidationContext(theLoai, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(theLoai, validationContext, validationResults, validateAllProperties: true);

                // Nếu có lỗi validation, trả về thông báo lỗi
                if (!isValid)
                {
                    var errorMessages = string.Join("\n", validationResults.Select(vr => vr.ErrorMessage));
                    // Trả về tất cả các lỗi validation dưới dạng Response
                    return new ValidationFailResponse(errorMessages);
                }

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
                    // Lưu lại giá trị cũ của PhanLoai để so sánh sau
                    var phanLoaiCu = theLoai.PhanLoai;

                    theLoai.TenTheLoai = command.TenTheLoai;
                    theLoai.MoTa = command.MoTa;
                    theLoai.PhanLoai = command.PhanLoai;

                    // Kiểm tra validation của đối tượng theLoai
                    var validationContext = new ValidationContext(theLoai, serviceProvider: null, items: null);
                    var validationResults = new List<ValidationResult>();
                    bool isValid = Validator.TryValidateObject(theLoai, validationContext, validationResults, validateAllProperties: true);

                    // Nếu có lỗi validation, trả về thông báo lỗi
                    if (!isValid)
                    {
                        var errorMessages = string.Join("\n", validationResults.Select(vr => vr.ErrorMessage));
                        // Trả về tất cả các lỗi validation dưới dạng Response
                        return new ValidationFailResponse(errorMessages);
                    }

                    //nếu phân loại thay đổi từ thu sang chi thì thay đổi số tiền trong tài khoản của các tài khoản liên quan đến thể loại này
                    // Kiểm tra xem phân loại có thay đổi không
                    if (phanLoaiCu != command.PhanLoai)
                    {
                        // Lấy các giao dịch có liên quan đến thể loại này
                        var giaoDichs = _context.GiaoDich.Where(x => x.TheLoai.Id == command.Id).ToList();

                        // Duyệt qua tất cả các giao dịch để điều chỉnh số dư của tài khoản
                        foreach (var giaoDich in giaoDichs)
                        {
                            // Lấy các tài khoản liên quan đến giao dịch (tài khoản chuyển và tài khoản nhận)
                            var taiKhoanGoc = giaoDich.TaiKhoanGoc;
                            var taiKhoanPhu = giaoDich.TaiKhoanPhu;

                            double soTien = giaoDich.TongTien*2; // vì nếu đổi từ thu sang chi thì số tiền sẽ thay đổi 2 lần

                            // Điều chỉnh số tiền dựa trên phân loại (Thu/Chi)
                            if (phanLoaiCu == "Thu" && command.PhanLoai == "Chi") // Nếu từ Thu chuyển sang Chi
                            {
                                soTien = -soTien; // Trừ số tiền trong tài khoản
                            }
                            else if (phanLoaiCu == "Chi" && command.PhanLoai == "Thu") // Nếu từ Chi chuyển sang Thu
                            {
                                soTien = soTien; // Cộng số tiền vào tài khoản
                            }

                            // Kiểm tra số lượng tài khoản tham gia
                            if (taiKhoanGoc != null)
                            {
                                taiKhoanGoc.CapNhatSoDu(-soTien); // Trừ tiền x2 từ tài khoản chuyển
                            }

                            if (taiKhoanPhu != null)
                            {
                                taiKhoanPhu.CapNhatSoDu(soTien); // Cộng tiền x2 vào tài khoản nhận
                            }
                        }

                    }

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