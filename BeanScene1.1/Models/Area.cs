using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeanScene1._1.Models
{
    public class Area
    {
        [Key]
        [DisplayName("Area ID")]
        
        public int AreaId { get; set; } 
        [DisplayName("Area Name")]
        [AllowedValues("Balcony","Outside", "Main")]
        
        public string AreaName { get; set; } = null!;

        public ICollection<Table> Tables { get; set; } = new List<Table>();
    }


}

