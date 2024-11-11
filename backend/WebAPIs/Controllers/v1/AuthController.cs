﻿using Application.Features.ThongKeFeatures;
using Application.Interface;
using Application.Services;
using Asp.Versioning;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebAPIs.Controllers.v1
{
    [Route("api/auth")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AuthController : BaseApiController
    {
        private readonly IUserService _userService;
        protected IApplicationDbContext _context;

        public AuthController(IUserService userService, IApplicationDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User model)
        {
            var user = await _userService.ValidateUserAsync(model.Username, model.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
            else
            {
                user.RememberMe = model.RememberMe; 
                _context.Users.Update(user);
                await _context.SaveChangesAsync(); 

                var identity = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.Username)
            }, "Cookies");

                var principal = new ClaimsPrincipal(identity);

                if (model.RememberMe)
                {
                    await HttpContext.SignInAsync("Cookies", principal, new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddDays(30)
                    });
                }
                else
                {
                    await HttpContext.SignInAsync("Cookies", principal);
                }
                return Ok(new { message = "Login successful", user });
            }          
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User model)
        {
            var user = await _userService.RegisterUserAsync(model.Username, model.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
            return Ok(new { message = "Login successful", user });
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return Ok(new { message = "Logout successful" });
        }
    }
}
