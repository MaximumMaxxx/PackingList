using System.ComponentModel.DataAnnotations;

namespace PackingList2.Models
{
    public class Item
    {
        [Key] public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "You must enter a quantity")]
        public int Count { get; set; }

        [Required(ErrorMessage = "Please enter a category")]
        public string Category { get; set; } = "";

        public string Notes { get; set; } = "";
    }
}