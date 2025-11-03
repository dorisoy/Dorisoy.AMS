namespace Dorisoy.AMS.view
{
    partial class BackupForm
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
            groupBox1 = new GroupBox();
            dgvBackupList = new DataGridView();
            BtnRestore = new Button();
            BtnBackup = new Button();
            btnClearDatabase = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBackupList).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgvBackupList);
            groupBox1.Location = new Point(24, 17);
            groupBox1.Margin = new Padding(4, 4, 4, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 4, 4, 4);
            groupBox1.Size = new Size(986, 384);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "备份与还原数据库";
            // 
            // dgvBackupList
            // 
            dgvBackupList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBackupList.Location = new Point(8, 28);
            dgvBackupList.Margin = new Padding(4, 4, 4, 4);
            dgvBackupList.MultiSelect = false;
            dgvBackupList.Name = "dgvBackupList";
            dgvBackupList.RowTemplate.Height = 23;
            dgvBackupList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBackupList.Size = new Size(954, 347);
            dgvBackupList.TabIndex = 0;
            // 
            // BtnRestore
            // 
            BtnRestore.Location = new Point(632, 429);
            BtnRestore.Margin = new Padding(4, 4, 4, 4);
            BtnRestore.Name = "BtnRestore";
            BtnRestore.Size = new Size(88, 33);
            BtnRestore.TabIndex = 1;
            BtnRestore.Text = "还原数据库";
            BtnRestore.UseVisualStyleBackColor = true;
            BtnRestore.Click += BtnRestore_Click;
            // 
            // BtnBackup
            // 
            BtnBackup.Location = new Point(488, 429);
            BtnBackup.Margin = new Padding(4, 4, 4, 4);
            BtnBackup.Name = "BtnBackup";
            BtnBackup.Size = new Size(88, 33);
            BtnBackup.TabIndex = 0;
            BtnBackup.Text = "备份数据库";
            BtnBackup.UseVisualStyleBackColor = true;
            BtnBackup.Click += BtnBackup_Click;
            // 
            // btnClearDatabase
            // 
            btnClearDatabase.Location = new Point(341, 429);
            btnClearDatabase.Margin = new Padding(4, 4, 4, 4);
            btnClearDatabase.Name = "btnClearDatabase";
            btnClearDatabase.Size = new Size(103, 33);
            btnClearDatabase.TabIndex = 2;
            btnClearDatabase.Text = "初始化数据库";
            btnClearDatabase.UseVisualStyleBackColor = true;
            btnClearDatabase.Click += btnClearDatabase_Click;
            // 
            // BackupForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1034, 486);
            Controls.Add(btnClearDatabase);
            Controls.Add(BtnBackup);
            Controls.Add(BtnRestore);
            Controls.Add(groupBox1);
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            Name = "BackupForm";
            Text = "备份与还原数据库";
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBackupList).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnRestore;
        private System.Windows.Forms.Button BtnBackup;
        private System.Windows.Forms.DataGridView dgvBackupList;
        private System.Windows.Forms.Button btnClearDatabase;
    }
}