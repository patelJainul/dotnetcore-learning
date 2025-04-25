using System.Security.Claims;
using ECommerceCart.Core.DTO;
using ECommerceCart.Core.ServiceContracts;
using ECommerceCart.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceCart.UI.Controllers
{
    [AllowAnonymous]
    public class ProductsController(IProductServices _productServices, ICartServices _cartServices)
        : Controller
    {
        public async Task<ActionResult> Index()
        {
            List<ProductResponse> products = await _productServices.GetAllProducts();
            CartResponse? cart = null;
            var parsedCartId = Guid.TryParse(Request.Cookies["cartId"], out var cartId);
            var parsedUserId = Guid.TryParse(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                out var userId
            );

            if (parsedUserId)
            {
                cart = await _cartServices.GetUserCart(userId);
            }
            else if (parsedCartId)
            {
                cart = await _cartServices.GetCart(cartId);
            }

            // Get view mode from cookie or set default to Card
            string viewMode = Request.Cookies["productViewMode"] ?? "Card";

            ProductsViewModel productsViewModel = new()
            {
                Products = products,
                Cart = cart,
                ViewMode = viewMode,
            };

            ViewBag.ViewMode = viewMode;
            return View(productsViewModel);
        }

        [HttpPost]
        public IActionResult ChangeView(string viewMode)
        {
            if (string.IsNullOrEmpty(viewMode))
            {
                viewMode = "Card";
            }

            // Save view mode preference to cookie
            Response.Cookies.Append(
                "productViewMode",
                viewMode,
                new CookieOptions
                {
                    Expires = DateTime.Now.AddYears(1),
                    HttpOnly = true,
                    IsEssential = true,
                }
            );

            // Redirect to the Index action of the ProductsController
            return RedirectToAction("Index", "Products");
        }
    }
}
