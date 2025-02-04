using System.Runtime.CompilerServices;
using Logging.Common;
using Logging.Interface;

namespace Logging;

public static partial class LogExtensions
{
    public static LogMetaData Here(
        this ILog logger,
        [CallerFilePath] string sourceFilePath = "",
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        var className = Path.GetFileNameWithoutExtension(sourceFilePath);
        return new LogMetaData(logger, className, memberName, sourceLineNumber);
    }
}
