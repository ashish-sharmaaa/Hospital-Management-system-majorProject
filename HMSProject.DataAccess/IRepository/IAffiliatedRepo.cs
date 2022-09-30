
using HMSProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMSProject.DataAccess.IRepository
{
    public interface IAffiliatedRepo : IRepository<Affiliated>
    {
        void Update(Affiliated affiliated);
    }
}
