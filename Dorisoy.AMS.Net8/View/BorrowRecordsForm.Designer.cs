namespace Dorisoy.AMS.view
{
    partial class BorrowRecordsForm
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

        private void InitializeComponent()
        {
            panel1 = new Panel();
            btnAdd = new Button();
            btnReturn = new Button();
            btnClose = new Button();
            splitContainer1 = new SplitContainer();
            groupBox1 = new GroupBox();
            dgvAvailable = new DataGridView();
            groupBox2 = new GroupBox();
            dgvBorrowed = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAvailable).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBorrowed).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnAdd);
            panel1.Controls.Add(btnReturn);
            panel1.Controls.Add(btnClose);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 544);
            panel1.Name = "panel1";
            panel1.Size = new Size(1265, 38);
            panel1.TabIndex = 0;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(12, 7);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 27);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "添加借用";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnReturn
            // 
            btnReturn.Location = new Point(97, 7);
            btnReturn.Name = "btnReturn";
            btnReturn.Size = new Size(75, 27);
            btnReturn.TabIndex = 1;
            btnReturn.Text = "归还";
            btnReturn.UseVisualStyleBackColor = true;
            btnReturn.Click += btnReturn_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(1178, 6);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 27);
            btnClose.TabIndex = 2;
            btnClose.Text = "关闭";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBox2);
            splitContainer1.Size = new Size(1265, 544);
            splitContainer1.SplitterDistance = 704;
            splitContainer1.SplitterWidth = 2;
            splitContainer1.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgvAvailable);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(704, 544);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "待借用资产";
            // 
            // dgvAvailable
            // 
            dgvAvailable.AllowUserToAddRows = false;
            dgvAvailable.AllowUserToDeleteRows = false;
            dgvAvailable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAvailable.Dock = DockStyle.Fill;
            dgvAvailable.Location = new Point(3, 19);
            dgvAvailable.Name = "dgvAvailable";
            dgvAvailable.ReadOnly = true;
            dgvAvailable.RowHeadersWidth = 82;
            dgvAvailable.Size = new Size(698, 522);
            dgvAvailable.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvBorrowed);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(559, 544);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "已借用资产";
            // 
            // dgvBorrowed
            // 
            dgvBorrowed.AllowUserToAddRows = false;
            dgvBorrowed.AllowUserToDeleteRows = false;
            dgvBorrowed.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBorrowed.Dock = DockStyle.Fill;
            dgvBorrowed.Location = new Point(3, 19);
            dgvBorrowed.Name = "dgvBorrowed";
            dgvBorrowed.ReadOnly = true;
            dgvBorrowed.RowHeadersWidth = 82;
            dgvBorrowed.Size = new Size(553, 522);
            dgvBorrowed.TabIndex = 0;
            // 
            // BorrowRecordsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1265, 582);
            Controls.Add(splitContainer1);
            Controls.Add(panel1);
            MinimizeBox = false;
            Name = "BorrowRecordsForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "资产借用管理";
            panel1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAvailable).EndInit();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBorrowed).EndInit();
            ResumeLayout(false);
        }

        private Panel panel1;
        private Button btnAdd;
        private Button btnReturn;
        private Button btnClose;
        private SplitContainer splitContainer1;
        private GroupBox groupBox1;
        private DataGridView dgvAvailable;
        private GroupBox groupBox2;
        private DataGridView dgvBorrowed;
    }
}
