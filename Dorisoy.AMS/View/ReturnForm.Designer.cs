namespace Dorisoy.AMS.view
{
    partial class ReturnForm
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
            this.lblReturnDate = new System.Windows.Forms.Label();
            this.dtpReturnDate = new System.Windows.Forms.DateTimePicker();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblAssetList = new System.Windows.Forms.Label();
            this.txtAssetList = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblReturnDate
            // 
            this.lblReturnDate.AutoSize = true;
            this.lblReturnDate.Location = new System.Drawing.Point(20, 20);
            this.lblReturnDate.Name = "lblReturnDate";
            this.lblReturnDate.Size = new System.Drawing.Size(53, 12);
            this.lblReturnDate.TabIndex = 0;
            this.lblReturnDate.Text = "归还日期：";
            // 
            // dtpReturnDate
            // 
            this.dtpReturnDate.Location = new System.Drawing.Point(130, 20);
            this.dtpReturnDate.Name = "dtpReturnDate";
            this.dtpReturnDate.Size = new System.Drawing.Size(300, 21);
            this.dtpReturnDate.TabIndex = 1;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(20, 70);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(53, 12);
            this.lblRemark.TabIndex = 2;
            this.lblRemark.Text = "归还备注：";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(130, 70);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Multiline = true;
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemark.Size = new System.Drawing.Size(300, 80);
            this.txtRemark.TabIndex = 3;
            // 
            // lblAssetList
            // 
            this.lblAssetList.AutoSize = true;
            this.lblAssetList.Location = new System.Drawing.Point(20, 170);
            this.lblAssetList.Name = "lblAssetList";
            this.lblAssetList.Size = new System.Drawing.Size(53, 12);
            this.lblAssetList.TabIndex = 4;
            this.lblAssetList.Text = "归还资产：";
            // 
            // txtAssetList
            // 
            this.txtAssetList.Location = new System.Drawing.Point(130, 170);
            this.txtAssetList.Name = "txtAssetList";
            this.txtAssetList.Multiline = true;
            this.txtAssetList.ReadOnly = true;
            this.txtAssetList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAssetList.Size = new System.Drawing.Size(300, 120);
            this.txtAssetList.TabIndex = 5;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(150, 310);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(100, 40);
            this.btnConfirm.TabIndex = 6;
            this.btnConfirm.Text = "确认归还";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(280, 310);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ReturnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 370);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtAssetList);
            this.Controls.Add(this.lblAssetList);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.lblRemark);
            this.Controls.Add(this.dtpReturnDate);
            this.Controls.Add(this.lblReturnDate);
            this.Name = "ReturnForm";
            this.Text = "资产归还";
            this.Load += new System.EventHandler(this.ReturnForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblReturnDate;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.Label lblAssetList;
        public System.Windows.Forms.DateTimePicker dtpReturnDate;
        public System.Windows.Forms.TextBox txtRemark;
        public System.Windows.Forms.TextBox txtAssetList;
        public System.Windows.Forms.Button btnConfirm;
        public System.Windows.Forms.Button btnCancel;
    }
}
