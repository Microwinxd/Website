using BeanScene1._1.Data;
using BeanScene1._1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BeanScene1._1.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin") || User.IsInRole("Staff"))
            {
                var AllReservations = await _context.Reservations
                    .Include(r => r.AppUser)
                    .Include(r => r.ReservationTables)
                    .ThenInclude(rt => rt.Table)
                    .ToListAsync();
                return View(AllReservations);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var reservations = await _context.Reservations
                .Where(r => r.UserId == userId)
                .Include(r => r.AppUser)
                .Include(r => r.ReservationTables)
                    .ThenInclude(rt => rt.Table)
                .ToListAsync();

            return View(reservations);
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            Reservations? reservation;

            if (User.IsInRole("Admin") || User.IsInRole("Staff"))
            {
                reservation = await _context.Reservations
                    .Include(r => r.AppUser)
                    .Include(r => r.ReservationTables)
                        .ThenInclude(rt => rt.Table)
                    .FirstOrDefaultAsync(r => r.ReservationsId == id);
            }
            else
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                reservation = await _context.Reservations
                    .Include(r => r.AppUser)
                    .Include(r => r.ReservationTables)
                        .ThenInclude(rt => rt.Table)
                    .FirstOrDefaultAsync(r => r.ReservationsId == id && r.UserId == userId);
            }

            if (reservation == null)
                return NotFound();

            return View(reservation);
        }


        // GET: Reservations/Create
        public IActionResult Create()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var reservation = new Reservations
            {
                Email = email ?? "",
                Status = "Pending"
            };

            return View(reservation);
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
    [Bind("Firstname,Lastname,Status,StartTime,Duration,Phone,Notes,ReservationType")]
    Reservations reservations)
        {
            // REMOVE Email from validation (because we set it server-side)
            ModelState.Remove(nameof(Reservations.Email));
            ModelState.Remove(nameof(Reservations.UserId));
            ModelState.Remove(nameof(Reservations.CreatedAt));

            if (!ModelState.IsValid)
                return View(reservations);

            reservations.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            reservations.Email = User.FindFirstValue(ClaimTypes.Email)!;
            reservations.CreatedAt = DateTime.UtcNow;

            _context.Add(reservations);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            Reservations? reservations;

            if (User.IsInRole("Admin") || User.IsInRole("Staff"))
            {
                reservations = await _context.Reservations
                    .Include(r => r.ReservationTables)
                        .ThenInclude(rt => rt.Table)
                    .FirstOrDefaultAsync(r => r.ReservationsId == id);
            }
            else
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                reservations = await _context.Reservations
                    .Include(r => r.ReservationTables)
                        .ThenInclude(rt => rt.Table)
                    .FirstOrDefaultAsync(r => r.ReservationsId == id && r.UserId == userId);
            }

            if (reservations == null)
                return NotFound();

            return View(reservations);
        }


        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
     [Bind("ReservationsId,Firstname,Lastname,Status,StartTime,Duration,Phone,Notes,ReservationType")]
    Reservations reservations)
        {
            Reservations? existing;

            if (User.IsInRole("Admin") || User.IsInRole("Staff"))
            {
                existing = await _context.Reservations
                    .Include(r => r.ReservationTables)
                    .FirstOrDefaultAsync(r => r.ReservationsId == reservations.ReservationsId);
            }
            else
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                existing = await _context.Reservations
                    .Include(r => r.ReservationTables)
                    .FirstOrDefaultAsync(r => r.ReservationsId == reservations.ReservationsId && r.UserId == userId);

            }

            if (existing == null)
                return NotFound();

            
            ModelState.Remove(nameof(Reservations.Email));
            ModelState.Remove(nameof(Reservations.UserId));
            ModelState.Remove(nameof(Reservations.CreatedAt));

            if (!ModelState.IsValid)
            { 
                reservations.ReservationTables = existing.ReservationTables;
                return View(reservations);
            }

            // UPDATE ALLOWED FIELDS ONLY
            existing.Firstname = reservations.Firstname;
            existing.Lastname = reservations.Lastname;
            existing.Status = reservations.Status;
            existing.StartTime = reservations.StartTime;
            existing.Duration = reservations.Duration;
            existing.Phone = reservations.Phone;
            existing.Notes = reservations.Notes;
            existing.ReservationType = reservations.ReservationType;


            // AUTO-FREE TABLE WHEN COMPLETE
            if (reservations.Status == "Complete")
            {
                var assignedTable = await _context.ReservationTables
                    .FirstOrDefaultAsync(rt => rt.ReservationId == existing.ReservationsId);

                if (assignedTable != null)
                    _context.ReservationTables.Remove(assignedTable);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Reservations? reservations;

            if (User.IsInRole("Admin") || User.IsInRole("Staff"))
            {
                reservations = await _context.Reservations
                    .Include(r => r.AppUser)
                    .FirstOrDefaultAsync(r => r.ReservationsId == id);
            }
            else
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                reservations = await _context.Reservations
                    .Include(r => r.AppUser)
                    .FirstOrDefaultAsync(r => r.ReservationsId == id && r.UserId == userId);
            }

            if (reservations == null)
                return NotFound();

            return View(reservations);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Reservations? reservations;

            if (User.IsInRole("Admin") || User.IsInRole("Staff"))
            {
                reservations = await _context.Reservations.FindAsync(id);
            }
            else
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                reservations = await _context.Reservations
                    .FirstOrDefaultAsync(r => r.ReservationsId == id && r.UserId == userId);
            }

            if (reservations == null)
                return NotFound();

            _context.Reservations.Remove(reservations);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET  /Reservations/AssignTable/5
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> AssignTable(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.ReservationTables)
                .FirstOrDefaultAsync(r => r.ReservationsId == id);

            if (reservation == null)
                return NotFound();

           
            var start = reservation.StartTime;
            var end = reservation.StartTime.AddMinutes(reservation.Duration);

            
            var allTables = await _context.Tables.ToListAsync();

            
            var bookedTableIds = await _context.ReservationTables
                .Include(rt => rt.Reservations)
                .Where(rt =>
                    rt.Reservations.StartTime < end &&
                    rt.Reservations.StartTime.AddMinutes(rt.Reservations.Duration) > start
                )
                .Select(rt => rt.TableId)
                .ToListAsync();

            
            var freeTables = allTables
                .Where(t => !bookedTableIds.Contains(t.TableId))
                .ToList();

            
            ViewBag.TableId = new SelectList(freeTables, "TableId", "TableName");

            return View(reservation);
        }



        // POST  /Reservations/AssignTable/5

        [Authorize(Roles = "Admin,Staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignTable(int id, int tableId)
        {
            var reservation = await _context.Reservations
                .Include(r => r.ReservationTables)
                .FirstOrDefaultAsync(r => r.ReservationsId == id);

            if (reservation == null)
                return NotFound();

            //  Calculate this reservation's time window
            var start = reservation.StartTime;
            var end = reservation.StartTime.AddMinutes(reservation.Duration);

            //  Check if this table is already booked at overlapping time
            var tableAlreadyBooked = await _context.ReservationTables
                .Include(rt => rt.Reservations)
                .AnyAsync(rt =>
                    rt.TableId == tableId &&
                    rt.ReservationId != id &&
                    rt.Reservations.StartTime < end &&
                    rt.Reservations.StartTime.AddMinutes(rt.Reservations.Duration) > start
                );

            if (tableAlreadyBooked)
            {
                ModelState.AddModelError("", "This table is already assigned for the selected time slot.");
                

                var bookedTableIds = await _context.ReservationTables
                    .Include(rt => rt.Reservations)
                    .Where(rt =>
                        rt.Reservations.StartTime < end &&
                        rt.Reservations.StartTime.AddMinutes(rt.Reservations.Duration) > start
                    )
                    .Select(rt => rt.TableId)
                    .ToListAsync();

                var freeTables = await _context.Tables
                    .Where(t => !bookedTableIds.Contains(t.TableId))
                    .ToListAsync();

                ViewBag.TableId = new SelectList(freeTables, "TableId", "TableName");

                return View(reservation);
            }

            // Ensure collection exists
            reservation.ReservationTables ??= new List<ReservationTable>();

            // Remove current assignment if changing tables
            var existing = reservation.ReservationTables.FirstOrDefault();
            if (existing != null)
                _context.ReservationTables.Remove(existing);

            // Assign the new table
            reservation.ReservationTables.Add(new ReservationTable
            {
                ReservationId = id,
                TableId = tableId
            });


            if (reservation.Status == "Pending")
                reservation.Status = "Confirmed";

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
