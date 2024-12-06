using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

[Table("Users")]
public class User : BaseEntity
{
    public String Name { get; set; }
    public String Username { get; set; }
    public String Password { get; set; }
    //public virtual ICollection<TaiKhoan> DSTaiKhoan { get; set; } = new Collection<TaiKhoan>();
}
