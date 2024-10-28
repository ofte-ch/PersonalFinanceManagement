using Application.Features;
using Application.Features.TaiKhoanFeatures.Commands;
using Application.Features.TaiKhoanFeatures.Queries;
using Application.Interface;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIs.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TaiKhoanController : BaseApiController
    {
        /// <summary>
        /// Tạo mới tài khoản
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateTaiKhoanCommand command)
        {
            return ResponseTemplate.get(this, await Mediator.Send(command));
        }
        /// <summary>
        /// Lấy toàn bộ tài khoản
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllTaiKhoanQuery()));
        }
        /// <summary>
        /// Lấy tài khoản bằng Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetTaiKhoanByIdQuery { Id = id }));
        }
        /// <summary>
        /// Xóa tài khoản bằng Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return ResponseTemplate.get(this, await Mediator.Send(new DeleteTaiKhoanCommand { Id = id }));
        }
        /// <summary>
        /// Cập nhật tài khoản bằng Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, UpdateTaiKhoanCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return ResponseTemplate.get(this, await Mediator.Send(command));
        }
    }
}
