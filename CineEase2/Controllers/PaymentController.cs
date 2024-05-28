using CineEase2.Data;
using CineEase2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CineEase2.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
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
            if (ModelState.IsValid)
            {
                // İndirim hesaplama
                if (model.Ticket.discount == "Student")
                {
                    model.Ticket.netprice = model.Ticket.price * 0.8f; // %20 öğrenci indirimi
                }
                else if (model.Ticket.discount == "Senior")
                {
                    model.Ticket.netprice = model.Ticket.price * 0.85f; // %15 yaşlı indirimi
                }
                else
                {
                    model.Ticket.netprice = model.Ticket.price;
                }

                // Ödeme başarılıysa bilet detaylarını veritabanına kaydedelim
                _context.Ticket.Add(model.Ticket);
                await _context.SaveChangesAsync();

                // Seçilen indirim seçeneği ve uygulanan indirimi göster
                ViewBag.SelectedDiscount = model.Ticket.discount;
                ViewBag.AppliedDiscount = model.Ticket.netprice != model.Ticket.price ? model.Ticket.netprice : 0.0f;

                ViewBag.Message = "Payment Successful!";
                return View("Confirmation", model);
            }

            ViewBag.DiscountTypes = GetDiscountTypes();
            return View("Index", model);
        }

        private List<SelectListItem> GetDiscountTypes()
        {
            return new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "Seçiniz" },
            new SelectListItem { Value = "Student", Text = "Öğrenci" },
            new SelectListItem { Value = "Senior", Text = "Yaşlı" }
        };
        }
    }
}
