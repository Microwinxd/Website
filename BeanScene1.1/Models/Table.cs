using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeanScene1._1.Models
{
    public class Table
    {
        [Key]
        [DisplayName("Table ID")]
        public int TableId { get; set; } 
        [ForeignKey(nameof(AreaId))]
        public int AreaId { get; set; }

        [DisplayName("Table Name")]
        [MaxLength(10)]
        public string TableName { get; set; } = null!;
        [MaxLength(10)]
        public string AreaName { get; set; } = null!;

        public Area Areas { get; set; }
        
        public ICollection<ReservationTable> ReservationTables { get; set; }
        
    }
}
