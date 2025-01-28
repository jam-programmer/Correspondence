using Application.Common;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract;


public interface IApiService
{
    Task<TResult> PostAsync<TResult>(ApiOption option, CancellationToken cancellationToken = default);
    Task PostWithOutResponseAsync(ApiOption option, CancellationToken cancellationToken = default);

    Task<TResult> GetAsync<TResult>(ApiOption option, CancellationToken cancellationToken = default);
}