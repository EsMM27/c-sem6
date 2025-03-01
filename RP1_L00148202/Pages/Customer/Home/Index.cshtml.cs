using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RP1.Models;
using RP1.Services;

namespace RP1_L00148202.Pages.Customer.Home
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Product> listOfProduct { get; set; }
        public IEnumerable<Category> listOfcategory { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public void OnGet()
        {
            listOfProduct = _unitOfWork.ProductRepo.GetAll();
            listOfcategory = _unitOfWork.CategoryRepo.GetAll();
            if (!string.IsNullOrEmpty(SearchString))
            {
                listOfProduct = listOfProduct.Where(p => p.Name.Contains(SearchString,StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}
