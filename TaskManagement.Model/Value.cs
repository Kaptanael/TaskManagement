using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Model
{
    [Table("Value")] 
    public class Value
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
