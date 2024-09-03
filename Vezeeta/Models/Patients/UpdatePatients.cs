namespace Vezeeta.Models.Patients
{
    public class UpdatePatients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string phone { get; set; }
        public string Email { get; set; }
        public int GenId { get; set; }
        public DateTime Date { get; set; }
        public int AreaId { get; set; }
        public IFormFile? Report_Url { get; set; }
        public string? Report { get; set; }
        public IFormFile? Image_Url { get; set; }
        public string? Image { get; set; }
    }
}
