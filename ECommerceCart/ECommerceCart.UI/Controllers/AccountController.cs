using ECommerceCart.Core.Domain.Entities.Identity;
using ECommerceCart.Core.DTO;
using ECommerceCart.Core.Enums;
using ECommerceCart.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceCart.UI.Controllers
{
    public class AccountController(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        SignInManager<ApplicationUser> signInManager,
        ICartServices cartServices
    ) : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly ICartServices _cartServices = cartServices;

        // GET: AccountController
        [HttpGet]
        [Authorize("NotAuthorized")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Authorize("NotAuthorized")]
        public async Task<IActionResult> Login(LoginDTO loginDTO, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDTO);
            }

            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "The email address is not registered.");
                return View(loginDTO);
            }

            var result = await _signInManager.PasswordSignInAsync(
                user,
                loginDTO.Password,
                false,
                false
            );
            if (result.Succeeded)
            {
                var cart = await _cartServices.GetUserCart(user.Id);
                Response.Cookies.Append("cartId", cart?.CartId.ToString() ?? string.Empty);
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(loginDTO);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            Response.Cookies.Delete("cartId");
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [Authorize("NotAuthorized")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Authorize("NotAuthorized")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(registerDTO);
            }

            var user = new ApplicationUser
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                PersonName = registerDTO.PersonName,
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (result.Succeeded)
            {
                var isCartExist = Guid.TryParse(Request.Cookies["cartId"], out var cartId);
                if (isCartExist)
                {
                    await _cartServices.AssignCartToUser(user.Id, cartId);
                }
                // Assign the user to the default role
                if (!await _roleManager.RoleExistsAsync(registerDTO.UserRole.ToString()))
                {
                    await _roleManager.CreateAsync(
                        new ApplicationRole() { Name = registerDTO.UserRole.ToString() }
                    );
                }
                {
                    await _roleManager.CreateAsync(
                        new ApplicationRole() { Name = UserRoleOptions.User.ToString() }
                    );
                }
                await _userManager.AddToRoleAsync(user, UserRoleOptions.User.ToString());
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(registerDTO);
        }
    }
}
