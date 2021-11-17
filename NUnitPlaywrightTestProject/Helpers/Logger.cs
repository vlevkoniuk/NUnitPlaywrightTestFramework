using System;
using NUnit.Framework;

namespace TestFramework.Helpers
{
    public class Logger
    {
        public  int logLevel;
        private enum MessageTypes : int
        {
            Fatal = 0,
            Error = 1,
            Warning = 2,
            Info = 3,
            Debug = 4
        }

        private  void logMessage(MessageTypes type, string message)
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

        public  void LogInfo(string message)
        {
            logMessage(MessageTypes.Info, message);
        }

        public  void LogWarning (string message)
        {
            logMessage(MessageTypes.Warning, message);
        }

        public  void LogError (string message)
        {
            logMessage(MessageTypes.Error, message);
        }

        public  void LogDebug (string message)
        {
            logMessage(MessageTypes.Debug, message);
        }

        public  void LogFatal (string message)
        {
            logMessage(MessageTypes.Fatal, message); 
        }
        
    }
}