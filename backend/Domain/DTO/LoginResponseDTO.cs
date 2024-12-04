using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class LoginResponseDTO
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
        public UserDto data { get; set; }
    }
}
