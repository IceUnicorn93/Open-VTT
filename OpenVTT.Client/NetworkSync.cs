using OpenVTT.NetworkMessage;
using System;
using System.Net.Sockets;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using OpenVTT.Logging;

namespace OpenVTT.Client
{
    public partial class NetworkSync : UserControl
    {
        private TcpClient tcpClient;
        private NetworkMessage.Client client;

        public int SERVER_PORT = 5000;
        public string SERVER_IP = "127.0.0.1";

        public Action SyncComplete;

        public NetworkSync()
        {
            Logger.Log("Class: NetworkSync | Constructor");

            InitializeComponent();

            lblCommand.Text = "";
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: NetworkSync | btnConnect_Click");

            if (tcpClient == null) // Connect
            {
                

                tcpClient = new TcpClient(SERVER_IP, SERVER_PORT);
                client = new NetworkMessage.Client(tcpClient);

                var cmh = new ClientMessageHandle(client);
                cmh.SetCommandLabel += SetCommandLabel;
                cmh.SetQueueLabel += SetQueueLabel;

                client.MessageReceived += cmh.MessageHandle;
                client.Start();

                lblConnectionState.Text = "Connected";
                panelState.BackColor = Color.Green;

                btnPull.Enabled = true;
                btnPush.Enabled = true;

                btnConnect.Text = "Disconnect";
            }
            else // Disconnect
            {
                client.Stop();
                tcpClient = null;

                lblConnectionState.Text = "Disconnected";
                panelState.BackColor = Color.FromKnownColor(KnownColor.ButtonShadow);

                btnPull.Enabled = false;
                btnPush.Enabled = false;

                btnConnect.Text = "Connect";
            }
        }

        private void btnPull_Click(object sender, EventArgs e)
        { // Request Directory from Server

            Logger.Log("Class: NetworkSync | btnPull_Click");

            if (MessageBox.Show("Clear all Notes (Yes) or only Update Difference(No)?", "Clear Notes?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var startupPath = Application.StartupPath;
                if(Directory.Exists(Path.Combine(startupPath, "Notes")))
                Directory.Delete(Path.Combine(startupPath, "Notes"), true);
                Directory.CreateDirectory(Path.Combine(startupPath, "Notes"));
            }

            var msg = new NetworkMessage.Message()
            {
                Definition = null,
                fileInformation = null,
                type = MessageType.RequestFileStructure,
                packageSize = 0,
            };
            msg.packageSize = msg.ToXML().Length;

            client.Write(msg.ToXML());
            lblCommand.Text = "Request File Structure";
        }

        private void btnPush_Click(object sender, EventArgs e)
        { // Send Directory to Server

            Logger.Log("Class: NetworkSync | btnPush_Click");

            var cmh = new ClientMessageHandle(null);
            var msg = new NetworkMessage.Message()
            {
                Definition = cmh.GetDefinition(),
                fileInformation = null,
                type = MessageType.SendFileStructure,
                packageSize = 0,
            };
            msg.packageSize = msg.ToXML().Length;

            client.Write(msg.ToXML());

            lblCommand.Text = "Send File Structure";
        }

        private void SetCommandLabel(string text)
        {
            Logger.Log("Class: NetworkSync | SetCommandLabel");

            lblCommand.Invoke((MethodInvoker)delegate { lblCommand.Text = text; });
        }

        private void SetQueueLabel(int howManyInQueue)
        {
            Logger.Log("Class: NetworkSync | SetQueueLabel");

            lblQueue.Invoke((MethodInvoker)delegate { lblQueue.Text = $"Queue: {howManyInQueue}"; });
        }
    }
}
