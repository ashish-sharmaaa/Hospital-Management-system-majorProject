
using HMSProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMSProject.DataAccess.IRepository
{
    public interface IPerscribtionRepo : IRepository<Perscribtion>
    {
        void Update(Perscribtion perscribtion);
    }
}
