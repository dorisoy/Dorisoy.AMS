namespace Dorisoy.AMS.view
{
    partial class ScrapForm
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
            this.lblAssetID = new System.Windows.Forms.Label();
            this.txtAssetID = new System.Windows.Forms.TextBox();
            this.lblAssetName = new System.Windows.Forms.Label();
            this.txtAssetName = new System.Windows.Forms.TextBox();
            this.lblWarehouse = new System.Windows.Forms.Label();
            this.txtWarehouse = new System.Windows.Forms.TextBox();
            this.lblCurrentQuantity = new System.Windows.Forms.Label();
            this.txtCurrentQuantity = new System.Windows.Forms.TextBox();
            this.lblScrapQuantity = new System.Windows.Forms.Label();
            this.numScrapQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblScrapReason = new System.Windows.Forms.Label();
            this.cmbScrapReason = new System.Windows.Forms.ComboBox();
            this.lblResponsiblePerson = new System.Windows.Forms.Label();
            this.txtResponsiblePerson = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numScrapQuantity)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAssetID
            // 
            this.lblAssetID.AutoSize = true;
            this.lblAssetID.Location = new System.Drawing.Point(20, 30);
            this.lblAssetID.Name = "lblAssetID";
            this.lblAssetID.Size = new System.Drawing.Size(53, 12);
            this.lblAssetID.TabIndex = 0;
            this.lblAssetID.Text = "资产编号";
            // 
            // txtAssetID
            // 
            this.txtAssetID.Location = new System.Drawing.Point(90, 26);
            this.txtAssetID.Name = "txtAssetID";
            this.txtAssetID.ReadOnly = true;
            this.txtAssetID.Size = new System.Drawing.Size(180, 21);
            this.txtAssetID.TabIndex = 1;
            // 
            // lblAssetName
            // 
            this.lblAssetName.AutoSize = true;
            this.lblAssetName.Location = new System.Drawing.Point(20, 60);
            this.lblAssetName.Name = "lblAssetName";
            this.lblAssetName.Size = new System.Drawing.Size(53, 12);
            this.lblAssetName.TabIndex = 2;
            this.lblAssetName.Text = "资产名称";
            // 
            // txtAssetName
            // 
            this.txtAssetName.Location = new System.Drawing.Point(90, 56);
            this.txtAssetName.Name = "txtAssetName";
            this.txtAssetName.ReadOnly = true;
            this.txtAssetName.Size = new System.Drawing.Size(180, 21);
            this.txtAssetName.TabIndex = 3;
            // 
            // lblWarehouse
            // 
            this.lblWarehouse.AutoSize = true;
            this.lblWarehouse.Location = new System.Drawing.Point(20, 90);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new System.Drawing.Size(53, 12);
            this.lblWarehouse.TabIndex = 4;
            this.lblWarehouse.Text = "所在仓库";
            // 
            // txtWarehouse
            // 
            this.txtWarehouse.Location = new System.Drawing.Point(90, 86);
            this.txtWarehouse.Name = "txtWarehouse";
            this.txtWarehouse.ReadOnly = true;
            this.txtWarehouse.Size = new System.Drawing.Size(180, 21);
            this.txtWarehouse.TabIndex = 5;
            // 
            // lblCurrentQuantity
            // 
            this.lblCurrentQuantity.AutoSize = true;
            this.lblCurrentQuantity.Location = new System.Drawing.Point(20, 120);
            this.lblCurrentQuantity.Name = "lblCurrentQuantity";
            this.lblCurrentQuantity.Size = new System.Drawing.Size(53, 12);
            this.lblCurrentQuantity.TabIndex = 6;
            this.lblCurrentQuantity.Text = "当前库存";
            // 
            // txtCurrentQuantity
            // 
            this.txtCurrentQuantity.Location = new System.Drawing.Point(90, 116);
            this.txtCurrentQuantity.Name = "txtCurrentQuantity";
            this.txtCurrentQuantity.ReadOnly = true;
            this.txtCurrentQuantity.Size = new System.Drawing.Size(180, 21);
            this.txtCurrentQuantity.TabIndex = 7;
            // 
            // lblScrapQuantity
            // 
            this.lblScrapQuantity.AutoSize = true;
            this.lblScrapQuantity.Location = new System.Drawing.Point(20, 30);
            this.lblScrapQuantity.Name = "lblScrapQuantity";
            this.lblScrapQuantity.Size = new System.Drawing.Size(53, 12);
            this.lblScrapQuantity.TabIndex = 0;
            this.lblScrapQuantity.Text = "报损数量";
            // 
            // numScrapQuantity
            // 
            this.numScrapQuantity.Location = new System.Drawing.Point(90, 26);
            this.numScrapQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numScrapQuantity.Name = "numScrapQuantity";
            this.numScrapQuantity.Size = new System.Drawing.Size(180, 21);
            this.numScrapQuantity.TabIndex = 1;
            this.numScrapQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblScrapReason
            // 
            this.lblScrapReason.AutoSize = true;
            this.lblScrapReason.Location = new System.Drawing.Point(20, 60);
            this.lblScrapReason.Name = "lblScrapReason";
            this.lblScrapReason.Size = new System.Drawing.Size(53, 12);
            this.lblScrapReason.TabIndex = 2;
            this.lblScrapReason.Text = "报损原因";
            // 
            // cmbScrapReason
            // 
            this.cmbScrapReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScrapReason.FormattingEnabled = true;
            this.cmbScrapReason.Items.AddRange(new object[] {
            "损坏报废",
            "丢失",
            "过期报废",
            "自然损耗",
            "质量问题",
            "其他原因"});
            this.cmbScrapReason.Location = new System.Drawing.Point(90, 56);
            this.cmbScrapReason.Name = "cmbScrapReason";
            this.cmbScrapReason.Size = new System.Drawing.Size(180, 20);
            this.cmbScrapReason.TabIndex = 3;
            // 
            // lblResponsiblePerson
            // 
            this.lblResponsiblePerson.AutoSize = true;
            this.lblResponsiblePerson.Location = new System.Drawing.Point(20, 90);
            this.lblResponsiblePerson.Name = "lblResponsiblePerson";
            this.lblResponsiblePerson.Size = new System.Drawing.Size(41, 12);
            this.lblResponsiblePerson.TabIndex = 4;
            this.lblResponsiblePerson.Text = "责任人";
            // 
            // txtResponsiblePerson
            // 
            this.txtResponsiblePerson.Location = new System.Drawing.Point(90, 86);
            this.txtResponsiblePerson.Name = "txtResponsiblePerson";
            this.txtResponsiblePerson.Size = new System.Drawing.Size(180, 21);
            this.txtResponsiblePerson.TabIndex = 5;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(20, 120);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(53, 12);
            this.lblRemark.TabIndex = 6;
            this.lblRemark.Text = "备注说明";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(90, 116);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(180, 50);
            this.txtRemark.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(80, 360);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 28);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "确认报损";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(180, 360);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblAssetID);
            this.groupBox1.Controls.Add(this.txtAssetID);
            this.groupBox1.Controls.Add(this.lblAssetName);
            this.groupBox1.Controls.Add(this.txtAssetName);
            this.groupBox1.Controls.Add(this.lblWarehouse);
            this.groupBox1.Controls.Add(this.txtWarehouse);
            this.groupBox1.Controls.Add(this.lblCurrentQuantity);
            this.groupBox1.Controls.Add(this.txtCurrentQuantity);
            this.groupBox1.Location = new System.Drawing.Point(15, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 150);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "资产信息";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblScrapQuantity);
            this.groupBox2.Controls.Add(this.numScrapQuantity);
            this.groupBox2.Controls.Add(this.lblScrapReason);
            this.groupBox2.Controls.Add(this.cmbScrapReason);
            this.groupBox2.Controls.Add(this.lblResponsiblePerson);
            this.groupBox2.Controls.Add(this.txtResponsiblePerson);
            this.groupBox2.Controls.Add(this.lblRemark);
            this.groupBox2.Controls.Add(this.txtRemark);
            this.groupBox2.Location = new System.Drawing.Point(15, 175);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(290, 175);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "报损信息";
            // 
            // ScrapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 405);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScrapForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "资产报损登记";
            ((System.ComponentModel.ISupportInitialize)(this.numScrapQuantity)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblAssetID;
        private System.Windows.Forms.TextBox txtAssetID;
        private System.Windows.Forms.Label lblAssetName;
        private System.Windows.Forms.TextBox txtAssetName;
        private System.Windows.Forms.Label lblWarehouse;
        private System.Windows.Forms.TextBox txtWarehouse;
        private System.Windows.Forms.Label lblCurrentQuantity;
        private System.Windows.Forms.TextBox txtCurrentQuantity;
        private System.Windows.Forms.Label lblScrapQuantity;
        private System.Windows.Forms.NumericUpDown numScrapQuantity;
        private System.Windows.Forms.Label lblScrapReason;
        private System.Windows.Forms.ComboBox cmbScrapReason;
        private System.Windows.Forms.Label lblResponsiblePerson;
        private System.Windows.Forms.TextBox txtResponsiblePerson;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}
