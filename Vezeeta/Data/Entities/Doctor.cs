using System.ComponentModel.DataAnnotations.Schema;

namespace Vezeeta.Data.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOFCases { get; set; }
        public string TechnicalLevel { get; set; }
        [ForeignKey(nameof(SpecializationId))]
        public Specialization Specialization { get; set; }
        public int SpecializationId { get; set; }
        public string DocNID { get; set; }
        public string DocPhone { get; set; }
        public List<string> appointments { get; set; }
    }
}
