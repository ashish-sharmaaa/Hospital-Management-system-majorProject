using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HMSProject.DataAccess.IRepository;
using HMSProject.Models.ViewModels;
using HMSProject.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace HMSProject.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
   
    
    public class Nurses : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public AdminViewModels AVM { get; set; }

        public Nurses(IUnitofWork unitofWork, IWebHostEnvironment hostEnvironment)
        {
            _unitofWork = unitofWork;
            _hostEnvironment = hostEnvironment;
        }
        [Authorize(Roles = "Admin,User")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddNurse(int? id)
        {
            AVM = new AdminViewModels() { Nurses = new HMSProject.Models.Nurses() };

            if(id != null)
            {
                AVM.Nurses = _unitofWork.Nurses.Get(id.GetValueOrDefault());
            }

            return View(AVM);
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNurse()
        {
            string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if(AVM.Nurses.Id == 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"Images\Nurses");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName+extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    AVM.Nurses.ImageUrl = @"\Images\Nurses\" + fileName + extension;

                    _unitofWork.Nurses.Add(AVM.Nurses);
                }
                else
                {
                    var nFromDb = _unitofWork.Nurses.Get(AVM.Nurses.Id);
                    
                    if(files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"Images\Nurses");
                        var extension_new = Path.GetExtension(files[0].FileName);
                        var imagePath = Path.Combine(webRootPath, nFromDb.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension_new), FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                        }

                        AVM.Nurses.ImageUrl = @"\Images\Nurses\" + fileName + extension_new;
                    }
                    else
                    {
                        AVM.Nurses.ImageUrl = nFromDb.ImageUrl;
                    }

                    _unitofWork.Nurses.Update(AVM.Nurses);
                }

                _unitofWork.Save();

                return RedirectToAction(nameof(Index));
        
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Detail(int id)
        {
            var nFromDb = _unitofWork.Nurses.Get(id);

            return View(nFromDb);
        }
        
        public IActionResult OnCall()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddOnCall(int? id)
        {
            AVM = new AdminViewModels()
            {
                OnCall = new HMSProject.Models.OnCall(),
                RoomsList = _unitofWork.Room.GetDropDownListForRoom(),
                NursesList = _unitofWork.Nurses.GetDropDownListForNurses()
            };

            if(id != null)
            {
                AVM.OnCall = _unitofWork.OnCall.Get(id.GetValueOrDefault());
            }

            return View(AVM);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOnCall()
        {
           
                if(AVM.OnCall.Id == 0)
                {
                    _unitofWork.OnCall.Add(AVM.OnCall);
                }
                else
                {
                    _unitofWork.OnCall.Update(AVM.OnCall);
                }

                _unitofWork.Save();

                return RedirectToAction(nameof(OnCall));
       
        }
        [Authorize(Roles = "Admin")]
        #region API CALLS

        public IActionResult GetAllN()
        {
            return Json(new { data = _unitofWork.Nurses.GetAll() });
        }
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllOC()
        {
            return Json(new { data = _unitofWork.OnCall.GetAll(includeProperties: "Room,Nurses") });
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult DeleteN(int id)
        {
            var nFromDb = _unitofWork.Nurses.Get(id);
            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, nFromDb.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            if(nFromDb == null)
            {
                return Json(new { success = false, message = "Error Deleting Nurse Record!" });
            }

            _unitofWork.Nurses.Remove(nFromDb);
            _unitofWork.Save();

            return Json(new { success = true, message = "Nurse Record Deleted Successfully!" });
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult DeleteOC(int id)
        {
            var ocFromDb = _unitofWork.OnCall.Get(id);

            if(ocFromDb == null)
            {
                return Json(new { success = false, message = "Error Deleting On Call Record!" });
            }

            _unitofWork.OnCall.Remove(ocFromDb);
            _unitofWork.Save();

            return Json(new { success = true, message = "On Call Record Deleted Successfully!" });
        }
        
        #endregion
    }
}
