using RP1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP1.DataAccess.Repository
{
    public class ApplicationUserRepo : Repository<ApplicationUser>, IApplicationUserRepo
    {
        private readonly AppDBContext _context;

        public ApplicationUserRepo(AppDBContext context) : base(context)
        {
            _context = context;
        }

        public ApplicationUser Get(string s)
        {
            if (string.IsNullOrEmpty(s)) 
            {
                return null!;
            }
            else
            {
                var user = _context.Users.Where(u => u.Id == s).FirstOrDefault();
                return user as ApplicationUser ?? throw new InvalidCastException("The user could not be cast to ApplicationUser.");
            }
        }
    }
}
