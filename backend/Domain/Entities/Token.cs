using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Tokens")]
    public class Token : BaseEntity
    {

        public String TokenValue { get; set; }

        public bool Revoked { get; set; }

        public bool Expired { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public Token() { }
    }
}
