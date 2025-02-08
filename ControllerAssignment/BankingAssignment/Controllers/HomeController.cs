using BankingAssignment.Model;
using Microsoft.AspNetCore.Mvc;

namespace BankingAssignment.Controllers
{
    public class HomeController : Controller
    {
        readonly BankAccount bankAccount = new();
        // GET: HomeController
        public IActionResult Index()
        {
            return Content("Welcome to the Banking Application");
        }


        [Route("account-details")]
        public IActionResult AccountDetails()
        {
            return Json(bankAccount.AccountDetails);
        }


        [Route("account-statement")]
        public IActionResult AccountStateMent()
        {

            return File("sample.pdf", "Application/pdf");

        }

        [Route("get-current-balance/{accountNumber?}")]
        public IActionResult GetCurrentBalance(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber))
            {
                return BadRequest("Account Number is required");
            }
            if (accountNumber != bankAccount.AccountDetails.AccountNumber)
            {
                return NotFound("Account Number not found");
            }
            return Content($"Current Balance: {bankAccount.AccountDetails.CurrentBalance}");
        }


    }

}
