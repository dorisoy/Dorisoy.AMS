namespace Dorisoy.AMS.view
{
    partial class RuleEditForm
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
            panel1 = new Panel();
            label4 = new Label();
            btnClose = new Button();
            btnSave = new Button();
            numNumberLength = new NumericUpDown();
            label3 = new Label();
            txtPrefix = new TextBox();
            label2 = new Label();
            txtKeywords = new TextBox();
            label1 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numNumberLength).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label4);
            panel1.Controls.Add(btnClose);
            panel1.Controls.Add(btnSave);
            panel1.Controls.Add(numNumberLength);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(txtPrefix);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(txtKeywords);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(15, 17);
            panel1.Margin = new Padding(4, 4, 4, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(516, 285);
            panel1.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.Red;
            label4.Location = new Point(303, 33);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(177, 17);
            label4.TabIndex = 8;
            label4.Text = "如果有多个关键字用逗号\",\"分隔";
            // 
            // btnClose
            // 
            btnClose.Location = new Point(196, 215);
            btnClose.Margin = new Padding(4, 4, 4, 4);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(88, 33);
            btnClose.TabIndex = 7;
            btnClose.Text = "取消";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(77, 215);
            btnSave.Margin = new Padding(4, 4, 4, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(88, 33);
            btnSave.TabIndex = 6;
            btnSave.Text = "保存";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // numNumberLength
            // 
            numNumberLength.Location = new Point(77, 144);
            numNumberLength.Margin = new Padding(4, 4, 4, 4);
            numNumberLength.Name = "numNumberLength";
            numNumberLength.Size = new Size(206, 23);
            numNumberLength.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 144);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(32, 17);
            label3.TabIndex = 4;
            label3.Text = "位数";
            // 
            // txtPrefix
            // 
            txtPrefix.Location = new Point(77, 84);
            txtPrefix.Margin = new Padding(4, 4, 4, 4);
            txtPrefix.Name = "txtPrefix";
            txtPrefix.Size = new Size(206, 23);
            txtPrefix.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 88);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(32, 17);
            label2.TabIndex = 2;
            label2.Text = "前缀";
            // 
            // txtKeywords
            // 
            txtKeywords.Location = new Point(77, 28);
            txtKeywords.Margin = new Padding(4, 4, 4, 4);
            txtKeywords.Name = "txtKeywords";
            txtKeywords.Size = new Size(206, 23);
            txtKeywords.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 33);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(44, 17);
            label1.TabIndex = 0;
            label1.Text = "关键字";
            // 
            // RuleEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(541, 332);
            Controls.Add(panel1);
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            Name = "RuleEditForm";
            Text = "角色编辑";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numNumberLength).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKeywords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numNumberLength;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label4;
    }
}