using System;

using System.Collections.Generic;
using System.Text;

using ABB.Robotics.ProductionScreen.Base;

using TpsViewZhongXunNameSpace.Robot;

namespace TpsViewZhongXunNameSpace.ZhongXun
{
    public class YamlFile : IDisposable
    {

        private const string strTaskName = "T_ROB1";
        private const string strDataModuleName_ModelData = "SharedModule";
        private const string strDataName_JobMode = "numJobMode";

        private const string strDataModuleName_YamlFile = "YAMLModule";
        private const string strDataName_YamlFileName = "strModelDataYamlFileName";

        #region Fields

        public string YamlFileName { get; set; }

        private List<string> yamlFileList = new List<string>();
        public List<string> YamlFileList
        {
            get { return yamlFileList; }
        }

        #endregion


        public YamlFile()
        {
        }

        #region Dispose

        private bool _disposed;

        ~YamlFile()
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
            this.yamlFileList.Clear();

            ABB.Robotics.Controllers.FileSystemDomain.FileSystem fileSystem = rwSystem.Controller.FileSystem;
            ABB.Robotics.Controllers.FileSystemDomain.ControllerFileSystemInfo[] fileInfoArray;
            ABB.Robotics.Controllers.FileSystemDomain.ControllerFileSystemInfo fileInfo;

            fileInfoArray = fileSystem.GetFilesAndDirectories("yaml");    
            for (int i = 0; i < fileInfoArray.Length; i++)
            {
                fileInfo = fileInfoArray[i];
                if (fileInfo.Name!="." && fileInfo.Name!="..")
                {
                    this.yamlFileList.Add(fileInfo.Name);
                }                
            }
            fileSystem.Dispose();

        }

        public void StartToWeld(RWSystem rwSystem)
        {
            ABB.Robotics.Controllers.RapidDomain.RapidData rapidData = rwSystem.Controller.Rapid.GetRapidData(strTaskName, strDataModuleName_ModelData, strDataName_JobMode);
            rapidData.Value = new ABB.Robotics.Controllers.RapidDomain.Num(2);
            rapidData.Dispose();

            rapidData = rwSystem.Controller.Rapid.GetRapidData(strTaskName, strDataModuleName_YamlFile, strDataName_YamlFileName);
            rapidData.Value = new ABB.Robotics.Controllers.RapidDomain.String(this.YamlFileName);
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
