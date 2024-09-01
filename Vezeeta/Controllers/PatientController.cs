using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vezeeta.Models.Patients;
using Vezeeta.Service.Interface;

namespace Vezeeta.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IAreaRepository _areaRepository;

        public PatientController(IPatientRepository patientRepository, IGenderRepository genderRepository, IAreaRepository areaRepository)
        {
            _patientRepository = patientRepository;
            _genderRepository = genderRepository;
            _areaRepository = areaRepository;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _patientRepository.GetAllList();

            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            var gen = await _genderRepository.GetAll();

            ViewBag.GenList = new SelectList(gen, "Id", "GenderName");

            var area = await _areaRepository.GetAll();

            ViewBag.AreaList = new SelectList(area, "Id", "AreaName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddPatient addPatient)
        {
            if (ModelState.IsValid)
            {
                var result = await _patientRepository.Create(addPatient);

                if (result == "Patient Added Successfully")
                {
                    return RedirectToAction(nameof(Index));
                }

                var gen = await _genderRepository.GetAll();

                ViewBag.GenList = new SelectList(gen, "Id", "GenderName");

                var area = await _areaRepository.GetAll();

                ViewBag.AreaList = new SelectList(area, "Id", "AreaName");

                ModelState.AddModelError("", result);
            }
            return View(addPatient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var patient = await _patientRepository.GetById(id);
            if (patient == null)
            {
                return NotFound();
            }

            // Mapping from GetAllPatients to UpdatePatients
            var updatePatient = new UpdatePatients
            {
                Id = patient.Id,
                Name = patient.Name,
                phone = patient.phone,
                Email = patient.Email,
                GenderId = patient.GenderId,
                Date = patient.Date,
                AreaId = patient.AreaId
            };

            var gen = await _genderRepository.GetAll();
            ViewBag.GenList = new SelectList(gen, "Id", "GenderName", updatePatient.GenderId);

            var area = await _areaRepository.GetAll();
            ViewBag.AreaList = new SelectList(area, "Id", "AreaName", updatePatient.AreaId);

            return View(updatePatient);
        }

        // POST: Patients/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(UpdatePatients patient)
        {
            if (ModelState.IsValid)
            {
                var result = await _patientRepository.Update(patient);
                if (result == "Update Done")
                {
                    return RedirectToAction(nameof(Index));
                }

                var gen = await _genderRepository.GetAll();

                ViewBag.GenList = new SelectList(gen, "Id", "GenderName", patient.GenderId);

                var area = await _areaRepository.GetAll();

                ViewBag.AreaList = new SelectList(area, "Id", "AreaName", patient.AreaId);

                ModelState.AddModelError("", result);
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await _patientRepository.GetById(id);

            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _patientRepository.DeleteById(id);

            if (result == "Deleye Done")
            {
                return RedirectToAction("Index", "Patient");
            }

            return View();
        }
    }
}
