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
        private const string strDataTypeModuleName_ModelData = "GeneralModule";
        private const string strDataType_ModelData = "RECORDModelData";
        private const string strDataName_ModelDataFirst = "rModelDataFirst";
        private const string strDataName_ModelOffset = "posModelOffset";
        private const string strDataName_ModelStart = "numModelOffsetStart";
        private const string strDataName_ModelQuantity = "numModelOffsetQuantity";
        private const string strDataName_JobMode = "numJobMode";

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

        public ABB.Robotics.Controllers.RapidDomain.Pos ModelLocation { get; set; }

        public ABB.Robotics.Controllers.RapidDomain.Pos ModelOffset { get; set; }

  
        private int modelOffsetStart ;
        public int ModelOffsetStart
        {
            get { return modelOffsetStart; }
            set{
                if (value >= 1)
                {
                    modelOffsetStart = value;
                }
                else
                {
                    modelOffsetStart = 1;
                }               
            }
        }

        private int modelOffsetQuantity;
        public int ModelOffsetQuantity
        {
            get { return modelOffsetQuantity; }
            set
            {
                if (value >= 1)
                {
                    modelOffsetQuantity = value;
                }
                else
                {
                    modelOffsetQuantity = 1;
                }
            }
        }

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
            rTemplateArray.Dispose();

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
                        this.ModelLocation = modelData.Location;
                    }
                }
            }
            rModelDataArray.Dispose();

            ABB.Robotics.Controllers.RapidDomain.RapidData rapidData = rwSystem.Controller.Rapid.GetRapidData(strTaskName, strDataModuleName_ModelData, strDataName_ModelOffset);
            this.ModelOffset = (ABB.Robotics.Controllers.RapidDomain.Pos)rapidData.Value;
            rapidData.Dispose();

            rapidData = rwSystem.Controller.Rapid.GetRapidData(strTaskName, strDataModuleName_ModelData, strDataName_ModelStart);
            ABB.Robotics.Controllers.RapidDomain.Num num = (ABB.Robotics.Controllers.RapidDomain.Num)rapidData.Value;
            this.ModelOffsetStart =(int)((double) num);
            rapidData.Dispose();

            rapidData = rwSystem.Controller.Rapid.GetRapidData(strTaskName, strDataModuleName_ModelData, strDataName_ModelQuantity);
             num = (ABB.Robotics.Controllers.RapidDomain.Num)rapidData.Value;
            this.ModelOffsetQuantity = (int)((double)num);
            rapidData.Dispose();
                    
        }

        public void ApplyData(RWSystem rwSystem)
        {
            //ErrorHandler.AddErrorMessage("MichaelLog", "ModelData.RefreshData");
            ABB.Robotics.Controllers.RapidDomain.RapidData rModelDataArray = rwSystem.Controller.Rapid.GetRapidData(strTaskName, strDataModuleName_ModelData, strDataName_ModelDataFirst);
            ABB.Robotics.Controllers.RapidDomain.ArrayData modelDataArray = (ABB.Robotics.Controllers.RapidDomain.ArrayData)rModelDataArray.Value;
            ABB.Robotics.Controllers.RapidDomain.UserDefined rModelData = (ABB.Robotics.Controllers.RapidDomain.UserDefined)rModelDataArray.ReadItem(1);
            for (int i = 1; i <= modelDataArray.Length; i++)
            {
                if (i > modelDataArray.Length)
                {
                    break;
                }
                ModelData modelData = new ModelData();
                if (i > this.WeldTemplateList.Count)
                {
                    modelData.Name = "";
                    modelData.Location = this.ModelLocation;          
                }
                else
                {
                    modelData.Name = this.weldTemplateList[i - 1];
                    modelData.Location = this.ModelLocation;          
                }
                rModelData.FillFromString(modelData.ToString());
                rModelDataArray.WriteItem(rModelData, i);
            }
            rModelData.Dispose();
            rModelDataArray.Dispose();

            ABB.Robotics.Controllers.RapidDomain.RapidData rapidData = rwSystem.Controller.Rapid.GetRapidData(strTaskName, strDataModuleName_ModelData, strDataName_ModelOffset);
            rapidData.Value = this.ModelOffset;
            rapidData.Dispose();

            rapidData = rwSystem.Controller.Rapid.GetRapidData(strTaskName, strDataModuleName_ModelData, strDataName_ModelStart);
            rapidData.Value= new  ABB.Robotics.Controllers.RapidDomain.Num( this.modelOffsetStart);
            rapidData.Dispose();

            rapidData = rwSystem.Controller.Rapid.GetRapidData(strTaskName, strDataModuleName_ModelData, strDataName_ModelQuantity);
            rapidData.Value = new ABB.Robotics.Controllers.RapidDomain.Num(this.ModelOffsetQuantity);
            rapidData.Dispose();

        }

        public void StartToWeld(RWSystem rwSystem)
        {
            ABB.Robotics.Controllers.RapidDomain.RapidData rapidData = rwSystem.Controller.Rapid.GetRapidData(strTaskName, strDataModuleName_ModelData, strDataName_JobMode);
            rapidData.Value = new ABB.Robotics.Controllers.RapidDomain.Num(1);
            rapidData.Dispose();

            ABB.Robotics.Controllers.IOSystemDomain.Signal signal = rwSystem.Controller.IOSystem.GetSignal("sgoPMPLC_1");
            ABB.Robotics.Controllers.IOSystemDomain.GroupSignal sgoPMPLC_1 = (ABB.Robotics.Controllers.IOSystemDomain.GroupSignal)signal;
            sgoPMPLC_1.GroupValue = 101;

            signal = rwSystem.Controller.IOSystem.GetSignal("sgiPLCCode_1");
            ABB.Robotics.Controllers.IOSystemDomain.GroupSignal sgiPLCCode_1 = (ABB.Robotics.Controllers.IOSystemDomain.GroupSignal)signal;
            if (sgiPLCCode_1.GroupValue == 101)
            {
                signal = rwSystem.Controller.IOSystem.GetSignal("sdoRunPart_1");
                ABB.Robotics.Controllers.IOSystemDomain.DigitalSignal sdoRunPart_1 = (ABB.Robotics.Controllers.IOSystemDomain.DigitalSignal)signal;
                sdoRunPart_1.Pulse();
            }                        
        }
    }
}
