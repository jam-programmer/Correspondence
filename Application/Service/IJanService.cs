using Application.Common;
using Application.DataTransferObject;
using Application.ViewModel;

namespace Application.Service;

public interface IJanService
{
    Task<BaseResult<JanUserDto>> ChackUserAccessForLoginAsync(LoginDto login);

    Task<List<JanUserViewModel>> GetUsersAsync();
}
