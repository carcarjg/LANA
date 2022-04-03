using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Device.Location;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;
using System.Media;

namespace Lana
{
    public partial class MainForm : Form
    {
        bool red = false;
        bool dark = false;
        List<string> callnotes = new List<string>();
        /// <summary>
        /// MainForm
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            ResolveAddressAsync();
            Text = "Lana CAD (UID: " + Properties.Settings.Default.CurrentUID + " )";
            timer1sec.Start();
            emrgRW.Stop();
            emrgRWBB.Stop();
            Logon.ActiveForm.Visible = false;
        }

        private void panicbutton_Click(object sender, EventArgs e)
        {
            //opens cansleation screen gives user 10 seconds to stop it. Plays alert
        }

        #region Location Data
        void ResolveAddressAsync()
        {
            GeoCoordinateWatcher watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
            bool started = false;
            watcher.MovementThreshold = 1.0; // set to one meter
            started = watcher.TryStart(false, TimeSpan.FromMilliseconds(1000));

            if (started)
            {
                CivicAddressResolver resolver = new CivicAddressResolver();

                resolver.ResolveAddressCompleted += new EventHandler<ResolveAddressCompletedEventArgs>(resolver_ResolveAddressCompleted);

                if (watcher.Position.Location.IsUnknown == false)
                {
                    resolver.ResolveAddressAsync(watcher.Position.Location);
                }
            }
        }

        void resolver_ResolveAddressCompleted(object sender, ResolveAddressCompletedEventArgs e)
        {
            if (!e.Address.IsUnknown)
            {
                //locationlable.Text = e.Address.AddressLine1 + "," + e.Address.City + e.Address.PostalCode;
            }
            else
            {
                GeoCoordinate GC = new GeoCoordinate();
                //locationlable.Text = "Address Unknown, Revert to Coordinates:  " + GC.Latitude + "," + GC.Longitude;
            }
        }
        #endregion

        #region Dark Modes
        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (red == true)
            {
                red = false;
            }
            if (dark == false)
            {
                this.BackColor = Color.Black;
                this.ForeColor = Color.White;
                groupBox2.BackColor = Color.Black;
                groupBox2.ForeColor = Color.White;
                multibox.BackColor = Color.Black;
                multibox.ForeColor = Color.White;
                quikcodebuttons.BackColor = Color.Black;
                quikcodebuttons.ForeColor = Color.White;
                richTextBox1.BackColor = Color.Black;
                richTextBox1.ForeColor = Color.White;
                menuStrip1.BackColor = Color.Black;
                menuStrip1.ForeColor = Color.White;
                QCbutton1.BackColor = Color.Black;
                QCbutton1.ForeColor = Color.White;
                QCbutton2.BackColor = Color.Black;
                QCbutton2.ForeColor = Color.White;
                QCbutton3.BackColor = Color.Black;
                QCbutton3.ForeColor = Color.White;
                timedatelab.BackColor = Color.Black;
                timedatelab.ForeColor = Color.White;
                dark = true;
            }
            else
            {
                this.BackColor = Color.White;
                this.ForeColor = Color.Black;
                groupBox2.BackColor = Color.White;
                groupBox2.ForeColor = Color.Black;
                multibox.BackColor = Color.White;
                multibox.ForeColor = Color.Black;
                quikcodebuttons.BackColor = Color.White;
                quikcodebuttons.ForeColor = Color.Black;
                richTextBox1.BackColor = Color.White;
                richTextBox1.ForeColor = Color.Black;
                menuStrip1.BackColor = Color.White;
                menuStrip1.ForeColor = Color.Black;
                QCbutton1.BackColor = Color.White;
                QCbutton1.ForeColor = Color.Black;
                QCbutton2.BackColor = Color.White;
                QCbutton2.ForeColor = Color.Black;
                QCbutton3.BackColor = Color.White;
                QCbutton3.ForeColor = Color.Black;
                timedatelab.BackColor = Color.White;
                timedatelab.ForeColor = Color.Black;
                dark = false;
            }

        }
        private void redModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dark == true)
            {
                dark = false;
            }
            if (red == false)
            {
                this.BackColor = Color.DarkRed;
                this.ForeColor = Color.White;
                groupBox2.BackColor = Color.DarkRed;
                groupBox2.ForeColor = Color.White;
                multibox.BackColor = Color.DarkRed;
                multibox.ForeColor = Color.White;
                quikcodebuttons.BackColor = Color.DarkRed;
                quikcodebuttons.ForeColor = Color.White;
                richTextBox1.BackColor = Color.DarkRed;
                richTextBox1.ForeColor = Color.White;
                menuStrip1.BackColor = Color.DarkRed;
                menuStrip1.ForeColor = Color.White;
                QCbutton1.BackColor = Color.DarkRed;
                QCbutton1.ForeColor = Color.White;
                QCbutton2.BackColor = Color.DarkRed;
                QCbutton2.ForeColor = Color.White;
                QCbutton3.BackColor = Color.DarkRed;
                QCbutton3.ForeColor = Color.White;
                timedatelab.BackColor = Color.DarkRed;
                timedatelab.ForeColor = Color.White;
                red = true;
            }
            else
            {
                this.BackColor = Color.White;
                this.ForeColor = Color.Black;
                groupBox2.BackColor = Color.White;
                groupBox2.ForeColor = Color.Black;
                multibox.BackColor = Color.White;
                multibox.ForeColor = Color.Black;
                quikcodebuttons.BackColor = Color.White;
                quikcodebuttons.ForeColor = Color.Black;
                richTextBox1.BackColor = Color.White;
                richTextBox1.ForeColor = Color.Black;
                menuStrip1.BackColor = Color.White;
                menuStrip1.ForeColor = Color.Black;
                QCbutton1.BackColor = Color.White;
                QCbutton1.ForeColor = Color.Black;
                QCbutton2.BackColor = Color.White;
                QCbutton2.ForeColor = Color.Black;
                QCbutton3.BackColor = Color.White;
                QCbutton3.ForeColor = Color.Black;
                timedatelab.BackColor = Color.White;
                timedatelab.ForeColor = Color.Black;
                red = false;
            }
        }
        #endregion

        private void timer1sec_Tick(object sender, EventArgs e)
        {
            switch (Properties.Settings.Default.UTCshow)
            {
                case true:
                    timedatelab.Text = DateTime.UtcNow.ToShortDateString() + " : " + DateTime.UtcNow.ToLongTimeString() + " UTC";
                    break;
                case false:
                    timedatelab.Text = DateTime.Now.ToShortDateString() + " : " + DateTime.Now.ToLongTimeString();
                    break;
            }
        }

        /// <summary>
        /// Used By TCP Cliant
        /// </summary>
        /// <param name="messageBytes"></param>
        /// <returns></returns>
        private static byte[] sendMessage(byte[] messageBytes)
        {
            const int bytesize = 1024 * 1024;
            try // Try connecting and send the message bytes
            {
                System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient("127.0.0.1", 1234); // Create a new connection
                NetworkStream stream = client.GetStream();

                stream.Write(messageBytes, 0, messageBytes.Length); // Write the bytes
                Console.WriteLine("Connected to the server");
                Console.WriteLine(messageBytes);
                Console.WriteLine("Waiting for response...");

                messageBytes = new byte[bytesize]; // Clear the message

                // Receive the stream of bytes
                stream.Read(messageBytes, 0, messageBytes.Length);

                // Clean up
                stream.Dispose();
                client.Close();
            }
            catch (Exception e) // Catch exceptions
            {
                Console.WriteLine(e.Message);
            }

            return messageBytes; // Return response
        }
        /// <summary>
        /// Used By Mini TCP Server
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="client"></param>
        private static void sendMessage(byte[] bytes, TcpClient client)
        {
            client.GetStream()
                .Write(bytes, 0,
                bytes.Length); // Send the stream
        }

        #region Mini TCP Server for New Calls
        //TODO: Ajust so cliant is listening to requests from a DISP console
        static void MiniServer(string[] args)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Any , 1234);
            TcpListener listener = new TcpListener(ep);
            listener.Start();

            Console.WriteLine(@"
            ===================================================
                   Started listening requests at: {0}:{1}
            ===================================================",
            ep.Address, ep.Port);

            // Run the loop continuously; this is the server.
            while (true)
            {
                const int bytesize = 1024 * 1024;

                string message = null;
                byte[] buffer = new byte[bytesize];

                var sender = listener.AcceptTcpClient();
                sender.GetStream().Read(buffer, 0, bytesize);

                // Read the message and perform different actions
                message = cleanMessage(buffer);

                try
                {
                    // Save the data sent by the client;
                    scallissue scallissue = JsonConvert.DeserializeObject<scallissue>(message); // Deserialize

                    if (scallissue.UID == Properties.Settings.Default.CurrentUID)
                    {
                        byte[] bytes = System.Text.Encoding.Unicode.GetBytes("0x1379");
                        sender.GetStream().Write(bytes, 0, bytes.Length); // Send the response

                        newcallRVC(scallissue);
                    }
                    else
                    {
                        byte[] bytes = System.Text.Encoding.Unicode.GetBytes("0x1111");
                        sender.GetStream().Write(bytes, 0, bytes.Length); // Send the response
                    }
                }
                catch (Exception)
                {
                    try
                    {
                        snewnote snewnote = JsonConvert.DeserializeObject<snewnote>(message); // Deserialize

                        if (snewnote.callID == Properties.Settings.Default.currentcallID)
                        {
                            byte[] bytes = System.Text.Encoding.Unicode.GetBytes("0x1379");
                            sender.GetStream().Write(bytes, 0, bytes.Length);

                            newnoteRVC(snewnote);
                        }
                        else
                        {
                            byte[] bytes = System.Text.Encoding.Unicode.GetBytes("0x1111");
                            sender.GetStream().Write(bytes, 0, bytes.Length); // Send the response
                        }
                    }
                    catch (Exception)
                    {
                        sstatusupdate sstatusupdate = JsonConvert.DeserializeObject<sstatusupdate>(message); // Deserialize

                        if (sstatusupdate.UID == Properties.Settings.Default.CurrentUID)
                        {
                            byte[] bytes = System.Text.Encoding.Unicode.GetBytes("0x1111");
                            sender.GetStream().Write(bytes, 0, bytes.Length);
                        }
                        else
                        {
                            byte[] bytes = System.Text.Encoding.Unicode.GetBytes("0x1111");
                            sender.GetStream().Write(bytes, 0, bytes.Length); // Send the response
                        }
                    }
                }

            }
        }
        private static string cleanMessage(byte[] bytes)
        {
            string message = System.Text.Encoding.Unicode.GetString(bytes);

            string messageToPrint = null;
            foreach (var nullChar in message)
            {
                if (nullChar != '\0')
                {
                    messageToPrint += nullChar;
                }
            }
            return messageToPrint;
        }
        private static void newnoteRVC(snewnote SNN)
        {
            MainForm MF = new MainForm();
            StringBuilder sb = new StringBuilder();
            foreach (string str in SNN.Notes)
            {
                sb.Append(str);
                sb.Append(@"\n");
            }
            MF.richTextBox2.Text = sb.ToString();
        }
        private static void newcallRVC(scallissue SCI)
        {
            MainForm MF = new MainForm();

            int pri = 0;
            var CodePRI = new List<KeyValuePair<string, int>>();
            #region Loading in Keypairs
            CodePRI.Add(new KeyValuePair<string, int>("10-15", 2));
            CodePRI.Add(new KeyValuePair<string, int>("10-17A", 1));
            CodePRI.Add(new KeyValuePair<string, int>("10-17B", 1));
            CodePRI.Add(new KeyValuePair<string, int>("10-17C", 1));
            CodePRI.Add(new KeyValuePair<string, int>("10-18", 3));
            CodePRI.Add(new KeyValuePair<string, int>("10-19", 0));
            CodePRI.Add(new KeyValuePair<string, int>("10-21", 0));
            CodePRI.Add(new KeyValuePair<string, int>("10-31A", 2));
            CodePRI.Add(new KeyValuePair<string, int>("10-31B", 2));
            CodePRI.Add(new KeyValuePair<string, int>("10-31C", 3));
            CodePRI.Add(new KeyValuePair<string, int>("10-31D", 3));
            CodePRI.Add(new KeyValuePair<string, int>("10-31E", 3));
            CodePRI.Add(new KeyValuePair<string, int>("10-46", 1));
            CodePRI.Add(new KeyValuePair<string, int>("10-31A", 2));
            CodePRI.Add(new KeyValuePair<string, int>("10-50", 2));
            CodePRI.Add(new KeyValuePair<string, int>("10-56", 2));
            CodePRI.Add(new KeyValuePair<string, int>("10-70", 3));
            CodePRI.Add(new KeyValuePair<string, int>("10-80", 3));
            CodePRI.Add(new KeyValuePair<string, int>("10-89", 3));
            CodePRI.Add(new KeyValuePair<string, int>("10-91", 1));
            CodePRI.Add(new KeyValuePair<string, int>("10-92", 1));
            CodePRI.Add(new KeyValuePair<string, int>("10-98", 3));
            CodePRI.Add(new KeyValuePair<string, int>("10-00", 4));
            CodePRI.Add(new KeyValuePair<string, int>("10-108", 4));

            /*CodePRI.Add(new KeyValuePair<string, int>("10-70", 3));
            CodePRI.Add(new KeyValuePair<string, int>("10-80", 3));
            CodePRI.Add(new KeyValuePair<string, int>("10-89", 3));
            CodePRI.Add(new KeyValuePair<string, int>("10-91", 1));
            CodePRI.Add(new KeyValuePair<string, int>("10-50", 2));*/

            //TODO: Load this in from file
            #endregion

            StringBuilder sb = new StringBuilder();
            foreach (string str in SCI.Notes)
            {
                sb.Append(str);
                sb.Append(@"\n");
            }
            MF.richTextBox2.Text = sb.ToString();

            /*
             * Pri 0 - Play Ding sound three times
             * Pri 1 - Low Alert Siren Sound three times
             * Pri 2 - Pop up, Medium Alert Siren repeat until ACK button pressed on Pop up
             * Pri 3 - Pop up, High Alert Siren repeat until ACK button pressed on Pop up, Flash Screen Red/White (or Red/Black if in any dark mode)
             * Pri 4 - Pop up, Harris EMRG Sound Repeat until ACK button pressed on Pop up, Flash Screen Red/White/Blue/Black
             */
            MF.richTextBox1.Text = SCI.body;
            string Dir4Audio = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase) + "Audio";
            bool OKpressed = false;
            switch (pri)
            {
                #region Low Pri Cases
                case 0:
                    SoundPlayer pri0 = new SoundPlayer(Dir4Audio + "Pri0.wav");
                    pri0.PlaySync();
                    pri0.PlaySync();
                    pri0.PlaySync();
                    break;
                case 1:
                    SoundPlayer pri1 = new SoundPlayer(Dir4Audio + "Low Pri Alarm.wav");
                    pri1.PlaySync();
                    pri1.PlaySync();
                    pri1.PlaySync();
                    break;
                #endregion
                case 2:
                    SoundPlayer pri2 = new SoundPlayer(Dir4Audio + "Med Pri Alarm.wav");
                    DialogResult p2 = MessageBox.Show(SCI.title, "Acknowledge", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    do
                    {
                        if (p2 == DialogResult.OK)
                        {
                            OKpressed = true;
                        }
                        pri2.Play();

                    } while (OKpressed == false);
                    break;
                case 3:
                    SoundPlayer pri3 = new SoundPlayer(Dir4Audio + "High Pri Alarm.wav");
                    DialogResult p3 = MessageBox.Show(SCI.title, "Acknowledge", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    do
                    {
                        pri3.PlayLooping();
                        MF.emrgRW.Start();
                        if (p3 == DialogResult.OK)
                        {
                            pri3.Stop();
                            MF.emrgRW.Stop();
                            OKpressed = true;
                        }

                    } while (OKpressed == false);
                    break;
                case 4:
                    SoundPlayer pri4 = new SoundPlayer(Dir4Audio + "Code 4 EMRG Call.wav");
                    DialogResult p4 = MessageBox.Show(SCI.title, "Acknowledge", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    do
                    {
                        pri4.PlayLooping();
                        MF.emrgRWBB.Start();
                        if (p4 == DialogResult.OK)
                        {
                            pri4.Stop();
                            MF.emrgRWBB.Stop();
                            OKpressed = true;
                        }
                    } while (OKpressed == false);
                    break;
            }
        }
        #endregion
        private void timedatelab_Click(object sender, EventArgs e)
        {
            switch (Properties.Settings.Default.UTCshow)
            {
                case true:
                    Properties.Settings.Default.UTCshow = false;
                    break;
                case false:
                    Properties.Settings.Default.UTCshow = true;
                    break;
            }
        }

        //TODO: Create new scallissue object and pass to newcallRVC
        #region admin toolstrip
        private void pri0ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pri1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pri2ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pri3ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pri4ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion
        int phase = 0;
        private void emrgRW_Tick(object sender, EventArgs e)
        {
            if (dark == true)
            {
                //Dark Red / Black
                switch (phase)
                {
                    case 0:
                        BackColor = Color.DarkRed;
                        menuStrip1.BackColor = Color.DarkRed;

                        ForeColor = Color.DarkRed;
                        menuStrip1.ForeColor = Color.DarkRed;
                        phase++;
                        break;
                    case 1:
                        BackColor = Color.Black;
                        menuStrip1.BackColor = Color.Black;
                        phase = 0;
                        break;
                }
            }
            else if (red == true)
            {
                //Dark Red / Black
                switch (phase)
                {
                    case 0:
                        BackColor = Color.DarkRed;
                        menuStrip1.BackColor = Color.DarkRed;
                        phase++;
                        break;
                    case 1:
                        BackColor = Color.Black;
                        menuStrip1.BackColor = Color.Black;
                        phase = 0;
                        break;
                }
            }
            else
            {
                //Red / White
                switch (phase)
                {
                    case 0:
                        BackColor = Color.DarkRed;
                        menuStrip1.BackColor = Color.DarkRed;
                        phase++;
                        break;
                    case 1:
                        BackColor = Color.White;
                        menuStrip1.BackColor = Color.White;
                        phase = 0;
                        break;
                }
            }
        }

        private void emrgRWBB_Tick(object sender, EventArgs e)
        {
            switch (phase)
            {
                case 0:
                    BackColor = Color.DarkRed;
                    menuStrip1.BackColor = Color.DarkRed;
                    phase++;
                    break;
                case 1:
                    BackColor = Color.White;
                    menuStrip1.BackColor = Color.White;
                    phase++;
                    break;
                case 2:
                    BackColor = Color.Black;
                    menuStrip1.BackColor = Color.Black;
                    phase++;
                    break;
                case 3:
                    BackColor = Color.Blue;
                    menuStrip1.BackColor = Color.Blue;
                    phase = 0;
                    break;
            }
        }

        bool logout = false;
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logout = true;
            if (Logon.ActiveForm != null)
            {
                Logon.ActiveForm.Visible = true;
                this.Close();
            }

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (logout != true)
            {
                timer1sec.Stop();
                emrgRW.Stop();
                emrgRWBB.Stop();
                Properties.Settings.Default.currentuser = "";
                Properties.Settings.Default.CurrentUID = "";
                Environment.Exit(1);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            callnotes.Add(notebox.Text);
            //send updated notes
        }
    }

    #region JSON Templates
    class callissue
    {
        public string UID { get; set; }
        public long callID { get; set; }
        public string code { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public List<string> Notes { get; set; }

        //public string
    }
    class newnote
    {
        public long callID { get; set; }
        public List<string> Notes { get; set; }
    }
    class statusupdate
    {
        public string UID { get; set; }
        public string code { get; set; }
    }
    #endregion

    #region JSON Templates Copies For Mini Server
    class scallissue
    {
        public string UID { get; set; }
        public long callID { get; set; }
        public string code { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public List<string> Notes { get; set; }

        //public string
    }
    class snewnote
    {
        public long callID { get; set; }
        public List<string> Notes { get; set; }
    }
    class sstatusupdate
    {
        public string UID { get; set; }
        public string code { get; set; }
    }
    #endregion
}
