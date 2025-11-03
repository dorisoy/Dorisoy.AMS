namespace Dorisoy.AMS.view
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            toolStripButton1 = new ToolStripButton();
            toolStrip1 = new ToolStrip();
            btnAdd = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnEdit = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            btnImport = new ToolStripButton();
            toolStripSeparator6 = new ToolStripSeparator();
            btnExport = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            btnPrint = new ToolStripButton();
            toolStripSeparator12 = new ToolStripSeparator();
            toolStripSeparator11 = new ToolStripSeparator();
            toolStripButton2 = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripButton3 = new ToolStripButton();
            toolStripSeparator13 = new ToolStripSeparator();
            btnManageNumber = new ToolStripButton();
            toolStripSeparator5 = new ToolStripSeparator();
            menuUserManagement = new ToolStripButton();
            toolStripSeparator8 = new ToolStripSeparator();
            btnManageLog = new ToolStripButton();
            toolStripSeparator10 = new ToolStripSeparator();
            btnManageData = new ToolStripButton();
            toolStripSeparator9 = new ToolStripSeparator();
            btnRegistration = new ToolStripButton();
            toolStripSeparator7 = new ToolStripSeparator();
            btnQuit = new ToolStripButton();
            txtSearch = new TextBox();
            btnSearch = new Button();
            cmbDepartment = new ComboBox();
            label1 = new Label();
            cmbStatus = new ComboBox();
            label2 = new Label();
            dataGridView1 = new DataGridView();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            panel1 = new Panel();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStripButton1
            // 
            toolStripButton1.Image = Properties.Resources.打印;
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(121, 35);
            toolStripButton1.Text = "打印A4";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(24, 24);
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnAdd, toolStripSeparator1, btnEdit, toolStripSeparator2, btnImport, toolStripSeparator6, btnExport, toolStripSeparator4, btnPrint, toolStripSeparator12, toolStripButton1, toolStripSeparator11, toolStripButton2, toolStripSeparator3, toolStripButton3, toolStripSeparator13, btnManageNumber, toolStripSeparator5, menuUserManagement, toolStripSeparator8, btnManageLog, toolStripSeparator10, btnManageData, toolStripSeparator9, btnRegistration, toolStripSeparator7, btnQuit });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0, 0, 5, 0);
            toolStrip1.Size = new Size(2961, 41);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnAdd
            // 
            btnAdd.Image = (Image)resources.GetObject("btnAdd.Image");
            btnAdd.ImageTransparentColor = Color.Magenta;
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(90, 35);
            btnAdd.Text = "添加";
            btnAdd.Click += btnAdd_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 41);
            // 
            // btnEdit
            // 
            btnEdit.Image = (Image)resources.GetObject("btnEdit.Image");
            btnEdit.ImageTransparentColor = Color.Magenta;
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(90, 35);
            btnEdit.Text = "编辑";
            btnEdit.Click += btnEdit_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 41);
            // 
            // btnImport
            // 
            btnImport.Image = (Image)resources.GetObject("btnImport.Image");
            btnImport.ImageTransparentColor = Color.Magenta;
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(90, 35);
            btnImport.Text = "导入";
            btnImport.Click += btnImport_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(6, 41);
            // 
            // btnExport
            // 
            btnExport.Image = (Image)resources.GetObject("btnExport.Image");
            btnExport.ImageTransparentColor = Color.Magenta;
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(90, 35);
            btnExport.Text = "导出";
            btnExport.Click += btnExport_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 41);
            // 
            // btnPrint
            // 
            btnPrint.Image = (Image)resources.GetObject("btnPrint.Image");
            btnPrint.ImageTransparentColor = Color.Magenta;
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(90, 35);
            btnPrint.Text = "打印";
            btnPrint.Click += btnPrint_Click;
            // 
            // toolStripSeparator12
            // 
            toolStripSeparator12.Name = "toolStripSeparator12";
            toolStripSeparator12.Size = new Size(6, 41);
            // 
            // toolStripSeparator11
            // 
            toolStripSeparator11.Name = "toolStripSeparator11";
            toolStripSeparator11.Size = new Size(6, 41);
            // 
            // toolStripButton2
            // 
            toolStripButton2.Image = Properties.Resources.借还;
            toolStripButton2.ImageTransparentColor = Color.Magenta;
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new Size(138, 35);
            toolStripButton2.Text = "借还登记";
            toolStripButton2.Click += toolStripButton2_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 41);
            // 
            // toolStripButton3
            // 
            toolStripButton3.Image = Properties.Resources.记录1;
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(138, 35);
            toolStripButton3.Text = "库存记录";
            toolStripButton3.Click += toolStripButton3_Click;
            // 
            // toolStripSeparator13
            // 
            toolStripSeparator13.Name = "toolStripSeparator13";
            toolStripSeparator13.Size = new Size(6, 41);
            // 
            // btnManageNumber
            // 
            btnManageNumber.Image = (Image)resources.GetObject("btnManageNumber.Image");
            btnManageNumber.ImageTransparentColor = Color.Magenta;
            btnManageNumber.Name = "btnManageNumber";
            btnManageNumber.Size = new Size(138, 35);
            btnManageNumber.Text = "编号设置";
            btnManageNumber.Click += btnManageNumber_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(6, 41);
            // 
            // menuUserManagement
            // 
            menuUserManagement.Image = (Image)resources.GetObject("menuUserManagement.Image");
            menuUserManagement.ImageTransparentColor = Color.Magenta;
            menuUserManagement.Name = "menuUserManagement";
            menuUserManagement.Size = new Size(138, 35);
            menuUserManagement.Text = "用户管理";
            menuUserManagement.Click += menuUserManagement_Click;
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new Size(6, 41);
            // 
            // btnManageLog
            // 
            btnManageLog.Image = (Image)resources.GetObject("btnManageLog.Image");
            btnManageLog.ImageTransparentColor = Color.Magenta;
            btnManageLog.Name = "btnManageLog";
            btnManageLog.Size = new Size(90, 35);
            btnManageLog.Text = "日志";
            btnManageLog.Click += btnManageLog_Click;
            // 
            // toolStripSeparator10
            // 
            toolStripSeparator10.Name = "toolStripSeparator10";
            toolStripSeparator10.Size = new Size(6, 41);
            // 
            // btnManageData
            // 
            btnManageData.Image = (Image)resources.GetObject("btnManageData.Image");
            btnManageData.ImageTransparentColor = Color.Magenta;
            btnManageData.Name = "btnManageData";
            btnManageData.Size = new Size(162, 35);
            btnManageData.Text = "数据库管理";
            btnManageData.Click += btnManageData_Click;
            // 
            // toolStripSeparator9
            // 
            toolStripSeparator9.Name = "toolStripSeparator9";
            toolStripSeparator9.Size = new Size(6, 41);
            // 
            // btnRegistration
            // 
            btnRegistration.Image = (Image)resources.GetObject("btnRegistration.Image");
            btnRegistration.ImageTransparentColor = Color.Magenta;
            btnRegistration.Name = "btnRegistration";
            btnRegistration.Size = new Size(138, 35);
            btnRegistration.Text = "注册信息";
            btnRegistration.Click += btnRegistration_Click;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new Size(6, 41);
            // 
            // btnQuit
            // 
            btnQuit.Image = (Image)resources.GetObject("btnQuit.Image");
            btnQuit.ImageTransparentColor = Color.Magenta;
            btnQuit.Name = "btnQuit";
            btnQuit.Size = new Size(138, 35);
            btnQuit.Text = "退出系统";
            btnQuit.Click += btnQuit_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(7, 10);
            txtSearch.Margin = new Padding(7, 8, 7, 8);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(401, 38);
            txtSearch.TabIndex = 2;
            txtSearch.KeyDown += txtSearch_KeyDown;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(427, 8);
            btnSearch.Margin = new Padding(7, 8, 7, 8);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(175, 59);
            btnSearch.TabIndex = 4;
            btnSearch.Text = "搜索";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // cmbDepartment
            // 
            cmbDepartment.FormattingEnabled = true;
            cmbDepartment.Items.AddRange(new object[] { "全部部门", "行政部", "财务部", "研发中心" });
            cmbDepartment.Location = new Point(754, 10);
            cmbDepartment.Margin = new Padding(7, 8, 7, 8);
            cmbDepartment.Name = "cmbDepartment";
            cmbDepartment.Size = new Size(277, 39);
            cmbDepartment.TabIndex = 7;
            cmbDepartment.SelectedIndexChanged += cmbDepartment_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(616, 21);
            label1.Margin = new Padding(7, 0, 7, 0);
            label1.Name = "label1";
            label1.Size = new Size(110, 31);
            label1.TabIndex = 8;
            label1.Text = "选择部门";
            // 
            // cmbStatus
            // 
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(1258, 10);
            cmbStatus.Margin = new Padding(7, 8, 7, 8);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(277, 39);
            cmbStatus.TabIndex = 9;
            cmbStatus.SelectedIndexChanged += cmbStatus_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1083, 21);
            label2.Margin = new Padding(7, 0, 7, 0);
            label2.Name = "label2";
            label2.Size = new Size(110, 31);
            label2.TabIndex = 10;
            label2.Text = "资产状态";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(7, 8);
            dataGridView1.Margin = new Padding(7, 8, 7, 8);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.RowTemplate.Height = 23;
            dataGridView1.Size = new Size(2947, 1669);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(dataGridView1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 126);
            tableLayoutPanel1.Margin = new Padding(7, 8, 7, 8);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 1646F));
            tableLayoutPanel1.Size = new Size(2961, 1685);
            tableLayoutPanel1.TabIndex = 11;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 47F));
            tableLayoutPanel2.Controls.Add(panel1, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(0, 41);
            tableLayoutPanel2.Margin = new Padding(7, 8, 7, 8);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tableLayoutPanel2.Size = new Size(2961, 85);
            tableLayoutPanel2.TabIndex = 12;
            // 
            // panel1
            // 
            panel1.Controls.Add(txtSearch);
            panel1.Controls.Add(cmbStatus);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(btnSearch);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(cmbDepartment);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(7, 8);
            panel1.Margin = new Padding(7, 8, 7, 8);
            panel1.Name = "panel1";
            panel1.Size = new Size(2947, 69);
            panel1.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2961, 1811);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(toolStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(7, 8, 7, 8);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Dorisoy.AMS 设备管理系统";
            WindowState = FormWindowState.Maximized;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ToolStripButton btnExport;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnManageNumber;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton menuUserManagement;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btnQuit;
        private System.Windows.Forms.ToolStripButton btnManageLog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton btnRegistration;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton btnManageData;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;
        private ToolStripSeparator toolStripSeparator12;
        private ToolStripSeparator toolStripSeparator13;
    }
}