using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RP1.Models;
using RP1.Services;
using System.Security.Claims;

namespace RP1_L00148202.Pages.Customer.Home
{
    [Authorize(Roles = "Customer,Admin")]
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public ShoppingCart ShoppingCart { get; set; }
        public Product Product { get; set; }

        public void OnGet(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ShoppingCart = new ShoppingCart
            {
                ApplicationUserId = claim.Value,
                Quantity = 1,
                Product = _unitOfWork.ProductRepo.GetProductCategory(id),
                ProductId = id
            };
        }

        public async Task<IActionResult> OnPost()
        {
            if (ShoppingCart == null || ShoppingCart.ProductId == 0 || ShoppingCart.Quantity == 0)
            {
                ModelState.AddModelError("", "Invalid shopping cart data.");
                return Page();
            }

            // Retrieve the current user's ID
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                ModelState.AddModelError("", "User is not authenticated.");
                return Page();
            }

            // Set the ApplicationUserId
            ShoppingCart.ApplicationUserId = claim.Value;

            // Check if the item already exists in the cart
            ShoppingCart shoppingCartFromDb = _unitOfWork.ShoppingCartRepo.IncrementItem(ShoppingCart.ApplicationUserId, ShoppingCart.ProductId);
            if (shoppingCartFromDb == null)
            {
                // Add new item to the cart
                _unitOfWork.ShoppingCartRepo.Add(ShoppingCart);
            }
            else
            {
                // Update existing item's quantity
                shoppingCartFromDb.Quantity += ShoppingCart.Quantity;
                _unitOfWork.ShoppingCartRepo.Update(shoppingCartFromDb);
            }

            // Save changes to the database
            await _unitOfWork.SaveAsync();

            if (ModelState.IsValid)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
