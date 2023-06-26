using OpenVTT.NetworkMessage;
using System;
using System.IO;
using System.Net.Sockets;

namespace OpenVTT.Client
{
    internal class ClientExample
    {
        int PORT_NO = 5000;
        string SERVER_IP = "127.0.0.1";

        void ClientMethod()
        {
            //---data to send to the server---
            string textToSend = DateTime.Now.ToString();

            //---create a TCPClient object at the IP and port no.---
            TcpClient client = new TcpClient(SERVER_IP, PORT_NO);

            var c = new NetworkMessage.Client(client);
            c.MessageReceived += (data, cl) =>
            {
                var okMessage = new Message()
                {
                    type = MessageType.Ok,
                    Definition = null,
                    fileInformation = null,
                    packageSize = 0,
                };
                okMessage.packageSize = okMessage.ToXML().Length;

                var message = Message.LoadFromXMLString(data);

                switch (message.type)
                {
                    case MessageType.Ok: // Server sended OK
                        break;
                    case MessageType.LengthDefinition: // Server has send the Package size for the next Package
                        c.tcpClient.ReceiveBufferSize = message.packageSize;
                        c.Write(okMessage.ToXML());
                        break;
                    case MessageType.RequestFileStructure: // Server requested the Server File Structure
                        //Read FileStructure
                        //Create Message (using DumpOject)
                        //Send Message (SendFileStructure)
                        break;
                    case MessageType.SendFileStructure: // Server sended the Client File Structure
                        //Cross Check with Server File Structure
                        //Find diffs and store them in a List
                        //Start Requesting files according to List
                        //Send RequestMessage
                        break;
                    case MessageType.RequestFileData: // Server requested a specific File
                        //Read specific File
                        //Create Message (using FileInformation)
                        //SendMessage (SendFileData)
                        break;
                    case MessageType.SendFileData: // Server sended a speccific File
                        //Update Specific File
                        //Remove Item from Request List
                        //Request next file
                        break;
                    case MessageType.TextMessage: // Server sended a Text Message
                        break;
                    default:
                        break;
                }
            };

            c.Start();

            //var message = new Message()
            //{
            //    type = MessageType.TextMessage,
            //    packageSize = 0,

            //    fileInformation = new FileInformation()
            //    {
            //        fileName = "Test :3",

            //        fileContent = null,
            //        fileLocation = "",
            //    }
            //};
            //Console.WriteLine("Send: " + message.fileInformation.fileName);
            //c.Write(message.ToXML());

            //SendDefinition(c);
            //Thread.Sleep(1000);
            //SendFile(c);

            Console.ReadLine();
            client.Close();
        }

        void SendDefinition(NetworkMessage.Client c)
        {
            var fileBytes = File.ReadAllBytes(@".\Test.png");

            Console.WriteLine("Sending : Definition");

            //Generate Message for Length Definition
            var m = new Message();
            m.type = MessageType.LengthDefinition;
            m.fileInformation.fileLocation = "test";
            m.fileInformation.fileContent = fileBytes;
            m.packageSize = fileBytes.Length;

            var size = m.ToXML().Length;
            m.fileInformation.fileContent = null;
            m.packageSize = size;

            c.Write(m.ToXML());
        }
        void SendFile(NetworkMessage.Client c)
        {
            var fileBytes = File.ReadAllBytes(@".\Test.png");

            Console.WriteLine("Sending : File");

            //Generate Message for Length Definition
            var m = new Message();
            m.type = MessageType.SendFileData;
            m.fileInformation.fileLocation = "test";
            m.fileInformation.fileContent = fileBytes;
            m.packageSize = fileBytes.Length;

            var size = m.ToXML().Length;
            m.packageSize = size;

            c.Write(m.ToXML());
        }
    }
}
