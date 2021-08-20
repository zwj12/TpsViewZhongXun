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
using TpsViewZhongXunNameSpace.Utility;

namespace TpsViewZhongXunNameSpace
{
    public class TpsFormRegister : TpsForm, ITpsViewActivation
    {
        private TpsResourceManager _tpsRm = null;
        private RWSystem rwSystem = null;

        private ABB.Robotics.Tps.Windows.Forms.MenuItem menuItem_Refresh;
        private ABB.Robotics.Tps.Windows.Forms.MenuItem menuItem_Apply;
        private ABB.Robotics.Tps.Windows.Forms.MenuItem menuItem_Close;
        private DataEditor dataEditor_Key;
        private TpsLabel tpsLabel_Key;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        public TpsFormRegister(TpsResourceManager rM, RWSystem rwSystem)
        {
            try
            {
                InitializeComponent();
                this._tpsRm = rM;
                this.rwSystem = rwSystem;
                this.InitializeTexts();
            }
            catch (System.Exception ex)
            {
                // If initialization of application fails a message box is shown
                GTPUMessageBox.Show(this.Parent
                    , null
                    , string.Format("An unexpected error occurred while starting up Weld Parameter Application. \n\n{0}", ex.Message)
                    , "Weld Parameter Application Start-up Error"
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
            this.dataEditor_Key = new ABB.Robotics.Tps.Windows.Forms.DataEditor();
            this.tpsLabel_Key = new ABB.Robotics.Tps.Windows.Forms.TpsLabel();
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
            // dataEditor_Key
            // 
            this.dataEditor_Key.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataEditor_Key.CaretVisible = false;
            this.dataEditor_Key.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.dataEditor_Key.Location = new System.Drawing.Point(96, 98);
            this.dataEditor_Key.Multiline = true;
            this.dataEditor_Key.Name = "dataEditor_Key";
            this.dataEditor_Key.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataEditor_Key.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.dataEditor_Key.SelectionLength = 0;
            this.dataEditor_Key.SelectionStart = 0;
            this.dataEditor_Key.SelectionVisible = false;
            this.dataEditor_Key.Size = new System.Drawing.Size(517, 40);
            this.dataEditor_Key.TabIndex = 2;
            this.dataEditor_Key.Text = "";
            this.dataEditor_Key.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.dataEditor_Key.Click += new System.EventHandler(this.dataEditor_Key_Click);
            // 
            // tpsLabel_Key
            // 
            this.tpsLabel_Key.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tpsLabel_Key.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.tpsLabel_Key.Location = new System.Drawing.Point(25, 98);
            this.tpsLabel_Key.Multiline = true;
            this.tpsLabel_Key.Name = "tpsLabel_Key";
            this.tpsLabel_Key.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.tpsLabel_Key.Size = new System.Drawing.Size(65, 24);
            this.tpsLabel_Key.TabIndex = 3;
            this.tpsLabel_Key.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.tpsLabel_Key.Title = "Key";
            // 
            // TpsFormRegister
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.tpsLabel_Key);
            this.Controls.Add(this.dataEditor_Key);
            // 
            // 
            // 
            this.MainMenu.MenuItems.Add(this.menuItem_Refresh);
            this.MainMenu.MenuItems.Add(this.menuItem_Apply);
            this.MainMenu.MenuItems.Add(this.menuItem_Close);
            this.Size = new System.Drawing.Size(640, 390);
            this.Text = "Register";
            this.Controls.SetChildIndex(this.dataEditor_Key, 0);
            this.Controls.SetChildIndex(this.tpsLabel_Key, 0);
            this.ResumeLayout(false);

        }

        #endregion

        #region ITpsViewActivation Members

        public void Activate()
        {
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
                ABB.Robotics.Controllers.RapidDomain.RapidData rapidData = rwSystem.Controller.Rapid.GetRapidData("T_ROB1", "SharedModule", "strUtilityKey");
                string strUtilityKey = rapidData.Value.ToString().Trim("\"".ToCharArray());
                rapidData.Dispose();
                this.dataEditor_Key.Text = strUtilityKey;

                this.menuItem_Apply.Enabled = false;
            }
            catch (Exception ex)
            {
                GTPUMessageBox.Show(this.Parent.Parent, null
                    , string.Format("An unexpected error occurred when reading RAPID data 'register'. Message {0}", ex.ToString())
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
                if (this.dataEditor_Key.Text=="!Quarter0")
                {
                    string robot_serial_number_high_part = this.rwSystem.Controller.Configuration.Read("MOC", "ROBOT_SERIAL_NUMBER", "rob_1", "robot_serial_number_high_part");
                    string robot_serial_number_low_part = this.rwSystem.Controller.Configuration.Read("MOC", "ROBOT_SERIAL_NUMBER", "rob_1", "robot_serial_number_low_part");
                    string strSerialNumber = robot_serial_number_high_part + "-" + robot_serial_number_low_part;
                    EncryptionHelper encryptionHelper = new EncryptionHelper();
                    string strKey = encryptionHelper.EncryptString(strSerialNumber);
                    this.dataEditor_Key.Text = strKey;
                }
                ABB.Robotics.Controllers.RapidDomain.RapidData rapidData = rwSystem.Controller.Rapid.GetRapidData("T_ROB1", "SharedModule", "strUtilityKey");
                ABB.Robotics.Controllers.RapidDomain.String strUtilityKey = new ABB.Robotics.Controllers.RapidDomain.String(this.dataEditor_Key.Text);
                rapidData.Value = strUtilityKey;
                rapidData.Dispose();

                this.menuItem_Apply.Enabled = false;
            }
            catch (Exception ex)
            {
                GTPUMessageBox.Show(this.Parent.Parent, null
                    , string.Format("An unexpected error occurred when applying RAPID data 'register'. Message {0}", ex.ToString())
                    , "System Error"
                    , System.Windows.Forms.MessageBoxIcon.Hand
                    , System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        private void menuItem_Refresh_Click(object sender, EventArgs e)
        {
            this.Invoke(this.UpdateGUI);
        }

        private void dataEditor_Key_Click(object sender, EventArgs e)
        {
            this.menuItem_Apply.Enabled = true;
        }

        void InitializeTexts()
        {
            //this.Text = _tpsRm.GetString("TXT_TpsFormWeldParameterTitle");

        }
    }
}