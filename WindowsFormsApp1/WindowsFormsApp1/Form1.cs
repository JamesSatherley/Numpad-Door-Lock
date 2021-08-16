using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.Ports;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        bool on = true;
        int code = 1234;

        public Form1()
        {
            InitializeComponent();

            serialPort2.BaudRate = 9600;
            serialPort2.DtrEnable = true;
            serialPort2.Open();

            serialPort2.DataReceived += serialPort2_DataReceived;
        }


        private void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string incoming = serialPort2.ReadLine();
            this.BeginInvoke(new LineReceivedEvent(LineReceived), incoming);
        }

        private delegate void LineReceivedEvent(string POT);

        private void LineReceived(string chara)
        {
            chara = chara.Replace("\n", "").Replace("\r", "");

            if (chara == "C")
            {
                label5.Text = "";
            }
            else if (chara == "#")
            {
                if (label5.Text.Length == 4) 
                {
                    if (Int32.Parse(label5.Text) == code)
                    {
                        open();
                        label5.Text = "";
                    }
                }
            }
            else if (chara == "D")
            {
                if (on != true)
                {
                    open();
                    label5.Text = "";
                }
            }

            if (chara.Length == 1)
            {
                try
                {
                    Int32.Parse(chara);
                    label5.Text = label5.Text + chara;
                }
                catch (FormatException)
                { }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            open();
        }

        private void open()
        {
            if (on == false)
            {
                button1.BackColor = Color.Green;
                serialPort2.Write(string.Format("CLOSE"));
                on = true;
            }
            else
            {
                button1.BackColor = Color.Red;
                serialPort2.Write(string.Format("OPEN"));
                on = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string num = textBox1.Text;
            if (num.Length != 4)
            {
                MessageBox.Show("New code must be 4 digits!");
            }
            else 
            {
                try
                {
                    code = Int32.Parse(num);
                    label6.Text = "- Current code is " + code;
                    textBox1.Text = "";
                }
                catch (FormatException)
                {
                    MessageBox.Show("New code must be numbers!");
                }
            }
        }
    }
}