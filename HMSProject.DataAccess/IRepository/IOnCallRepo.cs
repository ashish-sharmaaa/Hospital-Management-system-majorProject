
using HMSProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMSProject.DataAccess.IRepository
{
    public interface IOnCallRepo : IRepository<OnCall>
    {
        void Update(OnCall onCall);
    }
}
