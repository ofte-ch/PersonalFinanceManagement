using Application.Features.TaiKhoanFeatures;
using Asp.Versioning;
using Domain.DTO;
using Domain.Entities;
using Domain.Request.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebAPIs.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/accounts")]
    public class TaiKhoanController : BaseApiController
    {
        /// <summary>
        /// Tạo mới tài khoản
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(AddAccountRequest request)
        {
            var command = new TaiKhoanFeatures.Create
            {
                TenTaiKhoan = request.TenTaiKhoan,
                LoaiTaiKhoanId = request.LoaiTaiKhoanId,
                SoDu = request.SoDu
                
            };

            var response = await Mediator.Send(command);
            return StatusCode(response.Code, response.Message);
        }
        /// <summary>
        /// Lấy toàn bộ tài khoản
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PagedResult<TaiKhoanDTO>>> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string? keyword = null)
        {
            var query = new TaiKhoanFeatures.GetAll
            {
                Page = page,
                Size = size,
                Keyword = keyword
            };

            var result = await Mediator.Send(query);
            return Ok(result);
        }
        /// <summary>
        /// Lấy tài khoản bằng Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<TaiKhoanDTO>> GetById(int id)
        {
            var query = new TaiKhoanFeatures.GetOne { Id = id };
            var result = await Mediator.Send(query);
            if (result == null)
            {
                return NotFound(); // Trả về 404 nếu không tìm thấy
            }

            return Ok(result);
        }
        /// <summary>
        /// Xóa tài khoản bằng Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await Mediator.Send(new TaiKhoanFeatures.Delete { Id = id });
            return Ok(response);
        }
        /// <summary>
        /// Cập nhật tài khoản bằng Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id,UpdateAccountRequest request)
        {
            var command = new TaiKhoanFeatures.Update
            {
                Id = id,
                TenTaiKhoan = request.TenTaiKhoan,
                LoaiTaiKhoanId = request.LoaiTaiKhoanId,
                SoDu = request.SoDu

            };

            var response = await Mediator.Send(command);
            return StatusCode(response.Code, response.Message);
        }
    }
}
