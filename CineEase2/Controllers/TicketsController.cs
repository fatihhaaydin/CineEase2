using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CineEase2.Data;
using CineEase2.Models;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;

namespace CineEase2.Controllers
{
    using Microsoft.AspNetCore.Identity;
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TicketsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ticket.ToListAsync());
        }

        [Authorize]
        // GET: Tickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,price,discount,netprice,CreditCardNumber,ExpirationDate,CVV,UserId")] Ticket model)
        {
            bool isPaymentSuccessful = false;

            if (ModelState.IsValid)
            {
                // Kullanıcının kimliğini al
                var user = await _userManager.GetUserAsync(User);

                // İndirim hesaplama
                if (model.discount == "Öğrenci")
                {
                    model.netprice = model.price * 0.8f; // %20 öğrenci indirimi
                }
                else if (model.discount == "65 yaş üstü")
                {
                    model.netprice = model.price * 0.85f; // %15 yaşlı indirimi
                }
                else if (model.discount == "7 yaş altı")
                {
                    model.netprice = model.price * 0.65f; // %35 küçük indirimi
                }
                else if (model.discount == "Yetişkin")
                {
                    model.netprice = model.price;
                }

                // Kullanıcının Id'sini Ticket nesnesine ata
                model.UserId = user.Id;

                _context.Ticket.Add(model);
                await _context.SaveChangesAsync();
                TempData["PaymentSuccess"] = "Ödeme başarıyla gerçekleştirildi.";
                isPaymentSuccessful = true;
                return Json(new { success = isPaymentSuccessful, message = "Ödeme başarıyla gerçekleştirildi.", netPrice = model.netprice });
            }

            return Json(new { success = isPaymentSuccessful, message = "Ödeme başarısız. Lütfen tekrar deneyin." });
        }
    }
}

