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
            chkCanDelete = new CheckBox();
            chkCanPrint = new CheckBox();
            chkCanExport = new CheckBox();
            chkCanImport = new CheckBox();
            chkCanBorrow = new CheckBox();
            chkCanViewStockRecords = new CheckBox();
            chkCanScrap = new CheckBox();
            chkCanInventoryCheck = new CheckBox();
            chkCanViewStockReport = new CheckBox();
            chkManageNumber = new CheckBox();
            chkManageLog = new CheckBox();
            chkManageData = new CheckBox();
            chkManageWarehouse = new CheckBox();
            chkManageUsers = new CheckBox();
            chkIsAdmin = new CheckBox();
            txtPassword = new TextBox();
            label2 = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            grpBasic = new GroupBox();
            grpBusiness = new GroupBox();
            grpSystem = new GroupBox();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(100, 20);
            txtUsername.Margin = new Padding(4);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(200, 23);
            txtUsername.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 23);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 1;
            label1.Text = "用户名：";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(100, 55);
            txtPassword.Margin = new Padding(4);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(200, 23);
            txtPassword.TabIndex = 2;
            txtPassword.PasswordChar = '*';
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 58);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(44, 17);
            label2.TabIndex = 3;
            label2.Text = "密码：";
            // 
            // grpBasic
            // 
            grpBasic.Location = new Point(20, 95);
            grpBasic.Name = "grpBasic";
            grpBasic.Size = new Size(490, 90);
            grpBasic.TabIndex = 4;
            grpBasic.TabStop = false;
            grpBasic.Text = "基础操作权限";
            // 
            // chkCanAdd
            // 
            chkCanAdd.AutoSize = true;
            chkCanAdd.Location = new Point(15, 25);
            chkCanAdd.Margin = new Padding(4);
            chkCanAdd.Name = "chkCanAdd";
            chkCanAdd.Size = new Size(63, 21);
            chkCanAdd.TabIndex = 0;
            chkCanAdd.Text = "新增";
            chkCanAdd.UseVisualStyleBackColor = true;
            // 
            // chkCanEdit
            // 
            chkCanEdit.AutoSize = true;
            chkCanEdit.Location = new Point(95, 25);
            chkCanEdit.Margin = new Padding(4);
            chkCanEdit.Name = "chkCanEdit";
            chkCanEdit.Size = new Size(63, 21);
            chkCanEdit.TabIndex = 1;
            chkCanEdit.Text = "编辑";
            chkCanEdit.UseVisualStyleBackColor = true;
            // 
            // chkCanDelete
            // 
            chkCanDelete.AutoSize = true;
            chkCanDelete.Location = new Point(175, 25);
            chkCanDelete.Margin = new Padding(4);
            chkCanDelete.Name = "chkCanDelete";
            chkCanDelete.Size = new Size(63, 21);
            chkCanDelete.TabIndex = 2;
            chkCanDelete.Text = "删除";
            chkCanDelete.UseVisualStyleBackColor = true;
            // 
            // chkCanPrint
            // 
            chkCanPrint.AutoSize = true;
            chkCanPrint.Location = new Point(255, 25);
            chkCanPrint.Margin = new Padding(4);
            chkCanPrint.Name = "chkCanPrint";
            chkCanPrint.Size = new Size(63, 21);
            chkCanPrint.TabIndex = 3;
            chkCanPrint.Text = "打印";
            chkCanPrint.UseVisualStyleBackColor = true;
            // 
            // chkCanExport
            // 
            chkCanExport.AutoSize = true;
            chkCanExport.Location = new Point(15, 55);
            chkCanExport.Margin = new Padding(4);
            chkCanExport.Name = "chkCanExport";
            chkCanExport.Size = new Size(63, 21);
            chkCanExport.TabIndex = 4;
            chkCanExport.Text = "导出";
            chkCanExport.UseVisualStyleBackColor = true;
            // 
            // chkCanImport
            // 
            chkCanImport.AutoSize = true;
            chkCanImport.Location = new Point(95, 55);
            chkCanImport.Margin = new Padding(4);
            chkCanImport.Name = "chkCanImport";
            chkCanImport.Size = new Size(63, 21);
            chkCanImport.TabIndex = 5;
            chkCanImport.Text = "导入";
            chkCanImport.UseVisualStyleBackColor = true;
            grpBasic.Controls.Add(chkCanAdd);
            grpBasic.Controls.Add(chkCanEdit);
            grpBasic.Controls.Add(chkCanDelete);
            grpBasic.Controls.Add(chkCanPrint);
            grpBasic.Controls.Add(chkCanExport);
            grpBasic.Controls.Add(chkCanImport);
            // 
            // grpBusiness
            // 
            grpBusiness.Location = new Point(20, 195);
            grpBusiness.Name = "grpBusiness";
            grpBusiness.Size = new Size(490, 90);
            grpBusiness.TabIndex = 5;
            grpBusiness.TabStop = false;
            grpBusiness.Text = "业务功能权限";
            // 
            // chkCanBorrow
            // 
            chkCanBorrow.AutoSize = true;
            chkCanBorrow.Location = new Point(15, 25);
            chkCanBorrow.Margin = new Padding(4);
            chkCanBorrow.Name = "chkCanBorrow";
            chkCanBorrow.Size = new Size(75, 21);
            chkCanBorrow.TabIndex = 0;
            chkCanBorrow.Text = "借还登记";
            chkCanBorrow.UseVisualStyleBackColor = true;
            // 
            // chkCanViewStockRecords
            // 
            chkCanViewStockRecords.AutoSize = true;
            chkCanViewStockRecords.Location = new Point(110, 25);
            chkCanViewStockRecords.Margin = new Padding(4);
            chkCanViewStockRecords.Name = "chkCanViewStockRecords";
            chkCanViewStockRecords.Size = new Size(75, 21);
            chkCanViewStockRecords.TabIndex = 1;
            chkCanViewStockRecords.Text = "库存记录";
            chkCanViewStockRecords.UseVisualStyleBackColor = true;
            // 
            // chkCanScrap
            // 
            chkCanScrap.AutoSize = true;
            chkCanScrap.Location = new Point(205, 25);
            chkCanScrap.Margin = new Padding(4);
            chkCanScrap.Name = "chkCanScrap";
            chkCanScrap.Size = new Size(75, 21);
            chkCanScrap.TabIndex = 2;
            chkCanScrap.Text = "报损登记";
            chkCanScrap.UseVisualStyleBackColor = true;
            // 
            // chkCanInventoryCheck
            // 
            chkCanInventoryCheck.AutoSize = true;
            chkCanInventoryCheck.Location = new Point(15, 55);
            chkCanInventoryCheck.Margin = new Padding(4);
            chkCanInventoryCheck.Name = "chkCanInventoryCheck";
            chkCanInventoryCheck.Size = new Size(75, 21);
            chkCanInventoryCheck.TabIndex = 3;
            chkCanInventoryCheck.Text = "盘点管理";
            chkCanInventoryCheck.UseVisualStyleBackColor = true;
            // 
            // chkCanViewStockReport
            // 
            chkCanViewStockReport.AutoSize = true;
            chkCanViewStockReport.Location = new Point(110, 55);
            chkCanViewStockReport.Margin = new Padding(4);
            chkCanViewStockReport.Name = "chkCanViewStockReport";
            chkCanViewStockReport.Size = new Size(75, 21);
            chkCanViewStockReport.TabIndex = 4;
            chkCanViewStockReport.Text = "库存报表";
            chkCanViewStockReport.UseVisualStyleBackColor = true;
            grpBusiness.Controls.Add(chkCanBorrow);
            grpBusiness.Controls.Add(chkCanViewStockRecords);
            grpBusiness.Controls.Add(chkCanScrap);
            grpBusiness.Controls.Add(chkCanInventoryCheck);
            grpBusiness.Controls.Add(chkCanViewStockReport);
            // 
            // grpSystem
            // 
            grpSystem.Location = new Point(20, 295);
            grpSystem.Name = "grpSystem";
            grpSystem.Size = new Size(490, 90);
            grpSystem.TabIndex = 6;
            grpSystem.TabStop = false;
            grpSystem.Text = "系统管理权限";
            // 
            // chkManageNumber
            // 
            chkManageNumber.AutoSize = true;
            chkManageNumber.Location = new Point(15, 25);
            chkManageNumber.Margin = new Padding(4);
            chkManageNumber.Name = "chkManageNumber";
            chkManageNumber.Size = new Size(75, 21);
            chkManageNumber.TabIndex = 0;
            chkManageNumber.Text = "编号设置";
            chkManageNumber.UseVisualStyleBackColor = true;
            // 
            // chkManageLog
            // 
            chkManageLog.AutoSize = true;
            chkManageLog.Location = new Point(110, 25);
            chkManageLog.Margin = new Padding(4);
            chkManageLog.Name = "chkManageLog";
            chkManageLog.Size = new Size(75, 21);
            chkManageLog.TabIndex = 1;
            chkManageLog.Text = "日志管理";
            chkManageLog.UseVisualStyleBackColor = true;
            // 
            // chkManageData
            // 
            chkManageData.AutoSize = true;
            chkManageData.Location = new Point(205, 25);
            chkManageData.Margin = new Padding(4);
            chkManageData.Name = "chkManageData";
            chkManageData.Size = new Size(87, 21);
            chkManageData.TabIndex = 2;
            chkManageData.Text = "数据库管理";
            chkManageData.UseVisualStyleBackColor = true;
            // 
            // chkManageWarehouse
            // 
            chkManageWarehouse.AutoSize = true;
            chkManageWarehouse.Location = new Point(15, 55);
            chkManageWarehouse.Margin = new Padding(4);
            chkManageWarehouse.Name = "chkManageWarehouse";
            chkManageWarehouse.Size = new Size(75, 21);
            chkManageWarehouse.TabIndex = 3;
            chkManageWarehouse.Text = "仓库管理";
            chkManageWarehouse.UseVisualStyleBackColor = true;
            // 
            // chkManageUsers
            // 
            chkManageUsers.AutoSize = true;
            chkManageUsers.Location = new Point(110, 55);
            chkManageUsers.Margin = new Padding(4);
            chkManageUsers.Name = "chkManageUsers";
            chkManageUsers.Size = new Size(75, 21);
            chkManageUsers.TabIndex = 4;
            chkManageUsers.Text = "用户管理";
            chkManageUsers.UseVisualStyleBackColor = true;
            grpSystem.Controls.Add(chkManageNumber);
            grpSystem.Controls.Add(chkManageLog);
            grpSystem.Controls.Add(chkManageData);
            grpSystem.Controls.Add(chkManageWarehouse);
            grpSystem.Controls.Add(chkManageUsers);
            // 
            // chkIsAdmin
            // 
            chkIsAdmin.AutoSize = true;
            chkIsAdmin.Location = new Point(20, 400);
            chkIsAdmin.Margin = new Padding(4);
            chkIsAdmin.Name = "chkIsAdmin";
            chkIsAdmin.Size = new Size(135, 21);
            chkIsAdmin.TabIndex = 7;
            chkIsAdmin.Text = "设为管理员（拥有所有权限）";
            chkIsAdmin.UseVisualStyleBackColor = true;
            chkIsAdmin.CheckedChanged += chkIsAdmin_CheckedChanged;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(130, 440);
            btnSave.Margin = new Padding(4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 35);
            btnSave.TabIndex = 8;
            btnSave.Text = "保存";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(280, 440);
            btnCancel.Margin = new Padding(4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 35);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // UserEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(530, 495);
            Controls.Add(txtUsername);
            Controls.Add(label1);
            Controls.Add(txtPassword);
            Controls.Add(label2);
            Controls.Add(grpBasic);
            Controls.Add(grpBusiness);
            Controls.Add(grpSystem);
            Controls.Add(chkIsAdmin);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UserEditForm";
            Text = "用户编辑";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpBasic;
        private System.Windows.Forms.CheckBox chkCanAdd;
        private System.Windows.Forms.CheckBox chkCanEdit;
        private System.Windows.Forms.CheckBox chkCanDelete;
        private System.Windows.Forms.CheckBox chkCanPrint;
        private System.Windows.Forms.CheckBox chkCanExport;
        private System.Windows.Forms.CheckBox chkCanImport;
        private System.Windows.Forms.GroupBox grpBusiness;
        private System.Windows.Forms.CheckBox chkCanBorrow;
        private System.Windows.Forms.CheckBox chkCanViewStockRecords;
        private System.Windows.Forms.CheckBox chkCanScrap;
        private System.Windows.Forms.CheckBox chkCanInventoryCheck;
        private System.Windows.Forms.CheckBox chkCanViewStockReport;
        private System.Windows.Forms.GroupBox grpSystem;
        private System.Windows.Forms.CheckBox chkManageNumber;
        private System.Windows.Forms.CheckBox chkManageLog;
        private System.Windows.Forms.CheckBox chkManageData;
        private System.Windows.Forms.CheckBox chkManageWarehouse;
        private System.Windows.Forms.CheckBox chkManageUsers;
        private System.Windows.Forms.CheckBox chkIsAdmin;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}