using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeanScene1._1.Models
{
    public class ReservationTable
    {
        
        
        public int TableId { get; set; }
       
        public int ReservationId { get; set; }

        public Table Table { get; set; }
        public Reservations Reservations { get; set; }
        

    }
}
