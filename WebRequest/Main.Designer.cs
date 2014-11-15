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
            this.lbl_result = new System.Windows.Forms.Label();
            this.btn_Browser = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_PwdGen = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ValidateImg)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_Output
            // 
            this.txt_Output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Output.Location = new System.Drawing.Point(3, 17);
            this.txt_Output.Multiline = true;
            this.txt_Output.Name = "txt_Output";
            this.txt_Output.Size = new System.Drawing.Size(936, 245);
            this.txt_Output.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
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
            // lbl_result
            // 
            this.lbl_result.AutoSize = true;
            this.lbl_result.Location = new System.Drawing.Point(383, 20);
            this.lbl_result.Name = "lbl_result";
            this.lbl_result.Size = new System.Drawing.Size(0, 12);
            this.lbl_result.TabIndex = 6;
            // 
            // btn_Browser
            // 
            this.btn_Browser.Location = new System.Drawing.Point(22, 64);
            this.btn_Browser.Name = "btn_Browser";
            this.btn_Browser.Size = new System.Drawing.Size(75, 23);
            this.btn_Browser.TabIndex = 7;
            this.btn_Browser.Text = "浏览";
            this.btn_Browser.UseVisualStyleBackColor = true;
            this.btn_Browser.Click += new System.EventHandler(this.btn_Browser_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_Output);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(942, 265);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "日志";
            // 
            // btn_PwdGen
            // 
            this.btn_PwdGen.Location = new System.Drawing.Point(142, 64);
            this.btn_PwdGen.Name = "btn_PwdGen";
            this.btn_PwdGen.Size = new System.Drawing.Size(75, 23);
            this.btn_PwdGen.TabIndex = 8;
            this.btn_PwdGen.Text = "随机密码";
            this.btn_PwdGen.UseVisualStyleBackColor = true;
            this.btn_PwdGen.Click += new System.EventHandler(this.btn_PwdGen_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 532);
            this.Controls.Add(this.btn_PwdGen);
            this.Controls.Add(this.btn_Browser);
            this.Controls.Add(this.lbl_result);
            this.Controls.Add(this.pic_ValidateImg);
            this.Controls.Add(this.btnGetImg);
            this.Controls.Add(this.btn_ValidatePhoneNumber);
            this.Controls.Add(this.panel1);
            this.Name = "Main";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_ValidateImg)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Output;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_ValidatePhoneNumber;
        private System.Windows.Forms.Button btnGetImg;
        private System.Windows.Forms.PictureBox pic_ValidateImg;
        private System.Windows.Forms.Label lbl_result;
        private System.Windows.Forms.Button btn_Browser;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_PwdGen;
    }
}

