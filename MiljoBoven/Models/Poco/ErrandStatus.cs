using System;
using System.ComponentModel.DataAnnotations;

namespace MiljoBoven.Models
{
    public class ErrandStatus
    {
        [Key]
        public String StatusId { set; get; }
        public String StatusName { set; get; }
    }
}