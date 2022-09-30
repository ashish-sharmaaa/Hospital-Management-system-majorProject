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
    
  
    public class Departments : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public AdminViewModels AVM { get; set; }

        public Departments(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        [Authorize(Roles = "Admin,User")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddDepartment(int? id)
        {
            AVM = new AdminViewModels()
            {
                Department = new HMSProject.Models.Department(),
                DoctorsList = _unitofWork.Doctors.GetDropDownListForDoctors()
            };

            if(id != null)
            {
                AVM.Department = _unitofWork.Department.Get(id.GetValueOrDefault());
            }

            return View(AVM);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDepartment()
        {
           
                if(AVM.Department.Id == 0)
                {
                    _unitofWork.Department.Add(AVM.Department);
                }
                else
                {
                    _unitofWork.Department.Update(AVM.Department);
                }

                _unitofWork.Save();

                return RedirectToAction(nameof(Index));
            
        }

        #region API CALLS

        public IActionResult GetAll()
        {
            return Json(new { data = _unitofWork.Department.GetAll(includeProperties: "Doctors") });
        }

        #endregion
    }
}
