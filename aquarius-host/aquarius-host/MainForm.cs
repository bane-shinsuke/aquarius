using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace aquarius_host
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();


        }

        enum STATUS
        {
            NOT_CONNECT,
            CONNECTED,
            ERROR,
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 0時判定
            
            SendChar('n');
            
        }

        private void SendChar(char c)
        {
            char[] buff = new char[1];


            buff[0] = c;
            // Send the one character buffer.
            serialPort1.Write(buff, 0, 1);

            this.label1.Text = "'" + c + "' was sent!!";
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
            SendChar('n');
        }
    }
}
