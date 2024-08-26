using AutoMapper;
using CaseStudyAPI.ServicesAbstract;
using CaseStudyBusiness.Abstract;
using CaseStudyBusiness.Dtos;
using CaseStudyEntity.Entity;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace CaseStudyAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;

        public UserService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper, JwtService jwtService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task ActivateUserAsync(int userId)
        {
            await _userRepository.ActivateUserAsync(userId);
        }

        public async Task DeactivateUserAsync(int userId)
        {
            await _userRepository.DeactivateUserAsync(userId);
        }

        public async Task ApproveSellerRequestAsync(int userId)
        {
            await _userRepository.ApproveSellerRequestAsync(userId);
        }

        public async Task RegisterUserAsync(UserCreateDto userCreateDto)
        {
            var user = _mapper.Map<User>(userCreateDto);
            user.RoleId = 2;
            user.PasswordHash = HashPassword(userCreateDto.Password);
            user.CreatedAt = DateTime.Now;

            var result = await _userRepository.CreateUserAsync(user);
            if (!result)
            {
                throw new Exception("Kullanıcı kaydı sırasında bir hata oluştu.");
            }
        }

        public async Task<UserLoginResponseDto> AuthenticateUserAsync(UserLoginDto login)
        {
            var user = await _userRepository.GetUserByEmailAsync(login.Email);
            if (user == null || !VerifyPassword(login.Password, user.PasswordHash))
            {
                throw new Exception("Kullanıcı adı veya şifre yanlış.");
            }

            // Eğer GenerateUserToken metodunuz User kabul ediyorsa:
            var token = _jwtService.GenerateUserToken(user);

            return new UserLoginResponseDto
            {
                User = _mapper.Map<UserDto>(user),
                Token = token
            };
        }


        public async Task UpdateUserByEmailAsync(string email, UserUpdateDto updatedUser)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            _mapper.Map(updatedUser, user);

            await _userRepository.UpdateAsync(user);
        }

        public async Task ChangeUserRoleAsync(int userId, int newRoleId)
        {
            await _userRepository.ChangeUserRoleAsync(userId, newRoleId);
        }

        private string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split(':');
            if (parts.Length != 2)
            {
                return false;
            }

            var salt = Convert.FromBase64String(parts[0]);
            var hash = Convert.FromBase64String(parts[1]);

            var testHash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);

            return hash.SequenceEqual(testHash);
        }
    }
}
