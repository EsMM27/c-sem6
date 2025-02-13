using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using RP1.DataAccess;
using RP1.Models;
using RP1.Services;

namespace RP1_L00148202.Pages.Admin.Products
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CreateModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoryID"] = new SelectList(
               _unitOfWork.CategoryRepo.GetAll()
                   .Select(c => new { c.Id, c.Name }), "Id", "Name");

            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var file = Request.Form.Files["files"];

            if (file != null && file.Length > 0)
            {
                string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images", "Products");
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(uploadFolder, fileName);


                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                Product.Image = Path.Combine("Images", "Products", fileName).Replace("\\", "/");
            }
            else
            {
                Console.WriteLine("No file uploaded.");
            }

            _unitOfWork.ProductRepo.Add(Product);

            await _unitOfWork.SaveAsync();

            return RedirectToPage("./Index");
        }





    }
}