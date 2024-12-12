using Application.Features.LoaiTaiKhoanFeatures;
using Asp.Versioning;
using Domain.DTO;
using Domain.Request.Account_Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIs.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/account-types")]
    public class LoaiTaiKhoanController : BaseApiController
    {
        /// <summary>
        /// Tạo mới loại tài khoản
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(LoaiTaiKhoanFeatures.Create command)
        {

            var response = await Mediator.Send(command);
            return StatusCode(response.Code, response.Message);
        }
        /// <summary>
        /// Lấy toàn bộ loại tài khoản
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PagedResult<LoaiTaiKhoanDTO>>> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string? keyword = null)
        {
            var query = new LoaiTaiKhoanFeatures.GetAll
            {
                Page = page,
                Size = size,
                Keyword = keyword
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        /// <summary>
        /// Lấy loại tài khoản bằng Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new LoaiTaiKhoanFeatures.GetOne { Id = id }));
        }
        /// <summary>
        /// Xóa loại tài khoản bằng Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return ResponseTemplate.get(this, await Mediator.Send(new LoaiTaiKhoanFeatures.Delete { Id = id }));
        }
        /// <summary>
        /// Cập nhật loại tài khoản bằng Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, UpdateAccountTypeRequest request)
        {
            //if (id != command.Id)
            //{
            //    return BadRequest();
            //}
            var command = new LoaiTaiKhoanFeatures.Update
            {
                Id = id,
                Ten = request.ten
            };

            var response = await Mediator.Send(command);
            return StatusCode(response.Code, response.Message);
        }
    }
}
