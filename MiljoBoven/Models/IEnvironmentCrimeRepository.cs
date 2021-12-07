using System.Linq;
using System.Threading.Tasks;

namespace MiljoBoven.Models
{

        public interface IEnvironmentCrimeRepository
        {
            // Kontrollen pratar med Interfacet men får datat ifrån Implementeringen FakeEnviormentCrimeRepository
            IQueryable<Department> Departments { get; }
            IQueryable<Errand> Errands { get; }
        
            IQueryable<ErrandStatus> ErrandsStatus { get; }
            IQueryable<Employee> Employees {
                get;
            }
            Task<Errand> GetErrandDetail(string id);
     
        }
    
}