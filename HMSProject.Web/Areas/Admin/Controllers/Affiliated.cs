﻿using System;
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
    [Authorize(Roles = SD.Admin)]
    [Authorize(Roles = SD.Manager)]
    public class Affiliated : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public AdminViewModels AVM { get; set; }

        public Affiliated(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddAffiliated(int? id)
        {
            AVM = new AdminViewModels()
            {
                Affiliated = new HMSProject.Models.Affiliated(),
                DoctorsList = _unitofWork.Doctors.GetDropDownListForDoctors(),
                DepartmentsList = _unitofWork.Department.GetDropDownListForDepartments()
            };

            if(id != null)
            {
                AVM.Affiliated = _unitofWork.Affiliated.Get(id.GetValueOrDefault());
            }

            return View(AVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAffiliated()
        {
         
                if(AVM.Affiliated.Id == 0)
                {
                    _unitofWork.Affiliated.Add(AVM.Affiliated);
                }
                else
                {
                    _unitofWork.Affiliated.Update(AVM.Affiliated);
                }

                _unitofWork.Save();

                return RedirectToAction(nameof(Index));
           
        }

        #region API CALLS

        public IActionResult GetAll()
        {
            return Json(new { data = _unitofWork.Affiliated.GetAll(includeProperties: "Doctors,Department") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var aFromDb = _unitofWork.Affiliated.Get(id);

            if(aFromDb == null)
            {
                return Json(new { success = false, message = "Error Deleting Affiliated With Record!" });
            }

            _unitofWork.Affiliated.Remove(aFromDb);
            _unitofWork.Save();

            return Json(new { success = true, message = "Affiliated With Record Deleted Successfully!" });
        }

        #endregion
    }
}
