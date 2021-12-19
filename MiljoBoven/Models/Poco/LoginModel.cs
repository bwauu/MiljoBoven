using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiljoBoven.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Vänligen fyll i användarnamn!")]
        [Display(Name = "Användarnamn:")] // Ändrar default från eng t sv
        public string UserName { get; set; }
        [Required(ErrorMessage = "Vänligen fyll i lösenord!")]
        [Display(Name = "Lösenord:")] // Ändrar default från eng t sv
        [UIHint("password")] // berättar om vilken typ. kommer inte visa några tecken.
        public string Password { get; set; }
        // ReturnURL används genom att skicka runt mellan olika sidor. vart man ska tillbaka. Man använder / för att berätta gå tillbaka där du kom ifrån
        public string ReturnURL { get; set; }
    }
}
