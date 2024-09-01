namespace Vezeeta.Models.Patients
{
    public class AddPatient
    {
        public string Name { get; set; }
        public string phone { get; set; }
        public string Email { get; set; }
        public int GenderId { get; set; }
        public DateTime Date { get; set; }
        public int AreaId { get; set; }
    }
}
