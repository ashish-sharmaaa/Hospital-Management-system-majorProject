
using HMSProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMSProject.DataAccess.IRepository
{
    public interface IUndergoesRepo : IRepository<Undergoes>
    {
        void Update(Undergoes undergoes);
    }
}
