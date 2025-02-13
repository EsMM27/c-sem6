using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RP1.DataAccess;
using RP1.DataAccess.Repository;
using RP1.Models;
using RP1.Services;

namespace RP1_L00148202.Pages.Admin.Categories
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public Category Category { get; set; }
        public void OnGet(int id)
        {
            Category = _unitOfWork.CategoryRepo.Get(id);
        }
        public IActionResult OnPost(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepo.Update(category);
                _unitOfWork.SaveAsync();
            }

            return RedirectToPage("Index");
        }

    }
}
