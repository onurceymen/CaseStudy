using CaseStudyBusiness.Abstract;
using CaseStudyData.Context;
using CaseStudyData.Repository;
using CaseStudyEntity.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseStudyBusiness.Concrete
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly CaseStudyDbContext _context;

        public UserRepository(CaseStudyDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task ActivateUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.Enabled = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeactivateUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.Enabled = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ApproveSellerRequestAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                // Örnek: seller rolü için role ID ayarlanması
                user.RoleId = 2; // 2'nin seller rolü olduğunu varsayıyoruz
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task ChangeUserRoleAsync(int userId, int newRoleId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.RoleId = newRoleId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            await _context.Set<User>().AddAsync(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
