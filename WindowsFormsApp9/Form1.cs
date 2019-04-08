using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using ZXing;
using System.Media;
using QRCoder;
using System.Drawing.Printing;
using MySql.Data.MySqlClient;
using System.Drawing.Drawing2D;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace WindowsFormsApp9
{
    public partial class Form1 : Form
    {
        SoundPlayer player;

        public Form1()
        {
            player = new SoundPlayer();
            InitializeComponent();
            printPreviewDialog1.Document = printDocument1;

            System.Drawing.Font Font1 = new Font("굴림", 11);
            System.Drawing.Font m_MainFont = new Font("돋음", 11);
        }
        MySqlConnection conn;

        //데이터 베이스 연결 
        private void Form1_Load(object sender, EventArgs e)
        {
            string connStr = string.Format(@"server=localhost;
                                              user=root;
                                              password=q1w2e3r412;
                                              database=billcode");
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
                MessageBox.Show("MySQL 연결 성공");
            }
            catch
            {
                conn.Close();
                MessageBox.Show("MySQL 연결 실패");
                Application.OpenForms["MainForm"].Close();      // 실패시 폼을 닫아준다.
            }
        }
        //사진불러오기
        private void button1_Click(object sender, EventArgs e)
        {

            txtQRCode.ResetText();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image file(*.png,*.jpeg,*.jpg)|*.png;*jpeg; *.jpg";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(dialog.FileName);
            }
        }
        public string decoded;
        //바코드 읽어서 가져오기
        private void button2_Click(object sender, EventArgs e)
        {
            BarcodeReader reader = new BarcodeReader();
            Result result = reader.Decode((Bitmap)pictureBox1.Image);
            if (result != null)
            {
                decoded += result.ToString() + ",";
                if (decoded != "")
                {
                    player.Play();
                    txtQRCode.Text = decoded;
                }
            }
            else
                MessageBox.Show("EROOR", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            player.Stop();

        }
        //큐알코드 나타내기
        private void button3_Click(object sender, EventArgs e)
        {
            QRCodeGenerator qr = new QRCodeGenerator();
            QRCodeData data = qr.CreateQrCode(txtQRCode.Text, QRCodeGenerator.ECCLevel.Q);
            QRCode code = new QRCode(data);
            if (txtQRCode.Text.Length == 300)
            {
                pic.Image = code.GetGraphic(4);
            }
            else
                pic.Image = code.GetGraphic(2);

        }
        //인쇄버튼 
        private void button4_Click(object sender, EventArgs e)
        {

            PrintDialog pd = new PrintDialog();
            PrintDocument doc = new PrintDocument();

            doc.PrintPage += printDocument1_PrintPage;
            if (pd.ShowDialog() == DialogResult.OK)
            {
                doc.Print();
            }

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(pic.Width, pic.Height);
            Graphics g = e.Graphics;
            Point drawPoint = new Point(10, 10);
            Font font = new Font("Lucida Console", 20,System.Drawing.FontStyle.Bold);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            g.DrawString("BillCode", font, drawBrush, drawPoint);
           // int DrawPlus = 0;

           // BarcodeReader reader = new BarcodeReader();
           // Result result = reader.Decode((Bitmap)pictureBox1.Image);
           // string strSelect = "SELECT * FROM billcode.billcode";
           //MySqlCommand cmd = new MySqlCommand(strSelect)

            g.DrawString("BillCode", font, drawBrush, drawPoint);
            pic.DrawToBitmap(bm, new Rectangle(0, 20, pic.Width, pic.Height));
            e.Graphics.DrawImage(bm, 0, 100);
            bm.Dispose();
        }

        //인쇄 미리보기 설정
        private void btn_Preview_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();// 필수

            pd.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);    // 프린트 페이지 이벤트헨들러
            
            PrintPreviewDialog ppd = new PrintPreviewDialog();
            ppd.Document = pd;
            ppd.ShowDialog();

        }
       
    }
}

