
using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedAt{ get; set; }
    }
}
