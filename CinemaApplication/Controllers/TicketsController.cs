using Cinema.Domain.DomainModels;
using Cinema.Domain.DTO;
using Cinema.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CinemaApplication.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        // GET: TicketsController
        public ActionResult Index()
        {
            var tickets = new TicketDto
            {
                Tickets = _ticketService.GetAllTickets(),
                Date = DateTime.Now
            };
            return View(tickets);
        }
        [HttpPost]
        public IActionResult Index(TicketDto dto)
        {
            var tickets = _ticketService.GetAllTickets()
                .Where(z => z.StartDate <= dto.Date && z.EndDate >= dto.Date).ToList();
            var model = new TicketDto
            {
                Tickets = tickets,
                Date = dto.Date
            };
            return View(model);
        }

        // GET: TicketsController/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = this._ticketService.GetDetailsForTicket(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: TicketsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("MovieName,MovieYear,MovieGenre,MovieDescription,MovieImage,TicketPrice,StartDate,EndDate")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                this._ticketService.CreateNewTicket(ticket);
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: TicketsController/Edit/5
        public ActionResult Edit(Guid? t)
        {
            if (t == null)
            {
                return NotFound();
            }

            var ticket = this._ticketService.GetDetailsForTicket(t);

            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: TicketsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, [Bind("Id,MovieName,MovieYear,MovieGenre,MovieDescription,MovieImage,TicketPrice,StartDate,EndDate")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._ticketService.UpdateExistingTicket(ticket);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: TicketsController/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = this._ticketService.GetDetailsForTicket(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: TicketsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            this._ticketService.DeleteTicket(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult AddTicketToCart(Guid? id)
        {
            var model = this._ticketService.GetShoppingCartInfo(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTicketToCart([Bind("TicketId", "Quantity")] AddToShoppingCartDto item)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._ticketService.AddToShoppingCart(item, userId);

            if (result)
            {
                return RedirectToAction("Index", "Tickets");
            }

            return View(item);
        }

        private bool TicketExists(Guid id)
        {
            return this._ticketService.GetDetailsForTicket(id) != null;
        }
    }
}
