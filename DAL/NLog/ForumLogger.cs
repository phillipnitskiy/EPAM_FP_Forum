using System;
using NLog;

namespace DAL.NLog
{
    public class ForumLogger : IForumLogger
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public void Info(string info)
        {
           _logger.Info(info);
           LogManager.Flush();
        }

        public void Error(string error)
        {
            _logger.Error(error);
            LogManager.Flush();

        }

        public void Error(Exception ex, string message)
        {
          _logger.Error(ex, message);
           LogManager.Flush();
        }
    }
}
