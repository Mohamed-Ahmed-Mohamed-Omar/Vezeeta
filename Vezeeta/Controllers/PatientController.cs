using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vezeeta.Models.Patients;
using Vezeeta.Service.Interface;

namespace Vezeeta.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IAreaRepository _areaRepository;

        public PatientController(IPatientRepository patientRepository, IAreaRepository areaRepository)
        {
            _patientRepository = patientRepository;
            _areaRepository = areaRepository;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _patientRepository.GetAllList();

            return View(data);
        }

        public async Task<IActionResult> Create()
        {

            var area = await _areaRepository.GetAll();

            ViewBag.AreaList = new SelectList(area, "Id", "AreaName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]AddPatient addPatient)
        {
            if (ModelState.IsValid)
            {
                var result = await _patientRepository.Create(addPatient);

                if (result == "Patient Added Successfully")
                {
                    return RedirectToAction(nameof(Index));
                }

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

            var areaList = await _areaRepository.GetAll();

            ViewBag.AreaList = new SelectList(areaList, "Id", "AreaName", patient.AreaId);

            // Convert patient to UpdatePatients if necessary
            var model = new UpdatePatients
            {
                Id = patient.Id,
                Name = patient.Name,
                phone = patient.phone,
                Email = patient.Email,
                GenId = patient.GenId,
                Date = patient.Date,
                AreaId = patient.AreaId,
                Image = patient.Image,
                Report = patient.Report
            };

            return View(model);
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
