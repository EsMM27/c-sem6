using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RP1.Models;
using RP1.Services;
using System.Security.Claims;

namespace RP1_L00148202.Pages.Customer.Cart
{
    public class IndexModel : PageModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        public double CartTotal;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                if (claim != null)
                {
                    // Get the shopping cart for the logged-in user, including Product & Category
                    ShoppingCartList = _unitOfWork.ShoppingCartRepo.GetShoppingCartProduct(claim.Value);

                    if (ShoppingCartList == null)
                    {
                        Console.WriteLine("ShoppingCartList is null.");
                        return;
                    }

                    // Calculate the cart total
                    CartTotal = 0; // reset before summing
                    foreach (var item in ShoppingCartList)
                    {
                        if (item?.Product == null)
                        {
                            Console.WriteLine($"Product is null for ShoppingCart ID: {item?.Id}");
                            continue;
                        }

                        CartTotal += item.Product.Price * item.Quantity;
                    }
                }
            }
        }



        public IActionResult OnPostPlus(int CartID)
        {
            var cart = _unitOfWork.ShoppingCartRepo.Get(CartID);
            _unitOfWork.ShoppingCartRepo.IncrementQty(cart, 1);
            return RedirectToPage("/Customer/Cart/Index");
        }

        public IActionResult OnPostMinus(int CartID)
        {
            var cart = _unitOfWork.ShoppingCartRepo.Get(CartID);
            if (cart.Quantity == 1)
            {
                _unitOfWork.ShoppingCartRepo.Delete(cart);
                _unitOfWork.SaveAsync();
            }
            else
            {
                _unitOfWork.ShoppingCartRepo.DecrementQty(cart, 1);
            }
            return RedirectToPage("/Customer/Cart/Index");
        }

        public IActionResult OnPostRemove(int CartID)
        {
            var cart = _unitOfWork.ShoppingCartRepo.Get(CartID);
            _unitOfWork.ShoppingCartRepo.Delete(cart);
            _unitOfWork.SaveAsync();
            return RedirectToPage("/Customer/Cart/Index");
        }
    }
}
