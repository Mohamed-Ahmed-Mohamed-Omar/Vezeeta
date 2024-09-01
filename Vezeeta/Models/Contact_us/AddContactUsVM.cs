using System.ComponentModel.DataAnnotations;

namespace Vezeeta.Models.Contact_us
{
    public class AddContactUsVM
    {
        [Required(ErrorMessage = "يرجى إدخال الاسم")]
        public string Name { get; set; }

        [Required(ErrorMessage = "يرجى إدخال رقم الهاتف")]
        public string phone { get; set; }

        [Required(ErrorMessage = "يرجى إدخال الرسالة")]
        public string Message { get; set; }
    }
}
