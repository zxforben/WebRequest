namespace XXX
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_Output = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_ValidatePhoneNumber = new System.Windows.Forms.Button();
            this.btnGetImg = new System.Windows.Forms.Button();
            this.pic_ValidateImg = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ValidateImg)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_Output
            // 
            this.txt_Output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Output.Location = new System.Drawing.Point(0, 0);
            this.txt_Output.Multiline = true;
            this.txt_Output.Name = "txt_Output";
            this.txt_Output.Size = new System.Drawing.Size(942, 265);
            this.txt_Output.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txt_Output);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 267);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(942, 265);
            this.panel1.TabIndex = 2;
            // 
            // btn_ValidatePhoneNumber
            // 
            this.btn_ValidatePhoneNumber.Location = new System.Drawing.Point(13, 13);
            this.btn_ValidatePhoneNumber.Name = "btn_ValidatePhoneNumber";
            this.btn_ValidatePhoneNumber.Size = new System.Drawing.Size(105, 25);
            this.btn_ValidatePhoneNumber.TabIndex = 3;
            this.btn_ValidatePhoneNumber.Text = "验证手机";
            this.btn_ValidatePhoneNumber.UseVisualStyleBackColor = true;
            this.btn_ValidatePhoneNumber.Click += new System.EventHandler(this.btn_ValidatePhoneNumber_Click);
            // 
            // btnGetImg
            // 
            this.btnGetImg.Location = new System.Drawing.Point(159, 15);
            this.btnGetImg.Name = "btnGetImg";
            this.btnGetImg.Size = new System.Drawing.Size(99, 23);
            this.btnGetImg.TabIndex = 4;
            this.btnGetImg.Text = "获取图片";
            this.btnGetImg.UseVisualStyleBackColor = true;
            this.btnGetImg.Click += new System.EventHandler(this.btn_GetImg_Click);
            // 
            // pic_ValidateImg
            // 
            this.pic_ValidateImg.Location = new System.Drawing.Point(293, 16);
            this.pic_ValidateImg.Name = "pic_ValidateImg";
            this.pic_ValidateImg.Size = new System.Drawing.Size(60, 22);
            this.pic_ValidateImg.TabIndex = 5;
            this.pic_ValidateImg.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 532);
            this.Controls.Add(this.pic_ValidateImg);
            this.Controls.Add(this.btnGetImg);
            this.Controls.Add(this.btn_ValidatePhoneNumber);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ValidateImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Output;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_ValidatePhoneNumber;
        private System.Windows.Forms.Button btnGetImg;
        private System.Windows.Forms.PictureBox pic_ValidateImg;
    }
}

