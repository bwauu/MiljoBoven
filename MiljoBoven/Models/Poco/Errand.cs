using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace MiljoBoven.Models
{
    public class Errand
    {
        public String RefNumber { get; set; }

        public int ErrandID{ get; set; }

        [Required(ErrorMessage = "Du måste fylla i vart brottet skedde någonstans.")]
        public String Place { get; set; }


        [Required(ErrorMessage = "Du måste fylla i vad för typ av brott.")]
        public String TypeOfCrime { get; set; }

        [Required(ErrorMessage = "Du måste fylla i när brottet skedde")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0,yyyy-MM-dd}")]
        public DateTime DateOfObservation { get; set; }

        public String Observation { get; set; }
        public String InvestigatorInfo { get; set; }
        public String InvestigatorAction { get; set; }

        [Required(ErrorMessage = "Du måste fylla i ditt namn")]
        public String InformerName { get; set; }

        [RegularExpression(pattern:@"^[0]{1}[0-9]{1,3}-[0-9]{5,9}$", ErrorMessage = "Formatet för mobilnummer ska vara xxxx-xxxxxxxxx")]
        [Required(ErrorMessage = "Du måste fylla i ditt mobilnummer")]
        public String InformerPhone { get; set; }
        public String StatusId { get; set; }
        public String DepartmentId { get; set; }
        public String EmployeeId { get; set; }
    }
}