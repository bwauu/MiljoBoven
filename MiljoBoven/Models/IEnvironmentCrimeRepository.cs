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

        void UpdateDepartment(int id, string DepartmentId);
    }

}