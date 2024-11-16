using Application.Interface;
using System.Text.Json.Serialization;

namespace Application.Response;

public class SuccessResponse : IResponse
{
    [JsonInclude]
    public string Message { get; set; }
    public int Data { get; set; }
    public int Code { get; set; }
    public SuccessResponse(int data)
    {
        Data= data;
        Code = 200;
        Message = "";
    }
    public SuccessResponse(string message)
    {
        Code = 200;
        Message = message;
    }
}

class CreatedResponse : SuccessResponse
{
    public CreatedResponse(string message) : base(message)
    {
        Code = 201;
    }
    public CreatedResponse(int data) : base(data)
    {
        Code = 201;
    }
}

