using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class FrmClient : Form
    {
        public FrmClient()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Communication.Instance.Connect();
                if (Communication.Instance.Login(textBox1.Text))
                {
                    button2.Enabled = false;
                    textBox1.ReadOnly = true;
                    button1.Enabled = true;
                    textBox2.Enabled = true;
                    Communication.Instance.ListenMessages(textBox3);
                }
            }
            catch (Exception se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void FrmClient_Load(object sender, EventArgs e)
        {
            button2.Enabled = true;
            textBox1.ReadOnly = false;
            button1.Enabled = false;
            textBox2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Communication.Instance.SendMessage(textBox2.Text);
        }
      

        private void FrmClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            Communication.Instance.EndCommunication();
        }
    }
}
