namespace Vezeeta.Models.Patients
{
    public class GetAllPatients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int GenderId { get; set; }
        public DateTime Date { get; set; }
        public string Area { get; set; }
        public int AreaId { get; set; }
    }
}
