using NLog;

namespace Travelndc.Common
{
    public class LogHelper
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static void Info(string message, string fileName = "INFO")
        {
            //把文件名通过属性传输
            logger.WithProperty("filename", fileName).Info(message);
        }
        public static void Debug(string message, string fileName = "DEBUG")
        {
            logger.WithProperty("filename", fileName).Debug(message);
        }
        public static void Error(string message, string fileName = "Error")
        {
            logger.WithProperty("filename", fileName).Error(message);
        }
        public static void Warn(string message, string fileName = "Warn")
        {
            logger.WithProperty("filename", fileName).Warn(message);
        }

    }
}
