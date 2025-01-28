using Application.Common;
using Application.Contract;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Command.Letter;

internal sealed class InsertLetterHandler
    : IRequestHandler<InsertLetterCommand, BaseResult>
{
    private readonly IRepository<LetterEntity> _letterRepository;
    private readonly ILogger<InsertLetterHandler> _logger;
    public InsertLetterHandler
        (IRepository<LetterEntity> letterRepository,
        ILogger<InsertLetterHandler> logger)
    {
        _letterRepository = letterRepository;
        _logger = logger;
    }
    public async Task<BaseResult> Handle
        (InsertLetterCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            LetterEntity letter = request.Letter.Adapt<LetterEntity>();
            letter.Status = Domain.Enum.StatusType.NotViewed;
            await _letterRepository.InsertAsync(letter, cancellationToken);
            return BaseResult.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return BaseResult.Fail(ResultType.System);
        }
    }
}
