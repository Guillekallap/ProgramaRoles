using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace ProgramaRoles.Utils
{
    public class UtilsLog
    {
        #region --Attributes--
        private static volatile UtilsLog _instance;
        string _path = ConfigurationManager.AppSettings["Path"];
        private StreamWriter _wr;

        #endregion --Attributes--

        #region --Singleton--

        public static UtilsLog Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(UtilsLog))
                    {
                        if (_instance == null)
                            _instance = new UtilsLog();
                    }
                }
                return _instance;
            }
        }

        #endregion --Singleton--

        #region --Constructors & Destructors--

        private UtilsLog()
        {
            try
            {

            }
            catch
            {
                //Si no hay seteado un logger, continua y listo....!!!!
            }
        }

        #endregion --Constructors & Destructors--

        #region --Methods--

        public void LogError(string message)
        {
            _wr = new StreamWriter(_path, true);
            _wr.WriteLine(DateTime.Now + ", " + message);
            _wr.Close();

        }

        public void LogError(string message, System.Exception ex)
        {
            _wr = new StreamWriter(_path, true);
            _wr.WriteLine(DateTime.Now + ", " + message);
            _wr.Close();

        }

        public void LogError(System.Exception ex)
        {
            _wr = new StreamWriter(_path, true);
            _wr.Write(DateTime.Now + ", " + ex.ToString());
            _wr.Close();

        }

        #endregion
    }
}