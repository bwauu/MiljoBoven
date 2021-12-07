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
        public Task<Errand> GetErrandDetail(int id)
        {
            return Task.Run(() =>
            {
                var errandDetail = Errands.Where(er => er.ErrandID == id).First();
                return errandDetail;
            });
        }
    }
}
