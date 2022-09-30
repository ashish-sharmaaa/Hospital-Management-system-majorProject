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
 
   
    public class Rooms : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public AdminViewModels AVM { get; set; }

        public Rooms(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        [Authorize(Roles = "Admin,User")] //here giving authority role to admin and user 
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddRoom(int? id)
        {
            AVM = new AdminViewModels() { Room = new HMSProject.Models.Room() };

            if(id != null)
            {
                AVM.Room = _unitofWork.Room.Get(id.GetValueOrDefault());
            }

            return View(AVM);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddRoom()
        {
           
                if(AVM.Room.Id == 0)
                {
                    _unitofWork.Room.Add(AVM.Room);
                }
                else
                {
                    _unitofWork.Room.Update(AVM.Room);
                }

                _unitofWork.Save();

                return RedirectToAction(nameof(Index));
       
        }
        [Authorize(Roles = "Admin")]
        #region API CALLS

        public IActionResult GetAll()
        {
            return Json(new { data = _unitofWork.Room.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var rFromDb = _unitofWork.Room.Get(id);

            if(rFromDb == null)
            {
                return Json(new { success = false, message = "Error Deleting Room Record!" });
            }

            _unitofWork.Room.Remove(rFromDb);
            _unitofWork.Save();

            return Json(new { success = true, message = "Room Record Deleted Successfully!" });
        }

        #endregion
    }
}
