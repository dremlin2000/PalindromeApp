using Microsoft.Extensions.Logging;
using Palindrome.Core.ViewModel;

namespace Palindrome.Core.Extensions
{
    public static class LoggerExtensions
    {
        public static void Log(this ILogger logger, string message, Severity severity)
        {
            switch (severity)
            {
                case Severity.Debug:
                    logger.LogDebug(message);
                    break;
                case Severity.Warning:
                    logger.LogWarning(message);
                    break;
                case Severity.Error:
                    logger.LogError(message);
                    break;
                case Severity.Critical:
                    logger.LogCritical(message);
                    break;
                case Severity.Info:
                    logger.LogInformation(message);
                    break;
                case Severity.Trace:
                    logger.LogTrace(message);
                    break;
            }
        }
    }
}
