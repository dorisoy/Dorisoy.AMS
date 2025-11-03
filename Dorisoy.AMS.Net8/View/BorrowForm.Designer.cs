namespace Dorisoy.AMS.view
{
    partial class BorrowForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BorrowForm));
            groupBox1 = new GroupBox();
            lblCurrentUser = new Label();
            label7 = new Label();
            lblDepartment = new Label();
            label5 = new Label();
            lblLocation = new Label();
            label4 = new Label();
            lblModel = new Label();
            label3 = new Label();
            lblCategory = new Label();
            label2 = new Label();
            lblAssetName = new Label();
            lblAssetID = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            numBorrowQuantity = new NumericUpDown();
            label12 = new Label();
            txtReason = new TextBox();
            label11 = new Label();
            dtExpectedReturnDate = new DateTimePicker();
            label10 = new Label();
            dtBorrowDate = new DateTimePicker();
            label9 = new Label();
            cmbBorrowedBy = new ComboBox();
            label8 = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numBorrowQuantity).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblCurrentUser);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(lblDepartment);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(lblLocation);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(lblModel);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(lblCategory);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(lblAssetName);
            groupBox1.Controls.Add(lblAssetID);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(15, 15);
            groupBox1.Margin = new Padding(6, 5, 6, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(6, 5, 6, 5);
            groupBox1.Size = new Size(1293, 414);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "资产信息";
            // 
            // lblCurrentUser
            // 
            lblCurrentUser.AutoSize = true;
            lblCurrentUser.Location = new Point(120, 337);
            lblCurrentUser.Margin = new Padding(6, 0, 6, 0);
            lblCurrentUser.Name = "lblCurrentUser";
            lblCurrentUser.Size = new Size(0, 31);
            lblCurrentUser.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(60, 337);
            label7.Margin = new Padding(6, 0, 6, 0);
            label7.Name = "label7";
            label7.Size = new Size(134, 31);
            label7.TabIndex = 11;
            label7.Text = "现使用人：";
            // 
            // lblDepartment
            // 
            lblDepartment.AutoSize = true;
            lblDepartment.Location = new Point(120, 274);
            lblDepartment.Margin = new Padding(6, 0, 6, 0);
            lblDepartment.Name = "lblDepartment";
            lblDepartment.Size = new Size(0, 31);
            lblDepartment.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(60, 274);
            label5.Margin = new Padding(6, 0, 6, 0);
            label5.Name = "label5";
            label5.Size = new Size(134, 31);
            label5.TabIndex = 9;
            label5.Text = "责任部门：";
            // 
            // lblLocation
            // 
            lblLocation.AutoSize = true;
            lblLocation.Location = new Point(120, 219);
            lblLocation.Margin = new Padding(6, 0, 6, 0);
            lblLocation.Name = "lblLocation";
            lblLocation.Size = new Size(0, 31);
            lblLocation.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(60, 219);
            label4.Margin = new Padding(6, 0, 6, 0);
            label4.Name = "label4";
            label4.Size = new Size(134, 31);
            label4.TabIndex = 7;
            label4.Text = "存放地点：";
            // 
            // lblModel
            // 
            lblModel.AutoSize = true;
            lblModel.Location = new Point(120, 164);
            lblModel.Margin = new Padding(6, 0, 6, 0);
            lblModel.Name = "lblModel";
            lblModel.Size = new Size(0, 31);
            lblModel.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(60, 164);
            label3.Margin = new Padding(6, 0, 6, 0);
            label3.Name = "label3";
            label3.Size = new Size(134, 31);
            label3.TabIndex = 5;
            label3.Text = "规格型号：";
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(120, 109);
            lblCategory.Margin = new Padding(6, 0, 6, 0);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(0, 31);
            lblCategory.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(60, 109);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(134, 31);
            label2.TabIndex = 3;
            label2.Text = "资产类别：";
            // 
            // lblAssetName
            // 
            lblAssetName.AutoSize = true;
            lblAssetName.Location = new Point(120, 55);
            lblAssetName.Margin = new Padding(6, 0, 6, 0);
            lblAssetName.Name = "lblAssetName";
            lblAssetName.Size = new Size(0, 31);
            lblAssetName.TabIndex = 2;
            // 
            // lblAssetID
            // 
            lblAssetID.AutoSize = true;
            lblAssetID.Location = new Point(60, 55);
            lblAssetID.Margin = new Padding(6, 0, 6, 0);
            lblAssetID.Name = "lblAssetID";
            lblAssetID.Size = new Size(0, 31);
            lblAssetID.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(60, 55);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(134, 31);
            label1.TabIndex = 0;
            label1.Text = "资产编号：";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(numBorrowQuantity);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(txtReason);
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(dtExpectedReturnDate);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(dtBorrowDate);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(cmbBorrowedBy);
            groupBox2.Controls.Add(label8);
            groupBox2.Location = new Point(15, 448);
            groupBox2.Margin = new Padding(6, 5, 6, 5);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(6, 5, 6, 5);
            groupBox2.Size = new Size(1293, 330);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "借用信息";
            // 
            // numBorrowQuantity
            // 
            numBorrowQuantity.Location = new Point(240, 91);
            numBorrowQuantity.Margin = new Padding(6, 5, 6, 5);
            numBorrowQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numBorrowQuantity.Name = "numBorrowQuantity";
            numBorrowQuantity.Size = new Size(286, 38);
            numBorrowQuantity.TabIndex = 8;
            numBorrowQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(60, 100);
            label12.Margin = new Padding(6, 0, 6, 0);
            label12.Name = "label12";
            label12.Size = new Size(134, 31);
            label12.TabIndex = 7;
            label12.Text = "借用数量：";
            // 
            // txtReason
            // 
            txtReason.Location = new Point(240, 219);
            txtReason.Margin = new Padding(6, 5, 6, 5);
            txtReason.Multiline = true;
            txtReason.Name = "txtReason";
            txtReason.Size = new Size(1036, 79);
            txtReason.TabIndex = 7;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(60, 219);
            label11.Margin = new Padding(6, 0, 6, 0);
            label11.Name = "label11";
            label11.Size = new Size(134, 31);
            label11.TabIndex = 6;
            label11.Text = "借用原因：";
            // 
            // dtExpectedReturnDate
            // 
            dtExpectedReturnDate.Location = new Point(720, 155);
            dtExpectedReturnDate.Margin = new Padding(6, 5, 6, 5);
            dtExpectedReturnDate.Name = "dtExpectedReturnDate";
            dtExpectedReturnDate.Size = new Size(556, 38);
            dtExpectedReturnDate.TabIndex = 5;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(540, 164);
            label10.Margin = new Padding(6, 0, 6, 0);
            label10.Name = "label10";
            label10.Size = new Size(134, 31);
            label10.TabIndex = 4;
            label10.Text = "预期归还：";
            // 
            // dtBorrowDate
            // 
            dtBorrowDate.Location = new Point(240, 155);
            dtBorrowDate.Margin = new Padding(6, 5, 6, 5);
            dtBorrowDate.Name = "dtBorrowDate";
            dtBorrowDate.Size = new Size(286, 38);
            dtBorrowDate.TabIndex = 3;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(60, 164);
            label9.Margin = new Padding(6, 0, 6, 0);
            label9.Name = "label9";
            label9.Size = new Size(134, 31);
            label9.TabIndex = 2;
            label9.Text = "借用日期：";
            // 
            // cmbBorrowedBy
            // 
            cmbBorrowedBy.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBorrowedBy.FormattingEnabled = true;
            cmbBorrowedBy.Location = new Point(720, 91);
            cmbBorrowedBy.Margin = new Padding(6, 5, 6, 5);
            cmbBorrowedBy.Name = "cmbBorrowedBy";
            cmbBorrowedBy.Size = new Size(556, 39);
            cmbBorrowedBy.TabIndex = 1;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(540, 100);
            label8.Margin = new Padding(6, 0, 6, 0);
            label8.Name = "label8";
            label8.Size = new Size(110, 31);
            label8.TabIndex = 0;
            label8.Text = "借用人：";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(979, 788);
            btnSave.Margin = new Padding(6, 5, 6, 5);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(150, 45);
            btnSave.TabIndex = 2;
            btnSave.Text = "确定";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(1141, 788);
            btnCancel.Margin = new Padding(6, 5, 6, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(150, 45);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // BorrowForm
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1327, 862);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(6, 5, 6, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BorrowForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "资产借用";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numBorrowQuantity).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label lblAssetID;
        private Label label1;
        private Label lblAssetName;
        private Label lblCategory;
        private Label label2;
        private Label lblModel;
        private Label label3;
        private Label lblLocation;
        private Label label4;
        private Label lblDepartment;
        private Label label5;
        private Label lblCurrentUser;
        private Label label7;
        private GroupBox groupBox2;
        private Button btnSave;
        private Button btnCancel;
        private ComboBox cmbBorrowedBy;
        private Label label8;
        private DateTimePicker dtBorrowDate;
        private Label label9;
        private DateTimePicker dtExpectedReturnDate;
        private Label label10;
        private TextBox txtReason;
        private Label label11;
        private NumericUpDown numBorrowQuantity;
        private Label label12;
    }
}
