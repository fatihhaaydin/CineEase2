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
        public async Task<IActionResult> Create([Bind("Id,price,discount,netprice,CreditCardNumber,ExpirationDate,CVV,UserId,SeatNumber,IsSold")] Ticket model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if (model.discount == "Öğrenci")
                {
                    model.netprice = model.price * 0.8f;
                }
                else if (model.discount == "65 yaş üstü")
                {
                    model.netprice = model.price * 0.85f;
                }
                else if (model.discount == "7 yaş altı")
                {
                    model.netprice = model.price * 0.65f;
                }
                else if (model.discount == "Yetişkin")
                {
                    model.netprice = model.price;
                }

                model.UserId = user.Id;
                model.IsSold = true;

                _context.Ticket.Add(model);
                await _context.SaveChangesAsync();

                return Json(new { success = true, selectedSeat = model.SeatNumber, message = "Ödeme başarıyla gerçekleştirildi." });
            }
            return Json(new { success = false, message = "Ödeme başarısız. Lütfen tekrar deneyin." });
        }
        [HttpGet]
        public async Task<IActionResult> GetSoldSeats()
        {
            // Veritabanından satılan koltukları getirin
            var soldSeats = await _context.Ticket
                .Where(t => t.IsSold) // Satılan koltukları filtreleyin
                .Select(t => t.SeatNumber) // Sadece koltuk numaralarını seçin
                .ToListAsync();

            return Json(soldSeats); // Satılan koltuk numaralarını JSON olarak döndürün
        }
        public IActionResult Success()
        {
            return View();
        }
    }

}

