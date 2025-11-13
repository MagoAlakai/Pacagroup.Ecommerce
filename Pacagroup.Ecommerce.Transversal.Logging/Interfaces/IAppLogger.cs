namespace Pacagroup.Ecommerce.Transversal.Logging.Interfaces;

public interface IAppLogger<T> where T : class
{
    void LogInformation(string message, params object[] args);
    void LogWarning(string message, params object[] args);
    void LogError(string message, params object[] args);
    void LogError(Exception e, string message, params object[] args);
    void LogDebug(string message, params object[] args);
}
