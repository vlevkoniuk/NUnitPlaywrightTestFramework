using System;
using NUnit.Framework;

namespace NUnitSeleniumTestProjectExample.Helpers
{
    public static class Logger
    {
        public static int logLevel;
        private enum MessageTypes : int
        {
            Fatal = 0,
            Error = 1,
            Warning = 2,
            Info = 3,
            Debug = 4
        }

        private static void logMessage(MessageTypes type, string message)
        {
            if ((int)type >= logLevel) 
            {
                if ((int)type > 1)
                {
                    TestContext.Out.WriteLineAsync(TestContext.CurrentContext.Test.MethodName + " => " 
                        + type.ToString() + ": " + message);
                    TestContext.Progress.WriteLine(TestContext.CurrentContext.Test.MethodName + " => " 
                        + type.ToString() + ": " + message);
                }
                else
                {
                    TestContext.Out.WriteLineAsync(TestContext.CurrentContext.Test.MethodName + " => " 
                        + type.ToString() + ": " + message);
                    TestContext.Error.WriteLine(TestContext.CurrentContext.Test.MethodName + " => " 
                        + type.ToString() + ": " + message);
                }
            }
        }

        public static void LogInfo(string message)
        {
            logMessage(MessageTypes.Info, message);
        }

        public static void LogWarning (string message)
        {
            logMessage(MessageTypes.Warning, message);
        }

        public static void LogError (string message)
        {
            logMessage(MessageTypes.Error, message);
        }

        public static void LogDebug (string message)
        {
            logMessage(MessageTypes.Debug, message);
        }

        public static void LogFatal (string message)
        {
            logMessage(MessageTypes.Fatal, message); 
        }
        
    }
}