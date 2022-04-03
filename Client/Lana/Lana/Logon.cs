using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lana
{
    public partial class Logon : Form
    {
        public Logon()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 1)
            {
                if (textBox2.Text.Length > 1)
                {
                    //TODO: PLEASE REMOVE THIS........
                    switch (textBox1.Text.ToUpper())
                    {
                        case "258":
                        case "ROSESAM":
                            if (textBox2.Text.ToUpper() == "2213")
                            {
                                Properties.Settings.Default.CurrentUID = "258";
                                Properties.Settings.Default.currentuser = "Gallini, Carsten";
                                MainForm MF = new MainForm();
                                MF.Show();
                                MF.TopLevel = true;
                            }
                            break;
                        case "DISPATCH":
                            if (textBox2.Text.ToUpper() == "2213")
                            {
                                //TODO: Do nothing as the Dispatcher view isnt done
                            }
                            break;
                    }
                }
            }
        }
    }
}
