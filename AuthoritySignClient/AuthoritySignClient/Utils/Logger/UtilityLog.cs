using System;
using System.Windows;
using DevExpress.Xpf.Core;
using NLog;

namespace AuthoritySignClient.Utils.Logger
{
    public class UtilityLog
    {
        private NLog.Logger _logger;


        public void Log(string message, bool showMessage = false)
        {
            _logger.Debug(message);

            if (showMessage)
                UIShowMessage(message);
        }

        public void Log(Exception ex, string message, bool showMessage = false)
        {
            _logger.Debug(ex, message);

            if (showMessage)
                UIShowMessage(message);
        }

        public void Log(Exception ex, bool showMessage = false)
        {
            _logger.Debug(ex);

            if(showMessage)
                UIShowMessage(GetRecursiveInnerException(ex));
        }

        public void Error(string errorText)
        {
            _logger.Error(errorText);
            UIShowError(errorText);
        }

        public void Error(Exception ex)
        {
            _logger.Error(ex);
            UIShowError(GetRecursiveInnerException(ex));
        }

        public string GetRecursiveInnerException(Exception ex)
        {
            Exception realerror = ex;
            while (realerror.InnerException != null)
                realerror = realerror.InnerException;
            return ex.Message + "\r\n" + realerror.Message + "\r\n===StackTrace\r\n" + ex.StackTrace + "\r\n===Source\r\n " + ex.Source;
        }

        public void UIShowMessage(string msg)
        {
            MessageBox.Show(msg, "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void UIShowError(string err)
        {
            DXMessageBox.Show(err, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        [NonSerialized]
        private static volatile UtilityLog _instance;

        [NonSerialized]
        private static readonly object syncRoot = new object();


        public static UtilityLog GetInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new UtilityLog();
                    }
                }
            }

            return _instance;
        }

        private UtilityLog()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }
    }
}
