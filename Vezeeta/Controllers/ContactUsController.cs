using Microsoft.AspNetCore.Mvc;
using Vezeeta.Models.Contact_us;
using Vezeeta.Service.Interface;

namespace Vezeeta.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly IContactUsRepository _contactUsRepository;

        public ContactUsController(IContactUsRepository contactUsRepository)
        {
            _contactUsRepository = contactUsRepository;
        }

        public async Task<IActionResult> Index()
        {
            var contactUsList = await _contactUsRepository.GetAllContactUs();

            return View(contactUsList); // Pass the list to the View
        }


        // GET: ContactUs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactUs/Create
        [HttpPost]
        public async Task<IActionResult> Create(AddContactUsVM addContactUsVm)
        {
            if (ModelState.IsValid)
            {
                var result = await _contactUsRepository.AddContactUs(addContactUsVm);

                if (result == "Contact Us Added Successfully")
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result); // Handle errors
            }
            return View(addContactUsVm);
        }
    }
}
