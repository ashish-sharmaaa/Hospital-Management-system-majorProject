
using HMSProject.DataAccess;
using HMSProject.DataAccess.IRepository;
using HMSProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HMSProject.DataAccess.Repository
{
    public class AffiliatedRepo : Repository<Affiliated>, IAffiliatedRepo
    {
        private readonly ApplicationDbContext _db;

        public AffiliatedRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Affiliated affiliated)
        {
            var aFromDb = _db.Affiliated.FirstOrDefault(i => i.Id == affiliated.Id);

            aFromDb.DepartmentId = affiliated.DepartmentId;
            aFromDb.DoctorId = affiliated.DoctorId;

            _db.SaveChanges();
        }
    }
}
