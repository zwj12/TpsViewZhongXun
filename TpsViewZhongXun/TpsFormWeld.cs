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
    public class TpsFormWeld : TpsForm, ITpsViewActivation
    {
        private TemplateData templateData = new TemplateData();


        private ABB.Robotics.Tps.Windows.Forms.MenuItem menuItem_Refresh;
        private ABB.Robotics.Tps.Windows.Forms.MenuItem menuItem_Apply;
        private ABB.Robotics.Tps.Windows.Forms.MenuItem menuItem_Close;

        private TpsResourceManager _tpsRm = null;
        private RWSystem rwSystem = null;
        private GroupBox groupBox1;
        private ABB.Robotics.Tps.Windows.Forms.ListBox listBox_Template;
        private GroupBox groupBox2;
        private ABB.Robotics.Tps.Windows.Forms.ListBox listBox_WeldTemplate;
        private ABB.Robotics.Tps.Windows.Forms.Button button_Add;
        private ABB.Robotics.Tps.Windows.Forms.Button button_Remove;

        private ModelData modelData = new ModelData();
        private NumEditor numEditor_TemplateZ;
        private NumEditor numEditor_TemplateY;
        private NumEditor numEditor_TemplateX;
        private TpsLabel tpsLabel_numZ;
        private TpsLabel tpsLabel_numY;
        private TpsLabel tpsLabel_numX;
        private TpsLabel tpsLabel_Template;
        private NumEditor numEditor_TemplateOffsetZ;
        private NumEditor numEditor_TemplateOffsetY;
        private NumEditor numEditor_TemplateOffsetX;
        private TpsLabel tpsLabel1;
        private ABB.Robotics.Tps.Windows.Forms.Button button_StartToWeld;
        private TpsLabel tpsLabel2;
        private NumEditor numEditor_TemplateStart;
        private NumEditor numEditor_TemplateQuantity;
        private TpsLabel tpsLabel3;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        public TpsFormWeld(TpsResourceManager rM, RWSystem rwSystem)
        {
            InitializeComponent();

            try
            {
                this._tpsRm = rM;
                this.rwSystem = rwSystem;
                this.InitializeTexts();
            }
            catch (System.Exception ex)
            {
                // If initialization of application fails a message box is shown
                GTPUMessageBox.Show(this.Parent
                    , null
                    , string.Format("An unexpected error occurred while starting up Weld Application. \n\n{0}", ex.Message)
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
            this.listBox_Template = new ABB.Robotics.Tps.Windows.Forms.ListBox();
            this.groupBox2 = new ABB.Robotics.Tps.Windows.Forms.GroupBox();
            this.listBox_WeldTemplate = new ABB.Robotics.Tps.Windows.Forms.ListBox();
            this.button_Add = new ABB.Robotics.Tps.Windows.Forms.Button();
            this.button_Remove = new ABB.Robotics.Tps.Windows.Forms.Button();
            this.numEditor_TemplateZ = new ABB.Robotics.Tps.Windows.Forms.NumEditor();
            this.numEditor_TemplateY = new ABB.Robotics.Tps.Windows.Forms.NumEditor();
            this.numEditor_TemplateX = new ABB.Robotics.Tps.Windows.Forms.NumEditor();
            this.tpsLabel_numZ = new ABB.Robotics.Tps.Windows.Forms.TpsLabel();
            this.tpsLabel_numY = new ABB.Robotics.Tps.Windows.Forms.TpsLabel();
            this.tpsLabel_numX = new ABB.Robotics.Tps.Windows.Forms.TpsLabel();
            this.tpsLabel_Template = new ABB.Robotics.Tps.Windows.Forms.TpsLabel();
            this.numEditor_TemplateOffsetZ = new ABB.Robotics.Tps.Windows.Forms.NumEditor();
            this.numEditor_TemplateOffsetY = new ABB.Robotics.Tps.Windows.Forms.NumEditor();
            this.numEditor_TemplateOffsetX = new ABB.Robotics.Tps.Windows.Forms.NumEditor();
            this.tpsLabel1 = new ABB.Robotics.Tps.Windows.Forms.TpsLabel();
            this.button_StartToWeld = new ABB.Robotics.Tps.Windows.Forms.Button();
            this.tpsLabel2 = new ABB.Robotics.Tps.Windows.Forms.TpsLabel();
            this.numEditor_TemplateStart = new ABB.Robotics.Tps.Windows.Forms.NumEditor();
            this.numEditor_TemplateQuantity = new ABB.Robotics.Tps.Windows.Forms.NumEditor();
            this.tpsLabel3 = new ABB.Robotics.Tps.Windows.Forms.TpsLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.groupBox1.Controls.Add(this.listBox_Template);
            this.groupBox1.HorizontalAlign = ABB.Robotics.Tps.Windows.Forms.HorAlign.Left;
            this.groupBox1.LineStyle = ABB.Robotics.Tps.Windows.Forms.LineStyle.SingleLine;
            this.groupBox1.Location = new System.Drawing.Point(11, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 310);
            this.groupBox1.Title = "模板库";
            this.groupBox1.VerticalAlign = ABB.Robotics.Tps.Windows.Forms.VerAlign.Center;
            // 
            // listBox_Template
            // 
            this.listBox_Template.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox_Template.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.listBox_Template.ListViewItemFont = 12F;
            this.listBox_Template.listviewLoactionWidth = 0;
            this.listBox_Template.Location = new System.Drawing.Point(20, 23);
            this.listBox_Template.Name = "listBox_Template";
            this.listBox_Template.ScrollButtonSize = ABB.Robotics.Tps.Windows.Forms.ScrollImageSize.Large;
            this.listBox_Template.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.listBox_Template.ShowSelection = true;
            this.listBox_Template.Size = new System.Drawing.Size(180, 271);
            this.listBox_Template.Sorting = ABB.Robotics.Tps.Windows.Forms.SortOrder.Ascending;
            this.listBox_Template.TabIndex = 1;
            this.listBox_Template.TopItemIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBox_WeldTemplate);
            this.groupBox2.HorizontalAlign = ABB.Robotics.Tps.Windows.Forms.HorAlign.Left;
            this.groupBox2.LineStyle = ABB.Robotics.Tps.Windows.Forms.LineStyle.SingleLine;
            this.groupBox2.Location = new System.Drawing.Point(410, 31);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(220, 173);
            this.groupBox2.Title = "焊接模板";
            this.groupBox2.VerticalAlign = ABB.Robotics.Tps.Windows.Forms.VerAlign.Center;
            // 
            // listBox_WeldTemplate
            // 
            this.listBox_WeldTemplate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox_WeldTemplate.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.listBox_WeldTemplate.ListViewItemFont = 12F;
            this.listBox_WeldTemplate.listviewLoactionWidth = 0;
            this.listBox_WeldTemplate.Location = new System.Drawing.Point(21, 23);
            this.listBox_WeldTemplate.Name = "listBox_WeldTemplate";
            this.listBox_WeldTemplate.ScrollButtonSize = ABB.Robotics.Tps.Windows.Forms.ScrollImageSize.Large;
            this.listBox_WeldTemplate.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.listBox_WeldTemplate.ShowSelection = true;
            this.listBox_WeldTemplate.Size = new System.Drawing.Size(180, 141);
            this.listBox_WeldTemplate.Sorting = ABB.Robotics.Tps.Windows.Forms.SortOrder.Ascending;
            this.listBox_WeldTemplate.TabIndex = 1;
            this.listBox_WeldTemplate.TopItemIndex = 0;
            // 
            // button_Add
            // 
            this.button_Add.BackColor = System.Drawing.Color.White;
            this.button_Add.BackgroundImage = null;
            this.button_Add.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.button_Add.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.button_Add.Image = null;
            this.button_Add.Location = new System.Drawing.Point(274, 40);
            this.button_Add.Name = "button_Add";
            this.button_Add.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.button_Add.Size = new System.Drawing.Size(97, 47);
            this.button_Add.TabIndex = 4;
            this.button_Add.Text = "->";
            this.button_Add.TextAlign = ABB.Robotics.Tps.Windows.Forms.ContentAlignmentABB.MiddleCenter;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // button_Remove
            // 
            this.button_Remove.BackColor = System.Drawing.Color.White;
            this.button_Remove.BackgroundImage = null;
            this.button_Remove.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.button_Remove.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.button_Remove.Image = null;
            this.button_Remove.Location = new System.Drawing.Point(274, 95);
            this.button_Remove.Name = "button_Remove";
            this.button_Remove.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.button_Remove.Size = new System.Drawing.Size(97, 47);
            this.button_Remove.TabIndex = 5;
            this.button_Remove.Text = "<-";
            this.button_Remove.TextAlign = ABB.Robotics.Tps.Windows.Forms.ContentAlignmentABB.MiddleCenter;
            this.button_Remove.Click += new System.EventHandler(this.button_Remove_Click);
            // 
            // numEditor_TemplateZ
            // 
            this.numEditor_TemplateZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numEditor_TemplateZ.CaretVisible = false;
            this.numEditor_TemplateZ.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.numEditor_TemplateZ.Location = new System.Drawing.Point(550, 240);
            this.numEditor_TemplateZ.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.numEditor_TemplateZ.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.numEditor_TemplateZ.Multiline = true;
            this.numEditor_TemplateZ.Name = "numEditor_TemplateZ";
            this.numEditor_TemplateZ.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.numEditor_TemplateZ.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.numEditor_TemplateZ.SelectionLength = 0;
            this.numEditor_TemplateZ.SelectionStart = 0;
            this.numEditor_TemplateZ.SelectionVisible = false;
            this.numEditor_TemplateZ.Size = new System.Drawing.Size(80, 24);
            this.numEditor_TemplateZ.TabIndex = 39;
            this.numEditor_TemplateZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // numEditor_TemplateY
            // 
            this.numEditor_TemplateY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numEditor_TemplateY.CaretVisible = false;
            this.numEditor_TemplateY.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.numEditor_TemplateY.Location = new System.Drawing.Point(451, 240);
            this.numEditor_TemplateY.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.numEditor_TemplateY.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.numEditor_TemplateY.Multiline = true;
            this.numEditor_TemplateY.Name = "numEditor_TemplateY";
            this.numEditor_TemplateY.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.numEditor_TemplateY.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.numEditor_TemplateY.SelectionLength = 0;
            this.numEditor_TemplateY.SelectionStart = 0;
            this.numEditor_TemplateY.SelectionVisible = false;
            this.numEditor_TemplateY.Size = new System.Drawing.Size(80, 24);
            this.numEditor_TemplateY.TabIndex = 41;
            this.numEditor_TemplateY.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // numEditor_TemplateX
            // 
            this.numEditor_TemplateX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numEditor_TemplateX.CaretVisible = false;
            this.numEditor_TemplateX.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.numEditor_TemplateX.Location = new System.Drawing.Point(352, 240);
            this.numEditor_TemplateX.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.numEditor_TemplateX.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.numEditor_TemplateX.Multiline = true;
            this.numEditor_TemplateX.Name = "numEditor_TemplateX";
            this.numEditor_TemplateX.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.numEditor_TemplateX.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.numEditor_TemplateX.SelectionLength = 0;
            this.numEditor_TemplateX.SelectionStart = 0;
            this.numEditor_TemplateX.SelectionVisible = false;
            this.numEditor_TemplateX.Size = new System.Drawing.Size(80, 24);
            this.numEditor_TemplateX.TabIndex = 40;
            this.numEditor_TemplateX.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // tpsLabel_numZ
            // 
            this.tpsLabel_numZ.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tpsLabel_numZ.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.tpsLabel_numZ.Location = new System.Drawing.Point(550, 210);
            this.tpsLabel_numZ.Multiline = true;
            this.tpsLabel_numZ.Name = "tpsLabel_numZ";
            this.tpsLabel_numZ.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.tpsLabel_numZ.Size = new System.Drawing.Size(48, 24);
            this.tpsLabel_numZ.TabIndex = 38;
            this.tpsLabel_numZ.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.tpsLabel_numZ.Title = "z";
            // 
            // tpsLabel_numY
            // 
            this.tpsLabel_numY.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tpsLabel_numY.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.tpsLabel_numY.Location = new System.Drawing.Point(451, 210);
            this.tpsLabel_numY.Multiline = true;
            this.tpsLabel_numY.Name = "tpsLabel_numY";
            this.tpsLabel_numY.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.tpsLabel_numY.Size = new System.Drawing.Size(54, 24);
            this.tpsLabel_numY.TabIndex = 36;
            this.tpsLabel_numY.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.tpsLabel_numY.Title = "y";
            // 
            // tpsLabel_numX
            // 
            this.tpsLabel_numX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tpsLabel_numX.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.tpsLabel_numX.Location = new System.Drawing.Point(352, 210);
            this.tpsLabel_numX.Multiline = true;
            this.tpsLabel_numX.Name = "tpsLabel_numX";
            this.tpsLabel_numX.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.tpsLabel_numX.Size = new System.Drawing.Size(36, 24);
            this.tpsLabel_numX.TabIndex = 37;
            this.tpsLabel_numX.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.tpsLabel_numX.Title = "x";
            // 
            // tpsLabel_Template
            // 
            this.tpsLabel_Template.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tpsLabel_Template.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.tpsLabel_Template.Location = new System.Drawing.Point(244, 240);
            this.tpsLabel_Template.Multiline = true;
            this.tpsLabel_Template.Name = "tpsLabel_Template";
            this.tpsLabel_Template.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.tpsLabel_Template.Size = new System.Drawing.Size(102, 24);
            this.tpsLabel_Template.TabIndex = 35;
            this.tpsLabel_Template.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.tpsLabel_Template.Title = "模板位置";
            // 
            // numEditor_TemplateOffsetZ
            // 
            this.numEditor_TemplateOffsetZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numEditor_TemplateOffsetZ.CaretVisible = false;
            this.numEditor_TemplateOffsetZ.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.numEditor_TemplateOffsetZ.Location = new System.Drawing.Point(550, 273);
            this.numEditor_TemplateOffsetZ.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.numEditor_TemplateOffsetZ.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.numEditor_TemplateOffsetZ.Multiline = true;
            this.numEditor_TemplateOffsetZ.Name = "numEditor_TemplateOffsetZ";
            this.numEditor_TemplateOffsetZ.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.numEditor_TemplateOffsetZ.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.numEditor_TemplateOffsetZ.SelectionLength = 0;
            this.numEditor_TemplateOffsetZ.SelectionStart = 0;
            this.numEditor_TemplateOffsetZ.SelectionVisible = false;
            this.numEditor_TemplateOffsetZ.Size = new System.Drawing.Size(80, 24);
            this.numEditor_TemplateOffsetZ.TabIndex = 43;
            this.numEditor_TemplateOffsetZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // numEditor_TemplateOffsetY
            // 
            this.numEditor_TemplateOffsetY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numEditor_TemplateOffsetY.CaretVisible = false;
            this.numEditor_TemplateOffsetY.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.numEditor_TemplateOffsetY.Location = new System.Drawing.Point(451, 273);
            this.numEditor_TemplateOffsetY.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.numEditor_TemplateOffsetY.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.numEditor_TemplateOffsetY.Multiline = true;
            this.numEditor_TemplateOffsetY.Name = "numEditor_TemplateOffsetY";
            this.numEditor_TemplateOffsetY.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.numEditor_TemplateOffsetY.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.numEditor_TemplateOffsetY.SelectionLength = 0;
            this.numEditor_TemplateOffsetY.SelectionStart = 0;
            this.numEditor_TemplateOffsetY.SelectionVisible = false;
            this.numEditor_TemplateOffsetY.Size = new System.Drawing.Size(80, 24);
            this.numEditor_TemplateOffsetY.TabIndex = 45;
            this.numEditor_TemplateOffsetY.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // numEditor_TemplateOffsetX
            // 
            this.numEditor_TemplateOffsetX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numEditor_TemplateOffsetX.CaretVisible = false;
            this.numEditor_TemplateOffsetX.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.numEditor_TemplateOffsetX.Location = new System.Drawing.Point(352, 273);
            this.numEditor_TemplateOffsetX.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.numEditor_TemplateOffsetX.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.numEditor_TemplateOffsetX.Multiline = true;
            this.numEditor_TemplateOffsetX.Name = "numEditor_TemplateOffsetX";
            this.numEditor_TemplateOffsetX.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.numEditor_TemplateOffsetX.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.numEditor_TemplateOffsetX.SelectionLength = 0;
            this.numEditor_TemplateOffsetX.SelectionStart = 0;
            this.numEditor_TemplateOffsetX.SelectionVisible = false;
            this.numEditor_TemplateOffsetX.Size = new System.Drawing.Size(80, 24);
            this.numEditor_TemplateOffsetX.TabIndex = 44;
            this.numEditor_TemplateOffsetX.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // tpsLabel1
            // 
            this.tpsLabel1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tpsLabel1.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.tpsLabel1.Location = new System.Drawing.Point(244, 273);
            this.tpsLabel1.Multiline = true;
            this.tpsLabel1.Name = "tpsLabel1";
            this.tpsLabel1.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.tpsLabel1.Size = new System.Drawing.Size(102, 24);
            this.tpsLabel1.TabIndex = 42;
            this.tpsLabel1.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.tpsLabel1.Title = "模块间隔";
            // 
            // button_StartToWeld
            // 
            this.button_StartToWeld.BackColor = System.Drawing.Color.White;
            this.button_StartToWeld.BackgroundImage = null;
            this.button_StartToWeld.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.button_StartToWeld.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.button_StartToWeld.Image = null;
            this.button_StartToWeld.Location = new System.Drawing.Point(274, 151);
            this.button_StartToWeld.Name = "button_StartToWeld";
            this.button_StartToWeld.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.button_StartToWeld.Size = new System.Drawing.Size(97, 47);
            this.button_StartToWeld.TabIndex = 46;
            this.button_StartToWeld.Text = "启动";
            this.button_StartToWeld.TextAlign = ABB.Robotics.Tps.Windows.Forms.ContentAlignmentABB.MiddleCenter;
            // 
            // tpsLabel2
            // 
            this.tpsLabel2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tpsLabel2.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.tpsLabel2.Location = new System.Drawing.Point(244, 306);
            this.tpsLabel2.Multiline = true;
            this.tpsLabel2.Name = "tpsLabel2";
            this.tpsLabel2.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.tpsLabel2.Size = new System.Drawing.Size(102, 24);
            this.tpsLabel2.TabIndex = 42;
            this.tpsLabel2.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.tpsLabel2.Title = "模块索引";
            // 
            // numEditor_TemplateStart
            // 
            this.numEditor_TemplateStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numEditor_TemplateStart.CaretVisible = false;
            this.numEditor_TemplateStart.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.numEditor_TemplateStart.Location = new System.Drawing.Point(352, 306);
            this.numEditor_TemplateStart.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.numEditor_TemplateStart.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numEditor_TemplateStart.Multiline = true;
            this.numEditor_TemplateStart.Name = "numEditor_TemplateStart";
            this.numEditor_TemplateStart.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.numEditor_TemplateStart.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.numEditor_TemplateStart.SelectionLength = 0;
            this.numEditor_TemplateStart.SelectionStart = 0;
            this.numEditor_TemplateStart.SelectionVisible = false;
            this.numEditor_TemplateStart.ShowDecimalPoint = false;
            this.numEditor_TemplateStart.Size = new System.Drawing.Size(80, 24);
            this.numEditor_TemplateStart.TabIndex = 44;
            this.numEditor_TemplateStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.numEditor_TemplateStart.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numEditor_TemplateQuantity
            // 
            this.numEditor_TemplateQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numEditor_TemplateQuantity.CaretVisible = false;
            this.numEditor_TemplateQuantity.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.numEditor_TemplateQuantity.Location = new System.Drawing.Point(550, 306);
            this.numEditor_TemplateQuantity.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.numEditor_TemplateQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numEditor_TemplateQuantity.Multiline = true;
            this.numEditor_TemplateQuantity.Name = "numEditor_TemplateQuantity";
            this.numEditor_TemplateQuantity.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.numEditor_TemplateQuantity.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.numEditor_TemplateQuantity.SelectionLength = 0;
            this.numEditor_TemplateQuantity.SelectionStart = 0;
            this.numEditor_TemplateQuantity.SelectionVisible = false;
            this.numEditor_TemplateQuantity.ShowDecimalPoint = false;
            this.numEditor_TemplateQuantity.Size = new System.Drawing.Size(80, 24);
            this.numEditor_TemplateQuantity.TabIndex = 44;
            this.numEditor_TemplateQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.numEditor_TemplateQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tpsLabel3
            // 
            this.tpsLabel3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tpsLabel3.Font = ABB.Robotics.Tps.Windows.Forms.TpsFont.Font12b;
            this.tpsLabel3.Location = new System.Drawing.Point(451, 306);
            this.tpsLabel3.Multiline = true;
            this.tpsLabel3.Name = "tpsLabel3";
            this.tpsLabel3.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(182)))));
            this.tpsLabel3.Size = new System.Drawing.Size(80, 24);
            this.tpsLabel3.TabIndex = 42;
            this.tpsLabel3.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.tpsLabel3.Title = "~";
            // 
            // TpsFormWeld
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.button_StartToWeld);
            this.Controls.Add(this.numEditor_TemplateOffsetZ);
            this.Controls.Add(this.numEditor_TemplateOffsetY);
            this.Controls.Add(this.numEditor_TemplateQuantity);
            this.Controls.Add(this.numEditor_TemplateStart);
            this.Controls.Add(this.tpsLabel3);
            this.Controls.Add(this.tpsLabel2);
            this.Controls.Add(this.numEditor_TemplateOffsetX);
            this.Controls.Add(this.tpsLabel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button_Remove);
            this.Controls.Add(this.numEditor_TemplateZ);
            this.Controls.Add(this.tpsLabel_numZ);
            this.Controls.Add(this.tpsLabel_numY);
            this.Controls.Add(this.numEditor_TemplateY);
            this.Controls.Add(this.numEditor_TemplateX);
            this.Controls.Add(this.tpsLabel_numX);
            this.Controls.Add(this.tpsLabel_Template);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_Add);
            // 
            // 
            // 
            this.MainMenu.MenuItems.Add(this.menuItem_Refresh);
            this.MainMenu.MenuItems.Add(this.menuItem_Apply);
            this.MainMenu.MenuItems.Add(this.menuItem_Close);
            this.Size = new System.Drawing.Size(640, 390);
            this.Text = "焊接模板设置";
            this.Controls.SetChildIndex(this.button_Add, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.tpsLabel_Template, 0);
            this.Controls.SetChildIndex(this.tpsLabel_numX, 0);
            this.Controls.SetChildIndex(this.numEditor_TemplateX, 0);
            this.Controls.SetChildIndex(this.numEditor_TemplateY, 0);
            this.Controls.SetChildIndex(this.tpsLabel_numY, 0);
            this.Controls.SetChildIndex(this.tpsLabel_numZ, 0);
            this.Controls.SetChildIndex(this.numEditor_TemplateZ, 0);
            this.Controls.SetChildIndex(this.button_Remove, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.tpsLabel1, 0);
            this.Controls.SetChildIndex(this.numEditor_TemplateOffsetX, 0);
            this.Controls.SetChildIndex(this.tpsLabel2, 0);
            this.Controls.SetChildIndex(this.tpsLabel3, 0);
            this.Controls.SetChildIndex(this.numEditor_TemplateStart, 0);
            this.Controls.SetChildIndex(this.numEditor_TemplateQuantity, 0);
            this.Controls.SetChildIndex(this.numEditor_TemplateOffsetY, 0);
            this.Controls.SetChildIndex(this.numEditor_TemplateOffsetZ, 0);
            this.Controls.SetChildIndex(this.button_StartToWeld, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
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
                this.listBox_Template.Items.Clear();
                this.listBox_WeldTemplate.Items.Clear();
                templateData.RefreshData(this.rwSystem);

                foreach (string item in templateData.TemplateList)
                {
                    if (!String.IsNullOrEmpty(item))
                    {
                        ABB.Robotics.Tps.Windows.Forms.ListBoxItem listViewItem = new ABB.Robotics.Tps.Windows.Forms.ListBoxItem(item);
                        this.listBox_Template.Items.Add(listViewItem);
                    }
                }

                foreach (string item in templateData.WeldTemplateList)
                {
                    if (!String.IsNullOrEmpty(item))
                    {
                        ABB.Robotics.Tps.Windows.Forms.ListBoxItem listViewItem = new ABB.Robotics.Tps.Windows.Forms.ListBoxItem(item);
                        this.listBox_WeldTemplate.Items.Add(listViewItem);
                    }
                }

                this.numEditor_TemplateX.Value =(decimal) this.templateData.Location.X;
                this.numEditor_TemplateY.Value = (decimal)this.templateData.Location.Y;
                this.numEditor_TemplateZ.Value = (decimal)this.templateData.Location.Z;

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
                    , string.Format("An unexpected error occurred when applying RAPID data 'weld data'. Message {0}", ex.ToString())
                    , "System Error"
                    , System.Windows.Forms.MessageBoxIcon.Hand
                    , System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        private void menuItem_Refresh_Click(object sender, EventArgs e)
        {
            this.Invoke(this.UpdateGUI);
        }

        private void MessageBoxEvent(Object sender, MessageBoxEventArgs e)
        {
        }

        private void dataControl_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.menuItem_Apply.Enabled = true;
        }

        void InitializeTexts()
        {
          
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            if (this.listBox_Template.SelectedItem!=null)
            {
                string strTemplate = this.listBox_Template.SelectedItem.Text;
                this.templateData.WeldTemplateList.Add(strTemplate);
                this.Invoke(this.UpdateGUI);
            }

            
        }

        private void button_Remove_Click(object sender, EventArgs e)
        {
            if (this.listBox_WeldTemplate.SelectedItem != null)
            {
                string strTemplate = this.listBox_WeldTemplate.SelectedItem.Text;
                this.templateData.WeldTemplateList.Remove(strTemplate);
                this.Invoke(this.UpdateGUI);
            }

        }

    }


}