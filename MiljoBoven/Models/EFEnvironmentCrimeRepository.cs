using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public void SaveErrand(Errand errand)
        {
            if (errand.ErrandId == 0)
            {
                var sequence = Sequences.Where(seq => seq.Id == 1).First();
                errand.RefNumber = "2021-45-" + sequence.CurrentValue;
                sequence.CurrentValue++;
                errand.StatusId = "S_A";
                context.Errands.Add(errand);
            }
            context.SaveChanges();
        }
        public Task<Errand> GetErrandDetail(int id)
        {
            return Task.Run(() =>
            {
                var errandDetail = Errands.Where(er => er.ErrandId == id).First();
                return errandDetail;
            });
        }
    }
}
