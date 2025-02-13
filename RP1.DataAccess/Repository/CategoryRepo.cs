using RP1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP1.DataAccess.Repository
{
    public class CategoryRepo : Repository<Category>, ICategoryRepo
    {
        private readonly AppDBContext _context;
        public CategoryRepo(AppDBContext context) : base(context)
        {
            _context = context;
        }

    }




}

