using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeanScene1._1.Models
{
    public class Report

    {
        [Key]
        public int ReportId { get; set; }
        public int TotalReservations { get; set; }
        public int Completed { get; set; }
        public int PendingOrConfirmed { get; set; }
        public int Cancelled { get; set; }
        public int TodayReservations { get; set; }
        public int ThisWeekReservations { get; set; }
        public int ThisMonthReservations { get; set; }
        public int UpcomingReservations { get; set; }
        public int OverdueReservations { get; set; }
        public int Breakfast { get; set; }
        public int Lunch { get; set; }
        public int Dinner { get; set; }


    }
}
