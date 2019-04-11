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



namespace WindowsFormsApp9
{
    public partial class Form1 : Form
    {
        SoundPlayer player;
        MySqlConnection connection =
        new MySqlConnection("Server=localhost;Database=billcode;Uid=root;Pwd=q1w2e3r412;");
        public Form1()
        {
            player = new SoundPlayer();
            InitializeComponent();
            printPreviewDialog1.Document = printDocument1;

            System.Drawing.Font Font1 = new Font("굴림", 11);
            System.Drawing.Font m_MainFont = new Font("돋음", 11);
        }

        //데이터 베이스 연결 
        private void Form1_Load(object sender, EventArgs e)
        {

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
            pic.Image = code.GetGraphic(2);


        }
        //인쇄버튼 
        private void button4_Click(object sender, EventArgs e)
        {

            PrintDialog pd = new PrintDialog();
            PrintDocument doc = new PrintDocument();


            PaperSize pkCustomSize = new PaperSize("First custom size", 200, 800);
            doc.DefaultPageSettings.PaperSize = pkCustomSize;


            doc.PrintPage += printDocument1_PrintPage;
            if (pd.ShowDialog() == DialogResult.OK)
            {
                doc.Print();
            }

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(pic.Width, pic.Height);
            string bm_size = pic.Width.ToString();
            Graphics g = e.Graphics;
            Point drawPoint = new Point(5, 5);
            Font font = new Font("굴림", 15, System.Drawing.FontStyle.Bold);
            Font font1 = new Font("굴림", 8, System.Drawing.FontStyle.Bold);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            //QR사이즈 알아보기 위함
            g.DrawString(bm_size, font1, drawBrush, 0, 0);


            g.DrawString("---BillCode---", font, drawBrush, drawPoint);
            int DrawPlus = 19;//글자 줄맞추는데 사용함

            string txtBar = txtQRCode.Text;
            txtBar = txtBar.Substring(0, txtBar.LastIndexOf(','));
            string[] result = txtBar.Split(',');

            for (int i = 0; i < result.Length; i++)
            {
                string bar_num = result[i];
                string bar_num_sub = bar_num.Substring(7, 5);
                //DB연결부분 
                string strSelect = "SELECT name, price FROM billcode_db WHERE no='" + bar_num_sub + "';";
                connection.Open();
                MySqlCommand command = new MySqlCommand(strSelect, connection);
                MySqlDataReader rdr = command.ExecuteReader();
                string str_Name = "";
                string str_No = "";
                while (rdr.Read())
                {
                    str_Name = string.Format("{0}", rdr["name"]);
                    str_No = string.Format("{0} ", rdr["price"]);
                }
                g.DrawString(str_Name.PadRight(10, '　') + str_No, font1, drawBrush, 10, 10 + DrawPlus);
                connection.Close();
                
                DrawPlus += 10;
            }

            pic.DrawToBitmap(bm, new Rectangle(0, 0, pic.Width, pic.Height));
            e.Graphics.DrawImage(bm, 0, DrawPlus + 20);
            bm.Dispose();

        }

        //인쇄 미리보기 설정
        private void btn_Preview_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();// 필수

            pd.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);    // 프린트 페이지 이벤트헨들러

            PrintPreviewDialog ppd = new PrintPreviewDialog();

            //미리보기 용지 크기
            pd.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("First Custom size", 200, 800);
            ppd.Document = pd;
            ppd.ShowDialog();

        }

    }
}

