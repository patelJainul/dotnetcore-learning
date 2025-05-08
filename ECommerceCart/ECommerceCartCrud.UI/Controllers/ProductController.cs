using ECommerceCartCrud.Core.DTO.CartDto;
using ECommerceCartCrud.Core.ServiceContracts;
using ECommerceCartCrud.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceCartCrud.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductController(IProductServices productServices, ICartServices cartServices)
        : Controller
    {
        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            var products = await productServices.GetAllProducts();
            _ = Guid.TryParse(Request.Cookies["cartId"], out var cartId);
            CartResponse? cart = null;
            if (cartId != default || cartId != Guid.Empty)
            {
                cart = await cartServices.GetCartById(cartId);
            }
            var productViewModel = new ProductViewModel { products = products, Cart = cart };
            return View(productViewModel);
        }
    }
}
