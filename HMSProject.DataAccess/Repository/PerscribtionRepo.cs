
using HMSProject.DataAccess;
using HMSProject.DataAccess.IRepository;
using HMSProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HMSProject.DataAccess.Repository
{
    public class PerscribtionRepo : Repository<Perscribtion>, IPerscribtionRepo
    {
        private readonly ApplicationDbContext _db;

        public PerscribtionRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Perscribtion perscribtion)
        {
            var pFromDb = _db.Perscribtions.FirstOrDefault(i => i.Id == perscribtion.Id);

            pFromDb.DoctorId = perscribtion.DoctorId;
            pFromDb.PatientId = perscribtion.PatientId;
            pFromDb.MedicationId = perscribtion.MedicationId;
            pFromDb.Date = perscribtion.Date;
            pFromDb.Details = perscribtion.Details;

            _db.SaveChanges();
        }
    }
}
