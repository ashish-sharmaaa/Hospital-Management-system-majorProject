
using HMSProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMSProject.DataAccess.IRepository
{
    public interface IAppointmentsRepo : IRepository<Appointments>
    {
        IEnumerable<SelectListItem> GetDropDownListForAppointments();

        void Update(Appointments appointments);
    }
}
