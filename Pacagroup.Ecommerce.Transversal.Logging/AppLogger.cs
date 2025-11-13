namespace Pacagroup.Ecommerce.Transversal.Logging;

public class AppLogger<T> (ILogger<T> logger) : IAppLogger<T> where T : class
{
    public void LogDebug(string message, params object[] args)
    {
        logger.LogDebug(message, args);
    }

    public void LogError(string message, params object[] args)
    {
        logger.LogError(message, args);
    }

    public void LogError(Exception e, string message, params object[] args)
    {
            logger.LogError(e, message, args);
    }

    public void LogInformation(string message, params object[] args)
    {
        logger.LogInformation(message, args);
    }

    public void LogWarning(string message, params object[] args)
    {
            logger.LogWarning(message, args);
    }
}
