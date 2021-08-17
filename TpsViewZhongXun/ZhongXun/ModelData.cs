using System;
using System.Collections.Generic;
using System.Text;

using ABB.Robotics.ProductionScreen.Base;

using TpsViewZhongXunNameSpace.Robot;

namespace TpsViewZhongXunNameSpace.ZhongXun
{
    public class ModelData : IDisposable
    {
        private const string strTaskName = "T_ROB1";
        private const string strDataTypeModuleName = "GeneralModule";
        private const string strDataType = "RECORDModelData";

         #region Fields

        public string Name { get; set; }
        
        public ABB.Robotics.Controllers.RapidDomain.Pos Location{ get; set; }


        #endregion

        public ModelData()
        {
        }

        #region Dispose

        private bool _disposed;

        ~ModelData()
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

        public override string ToString()
        {
            return string.Format("[\"{0}\",{1}]"
                , this.Name, this.Location.ToString());
        }

        public void RefreshData(RWSystem rwSystem, string strModelData)
        {
            ABB.Robotics.Controllers.RapidDomain.UserDefined abbUserDefinedDataType = new ABB.Robotics.Controllers.RapidDomain.UserDefined(rwSystem.Controller.Rapid.GetRapidDataType(new string[] { strTaskName, strDataTypeModuleName, strDataType }));
            abbUserDefinedDataType.FillFromString(strModelData);

            int i = 0;
            this.Name = abbUserDefinedDataType.Components[i++].ToString().Trim(new char[]{'"'});
            this.Location = (ABB.Robotics.Controllers.RapidDomain.Pos)abbUserDefinedDataType.Components[i++];
            abbUserDefinedDataType.Dispose();
        }
    }
}
