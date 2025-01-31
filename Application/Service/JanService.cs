using Application.Common;
using Application.Contract;
using Application.DataTransferObject;
using Application.Policy;
using Application.ViewModel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Service;

public class JanService : IJanService
{
    private readonly AsyncRetryPolicy _apiPolicy;
    private readonly IApiService _apiService;
    private readonly ILogger<JanService> _logger;
    private readonly IOptions<Setting> _options;
    public JanService
        (IOptions<Setting> options,
        IApiService apiService,
        ILogger<JanService> logger
        )
    {
        _options = options;
        _apiService = apiService;
        _logger = logger;
        _apiPolicy = PollyPolicy.CreateRetryPolicy(_logger, TimeSpan.FromSeconds(3));
    }
   
    public async Task<List<JanUserViewModel>> GetUsersAsync()
    {
        List<JanUserViewModel>? users = null;
        await _apiPolicy.ExecuteAsync(async () =>
        {
            users = await _apiService.PostAsync<List<JanUserViewModel>>(new ApiOption()
            {
                BaseUrl = "https://user.jansnak.com/auth/list"
            });
        });
        return users!;
    }
    public async Task<BaseResult<JanUserDto>> ChackUserAccessForLoginAsync(LoginDto login)
    {
        JanUserDto? userAccess = new();
        await _apiPolicy.ExecuteAsync(async () =>
        {
            userAccess = await _apiService.PostAsync<JanUserDto>
            (new ApiOption()
            {
                BaseUrl = _options.Value.BaseUrl + login.path,
                DataBody = login
            });
        });
        if (userAccess == null)
        {

            return BaseResult<JanUserDto>.Fail(ResultType.Client);
        }
        if (userAccess.success)
        {
            return BaseResult<JanUserDto>.Success(userAccess);
        }
        return BaseResult<JanUserDto>.Fail(ResultType.System);
    }
}
