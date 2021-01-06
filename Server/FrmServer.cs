﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class FrmServer : Form
    {
        private Server server;
        public FrmServer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            server = new Server();
            Thread serverThread = new Thread(server.Listen);
            serverThread.IsBackground = true;
            serverThread.Start();

            button2.Enabled = true;
            button1.Enabled = false;
            this.Text = "Server is working";

        }



        private void FrmServer_Load(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;

            server.Stop();
            this.Text = "Server is stopped";

        }


    }
}
