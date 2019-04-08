namespace PrintDiary
{
    partial class FrmDiary
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
			this.lblDate = new System.Windows.Forms.Label();
			this.lblWeather = new System.Windows.Forms.Label();
			this.lblTitle = new System.Windows.Forms.Label();
			this.lbl_Contens = new System.Windows.Forms.Label();
			this.dtpToday = new System.Windows.Forms.DateTimePicker();
			this.CbWeather = new System.Windows.Forms.ComboBox();
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.txtContens = new System.Windows.Forms.TextBox();
			this.btn_PageSetting = new System.Windows.Forms.Button();
			this.btn_Preview = new System.Windows.Forms.Button();
			this.btn_Print = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblDate
			// 
			this.lblDate.AutoSize = true;
			this.lblDate.Location = new System.Drawing.Point(12, 9);
			this.lblDate.Name = "lblDate";
			this.lblDate.Size = new System.Drawing.Size(65, 12);
			this.lblDate.TabIndex = 0;
			this.lblDate.Text = "날짜 선택 :";
			// 
			// lblWeather
			// 
			this.lblWeather.AutoSize = true;
			this.lblWeather.Location = new System.Drawing.Point(12, 34);
			this.lblWeather.Name = "lblWeather";
			this.lblWeather.Size = new System.Drawing.Size(65, 12);
			this.lblWeather.TabIndex = 0;
			this.lblWeather.Text = "날씨 선택 :";
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.Location = new System.Drawing.Point(12, 59);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(65, 12);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "제목 작성 :";
			// 
			// lbl_Contens
			// 
			this.lbl_Contens.AutoSize = true;
			this.lbl_Contens.Location = new System.Drawing.Point(12, 84);
			this.lbl_Contens.Name = "lbl_Contens";
			this.lbl_Contens.Size = new System.Drawing.Size(65, 12);
			this.lbl_Contens.TabIndex = 0;
			this.lbl_Contens.Text = "내용 입력 :";
			// 
			// dtpToday
			// 
			this.dtpToday.Location = new System.Drawing.Point(79, 5);
			this.dtpToday.Name = "dtpToday";
			this.dtpToday.Size = new System.Drawing.Size(164, 21);
			this.dtpToday.TabIndex = 1;
			// 
			// CbWeather
			// 
			this.CbWeather.FormattingEnabled = true;
			this.CbWeather.Items.AddRange(new object[] {
            "맑음",
            "흐림",
            "비",
            "눈"});
			this.CbWeather.Location = new System.Drawing.Point(79, 30);
			this.CbWeather.Name = "CbWeather";
			this.CbWeather.Size = new System.Drawing.Size(164, 20);
			this.CbWeather.TabIndex = 2;
			// 
			// txtTitle
			// 
			this.txtTitle.Location = new System.Drawing.Point(79, 55);
			this.txtTitle.MaxLength = 120;
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.Size = new System.Drawing.Size(360, 21);
			this.txtTitle.TabIndex = 3;
			// 
			// txtContens
			// 
			this.txtContens.Location = new System.Drawing.Point(79, 81);
			this.txtContens.Multiline = true;
			this.txtContens.Name = "txtContens";
			this.txtContens.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtContens.Size = new System.Drawing.Size(359, 200);
			this.txtContens.TabIndex = 4;
			// 
			// btn_PageSetting
			// 
			this.btn_PageSetting.Location = new System.Drawing.Point(179, 287);
			this.btn_PageSetting.Name = "btn_PageSetting";
			this.btn_PageSetting.Size = new System.Drawing.Size(97, 23);
			this.btn_PageSetting.TabIndex = 5;
			this.btn_PageSetting.Text = "페이지 설정";
			this.btn_PageSetting.UseVisualStyleBackColor = true;
			this.btn_PageSetting.Click += new System.EventHandler(this.btn_PageSetting_Click);
			// 
			// btn_Preview
			// 
			this.btn_Preview.Location = new System.Drawing.Point(282, 287);
			this.btn_Preview.Name = "btn_Preview";
			this.btn_Preview.Size = new System.Drawing.Size(75, 23);
			this.btn_Preview.TabIndex = 6;
			this.btn_Preview.Text = "미리 보기";
			this.btn_Preview.UseVisualStyleBackColor = true;
			this.btn_Preview.Click += new System.EventHandler(this.btn_Preview_Click);
			// 
			// btn_Print
			// 
			this.btn_Print.Location = new System.Drawing.Point(363, 287);
			this.btn_Print.Name = "btn_Print";
			this.btn_Print.Size = new System.Drawing.Size(75, 23);
			this.btn_Print.TabIndex = 7;
			this.btn_Print.Text = "인쇄하기";
			this.btn_Print.UseVisualStyleBackColor = true;
			// 
			// FrmDiary
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(451, 322);
			this.Controls.Add(this.btn_Print);
			this.Controls.Add(this.btn_Preview);
			this.Controls.Add(this.btn_PageSetting);
			this.Controls.Add(this.txtContens);
			this.Controls.Add(this.txtTitle);
			this.Controls.Add(this.CbWeather);
			this.Controls.Add(this.dtpToday);
			this.Controls.Add(this.lbl_Contens);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.lblWeather);
			this.Controls.Add(this.lblDate);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmDiary";
			this.Text = "일기장";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblWeather;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lbl_Contens;
        private System.Windows.Forms.DateTimePicker dtpToday;
        private System.Windows.Forms.ComboBox CbWeather;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtContens;
        private System.Windows.Forms.Button btn_PageSetting;
        private System.Windows.Forms.Button btn_Preview;
        private System.Windows.Forms.Button btn_Print;
    }
}

