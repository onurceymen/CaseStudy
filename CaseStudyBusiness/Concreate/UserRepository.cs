using CaseStudyBusiness.Abstract;
using CaseStudyData.Context;
using CaseStudyData.Repository;
using CaseStudyEntity.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyBusiness.Concreate
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly CaseStudyDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserRepository(CaseStudyDbContext context, UserManager<User> userManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task ActivateUserAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.Enabled = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeactivateUserAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.Enabled = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ApproveSellerRequestAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                // Example: setting role ID for seller
                user.RoleId = 2; // assuming 2 is the ID for the seller role
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

        public async Task ChangeUserRoleAsync(string userId, string newRoleId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.RoleId = int.Parse(newRoleId);
                await _context.SaveChangesAsync();
            }
        }
    }

}
