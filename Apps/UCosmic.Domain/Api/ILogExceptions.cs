using System;

namespace UCosmic.Domain
{
    public interface ILogExceptions
    {
        void LogException(Exception exception);
    }
}