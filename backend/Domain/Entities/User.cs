using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

[Table("Users")]
public class User : BaseEntity
{
    public String Username { get; set; }
    public String Password { get; set; }
    public bool RememberMe { get; set; }
}
