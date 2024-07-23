using CaseStudyAPI.ServicesAbstract;
using CaseStudyBusiness.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseStudyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest($"Kullanıcılar getirilirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                var user = await _userService.GetUserByEmailAsync(email);
                if (user == null)
                {
                    return NotFound("Kullanıcı bulunamadı.");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest($"Kullanıcı getirilirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPost("activate/{userId}")]
        public async Task<IActionResult> ActivateUser(string userId)
        {
            try
            {
                await _userService.ActivateUserAsync(userId);
                return Ok("Kullanıcı başarıyla aktif edildi.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Kullanıcı aktif edilirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPost("deactivate/{userId}")]
        public async Task<IActionResult> DeactivateUser(string userId)
        {
            try
            {
                await _userService.DeactivateUserAsync(userId);
                return Ok("Kullanıcı başarıyla pasif edildi.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Kullanıcı pasif edilirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPost("approve/{userId}")]
        public async Task<IActionResult> ApproveSellerRequest(string userId)
        {
            try
            {
                await _userService.ApproveSellerRequestAsync(userId);
                return Ok("Satıcı olma isteği başarıyla onaylandı.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Satıcı olma isteği onaylanırken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDto userDto, [FromQuery] string password)
        {
            try
            {
                if (userDto == null || string.IsNullOrEmpty(password))
                {
                    return BadRequest("Geçersiz kullanıcı bilgileri veya şifre.");
                }

                await _userService.RegisterUserAsync(userDto, password);
                return Ok("Kullanıcı başarıyla kaydedildi.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Kullanıcı kaydedilirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateUser(string email, string password)
        {
            try
            {
                var user = await _userService.AuthenticateUserAsync(email, password);
                if (user == null)
                {
                    return Unauthorized("Kullanıcı adı veya şifre yanlış.");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest($"Kullanıcı kimlik doğrulaması yapılırken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPut("{email}")]
        public async Task<IActionResult> UpdateUserByEmail(string email, [FromBody] UserDto updatedUser)
        {
            try
            {
                await _userService.UpdateUserByEmailAsync(email, updatedUser);
                return Ok("Kullanıcı bilgileri başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Kullanıcı güncellenirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPost("change-role/{userId}")]
        public async Task<IActionResult> ChangeUserRole(string userId, [FromQuery] string newRoleId)
        {
            try
            {
                await _userService.ChangeUserRoleAsync(userId, newRoleId);
                return Ok("Kullanıcı rolü başarıyla değiştirildi.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Kullanıcı rolü değiştirilirken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
