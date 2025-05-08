using ECommerceCartCrud.Core.DTO.CartDto;
using ECommerceCartCrud.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceCartCrud.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class CartController(ICartServices cartServices) : Controller
    {
        // GET: CartController
        public async Task<ActionResult> Index()
        {
            _ = Guid.TryParse(Request.Cookies["cartId"], out var cartId);
            CartResponse? cart = null;
            if (cartId != default || cartId != Guid.Empty)
            {
                cart = await cartServices.GetCartById(cartId);
            }
            return View(cart);
        }

        [HttpPost]
        public async Task<ActionResult> AddToCart(
            Guid productId,
            int quantity = 1,
            string? redirectUrl = null
        )
        {
            _ = Guid.TryParse(Request.Cookies["cartId"], out var cartId);
            var req = new AddProductToCartRequest() { ProductId = productId, Quantity = quantity };
            if (cartId != default || cartId != Guid.Empty)
            {
                req.CartId = cartId;
            }
            else
            {
                req.CartId = Guid.NewGuid();
            }
            var res = await cartServices.AddToCart(req);
            if (res == null)
            {
                return BadRequest("Failed to add product to cart.");
            }

            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(30),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
            };
            Response.Cookies.Append("cartId", res.CartId.ToString(), cookieOptions);
            if (redirectUrl is not null && Url.IsLocalUrl(redirectUrl))
            {
                return Redirect(redirectUrl);
            }
            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public async Task<ActionResult> RemoveFromCart(Guid productId, string? redirectUrl = null)
        {
            _ = Guid.TryParse(Request.Cookies["cartId"], out var cartId);
            var req = new RemoveProductFromCartRequest() { ProductId = productId, CartId = cartId };
            var res = await cartServices.RemoveProductFromCart(req);
            if (res == null)
            {
                return BadRequest("Failed to remove product from cart.");
            }
            if (redirectUrl is not null && Url.IsLocalUrl(redirectUrl))
            {
                return Redirect(redirectUrl);
            }
            return RedirectToAction("Index", "Product");
        }
    }
}
