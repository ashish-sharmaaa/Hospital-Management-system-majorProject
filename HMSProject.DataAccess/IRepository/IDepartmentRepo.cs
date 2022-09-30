
using HMSProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMSProject.DataAccess.IRepository
{
    public interface IDepartmentRepo : IRepository<Department>
    {
        IEnumerable<SelectListItem> GetDropDownListForDepartments();

        void Update(Department department);
    }
}
