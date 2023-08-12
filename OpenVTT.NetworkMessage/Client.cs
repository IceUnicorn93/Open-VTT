using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OpenVTT.NetworkMessage
{
    internal class Client
    {
        internal ClientMessageHandle ClientMessageHandle;

        internal TcpClient tcpClient;

        internal Action<string, Client> MessageReceived;

        private Task readTask;
        private NetworkStream nwStream;

        private DateTime lastMessageReceived = DateTime.Now;

        public Client(TcpClient client)
        {
            tcpClient = client;
            readTask = new Task(ReadLoop);
        }

        public void Start()
        {
            readTask.Start();
        }

        public void Stop()
        {
            tcpClient.Close();
        }

        public void Write(string s)
        {
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(s);
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);
        }

        private void ReadLoop()
        {
            while (tcpClient != null && tcpClient.Client != null && tcpClient.Connected)
            {
                if(DateTime.Now - lastMessageReceived > TimeSpan.FromHours(12)) // Kill the Client if there is no Action for 12 Hours
                {
                    tcpClient.Dispose();
                    break;
                }

                nwStream = tcpClient.GetStream();

                int oldRead = tcpClient.Available;
                Task.Delay(500).Wait();
                int newRead = tcpClient.Available;

                if (oldRead == newRead && tcpClient.Available > 0) // if equal, all the data has been read
                {
                    lastMessageReceived = DateTime.Now;

                    try
                    {
                        //---get the incoming data through a network stream---
                        byte[] buffer = new byte[tcpClient.ReceiveBufferSize];

                        //---read incoming stream---
                        int bytesRead = nwStream.Read(buffer, 0, tcpClient.ReceiveBufferSize);

                        //---convert the data received into a string---
                        string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                        if (MessageReceived != null) { MessageReceived(dataReceived, this); }
                    }
                    catch //(SocketException ex)
                    { }
                    //catch //(IOException ex)
                    //{ }
                }
            }
            Console.WriteLine("Client Loop Stopped");
        }
    }
}
