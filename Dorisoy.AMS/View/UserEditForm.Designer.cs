namespace Dorisoy.AMS.view
{
    partial class UserEditForm
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
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkCanAdd = new System.Windows.Forms.CheckBox();
            this.chkCanEdit = new System.Windows.Forms.CheckBox();
            this.chkCanPrint = new System.Windows.Forms.CheckBox();
            this.chkCanExport = new System.Windows.Forms.CheckBox();
            this.chkCanImport = new System.Windows.Forms.CheckBox();
            this.chkManageNumber = new System.Windows.Forms.CheckBox();
            this.chkIsAdmin = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkManageLog = new System.Windows.Forms.CheckBox();
            this.chkManageData = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(119, 40);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(117, 21);
            this.txtUsername.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "用户名：";
            // 
            // chkCanAdd
            // 
            this.chkCanAdd.AutoSize = true;
            this.chkCanAdd.Location = new System.Drawing.Point(62, 121);
            this.chkCanAdd.Name = "chkCanAdd";
            this.chkCanAdd.Size = new System.Drawing.Size(72, 16);
            this.chkCanAdd.TabIndex = 2;
            this.chkCanAdd.Text = "添加资产";
            this.chkCanAdd.UseVisualStyleBackColor = true;
            // 
            // chkCanEdit
            // 
            this.chkCanEdit.AutoSize = true;
            this.chkCanEdit.Location = new System.Drawing.Point(62, 154);
            this.chkCanEdit.Name = "chkCanEdit";
            this.chkCanEdit.Size = new System.Drawing.Size(72, 16);
            this.chkCanEdit.TabIndex = 3;
            this.chkCanEdit.Text = "修改资产";
            this.chkCanEdit.UseVisualStyleBackColor = true;
            // 
            // chkCanPrint
            // 
            this.chkCanPrint.AutoSize = true;
            this.chkCanPrint.Location = new System.Drawing.Point(62, 189);
            this.chkCanPrint.Name = "chkCanPrint";
            this.chkCanPrint.Size = new System.Drawing.Size(48, 16);
            this.chkCanPrint.TabIndex = 4;
            this.chkCanPrint.Text = "打印";
            this.chkCanPrint.UseVisualStyleBackColor = true;
            // 
            // chkCanExport
            // 
            this.chkCanExport.AutoSize = true;
            this.chkCanExport.Location = new System.Drawing.Point(62, 228);
            this.chkCanExport.Name = "chkCanExport";
            this.chkCanExport.Size = new System.Drawing.Size(48, 16);
            this.chkCanExport.TabIndex = 5;
            this.chkCanExport.Text = "导出";
            this.chkCanExport.UseVisualStyleBackColor = true;
            // 
            // chkCanImport
            // 
            this.chkCanImport.AutoSize = true;
            this.chkCanImport.Location = new System.Drawing.Point(62, 262);
            this.chkCanImport.Name = "chkCanImport";
            this.chkCanImport.Size = new System.Drawing.Size(48, 16);
            this.chkCanImport.TabIndex = 6;
            this.chkCanImport.Text = "导入";
            this.chkCanImport.UseVisualStyleBackColor = true;
            // 
            // chkManageNumber
            // 
            this.chkManageNumber.AutoSize = true;
            this.chkManageNumber.Location = new System.Drawing.Point(62, 296);
            this.chkManageNumber.Name = "chkManageNumber";
            this.chkManageNumber.Size = new System.Drawing.Size(72, 16);
            this.chkManageNumber.TabIndex = 7;
            this.chkManageNumber.Text = "编号设置";
            this.chkManageNumber.UseVisualStyleBackColor = true;
            // 
            // chkIsAdmin
            // 
            this.chkIsAdmin.AutoSize = true;
            this.chkIsAdmin.Location = new System.Drawing.Point(62, 391);
            this.chkIsAdmin.Name = "chkIsAdmin";
            this.chkIsAdmin.Size = new System.Drawing.Size(84, 16);
            this.chkIsAdmin.TabIndex = 8;
            this.chkIsAdmin.Text = "设为管理员";
            this.chkIsAdmin.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(119, 82);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(117, 21);
            this.txtPassword.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "密码:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(80, 449);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(192, 449);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkManageLog
            // 
            this.chkManageLog.AutoSize = true;
            this.chkManageLog.Location = new System.Drawing.Point(62, 327);
            this.chkManageLog.Name = "chkManageLog";
            this.chkManageLog.Size = new System.Drawing.Size(48, 16);
            this.chkManageLog.TabIndex = 13;
            this.chkManageLog.Text = "日志";
            this.chkManageLog.UseVisualStyleBackColor = true;
            // 
            // chkManageData
            // 
            this.chkManageData.AutoSize = true;
            this.chkManageData.Location = new System.Drawing.Point(62, 360);
            this.chkManageData.Name = "chkManageData";
            this.chkManageData.Size = new System.Drawing.Size(84, 16);
            this.chkManageData.TabIndex = 14;
            this.chkManageData.Text = "管理数据库";
            this.chkManageData.UseVisualStyleBackColor = true;
            // 
            // UserEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 541);
            this.Controls.Add(this.chkManageData);
            this.Controls.Add(this.chkManageLog);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.chkIsAdmin);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.chkManageNumber);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkCanImport);
            this.Controls.Add(this.chkCanAdd);
            this.Controls.Add(this.chkCanExport);
            this.Controls.Add(this.chkCanEdit);
            this.Controls.Add(this.chkCanPrint);
            this.MaximizeBox = false;
            this.Name = "UserEditForm";
            this.Text = "UserEditForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkCanAdd;
        private System.Windows.Forms.CheckBox chkCanEdit;
        private System.Windows.Forms.CheckBox chkCanPrint;
        private System.Windows.Forms.CheckBox chkCanExport;
        private System.Windows.Forms.CheckBox chkCanImport;
        private System.Windows.Forms.CheckBox chkManageNumber;
        private System.Windows.Forms.CheckBox chkIsAdmin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkManageLog;
        private System.Windows.Forms.CheckBox chkManageData;
    }
}