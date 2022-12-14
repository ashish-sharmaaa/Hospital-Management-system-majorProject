
using HMSProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMSProject.DataAccess.IRepository
{
    public interface IProcedureRepo : IRepository<Procedure>
    {
        IEnumerable<SelectListItem> GetDropDownListForProcedure();

        void Update(Procedure procedure);
    }
}
