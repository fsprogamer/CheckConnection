using System;
using System.Collections.Generic;
using log4net;

namespace Common
{
    public interface ILogCreator
    {
        ILog GetTypeLogger<T>() where T : class;
    }
    public class LogCreator : ILogCreator
    {
        private static readonly IDictionary<Type, ILog> loggers = new Dictionary<Type, ILog>();
        private static readonly object lockObject = new object();

        public ILog GetTypeLogger<T>() where T : class
        {
            var loggerType = typeof(T);
            lock (lockObject)
            {
                if (loggers.ContainsKey(loggerType))
                {
                    return loggers[typeof(T)];
                }
                var logger = LogManager.GetLogger(loggerType);
                loggers[loggerType] = logger;
                return logger;
            }
        }
    }
}
