
using HMSProject.DataAccess;
using HMSProject.DataAccess.IRepository;
using HMSProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HMSProject.DataAccess.Repository
{
    public class UserRepo : Repository<ApplicationUser>, IUserRepo
    {
        private readonly ApplicationDbContext _db;

        public UserRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Lock(string userId)
        {
            var uFromDb = _db.applicationUser.FirstOrDefault(i => i.Id == userId);
            uFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            _db.SaveChanges();
        }

        public void UnLock(string userId)
        {
            var uFromDb = _db.applicationUser.FirstOrDefault(i => i.Id == userId);
            uFromDb.LockoutEnd = DateTime.Now;
            _db.SaveChanges();
        }
    }
}
