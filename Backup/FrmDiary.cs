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
        //"m"�� ���������� �ǹ� " _  " �� ����ϰ� ������ �����ϱ� ���Ѱ�

        private Font m_MainFont = null; //Font ����m_MainFont ���� ����.
        private Font m_SubFont = null;//Font ����m_SubFont ���� ����
        private Font m_smallFont = null;//Font ����m_smallFont ���� ����.

        private PageSettings m_PageSetting = null;//������ ����
        //PageSettings : �μ��������� ������ ������ ����

        private Bitmap m_BackBmp = null;
        private Bitmap[] m_Weather = new Bitmap[4];

        public FrmDiary()//������
        {
            InitializeComponent();//������ �ʱ�ȭ ���ִ� ������ ���
            //�۾�

            m_MainFont = new Font("����", 15, FontStyle.Bold);//����ü,����ũ��,���ڽ�Ÿ��(���Լ���);
            m_SubFont = new Font("����ü", 13);//����ü,����ũ��
            m_smallFont = new Font("����ü",10);//����ü,���ڽ�Ÿ��

            //�̹��� �ҷ�����
            m_BackBmp = new Bitmap(GetType(),"LetterBackground.jpg");

            //����������
            for(int i = 0; i<4;i++)
            {
                string strIcon = string.Format("Weather0{0}.gif", i + 1);
                m_Weather[i] = new Bitmap(GetType(), strIcon);
                    
            }
            CbWeather.SelectedIndex = 0;
            //CbWeather---> Form.��¥����combobox
            
        }

        private void btn_PageSetting_Click(object sender, EventArgs e)//����������
        {
            PageSetupDialog psd = new PageSetupDialog();
            //PageSetupDialog : ���������� �μ⼳��;

            if (this.m_PageSetting == null)//������ ������ �ȉ�����?
            {
                this.m_PageSetting = new PageSettings(); //���� �����.
            }
            psd.PageSettings = this.m_PageSetting; //psd = this �� �ΰ��� �����ȴ�.
            psd.ShowDialog();//psd�� ��ȭ���ڸ� ����.
            
            

        }

		private void btn_Preview_Click(object sender, EventArgs e)
		{
			PrintDocument pd = new PrintDocument();// �ʼ�
			
			pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);	// ����Ʈ ������ �̺�Ʈ��鷯
			
			if(this.m_PageSetting != null)
				pd.DefaultPageSettings = this.m_PageSetting;
				������
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
		/// 2008.11.21 AM 10.45 �ۼ�
		/// ���� ȭ�� �׸��� �޼���
		/// </summary>
		/// <param name="g">GDI+ �Ű�����</param>
		private void PrintDocument(Graphics g)
		{
			g.FillRectangle(Brushes.White, 100,50,600,800); //��ü �簢��
			g.DrawImage(m_BackBmp,100,50); //����̹��� �׸���
			g.DrawImage(m_Weather[CbWeather.SelectedIndex],380,120); //���� ������
			g.DrawString(CbWeather.SelectedItem.ToString(), this.m_MainFont, Brushes.Black, 375, 160);//���� ���� ���
			g.DrawString(this.dtpToday.Text , this.m_SubFont, Brushes.Brown,380,236); //��¥ ���
			
			StringFormat sf = new StringFormat();
			sf.Alignment = StringAlignment.Near;//	��������
			sf.LineAlignment = StringAlignment.Center; //  ��������
			Rectangle rect = new Rectangle(100,280,500,this.m_MainFont.Height *3);// ������ϴ� ��������
			g.DrawString(this.txtTitle.Text, this.m_MainFont, Brushes.Black, rect , sf);// �������
			g.DrawString(this.txtContens.Text, this.m_SubFont, Brushes.Black ,rect); // �������
			
		}
    }
}