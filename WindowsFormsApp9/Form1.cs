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
using System.Diagnostics;
using System.IO.Ports;

namespace WindowsFormsApp9
{
    public partial class Form1 : Form
    {
        SoundPlayer player;
        MySqlConnection connection =
        new MySqlConnection("Server = 3.93.3.192; Database = billcode_db; Uid = billcode; Pwd = 201301557aws;");


        public Form1()
        {
            player = new SoundPlayer();
            InitializeComponent();
            printPreviewDialog1.Document = printDocument1;

            System.Drawing.Font Font1 = new Font("굴림", 11);
            System.Drawing.Font m_MainFont = new Font("돋음", 11);
            
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string bar_num = textBox1.Text;

                string code = bar_num.Substring(0, 13);
                string full_day = bar_num.Substring(13, 6);

                string strSelect = "SELECT product_name, product_price FROM products_2 WHERE product_code = '" + code + "';";
                connection.Open();
                MySqlCommand command = new MySqlCommand(strSelect, connection);
                MySqlDataReader rdr = command.ExecuteReader();

                String[] arr = new String[4];

                //유통기한
                string s2 = "20" + full_day;
                string year = s2.Substring(0, 4);
                string month = s2.Substring(4, 2);
                string day = s2.Substring(6, 2);

                while (rdr.Read())
                {
                    arr[0] = code;
                    arr[1] = string.Format("{0}", rdr["product_name"]);
                    arr[2] = string.Format("{0}", rdr["product_price"]);
                    arr[3] = year + "년" + month + "월" + day + "일까지";
                }
                connection.Close();
                ListViewItem lvt = new ListViewItem(arr);
                listView1.Items.Add(lvt);
                textBox1.Text = "";
            }
        }


        public string result;

        //private void ListToText_Click_1(object sender, EventArgs e)
        //{
        //    for (int i = 0; i <= listView1.Items.Count - 1; i++)
        //    {
        //        string lv = listView1.Items[i].SubItems[0].Text;

        //        string full_day = listView1.Items[i].SubItems[3].Text;
        //        string s1 = full_day.Substring(2, 2) + full_day.Substring(5, 2) + full_day.Substring(8, 2);

        //        string strSelect = "SELECT product_id FROM products_2 WHERE product_code='" + lv + "';";
        //        connection.Open();
        //        MySqlCommand command = new MySqlCommand(strSelect, connection);
        //        MySqlDataReader rdr = command.ExecuteReader();
        //        while (rdr.Read())
        //        {

        //            result += string.Format("{0}", rdr["product_id"]) + "*" + s1 + ",";
        //        }
        //        connection.Close();
        //    }

        //    textBox2.Text = result;
        //}

        // product_code 로 qr코드 생성
        private void ListToText_Click_1(object sender, EventArgs e)
        {

            for (int i = 0; i <= listView1.Items.Count - 1; i++)
            {

                string full_day = listView1.Items[i].SubItems[3].Text;
                string s1 = full_day.Substring(2, 2) + full_day.Substring(5, 2) + full_day.Substring(8, 2);

                result += listView1.Items[i].SubItems[0].Text + s1 + ",";
            }
            textBox2.Text = result;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string txtBar = textBox2.Text;
            txtBar = txtBar.Substring(0, txtBar.LastIndexOf(','));
            
            QRCodeGenerator qr = new QRCodeGenerator();
            QRCodeData data = qr.CreateQrCode(txtBar, QRCodeGenerator.ECCLevel.Q);
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
            Font font = new Font("한초롬돋움", 10, System.Drawing.FontStyle.Bold);
            Font font1 = new Font("굴림", 6);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            g.DrawString("=========================================== ", font1, drawBrush, 0,3);
            g.DrawString("　　　　BillCode ", font, drawBrush, 6,7);
            g.DrawString("=========================================== ", font1, drawBrush, 0,20);

            int DrawPlus = 19;//글자 줄맞추는데 사용함

            string txtBar = textBox2.Text;
            txtBar = txtBar.Substring(0, txtBar.LastIndexOf(','));
            string[] result = txtBar.Split(',');
            int total = 0;
            string str_Name = "";
            string str_No = "";
            for (int i = 0; i < result.Length; i++)
            {
                string bar_num = result[i];
                string bar_num_sub = bar_num.Substring(0, 13);
                //DB연결부분 
                string strSelect = "SELECT product_name, product_price FROM products_2 WHERE product_code = '" + bar_num_sub + "';";
                connection.Open();
                MySqlCommand command = new MySqlCommand(strSelect, connection);
                MySqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    str_Name = string.Format("{0}", rdr["product_name"]);
                    str_No = string.Format("{0}", rdr["product_price"]);
                    total += int.Parse(str_No); 
                }
                g.DrawString("ㆍ"+str_Name.PadRight(16, '　') + str_No, font1, drawBrush, 10, 10 + DrawPlus);
                connection.Close();
                
                DrawPlus += 10;
            }
            string str_to;
            str_to = "    총 합계 : "+Convert.ToString(total);
            g.DrawString(str_to, font1, drawBrush, 10, 20 + DrawPlus);
            pic.DrawToBitmap(bm, new Rectangle(0, 0, pic.Width, pic.Height));

            g.DrawString("------------------------------------------- ", font1, drawBrush, 2, DrawPlus + 27);
            e.Graphics.DrawImage(bm, 0, DrawPlus + 35);
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
        
        private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            //e.Graphics.FillRectangle(Brushes.Pink, e.Bounds);
            //e.DrawText();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

