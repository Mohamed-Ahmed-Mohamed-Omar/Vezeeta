using System.ComponentModel.DataAnnotations.Schema;

namespace Vezeeta.Data.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string phone { get; set; }
        public string Email { get; set; }
        public int GenId { get; set; } 
        public DateTime Date { get; set; }
        [ForeignKey(nameof(AreaId))]
        public Area Area { get; set; }
        public int AreaId { get; set; }
        public string? Report { get; set; }
        public string? Image { get; set; }
    }
}
