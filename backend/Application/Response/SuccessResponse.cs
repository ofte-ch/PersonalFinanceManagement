using Application.Interface;

namespace Application.Response;

public class SuccessResponse : IResponse
{
    public int Data { get; set; }
    public int Code { get; set; }
    public SuccessResponse(int data)
    {
        Data = data;
        Code = 200;
    }
}

class CreatedResponse : SuccessResponse
{
    public CreatedResponse(int data) : base(data)
    {
        Code = 201;
    }
}