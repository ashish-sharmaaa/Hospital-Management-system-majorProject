
using HMSProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMSProject.DataAccess.IRepository
{
    public interface IDoctorsRepo : IRepository<Doctors>
    {
        IEnumerable<SelectListItem> GetDropDownListForDoctors();

        void Update(Doctors doctors);
    }
}
