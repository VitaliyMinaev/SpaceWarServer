using SpaceWar.Api.Contracts.v1.Responses.Abstract;

namespace SpaceWar.Api.Contracts.v1.Responses;

public class CreateTaskResponse : BaseResponse
{
    public string title { get; set; }
}
