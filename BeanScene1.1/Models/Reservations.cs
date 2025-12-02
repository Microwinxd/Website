using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeanScene1._1.Models
{
    public class Reservations
    {
        [Key]
        public int ReservationsId { get; set; }

        [DisplayName("First Name")]
        [Required]
        [MaxLength(30)]
        public string Firstname { get; set; } = string.Empty;
        
        [DisplayName("Last Name")]
        [Required]
        [MaxLength(30)]
        public string Lastname { get; set; } = string.Empty;

        [Required]
        [RegularExpression("Pending|Confirmed|Cancelled|Complete")]
        public string Status { get; set; } = "Pending";
        [Required]
        [MaxLength(20)]
        public string ReservationType { get; set; } = "Breakfast";


        [Required]
        [DataType(DataType.DateTime)]
        [DisplayName("Start Time")]
        public DateTime StartTime { get; set; }

        [Required]
        [DisplayName("Duration(In minutes)")]
        public int Duration { get; set; }


        [Required]
        [DisplayName("Email")]
        public string Email { get; set; } = string.Empty;
        
        [Phone]
        [StringLength(10, MinimumLength = 10)]
        [Required]
        [DisplayName("Phone")]
        public string Phone { get; set; } = string.Empty;


        [MaxLength(30)]
        [DisplayName("Notes")]
        public string Notes { get; set; } = "None";

        [Required]
        public string UserId { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        [DisplayName("Creation time")]
        public DateTime CreatedAt { get; set; } = DateTime.Now; 

        [ValidateNever]
        [ForeignKey("UserId")]
        public AppUser? AppUser { get; set; }
        
        [ValidateNever]
        public ICollection<ReservationTable> ReservationTables { get; set; } = new List<ReservationTable>();
        
       

    }

    }

