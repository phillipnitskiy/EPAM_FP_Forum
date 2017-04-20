using System;

namespace DAL.NLog
{
    public interface IForumLogger
    {
        void Info(string info);
        void Error(string error);
        void Error(Exception ex, string message);
    }
}
