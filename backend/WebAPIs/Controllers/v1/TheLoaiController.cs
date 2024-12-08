using Application.Features.TheLoaiFeatures;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIs.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/types")]
    public class TheLoaiController : BaseApiController
    {
        /// <summary>
        /// Tạo mới thể loại
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(TheLoaiFeatures.Create command)
        {
            return ResponseTemplate.get(this, await Mediator.Send(command));
        }
        /// <summary>
        /// Lấy toàn bộ thể loại
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new TheLoaiFeatures.GetAll()));
        }
        /// <summary>
        /// Lấy thể loại bằng Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new TheLoaiFeatures.GetOne { Id = id }));
        }
        /// <summary>
        /// Xóa thể loại bằng Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return ResponseTemplate.get(this, await Mediator.Send(new TheLoaiFeatures.Delete { Id = id }));
        }
        /// <summary>
        /// Cập nhật thể loại bằng Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        [Authorize]
        public async Task<IActionResult> Update(int id, TheLoaiFeatures.Update command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return ResponseTemplate.get(this, await Mediator.Send(command));
        }
    }
}
