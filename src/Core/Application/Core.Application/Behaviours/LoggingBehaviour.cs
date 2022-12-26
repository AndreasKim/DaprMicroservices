using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Core.Application.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    ////private readonly ICurrentUserService _currentUserService;
    ////private readonly IIdentityService _identityService;

    //public LoggingBehaviour(ILogger logger, ICurrentUserService currentUserService, IIdentityService identityService)
    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
        //_currentUserService = currentUserService;
        //_identityService = identityService;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        //var userId = _currentUserService.UserId ?? string.Empty;
        //string userName = string.Empty;

        //if (!string.IsNullOrEmpty(userId))
        //{
        //    userName = await _identityService.GetUserNameAsync(userId);
        //}

        //_logger.Information("Core Request: {Name} {@UserId} {@UserName} {@Request}",
        //    requestName, userId, userName, request);      
        _logger.LogInformation("Core Request: {Name} {@UserId} {@UserName} {@Request}",
            requestName, "UserId", "UserName", request);
    }
}
