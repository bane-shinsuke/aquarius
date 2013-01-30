using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace aquarius_host
{
    public partial class ClockController : Form
    {
        const String WORK_DIR = @"c:\temp\aquarius-work"; 
        public ClockController()
        {
            InitializeComponent();
            if(Directory.Exists(WORK_DIR)) Directory.Delete(WORK_DIR, true);
            Directory.CreateDirectory(WORK_DIR);


        }

        enum STATUS
        {
            NOT_CONNECT,
            CONNECTED,
            ERROR,
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // REST API 判定
            if(File.Exists(WORK_DIR + "/on")) {
                SendChar('n');
                File.Delete(WORK_DIR + "/on");
            }
            else if(File.Exists(WORK_DIR + "/off")) {
                SendChar('f');
                File.Delete(WORK_DIR + "/off");
            }

            // 0時判定
            DateTime now = DateTime.Now;
            if ((now.Hour == 23 || now.Hour == 11)&& now.Minute == 59 && now.Second == 57)
            {

                SendChar('f');
                Thread.Sleep(2000);
                SendChar('n');

                notifyIcon1.BalloonTipText = "Clock was synchronized !!";
                notifyIcon1.ShowBalloonTip(5000);
            }
            
            
        }

        private void SendChar(char c)
        {
            char[] buff = new char[1];


            buff[0] = c;
            // Send the one character buffer.
            serialPort1.Write(buff, 0, 1);

            String message = String.Empty;

            if (c == 'f')
            {
                message = "Power Off !!";
                this.label2.BackColor = System.Drawing.SystemColors.Info;
            }

            else if (c == 'n')
            {
                message = "Power On !!";
                this.label2.BackColor = Color.MistyRose;
            }

            LogWrite(message);

            this.label2.Text = message;
        }

        private static void LogWrite(string result)
        {
            string logfolder = @"c:\temp\aquarius-log\";
            //実行ファイルと同フォルダにlogフォルダを作成する 
            System.IO.Directory.CreateDirectory(logfolder);

            string logfile = logfolder + @"result.log";

            //現在時刻を取得 
            DateTime dtNow = DateTime.Now;
            string timefmt = dtNow.ToString("yyyy/MM/dd HH:mm:ss\t");

            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            System.IO.StreamWriter writer = new System.IO.StreamWriter(logfile, true, sjisEnc);

            //追記で日時と結果を書き込む 
            writer.WriteLine(timefmt + result);
            writer.Close();
        } 

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.label1.Text = STATUS.NOT_CONNECT.ToString();
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            try
            {

                serialPort1.PortName = this.textBox1.Text;
                serialPort1.BaudRate = 9600;
                serialPort1.Open();
                this.label1.Text = STATUS.CONNECTED.ToString();
                this.label1.BackColor = Color.MistyRose;

                this.button1.Enabled = true;

                this.timer1.Enabled = true;
            }
            catch
            {
                this.label1.Text = STATUS.ERROR.ToString();
                //this.label1.Text += "\n";

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendChar('f');
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendChar('n');
        }

        

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F)
            {
                SendChar('f');

            }
            else if (keyData == Keys.N)
            {
                SendChar('n');

            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
                this.button1.Enabled = this.radioButton2.Checked;
                this.button2.Enabled = this.radioButton2.Checked;

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
}
