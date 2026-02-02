namespace Dorisoy.AMS.view
{
    partial class ScanBarcodeForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panel1 = new Panel();
            btnClose = new Button();
            lblStatus = new Label();
            picCamera = new PictureBox();
            groupBox1 = new GroupBox();
            lblAssetInfo = new Label();
            panel2 = new Panel();
            btnReturn = new Button();
            btnBorrow = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picCamera).BeginInit();
            groupBox1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(lblStatus);
            panel1.Controls.Add(btnClose);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 482);
            panel1.Name = "panel1";
            panel1.Size = new Size(784, 40);
            panel1.TabIndex = 0;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Location = new Point(697, 8);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 27);
            btnClose.TabIndex = 0;
            btnClose.Text = "关闭";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("微软雅黑", 10F);
            lblStatus.ForeColor = Color.Blue;
            lblStatus.Location = new Point(12, 11);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(135, 20);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "正在启动摄像头...";
            // 
            // picCamera
            // 
            picCamera.BackColor = Color.Black;
            picCamera.Dock = DockStyle.Fill;
            picCamera.Location = new Point(0, 0);
            picCamera.Name = "picCamera";
            picCamera.Size = new Size(584, 482);
            picCamera.SizeMode = PictureBoxSizeMode.Zoom;
            picCamera.TabIndex = 1;
            picCamera.TabStop = false;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(panel2);
            groupBox1.Controls.Add(lblAssetInfo);
            groupBox1.Dock = DockStyle.Right;
            groupBox1.Location = new Point(584, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(200, 482);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "扫描结果";
            // 
            // lblAssetInfo
            // 
            lblAssetInfo.Dock = DockStyle.Fill;
            lblAssetInfo.Font = new Font("微软雅黑", 9F);
            lblAssetInfo.Location = new Point(3, 19);
            lblAssetInfo.Name = "lblAssetInfo";
            lblAssetInfo.Padding = new Padding(5);
            lblAssetInfo.Size = new Size(194, 460);
            lblAssetInfo.TabIndex = 0;
            lblAssetInfo.Text = "请将条码对准摄像头进行扫描...";
            // 
            // panel2
            // 
            panel2.Controls.Add(btnReturn);
            panel2.Controls.Add(btnBorrow);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(3, 409);
            panel2.Name = "panel2";
            panel2.Size = new Size(194, 70);
            panel2.TabIndex = 1;
            // 
            // btnBorrow
            // 
            btnBorrow.Enabled = false;
            btnBorrow.Location = new Point(10, 10);
            btnBorrow.Name = "btnBorrow";
            btnBorrow.Size = new Size(80, 50);
            btnBorrow.TabIndex = 0;
            btnBorrow.Text = "借出";
            btnBorrow.UseVisualStyleBackColor = true;
            btnBorrow.Click += btnBorrow_Click;
            // 
            // btnReturn
            // 
            btnReturn.Enabled = false;
            btnReturn.Location = new Point(100, 10);
            btnReturn.Name = "btnReturn";
            btnReturn.Size = new Size(80, 50);
            btnReturn.TabIndex = 1;
            btnReturn.Text = "归还";
            btnReturn.UseVisualStyleBackColor = true;
            btnReturn.Click += btnReturn_Click;
            // 
            // ScanBarcodeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 522);
            Controls.Add(picCamera);
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            MinimizeBox = false;
            Name = "ScanBarcodeForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "扫码借还";
            FormClosing += ScanBarcodeForm_FormClosing;
            Load += ScanBarcodeForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picCamera).EndInit();
            groupBox1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Panel panel1;
        private Button btnClose;
        private Label lblStatus;
        private PictureBox picCamera;
        private GroupBox groupBox1;
        private Label lblAssetInfo;
        private Panel panel2;
        private Button btnBorrow;
        private Button btnReturn;
    }
}
