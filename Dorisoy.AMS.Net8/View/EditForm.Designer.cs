namespace Dorisoy.AMS.view
{
    partial class EditForm
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
            label1 = new Label();
            txtAssetID = new TextBox();
            txtCategory = new TextBox();
            label2 = new Label();
            txtName = new TextBox();
            label3 = new Label();
            txtModel = new TextBox();
            label4 = new Label();
            label5 = new Label();
            txtUnit = new TextBox();
            label6 = new Label();
            cmbLocation = new ComboBox();
            label8 = new Label();
            cmbUser = new ComboBox();
            numQuantity = new NumericUpDown();
            lblMinQuantity = new Label();
            numMinQuantity = new NumericUpDown();
            label7 = new Label();
            label10 = new Label();
            txtRemark = new TextBox();
            label11 = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            cmbDepartment = new ComboBox();
            lblStatus = new Label();
            cmbStatus = new ComboBox();
            grpBarcode = new GroupBox();
            picBarcode = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinQuantity).BeginInit();
            grpBarcode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picBarcode).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(33, 17);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 0;
            label1.Text = "资产编号";
            // 
            // txtAssetID
            // 
            txtAssetID.Location = new Point(102, 17);
            txtAssetID.Margin = new Padding(4);
            txtAssetID.Name = "txtAssetID";
            txtAssetID.ReadOnly = true;
            txtAssetID.Size = new Size(200, 23);
            txtAssetID.TabIndex = 1;
            // 
            // txtCategory
            // 
            txtCategory.Location = new Point(102, 109);
            txtCategory.Margin = new Padding(4);
            txtCategory.Name = "txtCategory";
            txtCategory.Size = new Size(200, 23);
            txtCategory.TabIndex = 3;
            txtCategory.Text = "电子电器";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(33, 115);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(56, 17);
            label2.TabIndex = 2;
            label2.Text = "资产类型";
            // 
            // txtName
            // 
            txtName.Location = new Point(102, 147);
            txtName.Margin = new Padding(4);
            txtName.Name = "txtName";
            txtName.Size = new Size(200, 23);
            txtName.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(33, 153);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(56, 17);
            label3.TabIndex = 4;
            label3.Text = "资产名称";
            // 
            // txtModel
            // 
            txtModel.Location = new Point(102, 186);
            txtModel.Margin = new Padding(4);
            txtModel.Name = "txtModel";
            txtModel.Size = new Size(200, 23);
            txtModel.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(33, 191);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(56, 17);
            label4.TabIndex = 6;
            label4.Text = "规格型号";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(33, 230);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(32, 17);
            label5.TabIndex = 8;
            label5.Text = "数量";
            // 
            // txtUnit
            // 
            txtUnit.Location = new Point(102, 300);
            txtUnit.Margin = new Padding(4);
            txtUnit.Name = "txtUnit";
            txtUnit.Size = new Size(200, 23);
            txtUnit.TabIndex = 11;
            txtUnit.Text = "台";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(33, 306);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(32, 17);
            label6.TabIndex = 10;
            label6.Text = "单位";
            // 
            // cmbLocation
            // 
            cmbLocation.FormattingEnabled = true;
            cmbLocation.Location = new Point(102, 339);
            cmbLocation.Margin = new Padding(4);
            cmbLocation.Name = "cmbLocation";
            cmbLocation.Size = new Size(200, 25);
            cmbLocation.TabIndex = 13;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(33, 382);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(56, 17);
            label8.TabIndex = 14;
            label8.Text = "所属部门";
            // 
            // cmbUser
            // 
            cmbUser.FormattingEnabled = true;
            cmbUser.Location = new Point(102, 415);
            cmbUser.Margin = new Padding(4);
            cmbUser.Name = "cmbUser";
            cmbUser.Size = new Size(200, 25);
            cmbUser.TabIndex = 17;
            // 
            // numQuantity
            // 
            numQuantity.Location = new Point(102, 227);
            numQuantity.Margin = new Padding(4);
            numQuantity.Name = "numQuantity";
            numQuantity.Size = new Size(201, 23);
            numQuantity.TabIndex = 18;
            numQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblMinQuantity
            // 
            lblMinQuantity.AutoSize = true;
            lblMinQuantity.Location = new Point(33, 268);
            lblMinQuantity.Margin = new Padding(4, 0, 4, 0);
            lblMinQuantity.Name = "lblMinQuantity";
            lblMinQuantity.Size = new Size(56, 17);
            lblMinQuantity.TabIndex = 29;
            lblMinQuantity.Text = "最低库存";
            // 
            // numMinQuantity
            // 
            numMinQuantity.Location = new Point(102, 262);
            numMinQuantity.Margin = new Padding(4);
            numMinQuantity.Name = "numMinQuantity";
            numMinQuantity.Size = new Size(201, 23);
            numMinQuantity.TabIndex = 30;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(33, 343);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(56, 17);
            label7.TabIndex = 19;
            label7.Text = "存放地点";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(33, 422);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(44, 17);
            label10.TabIndex = 20;
            label10.Text = "使用人";
            // 
            // txtRemark
            // 
            txtRemark.Location = new Point(102, 456);
            txtRemark.Margin = new Padding(4);
            txtRemark.Name = "txtRemark";
            txtRemark.Size = new Size(200, 23);
            txtRemark.TabIndex = 23;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(33, 462);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(32, 17);
            label11.TabIndex = 22;
            label11.Text = "备注";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(83, 565);
            btnSave.Margin = new Padding(4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(88, 33);
            btnSave.TabIndex = 24;
            btnSave.Text = "保存";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(198, 565);
            btnCancel.Margin = new Padding(4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 33);
            btnCancel.TabIndex = 25;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // cmbDepartment
            // 
            cmbDepartment.FormattingEnabled = true;
            cmbDepartment.Location = new Point(103, 378);
            cmbDepartment.Margin = new Padding(4);
            cmbDepartment.Name = "cmbDepartment";
            cmbDepartment.Size = new Size(199, 25);
            cmbDepartment.TabIndex = 26;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(33, 504);
            lblStatus.Margin = new Padding(4, 0, 4, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(32, 17);
            lblStatus.TabIndex = 27;
            lblStatus.Text = "状态";
            // 
            // cmbStatus
            // 
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(103, 494);
            cmbStatus.Margin = new Padding(4);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(199, 25);
            cmbStatus.TabIndex = 28;
            // 
            // grpBarcode
            // 
            grpBarcode.Controls.Add(picBarcode);
            grpBarcode.Location = new Point(327, 17);
            grpBarcode.Margin = new Padding(4);
            grpBarcode.Name = "grpBarcode";
            grpBarcode.Padding = new Padding(4);
            grpBarcode.Size = new Size(368, 101);
            grpBarcode.TabIndex = 31;
            grpBarcode.TabStop = false;
            grpBarcode.Text = "资产条码";
            // 
            // picBarcode
            // 
            picBarcode.BackColor = Color.White;
            picBarcode.Location = new Point(8, 24);
            picBarcode.Margin = new Padding(4);
            picBarcode.Name = "picBarcode";
            picBarcode.Size = new Size(352, 69);
            picBarcode.SizeMode = PictureBoxSizeMode.Zoom;
            picBarcode.TabIndex = 0;
            picBarcode.TabStop = false;
            // 
            // EditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(714, 660);
            Controls.Add(grpBarcode);
            Controls.Add(numMinQuantity);
            Controls.Add(lblMinQuantity);
            Controls.Add(cmbStatus);
            Controls.Add(lblStatus);
            Controls.Add(cmbDepartment);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtRemark);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label7);
            Controls.Add(numQuantity);
            Controls.Add(cmbUser);
            Controls.Add(label8);
            Controls.Add(cmbLocation);
            Controls.Add(txtUnit);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(txtModel);
            Controls.Add(label4);
            Controls.Add(txtName);
            Controls.Add(label3);
            Controls.Add(txtCategory);
            Controls.Add(label2);
            Controls.Add(txtAssetID);
            Controls.Add(label1);
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "EditForm";
            Text = "EditForm";
            ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinQuantity).EndInit();
            grpBarcode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picBarcode).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAssetID;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbUser;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblMinQuantity;
        private System.Windows.Forms.NumericUpDown numMinQuantity;
        private System.Windows.Forms.GroupBox grpBarcode;
        private System.Windows.Forms.PictureBox picBarcode;
    }
}