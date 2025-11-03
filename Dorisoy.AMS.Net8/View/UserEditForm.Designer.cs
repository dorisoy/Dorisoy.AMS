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
            txtUsername = new TextBox();
            label1 = new Label();
            chkCanAdd = new CheckBox();
            chkCanEdit = new CheckBox();
            chkCanPrint = new CheckBox();
            chkCanExport = new CheckBox();
            chkCanImport = new CheckBox();
            chkManageNumber = new CheckBox();
            chkIsAdmin = new CheckBox();
            txtPassword = new TextBox();
            label2 = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            chkManageLog = new CheckBox();
            chkManageData = new CheckBox();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(139, 33);
            txtUsername.Margin = new Padding(4, 4, 4, 4);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(163, 23);
            txtUsername.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 36);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 1;
            label1.Text = "用户名：";
            // 
            // chkCanAdd
            // 
            chkCanAdd.AutoSize = true;
            chkCanAdd.Location = new Point(34, 146);
            chkCanAdd.Margin = new Padding(4, 4, 4, 4);
            chkCanAdd.Name = "chkCanAdd";
            chkCanAdd.Size = new Size(75, 21);
            chkCanAdd.TabIndex = 2;
            chkCanAdd.Text = "添加资产";
            chkCanAdd.UseVisualStyleBackColor = true;
            // 
            // chkCanEdit
            // 
            chkCanEdit.AutoSize = true;
            chkCanEdit.Location = new Point(34, 175);
            chkCanEdit.Margin = new Padding(4, 4, 4, 4);
            chkCanEdit.Name = "chkCanEdit";
            chkCanEdit.Size = new Size(75, 21);
            chkCanEdit.TabIndex = 3;
            chkCanEdit.Text = "修改资产";
            chkCanEdit.UseVisualStyleBackColor = true;
            // 
            // chkCanPrint
            // 
            chkCanPrint.AutoSize = true;
            chkCanPrint.Location = new Point(32, 204);
            chkCanPrint.Margin = new Padding(4, 4, 4, 4);
            chkCanPrint.Name = "chkCanPrint";
            chkCanPrint.Size = new Size(51, 21);
            chkCanPrint.TabIndex = 4;
            chkCanPrint.Text = "打印";
            chkCanPrint.UseVisualStyleBackColor = true;
            // 
            // chkCanExport
            // 
            chkCanExport.AutoSize = true;
            chkCanExport.Location = new Point(32, 233);
            chkCanExport.Margin = new Padding(4, 4, 4, 4);
            chkCanExport.Name = "chkCanExport";
            chkCanExport.Size = new Size(51, 21);
            chkCanExport.TabIndex = 5;
            chkCanExport.Text = "导出";
            chkCanExport.UseVisualStyleBackColor = true;
            // 
            // chkCanImport
            // 
            chkCanImport.AutoSize = true;
            chkCanImport.Location = new Point(32, 262);
            chkCanImport.Margin = new Padding(4, 4, 4, 4);
            chkCanImport.Name = "chkCanImport";
            chkCanImport.Size = new Size(51, 21);
            chkCanImport.TabIndex = 6;
            chkCanImport.Text = "导入";
            chkCanImport.UseVisualStyleBackColor = true;
            // 
            // chkManageNumber
            // 
            chkManageNumber.AutoSize = true;
            chkManageNumber.Location = new Point(32, 291);
            chkManageNumber.Margin = new Padding(4, 4, 4, 4);
            chkManageNumber.Name = "chkManageNumber";
            chkManageNumber.Size = new Size(75, 21);
            chkManageNumber.TabIndex = 7;
            chkManageNumber.Text = "编号设置";
            chkManageNumber.UseVisualStyleBackColor = true;
            // 
            // chkIsAdmin
            // 
            chkIsAdmin.AutoSize = true;
            chkIsAdmin.Location = new Point(32, 378);
            chkIsAdmin.Margin = new Padding(4, 4, 4, 4);
            chkIsAdmin.Name = "chkIsAdmin";
            chkIsAdmin.Size = new Size(87, 21);
            chkIsAdmin.TabIndex = 8;
            chkIsAdmin.Text = "设为管理员";
            chkIsAdmin.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(139, 92);
            txtPassword.Margin = new Padding(4, 4, 4, 4);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(163, 23);
            txtPassword.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(32, 95);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(35, 17);
            label2.TabIndex = 10;
            label2.Text = "密码:";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(13, 438);
            btnSave.Margin = new Padding(4, 4, 4, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(130, 33);
            btnSave.TabIndex = 11;
            btnSave.Text = "保存";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(174, 438);
            btnCancel.Margin = new Padding(4, 4, 4, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(128, 33);
            btnCancel.TabIndex = 12;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // chkManageLog
            // 
            chkManageLog.AutoSize = true;
            chkManageLog.Location = new Point(32, 320);
            chkManageLog.Margin = new Padding(4, 4, 4, 4);
            chkManageLog.Name = "chkManageLog";
            chkManageLog.Size = new Size(51, 21);
            chkManageLog.TabIndex = 13;
            chkManageLog.Text = "日志";
            chkManageLog.UseVisualStyleBackColor = true;
            // 
            // chkManageData
            // 
            chkManageData.AutoSize = true;
            chkManageData.Location = new Point(32, 349);
            chkManageData.Margin = new Padding(4, 4, 4, 4);
            chkManageData.Name = "chkManageData";
            chkManageData.Size = new Size(87, 21);
            chkManageData.TabIndex = 14;
            chkManageData.Text = "管理数据库";
            chkManageData.UseVisualStyleBackColor = true;
            // 
            // UserEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(321, 493);
            Controls.Add(chkManageData);
            Controls.Add(chkManageLog);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(label2);
            Controls.Add(txtPassword);
            Controls.Add(chkIsAdmin);
            Controls.Add(txtUsername);
            Controls.Add(chkManageNumber);
            Controls.Add(label1);
            Controls.Add(chkCanImport);
            Controls.Add(chkCanAdd);
            Controls.Add(chkCanExport);
            Controls.Add(chkCanEdit);
            Controls.Add(chkCanPrint);
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            Name = "UserEditForm";
            Text = "UserEditForm";
            ResumeLayout(false);
            PerformLayout();

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