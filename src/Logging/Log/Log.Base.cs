#nullable enable
using Logging.Common;
using Logging.Interface;
using Serilog;
using Serilog.Events;

namespace Logging;

public partial class Log : ILog
{
    private readonly ILogger _logger;

    /// <summary>
    /// The class type for the logger. Defaults to <see cref="Log"/>.
    /// Note: This is done this way due to static classes not passable as a type generic.
    /// </summary>
    protected Type ClassType = typeof(Log);

    public Log(ILogger logger)
    {
        _logger = logger;
    }

    public ILogger GetLogger() => _logger;

    public bool IsLogLevelEnabled(LogEventLevel logLevel = LogEventLevel.Debug) => _logger.IsEnabled(logLevel);

    private LogMetaData Write(
        LogEventLevel logLevel,
        string messageTemplate,
        string sourceFilePath = "",
        string memberName = "",
        int sourceLineNumber = 0,
        params object?[]? propertyValues
    )
    {
        var logMetaData = new LogMetaData(this, GetClassName(sourceFilePath), memberName, sourceLineNumber)
        {
            LogLevel = logLevel,
            MessageTemplate = messageTemplate,
            PropertyValues = propertyValues,
        };

        logMetaData.Write();
        return logMetaData;
    }

    private LogMetaData Write(
        LogEventLevel logLevel,
        Exception exception,
        string messageTemplate,
        string sourceFilePath = "",
        string memberName = "",
        int sourceLineNumber = 0,
        params object?[]? propertyValues
    )
    {
        var logMetaData = new LogMetaData(this, GetClassName(sourceFilePath), memberName, sourceLineNumber)
        {
            Exception = exception,
            LogLevel = logLevel,
            MessageTemplate = messageTemplate,
            PropertyValues = propertyValues,
        };

        logMetaData.Write();
        return logMetaData;
    }

    private static string GetClassName(string sourceFilePath) => Path.GetFileNameWithoutExtension(sourceFilePath);
}
