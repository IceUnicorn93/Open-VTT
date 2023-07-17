using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OpenVTT.NetworkMessage
{
    internal class ClientMessageHandle
    {
        private Message nextMessage = new Message();
        private Message lengthMessage = new Message() { Definition = null, fileInformation = null, packageSize = 0, type = MessageType.LengthDefinition };

        private List<DirectoryDefinition> Definition;
        private List<Message> RequestQueue;

        private Client client;

        public Action SyncComplete;
        public Action<string> SetCommandLabel;
        public Action<int> SetQueueLabel;

        public ClientMessageHandle(Client c)
        {
            client = c;
        }

        internal void MessageHandle(string data, Client client)
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
                    if (nextMessage == null) return;
                    if(SetCommandLabel != null) SetCommandLabel("Accepted Last Message");
                    client.Write(nextMessage.ToXML());
                    nextMessage = null;
                    break;
                case MessageType.LengthDefinition: // Server has send the Package size for the next Package
                    if (SetCommandLabel != null) SetCommandLabel("Received LengthDefinition");
                    client.tcpClient.ReceiveBufferSize = message.packageSize;
                    client.Write(okMessage.ToXML());
                    break;
                case MessageType.RequestFileStructure: // Server requested the Client File Structure
                    if (SetCommandLabel != null) SetCommandLabel("Requested FileStructure");
                    var def = GetDefinition();
                    nextMessage = new NetworkMessage.Message()
                    {
                        Definition = def,
                        fileInformation = null,
                        packageSize = 0,
                        type = MessageType.SendFileStructure
                    };
                    nextMessage.packageSize = nextMessage.ToXML().Length;

                    lengthMessage.packageSize = nextMessage.ToXML().Length;
                    client.Write(lengthMessage.ToXML());
                    break;
                case MessageType.SendFileStructure: // Server sended the Server File Structure
                    if (SetCommandLabel != null) SetCommandLabel("Received FileStructure");
                    Definition = message.Definition;
                    CrossCheckDirectories();
                    break;
                case MessageType.RequestFileData: // Server requested a specific File
                    if (SetCommandLabel != null) SetCommandLabel("Requested FileData");
                    SendFile(message.fileInformation.fileLocation, client);
                    break;
                case MessageType.SendFileData: // Server sended a speccific File
                    if (SetCommandLabel != null) SetCommandLabel("Received FileData");
                    CreateFile(message.fileInformation);
                    break;
                case MessageType.TextMessage: // Server sended a Text Message
                    if (SetCommandLabel != null) SetCommandLabel("Push complete");
                    SyncComplete?.Invoke();
                    break;
                default:
                    break;
            }
        }

        internal List<DirectoryDefinition> GetDefinition()
        {
            var startupPath = Application.StartupPath;

            if (!Directory.Exists(Path.Combine(startupPath, "Notes")))
                Directory.CreateDirectory(Path.Combine(startupPath, "Notes"));

            var files = Directory.GetFileSystemEntries(Path.Combine(startupPath, "Notes"), "*", SearchOption.AllDirectories).ToList();
            var Definition = new List<DirectoryDefinition>();
            foreach (var entry in files)
            {
                var fi = new FileInfo(entry);

                var info = (fi.Attributes == FileAttributes.Directory ? "Directory" : "File");

                Definition.Add(new DirectoryDefinition { Name = entry.Replace(startupPath, ""), Type = info, LastWriteTime = fi.LastWriteTime });
            }

            return Definition;
        }

        private void CrossCheckDirectories()
        {
            SetCommandLabel("Checking Definition");

            var localDef = GetDefinition();

            var diff = Definition.Except(localDef).ToList();

            diff.Where(n => n.Type == "Directory").ToList().ForEach(n => Directory.CreateDirectory(Path.Combine(Application.StartupPath, n.Name.Remove(0, 1))));
            diff.RemoveAll(n => n.Type == "Directory");

            diff.RemoveAll(n =>
            {
                var path = Path.Combine(Application.StartupPath, n.Name.Remove(0, 1));
                var f = new FileInfo(path);
                return f.Exists && f.LastWriteTime >= n.LastWriteTime;
            });

            if(SetQueueLabel != null) SetQueueLabel(diff.Count);

            RequestQueue = new List<Message>();

            foreach (var entry in diff)
                RequestQueue.Add(new Message
                {
                    Definition = null,
                    packageSize = 0,
                    type = MessageType.RequestFileData,
                    fileInformation = new FileInformation
                    {
                        fileContent = null,
                        fileLocation = entry.Name,
                        LastWriteTime = null,
                    }
                });

            if (RequestQueue.Count > 0)
            {
                if (SetCommandLabel != null) SetCommandLabel("Requesting File");

                client.Write(RequestQueue.First().ToXML());
            }
            else
            {
                if (SetCommandLabel != null) SetCommandLabel("It's synced up");

                if(SetQueueLabel != null) SetQueueLabel(0);

                if(SyncComplete != null) SyncComplete();
            }
        }

        private void CreateFile(FileInformation fileInformation)
        {
            var startupPath = Application.StartupPath;
            var path = Path.Combine(startupPath, fileInformation.fileLocation.Remove(0, 1));

            if (File.Exists(path))
            {
                FileInfo f = new FileInfo(path);
                if (f.LastWriteTime < fileInformation.LastWriteTime) //sended file is never
                    File.WriteAllBytes(path, fileInformation.fileContent);
            }
            else
                File.WriteAllBytes(path, fileInformation.fileContent);

            var fi = new FileInfo(path);
            fi.LastWriteTime = fileInformation.LastWriteTime ?? DateTime.Now;

            RequestQueue.Remove(RequestQueue.First());
            if (SetQueueLabel != null) SetQueueLabel(RequestQueue.Count);
            if (RequestQueue.Count > 0)
                client.Write(RequestQueue.First().ToXML());
            else
            {
                if(SetCommandLabel != null) SetCommandLabel("It's synced up");
                if(SyncComplete != null) SyncComplete();
            }
        }

        private void SendFile(string fileLocation, Client client)
        {
            var startupPath = Application.StartupPath;
            var path = Path.Combine(startupPath, fileLocation.Remove(0, 1));

            var bytes = File.ReadAllBytes(path);

            var fi = new FileInfo(path);
            var date = fi.LastWriteTime;

            nextMessage = new Message()
            {
                Definition = null,
                fileInformation = new FileInformation
                {
                    fileContent = bytes,
                    fileLocation = path.Replace(startupPath, ""),
                    LastWriteTime = date
                },
                packageSize = bytes.Length,
                type = MessageType.SendFileData,
            };
            nextMessage.packageSize = nextMessage.ToXML().Length;


            lengthMessage.packageSize = nextMessage.ToXML().Length;
            client.Write(lengthMessage.ToXML());
        }
    }
}
