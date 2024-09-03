namespace Vezeeta.Models.Patients
{
    public class GetAllPatients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string phone { get; set; }
        public string Email { get; set; }
        public int GenId { get; set; }
        public DateTime Date { get; set; }
        public int AreaId { get; set; }
        public string Area { get; set; }
        public string Image { get; set; }
        public string Report { get; set; }
    }
}
