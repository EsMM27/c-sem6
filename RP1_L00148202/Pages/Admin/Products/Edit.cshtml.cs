using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RP1.DataAccess;
using RP1.Models;
using RP1.Services;

namespace RP1_L00148202.Pages.Admin.Products
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public EditModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync([FromRoute] int id)
        {
            Product = _unitOfWork.ProductRepo.Get(id);
            if (Product == null)
            {
                Console.WriteLine($"Product with ID {id} not found.");
                return NotFound();
            }


            ViewData["CategoryID"] = new SelectList(
               _unitOfWork.CategoryRepo.GetAll()
                   .Select(c => new { c.Id, c.Name }), "Id", "Name", Product.CategoryID);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingProduct = _unitOfWork.ProductRepo.Get(Product.ID);
            if (existingProduct == null)
            {
                return NotFound();
            }

            var file = Request.Form.Files["files"];

            if (file != null && file.Length > 0)
            {
                string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images", "Products");
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                // Remove old image
                if (!string.IsNullOrEmpty(existingProduct.Image))
                {
                    string oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, existingProduct.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Save new image
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
                Product.Image = existingProduct.Image; // Retain old image if no new file is uploaded
            }

            existingProduct.Name = Product.Name;
            existingProduct.Description = Product.Description;
            existingProduct.Price = Product.Price;
            existingProduct.CategoryID = Product.CategoryID;
            existingProduct.Image = Product.Image;

            _unitOfWork.ProductRepo.Update(existingProduct);
            await _unitOfWork.SaveAsync();

            return RedirectToPage("./Index");
        }
    }
}
