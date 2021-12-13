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

        void SaveErrand(Errand errand); // // SaveErrand implementeras av EFEnvironmentCrimeRepository.cs som senare används i CoordinatorController & CitizenControllers action för respektive vy "Thanks"


        // int id == someId action som är det användaren väljer och string är gammalt värde.
        void UpdateDepartment(int id, string DepartmentId); //  Imp av EFECR.cs används i "SaveManagerActions" som ligger i "ManagerController". & Används i "SaveDepartment" som ligger i "CoordinatorController".
        void UpdateEmployee(int id, string DepartmentId); //  Imp av EFECR.cs används i "SaveManagerActions" som ligger i "ManagerController".
        void UpdateStatus(int id, string DepartmentId); //  Imp av EFECR.cs används i "SaveManagerActions" som ligger i "ManagerController". & Används i "UploadFiles" som ligger i "InvestigatorController".
        void UpdateInfo(int id, string InvestigatorInfo); //  Imp av EFECR.cs används i "SaveManagerActions" som ligger i "ManagerController". & Används i "UploadFiles" som ligger i "InvestigatorController".
        void UpdateAction(int someID, string events); //  Imp av EFECR.cs Används i "UploadFiles" som ligger i "InvestigatorController".

        void UpdateSamples(Sample sample); //  Imp av EFECR.cs Används i "UploadFiles" som ligger i "InvestigatorController".

        void UpdatePictures(Picture picture); //  Imp av EFECR.cs Används i "UploadFiles" som ligger i "InvestigatorController".


    }

}