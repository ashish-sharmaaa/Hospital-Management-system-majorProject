
using HMSProject.DataAccess;
using HMSProject.DataAccess.IRepository;
using HMSProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HMSProject.DataAccess.Repository
{
    public class ProcedureRepo : Repository<Procedure>, IProcedureRepo
    {
        private readonly ApplicationDbContext _db;

        public ProcedureRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetDropDownListForProcedure()
        {
            return _db.Procedures.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(Procedure procedure)
        {
            var pFromDb = _db.Procedures.FirstOrDefault(i => i.Id == procedure.Id);

            pFromDb.Name = procedure.Name;
            pFromDb.Price = procedure.Price;

            _db.SaveChanges();
        }
    }
}
