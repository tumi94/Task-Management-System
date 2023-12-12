using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;

namespace Task_Management_System__Server_.Model
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; } = "";
        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; } = "";
        [Column(TypeName = "nvarchar(50)")]
        public string Status { get; set; } = "";
    }
}
