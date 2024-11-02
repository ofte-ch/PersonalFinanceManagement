using Application.Features.ThongKeFeatures;
using Asp.Versioning;
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
        /// <returns>Danh sách giao dịch trong khoảng thời gian</returns>
        [HttpPost("ByDateRange")]
        public async Task<IActionResult> GetByDateRange([FromBody] ThongKeFeatures.GetThongKe query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
