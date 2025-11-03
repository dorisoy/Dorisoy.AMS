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
            this.lblBorrowedBy = new System.Windows.Forms.Label();
            this.txtBorrowedBy = new System.Windows.Forms.TextBox();
            this.lblReason = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.lblBorrowDate = new System.Windows.Forms.Label();
            this.dtpBorrowDate = new System.Windows.Forms.DateTimePicker();
            this.lblReturnDate = new System.Windows.Forms.Label();
            this.dtpReturnDate = new System.Windows.Forms.DateTimePicker();
            this.lblAssetList = new System.Windows.Forms.Label();
            this.txtAssetList = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblBorrowedBy
            // 
            this.lblBorrowedBy.AutoSize = true;
            this.lblBorrowedBy.Location = new System.Drawing.Point(20, 20);
            this.lblBorrowedBy.Name = "lblBorrowedBy";
            this.lblBorrowedBy.Size = new System.Drawing.Size(53, 12);
            this.lblBorrowedBy.TabIndex = 0;
            this.lblBorrowedBy.Text = "借用人：";
            // 
            // txtBorrowedBy
            // 
            this.txtBorrowedBy.Location = new System.Drawing.Point(130, 20);
            this.txtBorrowedBy.Name = "txtBorrowedBy";
            this.txtBorrowedBy.Size = new System.Drawing.Size(300, 21);
            this.txtBorrowedBy.TabIndex = 1;
            // 
            // lblReason
            // 
            this.lblReason.AutoSize = true;
            this.lblReason.Location = new System.Drawing.Point(20, 70);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(53, 12);
            this.lblReason.TabIndex = 2;
            this.lblReason.Text = "借用原因：";
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(130, 70);
            this.txtReason.Name = "txtReason";
            this.txtReason.Multiline = true;
            this.txtReason.Size = new System.Drawing.Size(300, 60);
            this.txtReason.TabIndex = 3;
            // 
            // lblBorrowDate
            // 
            this.lblBorrowDate.AutoSize = true;
            this.lblBorrowDate.Location = new System.Drawing.Point(20, 150);
            this.lblBorrowDate.Name = "lblBorrowDate";
            this.lblBorrowDate.Size = new System.Drawing.Size(53, 12);
            this.lblBorrowDate.TabIndex = 4;
            this.lblBorrowDate.Text = "借用日期：";
            // 
            // dtpBorrowDate
            // 
            this.dtpBorrowDate.Location = new System.Drawing.Point(130, 150);
            this.dtpBorrowDate.Name = "dtpBorrowDate";
            this.dtpBorrowDate.Size = new System.Drawing.Size(300, 21);
            this.dtpBorrowDate.TabIndex = 5;
            // 
            // lblReturnDate
            // 
            this.lblReturnDate.AutoSize = true;
            this.lblReturnDate.Location = new System.Drawing.Point(20, 200);
            this.lblReturnDate.Name = "lblReturnDate";
            this.lblReturnDate.Size = new System.Drawing.Size(77, 12);
            this.lblReturnDate.TabIndex = 6;
            this.lblReturnDate.Text = "预期归还日期：";
            // 
            // dtpReturnDate
            // 
            this.dtpReturnDate.Location = new System.Drawing.Point(130, 200);
            this.dtpReturnDate.Name = "dtpReturnDate";
            this.dtpReturnDate.Size = new System.Drawing.Size(300, 21);
            this.dtpReturnDate.TabIndex = 7;
            // 
            // lblAssetList
            // 
            this.lblAssetList.AutoSize = true;
            this.lblAssetList.Location = new System.Drawing.Point(20, 250);
            this.lblAssetList.Name = "lblAssetList";
            this.lblAssetList.Size = new System.Drawing.Size(53, 12);
            this.lblAssetList.TabIndex = 8;
            this.lblAssetList.Text = "借用资产：";
            // 
            // txtAssetList
            // 
            this.txtAssetList.Location = new System.Drawing.Point(130, 250);
            this.txtAssetList.Name = "txtAssetList";
            this.txtAssetList.Multiline = true;
            this.txtAssetList.ReadOnly = true;
            this.txtAssetList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAssetList.Size = new System.Drawing.Size(300, 100);
            this.txtAssetList.TabIndex = 9;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(150, 370);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(100, 40);
            this.btnConfirm.TabIndex = 10;
            this.btnConfirm.Text = "确认借用";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(280, 370);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // BorrowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 430);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtAssetList);
            this.Controls.Add(this.lblAssetList);
            this.Controls.Add(this.dtpReturnDate);
            this.Controls.Add(this.lblReturnDate);
            this.Controls.Add(this.dtpBorrowDate);
            this.Controls.Add(this.lblBorrowDate);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.lblReason);
            this.Controls.Add(this.txtBorrowedBy);
            this.Controls.Add(this.lblBorrowedBy);
            this.Name = "BorrowForm";
            this.Text = "资产借用";
            this.Load += new System.EventHandler(this.BorrowForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBorrowedBy;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.Label lblBorrowDate;
        private System.Windows.Forms.Label lblReturnDate;
        private System.Windows.Forms.Label lblAssetList;
        public System.Windows.Forms.TextBox txtBorrowedBy;
        public System.Windows.Forms.TextBox txtReason;
        public System.Windows.Forms.DateTimePicker dtpBorrowDate;
        public System.Windows.Forms.DateTimePicker dtpReturnDate;
        public System.Windows.Forms.TextBox txtAssetList;
        public System.Windows.Forms.Button btnConfirm;
        public System.Windows.Forms.Button btnCancel;
    }
}
