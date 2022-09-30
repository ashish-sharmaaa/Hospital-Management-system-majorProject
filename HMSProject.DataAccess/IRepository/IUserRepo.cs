
using HMSProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMSProject.DataAccess.IRepository
{
    public interface IUserRepo : IRepository<ApplicationUser>
    {
        void Lock(string userId);

        void UnLock(string userId);
    }
}
