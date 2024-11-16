using Application.Features.TaiKhoanFeatures;
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
        public async Task<IActionResult> Create(TaiKhoanFeatures.Create command)
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
            return Ok(await Mediator.Send(new TaiKhoanFeatures.GetAll()));
        }
        /// <summary>
        /// Lấy tài khoản bằng Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new TaiKhoanFeatures.GetOne { Id = id }));
        }
        /// <summary>
        /// Xóa tài khoản bằng Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return ResponseTemplate.get(this, await Mediator.Send(new TaiKhoanFeatures.Delete { Id = id }));
        }
        /// <summary>
        /// Cập nhật tài khoản bằng Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, TaiKhoanFeatures.Update command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return ResponseTemplate.get(this, await Mediator.Send(command));
        }
    }
}
