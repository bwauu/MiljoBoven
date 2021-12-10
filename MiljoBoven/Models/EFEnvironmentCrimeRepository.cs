using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MiljoBoven.Models
{
    public class EFEnvironmentCrimeRepository : IEnvironmentCrimeRepository
    {
        // DBBS3
        // Need connection to communicate with Database. Therefore we need a constructor
        private ApplicationDbContext context;

        public EFEnvironmentCrimeRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        // Next step is to implement all behaviors from the interface. As follows

        public IQueryable<Department> Departments => context.Departments;
        public IQueryable<Errand> Errands => context.Errands;

        public IQueryable<ErrandStatus> ErrandStatuses => context.ErrandStatuses;
        public IQueryable<Employee> Employees => context.Employees;
        public IQueryable<Picture> Pictures => context.Pictures;
        public IQueryable<Sample> Samples => context.Samples;
        public IQueryable<Sequence> Sequences => context.Sequences;

        // Task<Errand> GetErrandDetail(int id);

        public void SaveTest()
        {
            context.SaveChanges();
        }

        public IEnumerable<Errand> SelectAll()
        {
            return context.Errands.ToList();
        }
        public void SaveErrand(Errand errand)
        {
            if (errand.ErrandId == 0)
            {
                var sequence = Sequences.Where(seq => seq.Id == 1).First();
                errand.RefNumber = "2021-45-" + sequence.CurrentValue;
                sequence.CurrentValue++;
                errand.StatusId = "S_A";
                context.Errands.Add(errand);
            }context.SaveChanges();
        }


        public Task<Errand> GetErrandDetail(int id)
        {
            return Task.Run(() =>
            {
                var errandDetail = Errands.Where(er => er.ErrandId == id).First();
                return errandDetail;
            });
        }

        public void UpdateDepartment(int id, string DepartmentId)
        {
            Errand entity = context.Errands.FirstOrDefault(err => err.ErrandId == id);
            
            if (entity != null)
            {
                entity.DepartmentId = DepartmentId;

            }
          
            context.SaveChanges();
        }
        public void UpdateEmployee(int id, string EmployeeId)
        {

            Errand dbEntry = context.Errands.FirstOrDefault(e => e.ErrandId == id);
            if (dbEntry != null)
            {
                dbEntry.EmployeeId = EmployeeId;
            }
            context.SaveChanges();
        }

        public void UpdateStatus(int id, string StatusId)
        {

            Errand dbEntry = context.Errands.FirstOrDefault(e => e.ErrandId == id);
            if (dbEntry != null)
            {
                dbEntry.StatusId = StatusId;

            }
            context.SaveChanges();
        }

        public void UpdateInfo(int id, string InvestigatorInfo)
        {
            Errand dbEntry = context.Errands.FirstOrDefault(e => e.ErrandId == id);
            if (dbEntry != null)
            {
                dbEntry.InvestigatorInfo = dbEntry.InvestigatorInfo + ";" + InvestigatorInfo;
            }
            context.SaveChanges();
        }
        public void UpdateAction(int id, string InvestigatorAction)
        {
            Errand dbEntry = context.Errands.FirstOrDefault(e => e.ErrandId == id);
            if (dbEntry != null)
            {

                dbEntry.InvestigatorAction = dbEntry.InvestigatorAction + ";" + InvestigatorAction;

            }
            context.SaveChanges();
        }


        public void UpdateSamples(Sample sample)
        {
            if (sample.SampleId == 0)
            {
                context.Samples.Add(sample);
            }
            context.SaveChanges();
        }

        public void UpdatePictures(Picture picture)
        {
            if (picture.PictureId == 0)
            {

                context.Pictures.Add(picture);
            }
            context.SaveChanges();
        }

        public void UpdateTest(Errand obj)
        {
            throw new NotImplementedException();
        }

        public void InsertTest(Errand obj)
        {
            throw new NotImplementedException();
        }
    }
}
