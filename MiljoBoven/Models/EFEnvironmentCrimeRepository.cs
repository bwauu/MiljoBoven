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
        public IQueryable<Errand> Errands => context.Errands.Include(e => e.Samples).Include(e => e.Pictures);

        public IQueryable<ErrandStatus> ErrandStatuses => context.ErrandStatuses;
        public IQueryable<Employee> Employees => context.Employees;
        public IQueryable<Picture> Pictures => context.Pictures;
        public IQueryable<Sample> Samples => context.Samples;
        public IQueryable<Sequence> Sequences => context.Sequences;
        public void Update(Errand errand)
        {
            var errandObj = context.Errands.Where(e => e.ErrandId == errand.ErrandId).FirstOrDefault();
            errandObj.DateOfObservation = errand.DateOfObservation;
            errandObj.DepartmentId = errand.DepartmentId;
            errandObj.EmployeeId = errand.EmployeeId;
            errandObj.ErrandId = errand.ErrandId;
            errandObj.InformerName = errand.InformerName;
            errandObj.InformerPhone = errand.InformerPhone;
            context.SaveChanges();
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

        public void UpdateDepartment(int someValue, string someNewValue)
        {
            var existingResult = context.Errands.SingleOrDefault(err => err.ErrandId == someValue);
         
            if (existingResult != null)
            {
                existingResult.DepartmentId = someNewValue; // Is this query result.DepartmentId the same as the new value string DepartmentId? aka result.SomeValue
            }
            context.SaveChanges();
        }
        public void UpdateEmployee(int someValue, string someNewValue)
        {

            var existingResult = context.Errands.SingleOrDefault(err => err.ErrandId == someValue);
            if (existingResult != null)
            {
                existingResult.EmployeeId = someNewValue;
            }
            context.SaveChanges();
        }

        public void UpdateStatus(int someValue, string someNewValue)
        {

            var existingResult = context.Errands.SingleOrDefault(e => e.ErrandId == someValue);
            if (existingResult != null)
            {
                existingResult.StatusId = someNewValue;

            }
            context.SaveChanges();
        }

        public void UpdateInfo(int someValue, string someNewValue)
        {
            var existingResult = context.Errands.SingleOrDefault(e => e.ErrandId == someValue);
            if (existingResult != null)
            {
                existingResult.InvestigatorInfo = existingResult.InvestigatorInfo + " " + someNewValue;
            }
            context.SaveChanges();
        }
        public void UpdateAction(int someValue, string someNewValue)
        {
            Errand existingResult = context.Errands.SingleOrDefault(e => e.ErrandId == someValue);
            if (existingResult != null)
            {     
                existingResult.InvestigatorAction = existingResult.InvestigatorAction + " " + someNewValue;

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


    }
}
