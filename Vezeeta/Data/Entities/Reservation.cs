using System.ComponentModel.DataAnnotations.Schema;

namespace Vezeeta.Data.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        [ForeignKey(nameof(PatientId))]
        public Patient Patient { get; set; }
        public int PatientId { get; set; }
        public string NumPatient { get; set; }
        [ForeignKey(nameof(DoctorId))]
        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }
        public string Clinic_Address { get; set; }
        public string Clinic_Num { get; set; }
    }
}
