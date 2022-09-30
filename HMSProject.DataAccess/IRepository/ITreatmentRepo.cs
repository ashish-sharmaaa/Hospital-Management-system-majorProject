
using HMSProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMSProject.DataAccess.IRepository
{
    public interface ITreatmentRepo : IRepository<Treatment>
    {
        void Update(Treatment treatment);
    }
}
