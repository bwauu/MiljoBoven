using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiljoBoven.Models
{

    public interface IEnvironmentCrimeRepository
    {
        // Kontrollen pratar med Interfacet men får datat ifrån Implementeringen EFEnvironmentCrimeRepository
        IQueryable<Department> Departments { get; }
        IQueryable<Employee> Employees { get; }
        IQueryable<Errand> Errands { get; }
        IQueryable<ErrandStatus> ErrandStatuses { get; }
        IQueryable<Picture> Pictures { get; }
        IQueryable<Sample> Samples { get; }
        IQueryable<Sequence> Sequences { get; }
        Task<Errand> GetErrandDetail(int id);

        void SaveErrand(Errand errand);
    
        
        // int id == someId action som är det användaren väljer och string är gammalt värde.
        void UpdateDepartment(int id, string DepartmentId);
        void UpdateEmployee(int id, string DepartmentId);
        void UpdateStatus(int id, string DepartmentId);
        void UpdateInfo(int id, string InvestigatorInfo);

        void UpdateSamples(Sample sample);

        void UpdatePictures(Picture picture);

        void UpdateTest(Errand obj);
        void InsertTest(Errand obj);
        void SaveTest();
        IEnumerable<Errand> SelectAll();
    }

}