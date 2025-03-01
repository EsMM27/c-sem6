using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RP1.Models;
using RP1.Services;

namespace RP1_L00148202.Pages.Customer.Home
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Product Product { get; set; }
        public void OnGet(int id)
        {
            Product = _unitOfWork.ProductRepo.Get(id);
        }
    }
}
