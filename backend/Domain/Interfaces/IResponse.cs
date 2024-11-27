using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface;

public interface IResponse
{
    string Message { get; }
    int Code { get; }
}
