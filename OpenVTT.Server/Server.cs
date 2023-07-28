using OpenVTT.NetworkMessage;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace OpenVTT.Server
{
    internal class Server
    {
        TcpListener listener;

        List<Client> clients;

        Task acceptClientsTask;

        bool run = true;

        public Server(string ip, int port)
        {
            IPAddress localAdd = IPAddress.Parse(ip);
            listener = new TcpListener(localAdd, port);
            clients = new List<Client>();

            acceptClientsTask = new Task(acceptClients);
        }

        public void Start()
        {
            Console.WriteLine("Listening...");
            listener.Start();

            acceptClientsTask.Start();
        }

        public void Stop()
        {
            run = false;

            listener.Stop();

            clients.ForEach(client => client.Stop());
        }

        private void acceptClients()
        {
            while (run)
            {
                TcpClient tcpClient = null;

                try
                {
                    tcpClient = listener.AcceptTcpClient();
                }
                catch // (SocketException e)
                {
                    //if (e.SocketErrorCode == SocketError.Interrupted) { }
                }

                if (tcpClient == null) continue;


                var c = new Client(tcpClient);

                var cmh = new ClientMessageHandle(c);
                cmh.SetCommandLabel += (message) => Console.WriteLine(message);
                cmh.SetQueueLabel += (count) => Console.Title = $"Request Queue Count: {count}";
                cmh.SyncComplete += () =>
                {
                    var msg = new Message()
                    {
                        type = MessageType.SyncComplete,
                        Definition = null,
                        fileInformation = null,
                        packageSize = 0,
                    };

                    cmh.SetCommandLabel("Callig for Synced up!");

                    c.Write(msg.ToXML());
                };

                c.ClientMessageHandle = cmh;


                c.MessageReceived += cmh.MessageHandle;
                c.Start();
                clients.Add(c);
            }
            Console.WriteLine("Server Loop Stopped");
        }
    }
}
