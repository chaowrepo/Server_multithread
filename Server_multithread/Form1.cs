using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Server_multithread {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            Server s = new Server(3000);
            s.ExceptionReceived += new Server.ExceptionReceivedHandler(s_ExceptionReceived);
        }

        void s_ExceptionReceived(exception e) {
            MessageBox.Show(string.Format("DateTime: {0} - UserName: {1} - OperatingSystem: {2} - Title: {3} - StackTrace: {4}", e.DateTime, e.UserName, e.OperatingSystem, e.Title, e.StackTrace));
        }

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);

            Environment.Exit(0);
        }

        private void Form1_Load(object sender, EventArgs e) {

        }
    }
}