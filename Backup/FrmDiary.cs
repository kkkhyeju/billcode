using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace PrintDiary
{
	/// <summary>
	/// 
	/// </summary>
    public partial class FrmDiary : Form
    {
        //"m"은 멤버변수라는 의미 " _  " 는 멤버하고 변수를 구분하기 위한것

        private Font m_MainFont = null; //Font 변수m_MainFont 값이 없음.
        private Font m_SubFont = null;//Font 변수m_SubFont 값이 없음
        private Font m_smallFont = null;//Font 변수m_smallFont 값이 없음.

        private PageSettings m_PageSetting = null;//페이지 세팅
        //PageSettings : 인쇄페이지에 적용할 설정을 지정

        private Bitmap m_BackBmp = null;
        private Bitmap[] m_Weather = new Bitmap[4];

        public FrmDiary()//생성자
        {
            InitializeComponent();//윈폼을 초기화 해주는 역할을 담당
            //작업

            m_MainFont = new Font("돋음", 15, FontStyle.Bold);//글자체,글자크기,글자스타일(굵게설정);
            m_SubFont = new Font("돋음체", 13);//글자체,글자크기
            m_smallFont = new Font("바탕체",10);//글자체,글자스타일

            //이미지 불러오기
            m_BackBmp = new Bitmap(GetType(),"LetterBackground.jpg");

            //날씨아이콘
            for(int i = 0; i<4;i++)
            {
                string strIcon = string.Format("Weather0{0}.gif", i + 1);
                m_Weather[i] = new Bitmap(GetType(), strIcon);
                    
            }
            CbWeather.SelectedIndex = 0;
            //CbWeather---> Form.날짜선택combobox
            
        }

        private void btn_PageSetting_Click(object sender, EventArgs e)//페이지설정
        {
            PageSetupDialog psd = new PageSetupDialog();
            //PageSetupDialog : 페이지관련 인쇄설정;

            if (this.m_PageSetting == null)//페이지 세팅이 안됬으면?
            {
                this.m_PageSetting = new PageSettings(); //새로 만든다.
            }
            psd.PageSettings = this.m_PageSetting; //psd = this 이 두개는 연동된다.
            psd.ShowDialog();//psd에 대화상자를 연다.
            
            

        }

		private void btn_Preview_Click(object sender, EventArgs e)
		{
			PrintDocument pd = new PrintDocument();// 필수
			
			pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);	// 프린트 페이지 이벤트헨들러
			
			if(this.m_PageSetting != null)
				pd.DefaultPageSettings = this.m_PageSetting;
				ㅁㄴㅇ
			PrintPreviewDialog ppd = new PrintPreviewDialog();
			ppd.Document = pd;
			ppd.ShowDialog();
			
		}

		void pd_PrintPage(object sender, PrintPageEventArgs e)
		{
			Graphics g = e.Graphics;
			PrintDocument(g);
			e.HasMorePages = false;
		}
		/// <summary>
		/// 2008.11.21 AM 10.45 작성
		/// 실제 화면 그리는 메서드
		/// </summary>
		/// <param name="g">GDI+ 매개변수</param>
		private void PrintDocument(Graphics g)
		{
			g.FillRectangle(Brushes.White, 100,50,600,800); //전체 사각형
			g.DrawImage(m_BackBmp,100,50); //배경이미지 그리기
			g.DrawImage(m_Weather[CbWeather.SelectedIndex],380,120); //날씨 아이콘
			g.DrawString(CbWeather.SelectedItem.ToString(), this.m_MainFont, Brushes.Black, 375, 160);//날씨 글자 출력
			g.DrawString(this.dtpToday.Text , this.m_SubFont, Brushes.Brown,380,236); //날짜 출력
			
			StringFormat sf = new StringFormat();
			sf.Alignment = StringAlignment.Near;//	세로정렬
			sf.LineAlignment = StringAlignment.Center; //  가로정렬
			Rectangle rect = new Rectangle(100,280,500,this.m_MainFont.Height *3);// 글출력하는 영역생성
			g.DrawString(this.txtTitle.Text, this.m_MainFont, Brushes.Black, rect , sf);// 내용출력
			g.DrawString(this.txtContens.Text, this.m_SubFont, Brushes.Black ,rect); // 내용출력
			
		}
    }
}