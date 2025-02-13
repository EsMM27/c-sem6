using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RP1.DataAccess;
using RP1.Models;
using RP1.Services;

namespace RP1_L00148202.Pages.Admin.Products
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IEnumerable<Product> Products;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IList<Product> Product { get; set; } = default;
        public void OnGet()
        {
            Products = _unitOfWork.ProductRepo.GetAll();
        }
    }
}
