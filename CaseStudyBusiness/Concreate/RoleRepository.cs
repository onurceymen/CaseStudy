using CaseStudyBusiness.Abstract;
using CaseStudyData.Context;
using CaseStudyEntity.Entity;

namespace CaseStudyData.Repository
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly CaseStudyDbContext _context;

        public RoleRepository(CaseStudyDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
