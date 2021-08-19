using System;

using System.Collections.Generic;
using System.Text;

using ABB.Robotics.ProductionScreen.Base;

using TpsViewZhongXunNameSpace.Robot;

namespace TpsViewZhongXunNameSpace.ZhongXun
{
    public class Setting : IDisposable
    {
        private const string strTaskName = "T_ROB1";
        private const string strDataModuleName_JobMode = "SharedModule";
        private const string strDataName_JobMode = "numJobMode";

        #region Fields

        public int JobMode { get; set; }

        #endregion

        public Setting()
        {
        }

        #region Dispose

        private bool _disposed;

        ~Setting()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {

            }
            _disposed = true;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        public void RefreshData(RWSystem rwSystem)
        {
            ABB.Robotics.Controllers.RapidDomain.RapidData rapidData = rwSystem.Controller.Rapid.GetRapidData(strTaskName, strDataModuleName_JobMode, strDataName_JobMode);
            ABB.Robotics.Controllers.RapidDomain.Num num = (ABB.Robotics.Controllers.RapidDomain.Num)rapidData.Value;
            this.JobMode = (int)((double)num);
            rapidData.Dispose();

        }

        public void ApplyData(RWSystem rwSystem)
        {

            ABB.Robotics.Controllers.RapidDomain.RapidData rapidData = rwSystem.Controller.Rapid.GetRapidData(strTaskName, strDataModuleName_JobMode, strDataName_JobMode);
            rapidData.Value = new ABB.Robotics.Controllers.RapidDomain.Num(this.JobMode);
            rapidData.Dispose();


        }

    }
}
