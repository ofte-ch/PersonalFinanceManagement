﻿using Application.Features.LoaiTaiKhoanFeatures;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIs.Controllers.v1
{
    [ApiVersion("1.0")]
    public class LoaiTaiKhoanController : BaseApiController
    {
        /// <summary>
        /// Tạo mới loại tài khoản
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(LoaiTaiKhoanFeatures.Create command)
        {
            return ResponseTemplate.get(this, await Mediator.Send(command));
        }
        /// <summary>
        /// Lấy toàn bộ loại tài khoản
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new LoaiTaiKhoanFeatures.GetAll()));
        }
        /// <summary>
        /// Lấy loại tài khoản bằng Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
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
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, LoaiTaiKhoanFeatures.Update command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return ResponseTemplate.get(this, await Mediator.Send(command));
        }
    }
}
