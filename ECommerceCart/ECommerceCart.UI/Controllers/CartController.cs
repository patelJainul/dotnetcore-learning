using System.Security.Claims;
using ECommerceCart.Core.DTO;
using ECommerceCart.Core.Helpers;
using ECommerceCart.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceCart.UI.Controllers
{
    public class CartController(ICartServices cartServices) : Controller
    {
        private readonly ICartServices _cartServices = cartServices;

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            CartResponse? cartData = null;
            Guid? userId = Guid.TryParse(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                out var parsedUserId
            )
                ? parsedUserId
                : null;

            Guid? cartId = Guid.TryParse(Request.Cookies["cartId"], out var parsedCartId)
                ? parsedCartId
                : null;

            if (userId != null)
            {
                cartData = await _cartServices.GetUserCart(userId.Value);
            }

            if (cartId != null)
            {
                cartData = await _cartServices.GetCart(cartId.Value);
            }
            return View(cartData);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddToCart(
            Guid productId,
            int quantity = 1,
            string? returnUrl = null
        )
        {
            ValidationHelper.ValidateGuid(productId, nameof(productId));
            Guid? userId = Guid.TryParse(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                out var parsedUserId
            )
                ? parsedUserId
                : null;

            var newCartItem = new AddCartRequest
            {
                UserId = userId,
                CartId = Guid.TryParse(Request.Cookies["cartId"], out var cartId) ? cartId : null,
                ProductId = productId,
                Quantity = quantity,
            };
            var cartItem = await _cartServices.AddUpdateToCart(newCartItem);
            if (cartItem == null)
            {
                return BadRequest("Failed to add item to cart.");
            }
            Response.Cookies.Append(
                "cartId",
                cartItem.CartId.ToString(),
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                }
            );
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(ProductsController.Index), "Products");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RemoveFromCart(Guid productId)
        {
            ValidationHelper.ValidateGuid(productId, nameof(productId));
            Guid? userId = Guid.TryParse(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                out var parsedUserId
            )
                ? parsedUserId
                : null;

            Guid? cartId = Guid.TryParse(Request.Cookies["cartId"], out var parsedCartId)
                ? parsedCartId
                : null;

            await _cartServices.RemoveFromCart(
                userId: userId,
                cartId: cartId,
                productId: productId
            );
            return RedirectToAction("Index", "Cart");
        }
    }
}
