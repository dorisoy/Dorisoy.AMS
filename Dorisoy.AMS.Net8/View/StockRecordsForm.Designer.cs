namespace Dorisoy.AMS.view
{
    partial class StockRecordsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            toolStrip1 = new ToolStrip();
            btnExport = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnClose = new ToolStripButton();
            panel1 = new Panel();
            btnSearch = new Button();
            txtSearch = new TextBox();
            label5 = new Label();
            cmbWarehouse = new ComboBox();
            label4 = new Label();
            cmbRecordType = new ComboBox();
            label3 = new Label();
            dtEnd = new DateTimePicker();
            label2 = new Label();
            dtStart = new DateTimePicker();
            label1 = new Label();
            dataGridView1 = new DataGridView();
            statusStrip1 = new StatusStrip();
            lblTotal = new ToolStripStatusLabel();
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(24, 24);
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnExport, toolStripSeparator1, btnClose });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1100, 31);
            toolStrip1.TabIndex = 0;
            // 
            // btnExport
            // 
            btnExport.ImageTransparentColor = Color.Magenta;
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(60, 28);
            btnExport.Text = "导出Excel";
            btnExport.Click += btnExport_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 31);
            // 
            // btnClose
            // 
            btnClose.ImageTransparentColor = Color.Magenta;
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(36, 28);
            btnClose.Text = "关闭";
            btnClose.Click += btnClose_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnSearch);
            panel1.Controls.Add(txtSearch);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(cmbWarehouse);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(cmbRecordType);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(dtEnd);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(dtStart);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 31);
            panel1.Name = "panel1";
            panel1.Size = new Size(1100, 50);
            panel1.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(1005, 12);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 28);
            btnSearch.TabIndex = 10;
            btnSearch.Text = "搜索";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(826, 14);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(170, 23);
            txtSearch.TabIndex = 9;
            txtSearch.KeyDown += txtSearch_KeyDown;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(764, 17);
            label5.Name = "label5";
            label5.Size = new Size(56, 17);
            label5.TabIndex = 8;
            label5.Text = "关键字：";
            // 
            // cmbWarehouse
            // 
            cmbWarehouse.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbWarehouse.FormattingEnabled = true;
            cmbWarehouse.Location = new Point(630, 13);
            cmbWarehouse.Name = "cmbWarehouse";
            cmbWarehouse.Size = new Size(120, 25);
            cmbWarehouse.TabIndex = 7;
            cmbWarehouse.SelectedIndexChanged += cmbWarehouse_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(580, 17);
            label4.Name = "label4";
            label4.Size = new Size(44, 17);
            label4.TabIndex = 6;
            label4.Text = "仓库：";
            // 
            // cmbRecordType
            // 
            cmbRecordType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRecordType.FormattingEnabled = true;
            cmbRecordType.Location = new Point(480, 13);
            cmbRecordType.Name = "cmbRecordType";
            cmbRecordType.Size = new Size(90, 25);
            cmbRecordType.TabIndex = 5;
            cmbRecordType.SelectedIndexChanged += cmbRecordType_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(430, 17);
            label3.Name = "label3";
            label3.Size = new Size(44, 17);
            label3.TabIndex = 4;
            label3.Text = "类型：";
            // 
            // dtEnd
            // 
            dtEnd.Format = DateTimePickerFormat.Short;
            dtEnd.Location = new Point(310, 13);
            dtEnd.Name = "dtEnd";
            dtEnd.Size = new Size(110, 23);
            dtEnd.TabIndex = 3;
            dtEnd.ValueChanged += dtEnd_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(278, 17);
            label2.Name = "label2";
            label2.Size = new Size(20, 17);
            label2.TabIndex = 2;
            label2.Text = "至";
            // 
            // dtStart
            // 
            dtStart.Format = DateTimePickerFormat.Short;
            dtStart.Location = new Point(160, 13);
            dtStart.Name = "dtStart";
            dtStart.Size = new Size(110, 23);
            dtStart.TabIndex = 1;
            dtStart.ValueChanged += dtStart_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 17);
            label1.Name = "label1";
            label1.Size = new Size(68, 17);
            label1.TabIndex = 0;
            label1.Text = "日期范围：";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 81);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(1100, 467);
            dataGridView1.TabIndex = 2;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblTotal });
            statusStrip1.Location = new Point(0, 548);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1100, 22);
            statusStrip1.TabIndex = 3;
            // 
            // lblTotal
            // 
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(167, 17);
            lblTotal.Text = "共 0 条记录 | 入库合计：0 | 出库合计：0";
            // 
            // StockRecordsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 570);
            Controls.Add(dataGridView1);
            Controls.Add(statusStrip1);
            Controls.Add(panel1);
            Controls.Add(toolStrip1);
            Name = "StockRecordsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "库存出入库记录";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton btnExport;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnClose;
        private Panel panel1;
        private Button btnSearch;
        private TextBox txtSearch;
        private Label label5;
        private ComboBox cmbWarehouse;
        private Label label4;
        private ComboBox cmbRecordType;
        private Label label3;
        private DateTimePicker dtEnd;
        private Label label2;
        private DateTimePicker dtStart;
        private Label label1;
        private DataGridView dataGridView1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblTotal;
    }
}
