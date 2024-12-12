using Application.Features.GiaoDichFeatures;
using Application.Features.TaiKhoanFeatures;
using Application.Interface;
using Asp.Versioning;
using Domain.DTO;
using Domain.Request.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebAPIs.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/transactions")]
    public class GiaoDichController : BaseApiController
    {
        /// <summary>
        /// Tạo mới giao dịch
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(GiaoDichFeatures.Create command)
        {
            var response = await Mediator.Send(command);
            return StatusCode(response.Code, response.Message);
        }
        /// <summary>
        /// Lấy toàn bộ giao dịch
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PagedResult<GiaoDichDTO>>> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string? keyword = null , [FromQuery] int? maTaiKhoan=null)
        {
            var query = new GiaoDichFeatures.GetAll
            {
                Page = page,
                Size = size,
                Keyword = keyword,
                MaTaiKhoan = maTaiKhoan
            };

            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Lấy giao dịch theo khoảng thời gian
        /// </summary>
        /// <returns></returns>
        [HttpGet("by-date-range")]
        public async Task<ActionResult<PagedResult<GiaoDichDTO>>> GetByDateRange([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] DateTime? TuNgay = null, [FromQuery] DateTime? DenNgay = null)
        {
            var query = new GiaoDichFeatures.GetByDateRange
            {
                Page = page,
                Size = size,
                TuNgay = TuNgay,
                DenNgay = DenNgay
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
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await Mediator.Send(new GiaoDichFeatures.Delete { Id = id });
            return StatusCode(response.Code, response.Message);
        }
        /// <summary>
        /// Cập nhật giao dịch bằng Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, UpdateTransactionRequest request)
        {
            //if (id != command.Id)
            //{
            //    return BadRequest();
            //}
            var command = new GiaoDichFeatures.Update
            {
                Id = id,
                TenGiaoDich = request.TenGiaoDich,
                NgayGiaoDich = request.NgayGiaoDich,
                TaiKhoanGocId = request.TaiKhoanGocId,
                TaiKhoanPhuId = request.TaiKhoanPhuId,
                TheLoaiId = request.TheLoaiId,
                TongTien = request.TongTien,
                GhiChu = request.GhiChu

            };
            var response = await Mediator.Send(command);
            return StatusCode(response.Code, response.Message);
        }
    }
}
