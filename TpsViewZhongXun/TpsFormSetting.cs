using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ABB.Robotics.Tps.Windows.Forms;

using ABB.Robotics.Tps.Taf;
using ABB.Robotics.Tps.Resources;
using ABB.Robotics.Controllers.IOSystemDomain;
using ABB.Robotics.ProductionScreen.Base;
using TpsViewZhongXunNameSpace.Robot;
using TpsViewZhongXunNameSpace.ZhongXun;

namespace TpsViewZhongXunNameSpace
{
    public class TpsFormSetting : TpsForm, ITpsViewActivation
    {
        #region Fields

        private const string CURRENT_MODULE_NAME = "TpsFormSetting";
        private TpsResourceManager _tpsRm = null;
        private RWSystem rwSystem = null;
        private Setting setting = null;

        #endregion
        private ABB.Robotics.Tps.Windows.Forms.MenuItem menuItem_Refresh;
        private ABB.Robotics.Tps.Windows.Forms.MenuItem menuItem_Apply;
        private ABB.Robotics.Tps.Windows.Forms.MenuItem menuItem_Close;
        private TpsLabel tpsLabel1;
        private ABB.Robotics.Tps.Windows.Forms.ComboBox comboBox_JobMode;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        public TpsFormSetting(TpsResourceManager rM, RWSystem rwSystem, Setting setting)
        {
            InitializeComponent();

            try
            {
                this._tpsRm = rM;
                this.rwSystem = rwSystem;
                this.setting = setting;

                this.InitializeTexts();
            }
            catch (System.Exception ex)
            {
                // If initialization of application fails a message box is shown
                GTPUMessageBox.Show(this.Parent
                    , null
                    , string.Format("An unexpected error occurred while starting up TpsFormScanData Application. \n\n{0}", ex.Message)
                    , "TpsFormScanData Application Start-up Error"
                    , MessageBoxIcon.Hand, MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Clean up any resources used by this class.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                try
                {
                    if (disposing)
                    {
                        //ToDo: Call the Dispose method of all FP SDK instances that may otherwise cause memory leak

                        if (components != null)
                        {
                            components.Dispose();
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
            this.menuItem_Refresh = new ABB.Robotics.Tps.Windows.Forms.MenuItem();
            this.menuItem_Apply = new ABB.Robotics.Tps.Windows.Forms.MenuItem();
            this.menuItem_Close = new ABB.Robotics.Tps.Windows.Forms.MenuItem();
            this.tpsLabel1 = new ABB.Robotics.Tps.Windows.Forms.TpsLabel();
            this.comboBox_JobMode = new ABB.Robotics.Tps.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // menuItem_Refresh
            // 
            this.menuItem_Refresh.Checked = false;
            this.menuItem_Refresh.DockToRight = true;
            this.menuItem_Refresh.Enabled = true;
            this.menuItem_Refresh.Image = null;
            this.menuItem_Refresh.ImageSelected = null;
            this.menuItem_Refresh.Index = 0;
            this.menuItem_Refresh.Pressed = false;
            this.menuItem_Refresh.Text = "Refresh";
            this.menuItem_Refresh.Width = 128;
            this.menuItem_Refresh.Click += new System.EventHandler(this.menuItem_Refresh_Click);
            // 
            // menuItem_Apply
            // 
            this.menuItem_Apply.Checked = false;
            this.menuItem_Apply.DockToRight = true;
            this.menuItem_Apply.Enabled = true;
            this.menuItem_Apply.Image = null;
            this.menuItem_Apply.ImageSelected = null;
            this.menuItem_Apply.Index = 1;
            this.menuItem_Apply.Pressed = false;
            this.menuItem_Apply.Text = "Apply";
            this.menuItem_Apply.Width = 128;
            this.menuItem_Apply.Click += new System.EventHandler(this.menuItem_Apply_Click);
            // 
            // menuItem_Close
            // 
            this.menuItem_Close.Checked = false;
            this.menuItem_Close.DockToRight = true;
            this.menuItem_Close.Enabled = true;
            this.menuItem_Close.Image = null;
            this.menuItem_Close.ImageSelected = null;
            this.menuItem_Close.Index = 2;
            this.menuItem_Close.Pressed = false;
            this.menuItem_Close.Text = "Close";
            this.menuItem_Close.Width = 128;
            this.menuItem_Close.Click += new System.EventHandler(this.menuItem_Close_Click);
            // 
            // tpsLabel1
            // 
            this.tpsLabel1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tpsLabel1.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.tpsLabel1.Location = new System.Drawing.Point(42, 79);
            this.tpsLabel1.Multiline = true;
            this.tpsLabel1.Name = "tpsLabel1";
            this.tpsLabel1.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.tpsLabel1.Size = new System.Drawing.Size(100, 24);
            this.tpsLabel1.TabIndex = 2;
            this.tpsLabel1.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.tpsLabel1.Title = "工作模式";
            // 
            // comboBox_JobMode
            // 
            this.comboBox_JobMode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.comboBox_JobMode.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.comboBox_JobMode.Items.Add("示教器");
            this.comboBox_JobMode.Items.Add("文件调用");
            this.comboBox_JobMode.Items.Add("PLC调用");
            this.comboBox_JobMode.Location = new System.Drawing.Point(196, 79);
            this.comboBox_JobMode.Name = "comboBox_JobMode";
            this.comboBox_JobMode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.comboBox_JobMode.Size = new System.Drawing.Size(150, 30);
            this.comboBox_JobMode.TabIndex = 3;
            this.comboBox_JobMode.Text = "";
            this.comboBox_JobMode.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.dataControl_PropertyChanged);
            // 
            // TpsFormSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.comboBox_JobMode);
            this.Controls.Add(this.tpsLabel1);
            // 
            // 
            // 
            this.MainMenu.MenuItems.Add(this.menuItem_Refresh);
            this.MainMenu.MenuItems.Add(this.menuItem_Apply);
            this.MainMenu.MenuItems.Add(this.menuItem_Close);
            this.Size = new System.Drawing.Size(640, 390);
            this.Text = "设置";
            this.Controls.SetChildIndex(this.tpsLabel1, 0);
            this.Controls.SetChildIndex(this.comboBox_JobMode, 0);
            this.ResumeLayout(false);

        }

        #endregion

        #region ITpsViewActivation Members

        public void Activate()
        {
            this.setting.RefreshData(this.rwSystem);
            this.Invoke(this.UpdateGUI);
            //throw new NotImplementedException();
        }

        public void Deactivate()
        {
            throw new NotImplementedException();
        }

        #endregion


        private void UpdateGUI(object sender, EventArgs e)
        {
            try
            {
                if (this.setting.JobMode >= 1 && this.setting.JobMode <= 3)
                {
                    this.comboBox_JobMode.SelectedIndex = this.setting.JobMode-1;
                }                
                this.menuItem_Apply.Enabled = false;
            }
            catch (Exception ex)
            {
                GTPUMessageBox.Show(this.Parent.Parent, null
                    , string.Format("An unexpected error occurred when reading RAPID data 'weld data'. Message {0}", ex.ToString())
                    , "System Error"
                    , System.Windows.Forms.MessageBoxIcon.Hand
                    , System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        private void menuItem_Close_Click(object sender, EventArgs e)
        {
            this.CloseMe();
        }

        private void menuItem_Apply_Click(object sender, EventArgs e)
        {
            try
            {
                this.setting.JobMode = this.comboBox_JobMode.SelectedIndex + 1;
                this.setting.ApplyData(this.rwSystem);
                this.menuItem_Apply.Enabled = false;
            }
            catch (Exception ex)
            {
                GTPUMessageBox.Show(this.Parent.Parent, null
                    , string.Format("An unexpected error occurred when applying RAPID data. Message {0}", ex.ToString())
                    , "System Error"
                    , System.Windows.Forms.MessageBoxIcon.Hand
                    , System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        private void menuItem_Refresh_Click(object sender, EventArgs e)
        {
            this.setting.RefreshData(this.rwSystem);
            this.Invoke(this.UpdateGUI);
        }

        private void dataControl_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.menuItem_Apply.Enabled = true;
        }

        void InitializeTexts()
        {

        }
    }
}