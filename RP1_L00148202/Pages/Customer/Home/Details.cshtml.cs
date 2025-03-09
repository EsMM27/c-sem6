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

        public IActionResult OnPost()
        {
            ShoppingCart shoppingCartFromDb = _unitOfWork.ShoppingCartRepo.IncrementItem(ShoppingCart.ApplicationUserId, ShoppingCart.ProductId);
            if (shoppingCartFromDb == null)
            {
                _unitOfWork.ShoppingCartRepo.Add(ShoppingCart);
                _unitOfWork.SaveAsync();
            }
            else
            {
                shoppingCartFromDb.Quantity += ShoppingCart.Quantity;
                _unitOfWork.ShoppingCartRepo.Update(shoppingCartFromDb);
                _unitOfWork.SaveAsync();
            }
            if (ModelState.IsValid)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
