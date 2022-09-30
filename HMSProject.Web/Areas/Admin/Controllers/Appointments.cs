using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HMSProject.DataAccess.IRepository;
using HMSProject.Models.ViewModels;
using HMSProject.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMSProject.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
   
    
    public class Appointments : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public AdminViewModels AVM { get; set; }

        public Appointments(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        [Authorize(Roles = "Admin,User,Employee")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin,User,Employee")]
        public IActionResult AddAppointment(int? id)
        {
            AVM = new AdminViewModels()
            {
                Appointments = new HMSProject.Models.Appointments(),
                DoctorsList = _unitofWork.Doctors.GetDropDownListForDoctors(),
                PatientsList = _unitofWork.Patients.GetDropDownListForPatients()
            };

            if(id != null)
            {
                AVM.Appointments = _unitofWork.Appointments.Get(id.GetValueOrDefault());
            }

            return View(AVM);
        }
        [Authorize(Roles = "Admin,User,Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAppointment()
        {
           
                if(AVM.Appointments.Id == 0)
                {
                    _unitofWork.Appointments.Add(AVM.Appointments);
                }
                else
                {
                    _unitofWork.Appointments.Update(AVM.Appointments);
                }

                _unitofWork.Save();

                return RedirectToAction(nameof(Index));
        
        }

        #region API CALLS

        public IActionResult GetAll()
        {
            return Json(new { data = _unitofWork.Appointments.GetAll(includeProperties: "Doctors,Patients") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var aFromDb = _unitofWork.Appointments.Get(id);

            if(aFromDb == null)
            {
                return Json(new { success = false, message = "Error Deleting Appointment!" });
            }

            _unitofWork.Appointments.Remove(aFromDb);
            _unitofWork.Save();

            return Json(new { success = true, message = "Appointment Deleted Successfully!" });
        }

        #endregion
    }
}
