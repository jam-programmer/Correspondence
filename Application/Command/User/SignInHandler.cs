using Application.Common;
using Application.Common.Resource;
using Application.Contract;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Command.User;

internal class SignInHandler
    : IRequestHandler<SignInCommand, BaseResult>
{
    private readonly IRepository<UserActivityEntity> _userActivityRepository;
    private readonly IRepository<UserEntity> _userRepository;
    private readonly ILogger<SignInHandler> _logger;
    public SignInHandler(
        ILogger<SignInHandler> logger,
    IRepository<UserActivityEntity> userActivityRepository,
        IRepository<UserEntity> userRepository)
    {
        _userActivityRepository = userActivityRepository;
        _userRepository = userRepository;
        _logger = logger;
    }
    public async Task<BaseResult> Handle(SignInCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            UserEntity? User = await _userRepository.GetAsync(g => g.UserName == request.username, cancellationToken);

            if (User is not null)
            {
                UserActivityEntity userActivity = new()
                {
                    DateTime = DateTime.Now,
                    UserId = User.Id,
                    Message = CustomMessage.SignInUser
                };
                await _userActivityRepository.InsertAsync(userActivity, cancellationToken);
                return BaseResult.Success();
            }
            User = new();
            User!.PhoneNumber = request.mobile!;
            User!.UserName = request.username!;
            User!.FullName = request.name!;
            User!.ImageUrl = request.image!;
            await _userRepository.InsertAsync(User, cancellationToken);
            UserActivityEntity activity = new()
            {
                DateTime = DateTime.Now,
                UserId = User.Id,
                Message = CustomMessage.FirstSignIn
            };
            await _userActivityRepository.InsertAsync(activity, cancellationToken);
            return BaseResult.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return BaseResult.Fail(ResultType.System);
        }

    }
}
