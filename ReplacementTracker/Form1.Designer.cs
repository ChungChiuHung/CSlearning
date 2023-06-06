namespace ReplacementTracker
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.plBaseTop = new System.Windows.Forms.Panel();
            this.plBaseMain = new System.Windows.Forms.Panel();
            this.lblHome = new System.Windows.Forms.Label();
            this.lblSetting = new System.Windows.Forms.Label();
            this.lblLogin = new System.Windows.Forms.Label();
            this.lblDown = new System.Windows.Forms.Label();
            this.lblUp = new System.Windows.Forms.Label();
            this.plBar = new System.Windows.Forms.Panel();
            this.plBaseTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // plBaseTop
            // 
            this.plBaseTop.Controls.Add(this.lblUp);
            this.plBaseTop.Controls.Add(this.lblDown);
            this.plBaseTop.Controls.Add(this.lblLogin);
            this.plBaseTop.Controls.Add(this.lblSetting);
            this.plBaseTop.Controls.Add(this.lblHome);
            this.plBaseTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.plBaseTop.Location = new System.Drawing.Point(0, 0);
            this.plBaseTop.Name = "plBaseTop";
            this.plBaseTop.Size = new System.Drawing.Size(1006, 79);
            this.plBaseTop.TabIndex = 0;
            // 
            // plBaseMain
            // 
            this.plBaseMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plBaseMain.Location = new System.Drawing.Point(0, 79);
            this.plBaseMain.Name = "plBaseMain";
            this.plBaseMain.Size = new System.Drawing.Size(1006, 642);
            this.plBaseMain.TabIndex = 1;
            // 
            // lblHome
            // 
            this.lblHome.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblHome.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHome.Location = new System.Drawing.Point(0, 0);
            this.lblHome.Name = "lblHome";
            this.lblHome.Size = new System.Drawing.Size(137, 79);
            this.lblHome.TabIndex = 0;
            this.lblHome.Text = "Home";
            this.lblHome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSetting
            // 
            this.lblSetting.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblSetting.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblSetting.Location = new System.Drawing.Point(853, 0);
            this.lblSetting.Name = "lblSetting";
            this.lblSetting.Size = new System.Drawing.Size(153, 79);
            this.lblSetting.TabIndex = 0;
            this.lblSetting.Text = "設定";
            this.lblSetting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLogin
            // 
            this.lblLogin.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblLogin.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblLogin.Location = new System.Drawing.Point(700, 0);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(153, 79);
            this.lblLogin.TabIndex = 1;
            this.lblLogin.Text = "登出/登入";
            this.lblLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDown
            // 
            this.lblDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDown.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblDown.Location = new System.Drawing.Point(547, 0);
            this.lblDown.Name = "lblDown";
            this.lblDown.Size = new System.Drawing.Size(153, 79);
            this.lblDown.TabIndex = 3;
            this.lblDown.Text = "下一頁";
            this.lblDown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUp
            // 
            this.lblUp.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblUp.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblUp.Location = new System.Drawing.Point(394, 0);
            this.lblUp.Name = "lblUp";
            this.lblUp.Size = new System.Drawing.Size(153, 79);
            this.lblUp.TabIndex = 4;
            this.lblUp.Text = "上一頁";
            this.lblUp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // plBar
            // 
            this.plBar.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.plBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.plBar.Location = new System.Drawing.Point(0, 79);
            this.plBar.Name = "plBar";
            this.plBar.Size = new System.Drawing.Size(1006, 3);
            this.plBar.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 721);
            this.Controls.Add(this.plBar);
            this.Controls.Add(this.plBaseMain);
            this.Controls.Add(this.plBaseTop);
            this.Name = "Form1";
            this.Text = "光組更換紀錄系統";
            this.plBaseTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel plBaseTop;
        private System.Windows.Forms.Panel plBaseMain;
        private System.Windows.Forms.Label lblSetting;
        private System.Windows.Forms.Label lblHome;
        private System.Windows.Forms.Label lblUp;
        private System.Windows.Forms.Label lblDown;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Panel plBar;
    }
}

