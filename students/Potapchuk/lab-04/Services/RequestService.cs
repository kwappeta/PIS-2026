using Application.Commands;
using Application.DTOs;
using Application.Interfaces;
using Application.Queries;

public class RequestService : IRequestService
{
    private readonly CreateRequestCommandHandler _createHandler;
    private readonly AssignGroupToRequestCommandHandler _assignHandler;
    private readonly ActivateRequestCommandHandler _activateHandler;
    private readonly ChangeRequestZoneCommandHandler _changeZoneHandler;
    private readonly CompleteRequestCommandHandler _completeHandler;

    private readonly GetRequestByIdQueryHandler _getByIdHandler;
    private readonly ListActiveRequestsQueryHandler _listActiveHandler;

    public RequestService(
        CreateRequestCommandHandler createHandler,
        AssignGroupToRequestCommandHandler assignHandler,
        ActivateRequestCommandHandler activateHandler,
        ChangeRequestZoneCommandHandler changeZoneHandler,
        CompleteRequestCommandHandler completeHandler,
        GetRequestByIdQueryHandler getByIdHandler,
        ListActiveRequestsQueryHandler listActiveHandler)
    {
        _createHandler = createHandler;
        _assignHandler = assignHandler;
        _activateHandler = activateHandler;
        _changeZoneHandler = changeZoneHandler;
        _completeHandler = completeHandler;
        _getByIdHandler = getByIdHandler;
        _listActiveHandler = listActiveHandler;
    }

    public Task<string> CreateRequestAsync(CreateRequestCommand command)
        => _createHandler.HandleAsync(command);

    public Task AssignGroupAsync(AssignGroupToRequestCommand command)
        => _assignHandler.HandleAsync(command);

    public Task ActivateRequestAsync(ActivateRequestCommand command)
        => _activateHandler.HandleAsync(command);

    public Task ChangeZoneAsync(ChangeRequestZoneCommand command)
        => _changeZoneHandler.HandleAsync(command);

    public Task CompleteRequestAsync(CompleteRequestCommand command)
        => _completeHandler.HandleAsync(command);

    public Task<RequestDto> GetRequestByIdAsync(GetRequestByIdQuery query)
        => _getByIdHandler.HandleAsync(query);

    public Task<List<RequestDto>> ListActiveRequestsAsync(ListActiveRequestsQuery query)
        => _listActiveHandler.HandleAsync(query);
}