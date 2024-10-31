using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

[Table("TheLoai")]
public class TheLoai : BaseEntity 
{
    public String TenTheLoai { get; set; }
    public String MoTa { get; set; }
    public String PhanLoai { get; set; }
}
