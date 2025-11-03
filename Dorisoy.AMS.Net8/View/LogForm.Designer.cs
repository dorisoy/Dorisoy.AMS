namespace Dorisoy.AMS.view
{
    partial class LogForm
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
            panel1 = new Panel();
            btnLogExport = new Button();
            btnSearch = new Button();
            dtpEnd = new DateTimePicker();
            dtpStart = new DateTimePicker();
            cmbOperationType = new ComboBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            panel2 = new Panel();
            dgvLogs = new DataGridView();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnLogExport);
            panel1.Controls.Add(btnSearch);
            panel1.Controls.Add(dtpEnd);
            panel1.Controls.Add(dtpStart);
            panel1.Controls.Add(cmbOperationType);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(15, 18);
            panel1.Margin = new Padding(4, 4, 4, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1017, 54);
            panel1.TabIndex = 0;
            // 
            // btnLogExport
            // 
            btnLogExport.Location = new Point(914, 9);
            btnLogExport.Margin = new Padding(4, 4, 4, 4);
            btnLogExport.Name = "btnLogExport";
            btnLogExport.Size = new Size(88, 33);
            btnLogExport.TabIndex = 7;
            btnLogExport.Text = "导出";
            btnLogExport.UseVisualStyleBackColor = true;
            btnLogExport.Click += btnLogExport_Click;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(818, 9);
            btnSearch.Margin = new Padding(4, 4, 4, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(88, 33);
            btnSearch.TabIndex = 6;
            btnSearch.Text = "查询";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // dtpEnd
            // 
            dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm";
            dtpEnd.Location = new Point(579, 12);
            dtpEnd.Margin = new Padding(4, 4, 4, 4);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(173, 23);
            dtpEnd.TabIndex = 5;
            // 
            // dtpStart
            // 
            dtpStart.CustomFormat = "yyyy-MM-dd HH:mm";
            dtpStart.Location = new Point(311, 12);
            dtpStart.Margin = new Padding(4, 4, 4, 4);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(177, 23);
            dtpStart.TabIndex = 4;
            // 
            // cmbOperationType
            // 
            cmbOperationType.FormattingEnabled = true;
            cmbOperationType.Location = new Point(88, 14);
            cmbOperationType.Margin = new Padding(4, 4, 4, 4);
            cmbOperationType.Name = "cmbOperationType";
            cmbOperationType.Size = new Size(122, 25);
            cmbOperationType.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(496, 17);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(68, 17);
            label3.TabIndex = 2;
            label3.Text = "结束时间：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(243, 17);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(68, 17);
            label2.TabIndex = 1;
            label2.Text = "开始时间：";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 17);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(68, 17);
            label1.TabIndex = 0;
            label1.Text = "操作类型：";
            // 
            // panel2
            // 
            panel2.Controls.Add(dgvLogs);
            panel2.Location = new Point(15, 82);
            panel2.Margin = new Padding(4, 4, 4, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(1017, 653);
            panel2.TabIndex = 1;
            // 
            // dgvLogs
            // 
            dgvLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLogs.Location = new Point(14, 20);
            dgvLogs.Margin = new Padding(4, 4, 4, 4);
            dgvLogs.Name = "dgvLogs";
            dgvLogs.RowTemplate.Height = 23;
            dgvLogs.Size = new Size(988, 629);
            dgvLogs.TabIndex = 0;
            // 
            // LogForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1046, 752);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            Name = "LogForm";
            Text = "日志查询";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLogs).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbOperationType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.Button btnLogExport;
    }
}