using BeanScene1._1.Data;
using BeanScene1._1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace BeanScene1._1.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string reportType = "Time")
        {

            var now = DateTime.Now;
            var today = now.Date;





            // ✅ Start of week (Monday)
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);

            // ✅ Start of month
            var startOfMonth = new DateTime(today.Year, today.Month, 1);


            ViewBag.ReportType = reportType;

            var report = new Report();

            if (reportType == "Time")
            {
                report.TodayReservations = await _context.Reservations
                     .CountAsync(r =>
                         r.StartTime >= today &&
                         r.StartTime < today.AddDays(1)
                     );
                report.ThisWeekReservations = await _context.Reservations
                    .CountAsync(r =>
                        r.StartTime >= startOfWeek
                    );
                report.ThisMonthReservations = await _context.Reservations
                    .CountAsync(r =>
                        r.StartTime >= startOfMonth
                    );
                if (reportType == "Status")
                {
                    report.UpcomingReservations = await _context.Reservations
                .CountAsync(r =>
                    r.StartTime > now &&
                    r.Status != "Complete" &&
                    r.Status != "Cancelled"
                );

                    // ✅ Overdue / Past (should be complete but isn’t)
                    report.OverdueReservations = await _context.Reservations
                        .CountAsync(r =>
                            r.StartTime < now &&
                            r.Status != "Complete"
                        );


                }
                if (reportType == "ReservationType")
                {
                    {
                        var breakfastCount = await _context.Reservations
                            .CountAsync(r => r.ReservationType == "Breakfast");

                        var lunchCount = await _context.Reservations
                            .CountAsync(r => r.ReservationType == "Lunch");

                        var dinnerCount = await _context.Reservations
                            .CountAsync(r => r.ReservationType == "Dinner");
                    }
                }

            }










            return View(report);
        } 
    }
    }

