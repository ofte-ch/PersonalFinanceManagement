using Application.Features.GiaoDichFeatures;
using Application.Features.TaiKhoanFeatures;
using Asp.Versioning;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIs.Controllers.v1
{
    [ApiVersion("1.0")]
    public class GiaoDichController : BaseApiController
    {
        /// <summary>
        /// Tạo mới giao dịch
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(GiaoDichFeatures.Create command)
        {
            return ResponseTemplate.get(this, await Mediator.Send(command));
        }
        /// <summary>
        /// Lấy toàn bộ giao dịch
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<PagedResult<GiaoDichDTO>>> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string? keyword = null)
        {
            var query = new GiaoDichFeatures.GetAll
            {
                Page = page,
                Size = size,
                Keyword = keyword
            };

            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Lấy giao dịch bằng Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GiaoDichFeatures.GetOne { Id = id }));
        }
        /// <summary>
        /// Xóa giao dịch bằng Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new GiaoDichFeatures.Delete { Id = id }));
        }
        /// <summary>
        /// Cập nhật giao dịch bằng Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, GiaoDichFeatures.Update command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
    }
}
