using WebStore.Enum;
using WebStore.Interface;
using WebStore.Models;
using WebStore.Repositories;

namespace WebStore.Service
{
    public class LoggingService
    {
        private readonly IRepository<NLog_Error> _logRep;
        private readonly IUnitOfWork _unitOfWork;

        public LoggingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logRep = new Repository<NLog_Error>(unitOfWork);
        }

        public void CreateNLog(string actionName, string controllerName, string Action,
            string ParameterStr, string userid, LogLevel leve)
        {
            NLog_Error saveData = new NLog_Error()
            {
                ActionName = actionName,
                ControllersName = controllerName,
                Data_Action = Action,
                SaveData = ParameterStr,
                UserId = userid,
                LogLevel = leve.ToString()
            };
            _logRep.Create(saveData);
            _logRep.Commit();
        }

        /// <summary>
        /// Returns the log information.
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <param name="result">The result.</param>
        /// <param name="savedata">The savedata.</param>
        /// <param name="loglevel">The loglevel.</param>
        /// <param name="data_action">The data action.</param>
        /// <param name="controllersname">The controllersname.</param>
        /// <param name="actionname">The actionname.</param>
        /// <returns></returns>
        private NLog_Error ReturnLogInfo(string userid, string result,
            string savedata, string loglevel, string data_action,
            string controllersname, string actionname)
        {
            NLog_Error package = new NLog_Error()
            {
                ActionName = actionname,
                ControllersName = controllersname,
                Data_Action = data_action,
                LogLevel = loglevel,
                SaveData = savedata,
                Result = result,
                UserId = userid
            };
            return package;
        }

        public void Save()
        {
            _unitOfWork.Save();
        }
    }
}