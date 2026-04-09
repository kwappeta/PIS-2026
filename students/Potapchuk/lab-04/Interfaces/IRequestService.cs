using Application.Commands;
using Application.Queries;
using Application.DTOs;

public interface IRequestService
{
    Task<string> CreateRequestAsync(CreateRequestCommand command);
    Task AssignGroupAsync(AssignGroupToRequestCommand command);
    Task ActivateRequestAsync(ActivateRequestCommand command);
    Task ChangeZoneAsync(ChangeRequestZoneCommand command);
    Task CompleteRequestAsync(CompleteRequestCommand command);

    Task<RequestDto> GetRequestByIdAsync(GetRequestByIdQuery query);
    Task<List<RequestDto>> ListActiveRequestsAsync(ListActiveRequestsQuery query);
}