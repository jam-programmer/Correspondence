using Polly.Retry;
using Polly;
using Microsoft.Extensions.Logging;
namespace Application.Policy;

public static class PollyPolicy
{
    public static AsyncRetryPolicy CreateRetryPolicy(ILogger
       logger, TimeSpan time, int retryCount = 3)
    {
        return Polly.Policy.Handle<Exception>()
            .WaitAndRetryAsync(retryCount: retryCount,
            sleepDurationProvider: retryTime => time * retryTime,
            onRetry: (exception, time, retryCount, context)
             =>
            {
                logger.LogInformation($"خطای فراخوانی وب سرویس - تلاش ناموفق {exception.Message}");
            });
    }
}
