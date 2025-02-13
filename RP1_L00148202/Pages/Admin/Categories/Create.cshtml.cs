using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RP1.DataAccess;
using RP1.DataAccess.Repository;
using RP1.Models;
using RP1.Services;

namespace RP1_L00148202.Pages.Admin.Categories
{
    public class CreateModel : PageModel
    {
        //readonly AppDBContext _context;
        //private readonly ICategoryRepo _categoryRepo;
        private readonly IUnitOfWork _unitOfWork;
        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public Category Category { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepo.Add(category);
                _unitOfWork.SaveAsync();
            }

            return RedirectToPage("Index");
        }

    }
}
