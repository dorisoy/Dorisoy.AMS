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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvBackupList = new System.Windows.Forms.DataGridView();
            this.BtnRestore = new System.Windows.Forms.Button();
            this.BtnBackup = new System.Windows.Forms.Button();
            this.btnClearDatabase = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBackupList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvBackupList);
            this.groupBox1.Location = new System.Drawing.Point(21, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(845, 271);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "备份与还原数据库";
            // 
            // dgvBackupList
            // 
            this.dgvBackupList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBackupList.Location = new System.Drawing.Point(7, 20);
            this.dgvBackupList.MultiSelect = false;
            this.dgvBackupList.Name = "dgvBackupList";
            this.dgvBackupList.RowTemplate.Height = 23;
            this.dgvBackupList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBackupList.Size = new System.Drawing.Size(818, 245);
            this.dgvBackupList.TabIndex = 0;
            // 
            // BtnRestore
            // 
            this.BtnRestore.Location = new System.Drawing.Point(421, 303);
            this.BtnRestore.Name = "BtnRestore";
            this.BtnRestore.Size = new System.Drawing.Size(75, 23);
            this.BtnRestore.TabIndex = 1;
            this.BtnRestore.Text = "还原数据库";
            this.BtnRestore.UseVisualStyleBackColor = true;
            this.BtnRestore.Click += new System.EventHandler(this.BtnRestore_Click);
            // 
            // BtnBackup
            // 
            this.BtnBackup.Location = new System.Drawing.Point(297, 303);
            this.BtnBackup.Name = "BtnBackup";
            this.BtnBackup.Size = new System.Drawing.Size(75, 23);
            this.BtnBackup.TabIndex = 0;
            this.BtnBackup.Text = "备份数据库";
            this.BtnBackup.UseVisualStyleBackColor = true;
            this.BtnBackup.Click += new System.EventHandler(this.BtnBackup_Click);
            // 
            // btnClearDatabase
            // 
            this.btnClearDatabase.Location = new System.Drawing.Point(163, 303);
            this.btnClearDatabase.Name = "btnClearDatabase";
            this.btnClearDatabase.Size = new System.Drawing.Size(88, 23);
            this.btnClearDatabase.TabIndex = 2;
            this.btnClearDatabase.Text = "初始化数据库";
            this.btnClearDatabase.UseVisualStyleBackColor = true;
            this.btnClearDatabase.Click += new System.EventHandler(this.btnClearDatabase_Click);
            // 
            // BackupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 343);
            this.Controls.Add(this.btnClearDatabase);
            this.Controls.Add(this.BtnBackup);
            this.Controls.Add(this.BtnRestore);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "BackupForm";
            this.Text = "备份与还原数据库";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBackupList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnRestore;
        private System.Windows.Forms.Button BtnBackup;
        private System.Windows.Forms.DataGridView dgvBackupList;
        private System.Windows.Forms.Button btnClearDatabase;
    }
}