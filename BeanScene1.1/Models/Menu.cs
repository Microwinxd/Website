using System.ComponentModel.DataAnnotations;

namespace BeanScene1._1.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        
        public string ItemName { get; set; } 
        
        [RegularExpression("Breakfast|Lunch|Dinner")]
        public string ItemType { get; set; } 
       
        public int ItemPrice { get; set; }
        
    }
}
