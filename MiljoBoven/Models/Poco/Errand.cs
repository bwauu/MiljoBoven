using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace MiljoBoven.Models
{
    public class Errand
    {
        public String RefNumber { set; get; }
        public String ErrandID { set; get; }

        [Required(ErrorMessage = "Du måste fylla i vart brottet skedde.")]
        public String Place { set; get; }


        [Required(ErrorMessage = "Du måste fylla i vad för typ av brott.")]
        public String TypeOfCrime { set; get; }

        [Required(ErrorMessage = "Du måste fylla i när brottet skedde")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0,yyyy-MM-dd}")]
        public DateTime DateOfObservation { set; get; }
    
        public String Observation { set; get; }
        public String InvestigatorInfo { set; get; }
        public String InvestigatorAction{set ; get; }
        
        [Required(ErrorMessage = "Du måste fylla i ditt namn")]
        public String InformerName { set; get; }
        
        [RegularExpression(pattern:@"^[0]{1}[0-9]{1,3}-[0-9]{5,9}$", ErrorMessage = "Formatet för mobilnummer ska vara xxxx-xxxxxxxxx")]
        [Required(ErrorMessage = "Du måste fylla i ditt mobilnummer")]
        public String InformerPhone { set; get; }
        public String StatusId { set; get; }
        public String DepartmentId { set; get; }
        public String EmployeeId { set; get; }
    }
}