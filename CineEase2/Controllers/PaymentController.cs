using CineEase2.Data;
using CineEase2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CineEase2.Controllers
{
    using Microsoft.AspNetCore.Identity;

    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PaymentController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new PaymentViewModel
            {
                Ticket = new Ticket
                {
                    price = 50.0f // Örnek sabit fiyat
                }
            };

            ViewBag.DiscountTypes = GetDiscountTypes();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(PaymentViewModel model)
        {
            bool isPaymentSuccessful = false;
            if (ModelState.IsValid)
            {
                // Kullanıcının kimliğini al
                var user = await _userManager.GetUserAsync(User);

                // İndirim hesaplama
                if (model.Ticket.discount == "Student")
                {
                    model.Ticket.netprice = model.Ticket.price * 0.8f; // %20 öğrenci indirimi
                }
                else if (model.Ticket.discount == "Senior")
                {
                    model.Ticket.netprice = model.Ticket.price * 0.85f; // %15 yaşlı indirimi
                }
                else if (model.Ticket.discount == "Küçük")
                {
                    model.Ticket.netprice = model.Ticket.price * 0.65f; // %35 küçük indirimi
                }
                else if (model.Ticket.discount == "Yetişkin")
                {
                    model.Ticket.netprice = model.Ticket.price;
                }

                // Kullanıcının Id'sini Ticket nesnesine ata
                model.Ticket.UserId = user.Id;

                _context.Ticket.Add(model.Ticket);
                await _context.SaveChangesAsync();
                TempData["PaymentSuccess"] = "Ödeme başarıyla gerçekleştirildi.";
                isPaymentSuccessful = true;
                return RedirectToAction("Index", "Ticket");
            }

            return Json(new { success = isPaymentSuccessful, message = "Ödeme başarısız. Lütfen tekrar deneyin." });
        }

        private List<SelectListItem> GetDiscountTypes()
        {
            return new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "Seçiniz" },
            new SelectListItem { Value = "Student", Text = "Öğrenci" },
            new SelectListItem { Value = "Senior", Text = "65 yaş üstü" },
            new SelectListItem { Value = "Küçük", Text = "7 yaş altı" },
            new SelectListItem { Value = "Yetişkin", Text = "Yetişkin" }
        };
        }
    }
}
