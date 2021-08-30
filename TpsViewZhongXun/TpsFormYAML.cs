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
    public class TpsFormYAML : TpsForm, ITpsViewActivation
    {
        #region Fields

        private const string CURRENT_MODULE_NAME = "TpsFormYAML";
        private TpsResourceManager _tpsRm = null;
        private RWSystem rwSystem = null;
        private TemplateData templateData;
        private YamlFile yamlFile;

        #endregion

        private ABB.Robotics.Tps.Windows.Forms.MenuItem menuItem_Refresh;
        private ABB.Robotics.Tps.Windows.Forms.MenuItem menuItem_Apply;
        private ABB.Robotics.Tps.Windows.Forms.MenuItem menuItem_Close;
        private GroupBox groupBox1;
        private ABB.Robotics.Tps.Windows.Forms.ListView listView_YamlFile;
        private ABB.Robotics.Tps.Windows.Forms.ColumnHeader columnHeader_FileName;
        private ABB.Robotics.Tps.Windows.Forms.TextBox textBox_YAML;
        private TpsLabel tpsLabel1;
        private ABB.Robotics.Tps.Windows.Forms.Button button_StartToWeld;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        public TpsFormYAML(TpsResourceManager rM, RWSystem rwSystem, TemplateData templateData, YamlFile yamlFile)
        {
            InitializeComponent();

            try
            {
                this._tpsRm = rM;
                this.rwSystem = rwSystem;
                this.templateData = templateData;
                this.yamlFile = yamlFile;

                this.InitializeTexts();
            }
            catch (System.Exception ex)
            {
                // If initialization of application fails a message box is shown
                GTPUMessageBox.Show(this.Parent
                    , null
                    , string.Format("An unexpected error occurred while starting up YAML Application. \n\n{0}", ex.Message)
                    , "Weld Application Start-up Error"
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
            this.groupBox1 = new ABB.Robotics.Tps.Windows.Forms.GroupBox();
            this.listView_YamlFile = new ABB.Robotics.Tps.Windows.Forms.ListView();
            this.columnHeader_FileName = new ABB.Robotics.Tps.Windows.Forms.ColumnHeader();
            this.textBox_YAML = new ABB.Robotics.Tps.Windows.Forms.TextBox();
            this.tpsLabel1 = new ABB.Robotics.Tps.Windows.Forms.TpsLabel();
            this.button_StartToWeld = new ABB.Robotics.Tps.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listView_YamlFile);
            this.groupBox1.HorizontalAlign = ABB.Robotics.Tps.Windows.Forms.HorAlign.Left;
            this.groupBox1.LineStyle = ABB.Robotics.Tps.Windows.Forms.LineStyle.SingleLine;
            this.groupBox1.Location = new System.Drawing.Point(12, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 300);
            this.groupBox1.Title = "文件数据库";
            this.groupBox1.VerticalAlign = ABB.Robotics.Tps.Windows.Forms.VerAlign.Center;
            // 
            // listView_YamlFile
            // 
            this.listView_YamlFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView_YamlFile.Columns.Add(this.columnHeader_FileName);
            this.listView_YamlFile.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.listView_YamlFile.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Clickable;
            this.listView_YamlFile.ListViewItemFont = 12F;
            this.listView_YamlFile.listviewLoactionWidth = 0;
            this.listView_YamlFile.Location = new System.Drawing.Point(14, 30);
            this.listView_YamlFile.Name = "listView_YamlFile";
            this.listView_YamlFile.ScrollButtonSize = ABB.Robotics.Tps.Windows.Forms.ScrollImageSize.Large;
            this.listView_YamlFile.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.listView_YamlFile.ShowSelection = true;
            this.listView_YamlFile.Size = new System.Drawing.Size(211, 254);
            this.listView_YamlFile.Sorting = ABB.Robotics.Tps.Windows.Forms.SortOrder.Ascending;
            this.listView_YamlFile.TabIndex = 0;
            this.listView_YamlFile.TopItemIndex = 0;
            this.listView_YamlFile.View = System.Windows.Forms.View.Details;
            this.listView_YamlFile.SelectedIndexChanged += new System.EventHandler(this.listView_YamlFile_SelectedIndexChanged);
            // 
            // columnHeader_FileName
            // 
            this.columnHeader_FileName.HeaderSorter = null;
            this.columnHeader_FileName.Text = "文件名";
            this.columnHeader_FileName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader_FileName.Width = 200;
            // 
            // textBox_YAML
            // 
            this.textBox_YAML.BackColor = System.Drawing.Color.LightGray;
            this.textBox_YAML.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_YAML.CaretVisible = false;
            this.textBox_YAML.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.textBox_YAML.Location = new System.Drawing.Point(401, 61);
            this.textBox_YAML.Multiline = true;
            this.textBox_YAML.Name = "textBox_YAML";
            this.textBox_YAML.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBox_YAML.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.textBox_YAML.SelectionLength = 0;
            this.textBox_YAML.SelectionStart = 0;
            this.textBox_YAML.SelectionVisible = false;
            this.textBox_YAML.Size = new System.Drawing.Size(207, 34);
            this.textBox_YAML.TabIndex = 3;
            this.textBox_YAML.Text = "";
            this.textBox_YAML.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBox_YAML.WordWrap = true;
            // 
            // tpsLabel1
            // 
            this.tpsLabel1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tpsLabel1.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.tpsLabel1.Location = new System.Drawing.Point(282, 61);
            this.tpsLabel1.Multiline = true;
            this.tpsLabel1.Name = "tpsLabel1";
            this.tpsLabel1.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.tpsLabel1.Size = new System.Drawing.Size(100, 24);
            this.tpsLabel1.TabIndex = 4;
            this.tpsLabel1.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.tpsLabel1.Title = "加载文件：";
            // 
            // button_StartToWeld
            // 
            this.button_StartToWeld.BackColor = System.Drawing.Color.White;
            this.button_StartToWeld.BackgroundImage = null;
            this.button_StartToWeld.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.button_StartToWeld.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.button_StartToWeld.Image = null;
            this.button_StartToWeld.Location = new System.Drawing.Point(401, 130);
            this.button_StartToWeld.Name = "button_StartToWeld";
            this.button_StartToWeld.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.button_StartToWeld.Size = new System.Drawing.Size(97, 47);
            this.button_StartToWeld.TabIndex = 47;
            this.button_StartToWeld.Text = "启动";
            this.button_StartToWeld.TextAlign = ABB.Robotics.Tps.Windows.Forms.ContentAlignmentABB.MiddleCenter;
            this.button_StartToWeld.Click += new System.EventHandler(this.button_StartToWeld_Click);
            // 
            // TpsFormYAML
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.button_StartToWeld);
            this.Controls.Add(this.tpsLabel1);
            this.Controls.Add(this.textBox_YAML);
            this.Controls.Add(this.groupBox1);
            // 
            // 
            // 
            this.MainMenu.MenuItems.Add(this.menuItem_Refresh);
            this.MainMenu.MenuItems.Add(this.menuItem_Apply);
            this.MainMenu.MenuItems.Add(this.menuItem_Close);
            this.Size = new System.Drawing.Size(640, 390);
            this.Text = "数据文件库";
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.textBox_YAML, 0);
            this.Controls.SetChildIndex(this.tpsLabel1, 0);
            this.Controls.SetChildIndex(this.button_StartToWeld, 0);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion


        #region ITpsViewActivation Members

        public void Activate()
        {
            this.templateData.RefreshData(this.rwSystem);
            this.yamlFile.RefreshData(this.rwSystem);
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
                this.listView_YamlFile.Items.Clear();
                for (int i = 0; i < this.yamlFile.YamlFileList.Count; i++)
                {
                    ABB.Robotics.Tps.Windows.Forms.ListViewItem listViewItem = new ABB.Robotics.Tps.Windows.Forms.ListViewItem(this.yamlFile.YamlFileList[i]);
                    this.listView_YamlFile.Items.Add(listViewItem);
                }
                this.listView_YamlFile.Sort();
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
            this.templateData.RefreshData(this.rwSystem);
            this.yamlFile.RefreshData(this.rwSystem);
                      
            this.Invoke(this.UpdateGUI);
        }

        private void dataControl_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.menuItem_Apply.Enabled = true;
        }

        void InitializeTexts()
        {

        }

        private void listView_YamlFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox_YAML.Text = this.listView_YamlFile.Items[this.listView_YamlFile.SelectedIndex].Text;
        }

        private void button_StartToWeld_Click(object sender, EventArgs e)
        {            
            if (!string.IsNullOrEmpty( this.textBox_YAML.Text))
            {
                this.yamlFile.YamlFileName = this.textBox_YAML.Text;
                this.yamlFile.StartToWeld(this.rwSystem);
            }
        }

    }
}