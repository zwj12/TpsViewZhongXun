using System;

using System.Collections.Generic;
using System.Text;

using ABB.Robotics.ProductionScreen.Base;

using TpsViewZhongXunNameSpace.Robot;

namespace TpsViewZhongXunNameSpace.ZhongXun
{

    public class TemplateData : IDisposable
    {
        private const string strTaskName = "T_ROB1";

        private const string strDataModuleName_TemplateList = "TaskModule";
        private const string strDataName_TemplateList = "strTemplateList";

        private const string strDataModuleName_WeldPartList = "JobWeldModule";
        private const string strDataName_WeldPartList = "strWeldPartList";
        private const string strDataName_WeldPartPosList = "strWeldPartPosList";
        private const string strDataName_WeldPartStart = "numModelStart";
        private const string strDataName_WeldPartQuantity = "numModelQuantity";

        private const string strDataModuleName_ModelData = "SharedModule";
        private const string strDataName_ModelDataFirst = "rModelDataFirst";
        private const string strDataName_ModelOffset = "posModelOffset";
        private const string strDataName_ModelStart = "numModelOffsetStart";
        private const string strDataName_ModelQuantity = "numModelOffsetQuantity";

        #region Fields

        private List<String> templateList = new List<string>();
        public List<String> TemplateList
        {
            get { return templateList; }
        }

        private List<String> weldTemplateList = new List<string>();
        public List<String> WeldTemplateList
        {
            get { return weldTemplateList; }
        }

        public ABB.Robotics.Controllers.RapidDomain.Pos Location { get; set; }

        #endregion

        public TemplateData()
        {
        }

        #region Dispose

        private bool _disposed;

        ~TemplateData()
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
            return string.Format("");
        }

        public void RefreshData(RWSystem rwSystem)
        {
            ErrorHandler.AddErrorMessage("MichaelLog", "ModelData.RefreshData");

            this.TemplateList.Clear();
            this.WeldTemplateList.Clear();

            ABB.Robotics.Controllers.RapidDomain.RapidData rTemplateArray = rwSystem.Controller.Rapid.GetRapidData(strTaskName, strDataModuleName_TemplateList, strDataName_TemplateList);
            if (rTemplateArray.IsArray)
            {
                ABB.Robotics.Controllers.RapidDomain.ArrayData templateArray = (ABB.Robotics.Controllers.RapidDomain.ArrayData)rTemplateArray.Value;
                for (int i = 1; i <= templateArray.Length; i++)
                {
                    ABB.Robotics.Controllers.RapidDomain.String template = (ABB.Robotics.Controllers.RapidDomain.String)templateArray[i];
                    this.TemplateList.Add(template.ToString().Trim(new char[1] { '"' }));
                }
            }

            ABB.Robotics.Controllers.RapidDomain.RapidData rModelDataArray = rwSystem.Controller.Rapid.GetRapidData(strTaskName, strDataModuleName_ModelData, strDataName_ModelDataFirst);
            if (rModelDataArray.IsArray)
            {
                ABB.Robotics.Controllers.RapidDomain.ArrayData modelDataArray = (ABB.Robotics.Controllers.RapidDomain.ArrayData)rModelDataArray.Value;
                for (int i = 1; i <= modelDataArray.Length; i++)
                {
                    ModelData modelData = new ModelData();
                    modelData.RefreshData(rwSystem, modelDataArray[i].ToString());
                    if (!String.IsNullOrEmpty(modelData.Name))
                    {
                        this.WeldTemplateList.Add(modelData.Name);
                    }                  
                    if (i == 1)
                    {
                        this.Location = modelData.Location;
                    }
                }
            }


            //int i = 0;
            //this.sched = Convert.ToInt32(abbUserDefinedDataType.Components[i++].ToString());
            //this.mode = Convert.ToInt32(abbUserDefinedDataType.Components[i++].ToString());
            //this.voltage = Convert.ToDecimal(abbUserDefinedDataType.Components[i++].ToString());
            //this.wirefeed = Convert.ToDecimal(abbUserDefinedDataType.Components[i++].ToString());
            //this.control = Convert.ToDecimal(abbUserDefinedDataType.Components[i++].ToString());
            //this.current = Convert.ToDecimal(abbUserDefinedDataType.Components[i++].ToString());
            //this.voltage2 = Convert.ToDecimal(abbUserDefinedDataType.Components[i++].ToString());
            //this.wirefeed2 = Convert.ToDecimal(abbUserDefinedDataType.Components[i++].ToString());
            //this.control2 = Convert.ToDecimal(abbUserDefinedDataType.Components[i++].ToString());

            //abbUserDefinedDataType.Dispose();
        }

    }
}
