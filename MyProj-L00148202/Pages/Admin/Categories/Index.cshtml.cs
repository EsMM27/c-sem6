using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RP1.DataAccess.Repository;
using RP1.Models;
using RP1.Services;


namespace MyProj_L00148202.Pages.Admin.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IEnumerable<Category> Categories;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            Categories = _unitOfWork.CategoryRepo.GetAll();
        }
    }
}
