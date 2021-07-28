using System;
using System.Collections.Generic;
using System.Text;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.RapidDomain;
using ABB.Robotics.Tps.Windows.Forms;

namespace TpsViewZhongXunNameSpace.Robot
{
    public class RWSystem : IDisposable
    {
        private bool _disposed;
        private Controller controller = null;
        public Controller Controller
        {
            get { return controller; }
        }

        private Task taskTROB1 = null;
        public Task TROB1
        {
            get { return taskTROB1; }
        }

        public RWSystem()
        {
            controller = new Controller();
            taskTROB1 = controller.Rapid.GetTask("T_ROB1");
        }

        ~RWSystem()
        {
            Dispose(false);
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                if (taskTROB1 != null)
                {
                    taskTROB1.Dispose();
                    taskTROB1 = null;
                }
                if (controller != null)
                {
                    controller.Dispose();
                    controller = null;
                }

            }
            _disposed = true;
        }


        public void ApplyTpsControl(string strTaskName, string strModuleName, string strDataName, TpsControl tpsControl)
        {
            if (tpsControl is NumEditor)
            {
                ABB.Robotics.Controllers.RapidDomain.RapidData rapidData = this.controller.Rapid.GetRapidData(strTaskName, strModuleName, strDataName);
                ABB.Robotics.Controllers.RapidDomain.Num number = new ABB.Robotics.Controllers.RapidDomain.Num(Convert.ToDouble(((NumEditor)tpsControl).Value));
                rapidData.Value = number;
                rapidData.Dispose();
            }
        }

        public void RefreshTpsControl(string strTaskName, string strModuleName, string strDataName, TpsControl tpsControl)
        {
            if (tpsControl is NumEditor)
            {
                ABB.Robotics.Controllers.RapidDomain.RapidData rapidData = this.controller.Rapid.GetRapidData(strTaskName, strModuleName, strDataName);
                ABB.Robotics.Controllers.RapidDomain.Num number = new ABB.Robotics.Controllers.RapidDomain.Num();
                number.FillFromString(rapidData.Value.ToString());
                ((NumEditor)tpsControl).Value = Convert.ToDecimal(number);
                rapidData.Dispose();
            }
        }
    }
}
