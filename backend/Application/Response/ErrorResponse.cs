using Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response;

public class ErrorResponse : IResponse
{
    public string Message { get; set; }
    public int Code { get; set; }

    public ErrorResponse(int code, string message)
    {
        Message = message;
        Code = code;
    }
}

class NotFoundResponse : ErrorResponse
{
    public NotFoundResponse(string message) : base(404, message) { }
}

class BadRequestResponse : ErrorResponse
{
    public BadRequestResponse(string message) : base(400, message) { }
}

class ValidationFailResponse : ErrorResponse
{
    public string Message { get; set; }
    public int Code { get; set; }
    public ValidationFailResponse(string message) : base(400, message) { }
}