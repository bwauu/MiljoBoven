using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiljoBoven.Models
{
    // EFEnvironmentCrimeRepositroy is a repository class that invoke sql database.
    public class EFEnvironmentCrimeRepository : IEnvironmentCrimeRepository
    {
        // DBBS3
        // Need connection to communicate with Database. Therefore we need a constructor
        private ApplicationDbContext context;
        private IHttpContextAccessor contextAccessor;

        public EFEnvironmentCrimeRepository(ApplicationDbContext context, IHttpContextAccessor contextAccessor)
        {
            this.context = context;
            this.contextAccessor = contextAccessor;
        }
        // Next step is to implement all behaviors from the interface. As follows

        public IQueryable<Department> Departments => context.Departments;
        public IQueryable<Errand> Errands => context.Errands.Include(e => e.Samples).Include(e => e.Pictures);

        public IQueryable<ErrandStatus> ErrandStatuses => context.ErrandStatuses;
        public IQueryable<Employee> Employees => context.Employees;
        public IQueryable<Picture> Pictures => context.Pictures;
        public IQueryable<Sample> Samples => context.Samples;
        public IQueryable<Sequence> Sequences => context.Sequences;

        // Anropas i vynerna ReportCrime@Coordinator && o i Index@Home.
        // Saves a errand via form @views.
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

        // Get Errand by int id
        public Task<Errand> GetErrandDetail(int id)
        {
            return Task.Run(() =>
            {
                var errandDetail = Errands.Where(er => er.ErrandId == id).First();
                return errandDetail;
            });
        }

        /************************************************************************  Update data context via logic in actioncontrollers START. *********************************************************/
        // Changes a current exsisting result in dbcontext to a new value by user action. aka update department
        public void UpdateDepartment(int someValue, string someNewValue)
        {
            var existingResult = context.Errands.SingleOrDefault(err => err.ErrandId == someValue);

            if (existingResult != null)
            {
                existingResult.DepartmentId = someNewValue;
            }
            context.SaveChanges();
        }
        // -||- update investigator
        public void UpdateEmployee(int someValue, string someNewValue)
        {

            var existingResult = context.Errands.SingleOrDefault(err => err.ErrandId == someValue);
            if (existingResult != null)
            {
                existingResult.EmployeeId = someNewValue;
            }
            context.SaveChanges();
        }
        // -||- update one errand's statusid.
        public void UpdateStatus(int someValue, string someNewValue)
        {

            var existingResult = context.Errands.SingleOrDefault(e => e.ErrandId == someValue);
            if (existingResult != null)
            {
                existingResult.StatusId = someNewValue;

            }
            context.SaveChanges();
        }
        // -||- update one errand's info. errands already existing InvestigatorInfo string will not be delete. The new value will be added instead - by seperation of one spaceblanc between already
        // existing result and the new value. 
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
        /* ************************************************************************  Update data context via logic in actioncontrollers. END *********************************************************/
        // gets errand by filtering right params.  
        public IQueryable<MyErrand> GetManagerErrandList()
        {
            // Nyckel för att hämta rätt data
            var userName = contextAccessor.HttpContext.User.Identity.Name;
            var errandList = from emp0 in Employees
                             join dep in Departments
                                on emp0.DepartmentId equals dep.DepartmentId
                             join err in Errands
                                on emp0.DepartmentId equals err.DepartmentId
                             join emp1 in Employees
                                on err.EmployeeId equals emp1.EmployeeId
                             join sta in ErrandStatuses
                                on err.StatusId equals sta.StatusId
                             where emp0.EmployeeId == userName

                             select new MyErrand
                             {
                                 EmployeeName = (emp1.EmployeeId == null ? "Ej tillsatt" : emp1.EmployeeName),
                                 DepartmentName = (err.DepartmentId == null ? "Ej tillsatt " : dep.DepartmentName),
                                 RefNumber = err.RefNumber,
                                 ErrandId = err.ErrandId,
                                 StatusName = sta.StatusName,
                                 TypeOfCrime = err.TypeOfCrime,
                                 DateOfObservation = err.DateOfObservation
                             };
            return errandList;
        }
        // Gets manager's employees by their association depId. 
        public IQueryable<MyErrand> GetManagerEmployeeList()
        {
            var userName = contextAccessor.HttpContext.User.Identity.Name;

            var errandList = from emp0 in Employees
                             join dep in Departments on emp0.DepartmentId equals dep.DepartmentId
                             join emp1 in Employees on dep.DepartmentId equals emp1.DepartmentId
                             where emp0.EmployeeId == userName

                             select new MyErrand
                             {
                                 EmployeeName = (emp1.EmployeeName)
                             };

            return errandList;
        }
        // Gets all errands by employees on specific unit.
        public IQueryable<MyErrand> GetCoordinatorErrandList()
        {

            var errandList = from err in Errands
                             join sta in ErrandStatuses on err.StatusId equals sta.StatusId
                             join dep in Departments on err.DepartmentId equals dep.DepartmentId
                             into departmentErrand
                             from depErr in departmentErrand.DefaultIfEmpty()
                             join emp in Employees on err.EmployeeId equals emp.EmployeeId
                             into employeeErrand
                             from empErr in employeeErrand.DefaultIfEmpty()

                             orderby err.RefNumber descending

                             select new MyErrand
                             {
                                 EmployeeName = (err.EmployeeId == null ? "Ej tillsatt" : empErr.EmployeeName),
                                 DepartmentName = (err.DepartmentId == null ? "Ej tillsatt " : depErr.DepartmentName),
                                 RefNumber = err.RefNumber,
                                 ErrandId = err.ErrandId,
                                 StatusName = sta.StatusName,
                                 TypeOfCrime = err.TypeOfCrime,
                                 DateOfObservation = err.DateOfObservation,


                             };
            return errandList;
        }
        // Gets all employees 
        public IQueryable<MyErrand> GetInvestigatorErrandList()
        {
            var userName = contextAccessor.HttpContext.User.Identity.Name;

            var errandList = from err in Errands
                             join sta in ErrandStatuses on err.StatusId equals sta.StatusId
                             join dep in Departments on err.DepartmentId equals dep.DepartmentId
                             into departmentErrand
                             from depErr in departmentErrand.DefaultIfEmpty()
                             join emp in Employees on err.EmployeeId equals emp.EmployeeId
                             into investigatorErrands
                             from empErr in investigatorErrands.DefaultIfEmpty()
                             where empErr.EmployeeId == userName
                             orderby err.RefNumber descending

                             select new MyErrand
                             {
                                 DateOfObservation = err.DateOfObservation,
                                 ErrandId = err.ErrandId,
                                 RefNumber = err.RefNumber,
                                 TypeOfCrime = err.TypeOfCrime,
                                 StatusName = sta.StatusName,
                                 DepartmentName = (err.DepartmentId == null ? "Ej tillsatt " : depErr.DepartmentName),
                                 EmployeeName = (err.EmployeeId == null ? "Ej tillsatt" : empErr.EmployeeName)
                             };
            return errandList;
        }

    }
}
