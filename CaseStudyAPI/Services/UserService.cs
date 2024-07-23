using CaseStudyAPI.ServicesAbstract;
using CaseStudyBusiness.Abstract;
using CaseStudyBusiness.Dtos;
using CaseStudyEntity.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public UserService(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync();
                return users.Select(u => new UserDto
                {
                    
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                   
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcılar getirilirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(email);
                if (user == null)
                {
                    throw new Exception("Kullanıcı bulunamadı.");
                }

                return new UserDto
                {
                  
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                   
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı getirilirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task ActivateUserAsync(string userId)
        {
            try
            {
                await _userRepository.ActivateUserAsync(userId);
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı aktif edilirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task DeactivateUserAsync(string userId)
        {
            try
            {
                await _userRepository.DeactivateUserAsync(userId);
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı pasif edilirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task ApproveSellerRequestAsync(string userId)
        {
            try
            {
                await _userRepository.ApproveSellerRequestAsync(userId);
            }
            catch (Exception ex)
            {
                throw new Exception("Satıcı olma isteği onaylanırken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task RegisterUserAsync(UserDto userDto, string password)
        {
            try
            {
                var user = new User
                {
                    UserName = userDto.Email,
                    Email = userDto.Email,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    CreatedAt = DateTime.Now,
                };

                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    throw new Exception("Kullanıcı kaydedilirken bir hata oluştu: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı kaydedilirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task<UserDto> AuthenticateUserAsync(string email, string password)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null || !await _userManager.CheckPasswordAsync(user, password)) // TODO: Seed aşamasında hangi şifrenin hash'lenmiş halini kaydettiğimizi bilmediğimizden burası SIKINTILI!
                {
                    throw new Exception("Kullanıcı adı veya şifre yanlış.");
                }

                return new UserDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                  
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı kimlik doğrulaması yapılırken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task UpdateUserByEmailAsync(string email, UserDto updatedUser)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    throw new Exception("Kullanıcı bulunamadı.");
                }

                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    throw new Exception("Kullanıcı güncellenirken bir hata oluştu: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı güncellenirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task ChangeUserRoleAsync(string userId, string newRoleId)
        {
            try
            {
                await _userRepository.ChangeUserRoleAsync(userId, newRoleId);
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı rolü değiştirilirken bir hata oluştu: " + ex.Message);
            }
        }
    }
}
