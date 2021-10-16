using System;
using System.Drawing;
using System.Collections;

// ABB namespaces
using ABB.Robotics.Controllers;
using ABB.Robotics.Tps.Taf;
using ABB.Robotics.Tps.Windows.Forms;

using ABB.Robotics.ProductionScreen;
using ABB.Robotics.ProductionScreen.Base;

using ABB.Robotics.Tps.Resources;
using ABB.Robotics.Diagnostics;
using ABB.Robotics.Controllers.RapidDomain;

using TpsViewZhongXunNameSpace.Robot;
using TpsViewZhongXunNameSpace.ZhongXun;
using TpsViewZhongXunNameSpace.Utility;

//
// The ProductionScreenApp attribute is used by the SetupEditor to help the user to 
// fill in the needed fields that may be a bit hard to find out otherwise.
//
// Note: The attribute is not needed to run the app
//

[module: ProductionScreenApp(
Assembly = "TpsViewZhongXun.dll",							// Name of the assembly
Type = "TpsViewZhongXunNameSpace.TpsViewZhongXun",		// Name of the type, ie <namespace>.<class>
About = "Text about this widget.",							// Description of the widget (tooltip in SetupEditor)
IndataHelp = "Text about the indata.")]						// Help text for indata (tooltip in SetupEditor)

namespace TpsViewZhongXunNameSpace
{
    /// <summary>
    /// Summary description of the view.
    /// </summary>
    public class TpsViewZhongXun : TpsForm, ITpsViewSetup, ITpsViewActivation
    {
        #region Fields

        //flags
        private bool _isExecuting = false;
        private bool _isInAuto = false;
        private bool _appInFocus = true;

        private RWSystem rwSystem = null;
        private TemplateData templateData = null;
        private YamlFile yamlFile = null;
        private Setting setting = null;

        private const string CURRENT_MODULE_NAME = "TpsViewZhongXun";

        #endregion

        
        #region MultiView

        private enum ActiveView
        {
            Desktop = 0,
            Weld = 1,
            YAML=2,
            Setting=3,
            Register=4,
        }

        private ActiveView _activeView = ActiveView.Desktop;
        private TpsFormWeld _viewWeld = null;
        private TpsFormYAML _viewYAML = null;
        private TpsFormSetting _viewSetting = null;
        private TpsFormRegister _viewRegister = null;

        private ITpsViewLaunchServices _iTpsSite;
        private TpsResourceManager _tpsRm = null;

        #endregion

 
        private TpsLabel tpsLabel_Title;
        private Button button_YAML;
        private Button button_Setting;
        private Button button_Register;
        private Button button_Weld;
 

        public TpsViewZhongXun()
        {
            InitializeComponent();            

            try
            {
                _tpsRm = new ABB.Robotics.Tps.Resources.TpsResourceManager("TpsViewZhongXunNameSpace.strings", ABB.Robotics.Taf.Base.TafAssembly.Load("TpsViewZhongXunTexts.dll"));
                InitializeTexts();   
            }
            catch (System.Exception ex)
            {
                GTPUMessageBox.Show(this.Parent
                    , null
                    , string.Format("An unexpected error occurred while starting up ZhongXun Application. \n\n{0}", ex.Message)
                    , "ZhongXun Application Start-up Error"
                    , System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxButtons.OK);
            }
 
        }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                try
                {
                    if (disposing)
                    {
                        //ToDo: Call the Dispose method of all FP SDK instances that may otherwise cause memory leak
                        if (this._tpsRm != null)
                        {
                            this._tpsRm = null;
                        }
                        if (this.rwSystem != null)
                        {
                            this.rwSystem.Dispose();
                            this.rwSystem = null;
                        }
                    }
                }
                finally
                {
                    base.Dispose(disposing);
                }
            }
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tpsLabel_Title = new ABB.Robotics.Tps.Windows.Forms.TpsLabel();
            this.button_Weld = new ABB.Robotics.Tps.Windows.Forms.Button();
            this.button_YAML = new ABB.Robotics.Tps.Windows.Forms.Button();
            this.button_Setting = new ABB.Robotics.Tps.Windows.Forms.Button();
            this.button_Register = new ABB.Robotics.Tps.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tpsLabel_Title
            // 
            this.tpsLabel_Title.BackColor = System.Drawing.Color.LightGray;
            this.tpsLabel_Title.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tpsLabel_Title.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font18b;
            this.tpsLabel_Title.ForeColor = System.Drawing.Color.Blue;
            this.tpsLabel_Title.Location = new System.Drawing.Point(122, 72);
            this.tpsLabel_Title.Multiline = true;
            this.tpsLabel_Title.Name = "tpsLabel_Title";
            this.tpsLabel_Title.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.tpsLabel_Title.Size = new System.Drawing.Size(414, 61);
            this.tpsLabel_Title.TabIndex = 6;
            this.tpsLabel_Title.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.tpsLabel_Title.Title = "ZhongXun Utility";
            // 
            // button_Weld
            // 
            this.button_Weld.BackColor = System.Drawing.Color.White;
            this.button_Weld.BackgroundImage = null;
            this.button_Weld.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.button_Weld.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.button_Weld.Image = null;
            this.button_Weld.Location = new System.Drawing.Point(70, 168);
            this.button_Weld.Name = "button_Weld";
            this.button_Weld.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.button_Weld.Size = new System.Drawing.Size(120, 120);
            this.button_Weld.TabIndex = 7;
            this.button_Weld.Text = "模板";
            this.button_Weld.TextAlign = ABB.Robotics.Tps.Windows.Forms.ContentAlignmentABB.MiddleCenter;
            this.button_Weld.Click += new System.EventHandler(this.button_Weld_Click);
            // 
            // button_YAML
            // 
            this.button_YAML.BackColor = System.Drawing.Color.White;
            this.button_YAML.BackgroundImage = null;
            this.button_YAML.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.button_YAML.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.button_YAML.Image = null;
            this.button_YAML.Location = new System.Drawing.Point(265, 168);
            this.button_YAML.Name = "button_YAML";
            this.button_YAML.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.button_YAML.Size = new System.Drawing.Size(120, 120);
            this.button_YAML.TabIndex = 8;
            this.button_YAML.Text = "文件";
            this.button_YAML.TextAlign = ABB.Robotics.Tps.Windows.Forms.ContentAlignmentABB.MiddleCenter;
            this.button_YAML.Click += new System.EventHandler(this.button_YAML_Click);
            // 
            // button_Setting
            // 
            this.button_Setting.BackColor = System.Drawing.Color.White;
            this.button_Setting.BackgroundImage = null;
            this.button_Setting.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.button_Setting.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.button_Setting.Image = null;
            this.button_Setting.Location = new System.Drawing.Point(461, 168);
            this.button_Setting.Name = "button_Setting";
            this.button_Setting.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.button_Setting.Size = new System.Drawing.Size(120, 120);
            this.button_Setting.TabIndex = 9;
            this.button_Setting.Text = "设置";
            this.button_Setting.TextAlign = ABB.Robotics.Tps.Windows.Forms.ContentAlignmentABB.MiddleCenter;
            this.button_Setting.Click += new System.EventHandler(this.button_Setting_Click);
            // 
            // button_Register
            // 
            this.button_Register.BackColor = System.Drawing.Color.White;
            this.button_Register.BackgroundImage = null;
            this.button_Register.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.button_Register.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.button_Register.Image = null;
            this.button_Register.Location = new System.Drawing.Point(519, 335);
            this.button_Register.Name = "button_Register";
            this.button_Register.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.button_Register.Size = new System.Drawing.Size(120, 41);
            this.button_Register.TabIndex = 10;
            this.button_Register.Text = "Register";
            this.button_Register.TextAlign = ABB.Robotics.Tps.Windows.Forms.ContentAlignmentABB.MiddleCenter;
            this.button_Register.Click += new System.EventHandler(this.button_Register_Click);
            // 
            // TpsViewZhongXun
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Controls.Add(this.button_Register);
            this.Controls.Add(this.button_Setting);
            this.Controls.Add(this.button_YAML);
            this.Controls.Add(this.button_Weld);
            this.Controls.Add(this.tpsLabel_Title);
            this.Size = new System.Drawing.Size(650, 390);
            this.Text = "中巽";
            this.Controls.SetChildIndex(this.tpsLabel_Title, 0);
            this.Controls.SetChildIndex(this.button_Weld, 0);
            this.Controls.SetChildIndex(this.button_YAML, 0);
            this.Controls.SetChildIndex(this.button_Setting, 0);
            this.Controls.SetChildIndex(this.button_Register, 0);
            this.ResumeLayout(false);

        }
        #endregion

        #region ITpsViewSetup Members

        /// <summary>
        /// This method is called by TAF when the control is closed down (before Dispose is called).
        /// </summary>	
        void ITpsViewSetup.Uninstall()
        {
            try
            {
                // Do uninstall

            }
            catch (Exception ex)
            {
                // Add error message to "ProdScr.log" file
                ErrorHandler.AddErrorMessage(CURRENT_MODULE_NAME, ex.ToString());
            }
        }

        /// <summary>
        /// This method is called by TAF when the control is installed in the framework (after the constructor is called).
        /// </summary>				
        bool ITpsViewSetup.Install(object sender, object data)
        {
            try
            {
                // Do install


                this.rwSystem = new RWSystem();
                this.templateData = new TemplateData();
                this.yamlFile = new YamlFile();
                this.setting = new Setting();


                if (sender is ITpsViewLaunchServices)
                {
                    this._iTpsSite = sender as ITpsViewLaunchServices;
                }

                this.CheckLicense();

                return true;
            }
            catch (Exception ex)
            {
                // Add error message to "ProdScr.log" file
                ErrorHandler.AddErrorMessage(CURRENT_MODULE_NAME, ex.ToString());
                return false;
            }
        }

        #endregion

        #region ITpsViewActivation Members

        /// <summary>
        /// This method is called by TAF when the control goes from the active state to the passive state, 
        /// and is no longer visible in the client view. This happens when the user presses another application button 
        /// on the task bar, or closes the application. Normally, any subscriptions to controller events are removed here.
        /// </summary>				
        void ITpsViewActivation.Deactivate()
        {
            try
            {
                if (_activeView == ActiveView.Weld)
                {
                    //this._viewWeld.UnSubscribe();
                }
                _appInFocus = false;
            }
            catch (Exception ex)
            {
                // Add error message to "ProdScr.log" file
                ErrorHandler.AddErrorMessage(CURRENT_MODULE_NAME, ex.ToString());
            }
        }

        /// <summary>
        /// This method is called by TAF when the control goes from the passive state to the active state, 
        /// i.e. becomes visible in the client view. Normally, this is where subscriptions to controller events are set up.
        /// </summary>				
        void ITpsViewActivation.Activate()
        {
            try
            {
                if (_activeView == ActiveView.Desktop) // If first view is active
                {
                }
                else if (_activeView == ActiveView.Weld)
                {
                    this._viewWeld.Activate();
                }
                else if (_activeView == ActiveView.YAML)
                {
                    this._viewYAML.Activate();
                }
                else if (_activeView == ActiveView.Setting)
                {
                    this._viewSetting.Activate();
                }

                _appInFocus = true;
            }
            catch (Exception ex)
            {
                // Add error message to "ProdScr.log" file
                ErrorHandler.AddErrorMessage(CURRENT_MODULE_NAME, ex.ToString());
            }
        }

        #endregion


        void InitializeTexts()
        {
            this.Text = _tpsRm.GetString("TXT_ABB_MENU_TITLE");
            this.tpsLabel_Title.Text = _tpsRm.GetString("TXT_ABB_MENU_TITLE");

        }

        private void DisplayErrorMessage(string message)
        {
            // Show GTPUMessageBox on exception
            GTPUMessageBox.Show(this.Parent.Parent, null
                , string.Format("Unable to open {0} view. \n\nDo the preparations described in 'Prepare signals and RAPID data.doc' and try open the view again.\n\nError message: {1}", "", message)
                , string.Format("{0}Start-up Error", "")
                , System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxButtons.OK);
        }

        /// <summary>
        /// Executes when Close button of Production/Test view is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void _onViewClosing(object sender, System.ComponentModel.CancelEventArgs args)
        {
            try
            {
                // Set active view

                _activeView = ActiveView.Desktop;


            }
            catch (System.Exception ex)
            {
                Debug.Assert(false, "_onViewClosing failed with message: ", ex.ToString());
            }
        }

        void _viewClosed(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.Control)
            {
                ((System.Windows.Forms.Control)sender).Dispose();
            }
        }

        private void button_Weld_Click(object sender, EventArgs e)
        {
            try
            {
                // Wait cursor if it is performance demanding to open the view...
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                // Set active view 
                _activeView = ActiveView.Weld;

                // Create view
                _viewWeld = new TpsFormWeld(this._tpsRm, this.rwSystem,this.templateData);

                // Set up subscription to Closing event of Production view
                _viewWeld.Closing += new System.ComponentModel.CancelEventHandler(_onViewClosing);
                _viewWeld.Closed += new EventHandler(_viewClosed);
                _viewWeld.ShowMe(this);

                // Ask Production view to set up its subscriptions to controller events
                _viewWeld.Activate();
            }
            catch (System.Exception ex)
            {
                DisplayErrorMessage(ex.Message);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        private void button_YAML_Click(object sender, EventArgs e)
        {
            try
            {
                // Wait cursor if it is performance demanding to open the view...
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                // Set active view 
                _activeView = ActiveView.YAML;

                // Create view
                _viewYAML = new TpsFormYAML(this._tpsRm, this.rwSystem, this.templateData,this.yamlFile);

                // Set up subscription to Closing event of Production view
                _viewYAML.Closing += new System.ComponentModel.CancelEventHandler(_onViewClosing);
                _viewYAML.Closed += new EventHandler(_viewClosed);
                _viewYAML.ShowMe(this);

                // Ask Production view to set up its subscriptions to controller events
                _viewYAML.Activate();
            }
            catch (System.Exception ex)
            {
                DisplayErrorMessage(ex.Message);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        private void button_Setting_Click(object sender, EventArgs e)
        {
            try
            {
                // Wait cursor if it is performance demanding to open the view...
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                // Set active view 
                _activeView = ActiveView.Setting;

                // Create view
                _viewSetting = new TpsFormSetting(this._tpsRm, this.rwSystem,this.setting);

                // Set up subscription to Closing event of Production view
                _viewSetting.Closing += new System.ComponentModel.CancelEventHandler(_onViewClosing);
                _viewSetting.Closed += new EventHandler(_viewClosed);
                _viewSetting.ShowMe(this);

                // Ask Production view to set up its subscriptions to controller events
                _viewSetting.Activate();
            }
            catch (System.Exception ex)
            {
                DisplayErrorMessage(ex.Message);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        void CheckLicense()
        {
            string robot_serial_number_high_part = this.rwSystem.Controller.Configuration.Read("MOC", "ROBOT_SERIAL_NUMBER", "rob_1", "robot_serial_number_high_part");
            string robot_serial_number_low_part = this.rwSystem.Controller.Configuration.Read("MOC", "ROBOT_SERIAL_NUMBER", "rob_1", "robot_serial_number_low_part");
            string strSerialNumber = robot_serial_number_high_part + "-" + robot_serial_number_low_part;
            EncryptionHelper encryptionHelper = new EncryptionHelper();
            string strKey = encryptionHelper.EncryptString(strSerialNumber);
            
            RapidData rapidData = rwSystem.Controller.Rapid.GetRapidData("T_ROB1", "SharedModule", "strUtilityKey");
            string strUtilityKey = rapidData.Value.ToString().Trim("\"".ToCharArray());
            rapidData.Dispose();

            DateTime now = this.rwSystem.Controller.DateTime;

            bool boolHasLicense = false;
            if (strUtilityKey == strKey)
            {
                boolHasLicense = true;
            }
            else if (this.rwSystem.Controller.IsVirtual && strUtilityKey != "michael")
            {
                boolHasLicense = true;
            }
            else if (now.Year == 2020 && strUtilityKey == "ZhongXun2020")
            {
                boolHasLicense = true;
            }
            else if (now.Year == 2021 && strUtilityKey == "ZhongXun2021")
            {
                boolHasLicense = true;
            }
            else if (now.Year == 2022 && strUtilityKey == "ZhongXun2022")
            {
                boolHasLicense = true;
            }

            if (boolHasLicense)
            {
                this.button_Weld.Enabled = true;
                this.button_YAML.Enabled = true;
                this.button_Setting.Enabled = true;
                this.button_Register.Visible = false;
            }
            else
            {
                this.button_Weld.Enabled = false;
                this.button_YAML.Enabled = false;
                this.button_Setting.Enabled = false;
                this.button_Register.Visible = true;
            }
        }
            
        private void button_Register_Click(object sender, EventArgs e)
        {
            try
            {
                // Wait cursor if it is performance demanding to open the view...
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                // Set active view 
                _activeView = ActiveView.Register;

                // Create view
                _viewRegister = new TpsFormRegister(this._tpsRm, this.rwSystem);

                // Set up subscription to Closing event of Production view
                _viewRegister.Closing += new System.ComponentModel.CancelEventHandler(_onViewClosing);
                _viewRegister.Closed += new EventHandler(_viewClosed);
                _viewRegister.ShowMe(this);

                // Ask Production view to set up its subscriptions to controller events
                _viewRegister.Activate();
            }
            catch (System.Exception ex)
            {
                DisplayErrorMessage(ex.Message);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

    }
}
