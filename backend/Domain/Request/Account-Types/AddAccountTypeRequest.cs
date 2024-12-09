using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Request.Account_Types
{
    public class AddAccountTypeRequest
    {
        public int userId {  get; set; }
        public string name { get; set; }
    }
}
