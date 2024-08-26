using CaseStudyAPI.Services;
using CaseStudyAPI.ServicesAbstract;
using CaseStudyBusiness.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CaseStudyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;

        public UserController(IUserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDto userCreateDto)
        {
            try
            {
                await _userService.RegisterUserAsync(userCreateDto);
                return Ok("Kullanıcı başarıyla kaydedildi.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Kullanıcı kaydedilirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginDto userDto)
        {
            try
            {
                var serviceResult = await _userService.AuthenticateUserAsync(userDto);
                return Ok(new { serviceResult.Token });
            }
            catch (Exception ex)
            {
                return BadRequest($"Kullanıcı kimlik doğrulaması yapılırken bir hata oluştu: {ex.Message}");
            }
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
        public async Task<IActionResult> ActivateUser(int userId)
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
        public async Task<IActionResult> DeactivateUser(int userId)
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
        public async Task<IActionResult> ApproveSellerRequest(int userId)
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

        [HttpPut("{email}")]
        public async Task<IActionResult> UpdateUserByEmail(string email, [FromBody] UserUpdateDto updatedUser)
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
        public async Task<IActionResult> ChangeUserRole(int userId, [FromQuery] int newRoleId)
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
