using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RP1.DataAccess.Repository;
using RP1.Models;
using RP1.Services;

namespace MyProj_L00148202.Pages.Admin.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Category Category { get; set; }

        public void OnGet(int id)
        {
            // Load the category from the database for display
            Category = _unitOfWork.CategoryRepo.Get(id);
        }

        public IActionResult OnPost()
        {
            if (Category?.Id == null)
                return RedirectToPage("Index");

            var categoryToDelete = _unitOfWork.CategoryRepo.Get(Category.Id);
            if (categoryToDelete == null)
                return RedirectToPage("Index");

            // Remove from DbSet
            _unitOfWork.CategoryRepo.Delete(categoryToDelete);
            // Save changes
            _unitOfWork.SaveAsync();

            // Go to Index
            return RedirectToPage("Index");
        }
    }
}