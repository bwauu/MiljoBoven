using System;
using System.ComponentModel.DataAnnotations;

namespace MiljoBoven.Models
{
    public class ErrandStatus
    {
        [Key]
        public String StatusId { get; set; }
        public String StatusName { get; set; }
    }
}