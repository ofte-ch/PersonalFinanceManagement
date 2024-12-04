using Application.Features.ThongKeFeatures;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIs.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ThongKeController : BaseApiController
    {
        /// <summary>
        /// Lấy dữ liệu thống kê trong khoảng thời gian
        /// </summary>
        /// <param name="query">Đối tượng chứa ngày bắt đầu và ngày kết thúc</param>
        /// <returns>Thống kê giao dịch theo thể loại trong khoảng thời gian</returns>
        [HttpPost("byTransactionType")]
        [Authorize]
        public async Task<IActionResult> GetByTransactionType([FromBody] ThongKeFeatures.GetThongKeTheoTheLoai query)
        {
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// Lấy dữ liệu thống kê trong khoảng thời gian
        /// </summary>
        /// <param name="query">Đối tượng chứa ngày bắt đầu và ngày kết thúc</param>
        /// <returns>Thống kê giao dịch theo tài khoản trong khoảng thời gian</returns>
        [HttpPost("byAccount")]
        [Authorize]
        public async Task<IActionResult> GetByAccount([FromBody] ThongKeFeatures.GetThongKeTheoTaiKhoan query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
