using OpenVTT.NetworkMessage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace OpenVTT.Server
{
    internal class ServerProgram
    {
        static void Main(string[] args)
        {
            var config = ServerConfiguration.LoadFromXMLString(File.ReadAllText(".\\config.xml"));
            
            //---listen at the specified IP and port no.---
            Server s = new Server(config.SERVER_IP, config.PORT_NO);
            s.Start();

            Console.WriteLine("Stop Server? PRESS ENTER");
            Console.ReadLine();

            s.Stop();
        }
    }

    public class ServerConfiguration
    {
        public string SERVER_IP = "127.0.0.1";
        public int PORT_NO = 5000;

        public string ToXML()
        {
            using (var stringwriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(stringwriter, this);
                return stringwriter.ToString();
            }
        }

        public static ServerConfiguration LoadFromXMLString(string xmlText)
        {
            using (var stringReader = new System.IO.StringReader(xmlText))
            {
                var serializer = new XmlSerializer(typeof(ServerConfiguration));
                return serializer.Deserialize(stringReader) as ServerConfiguration;
            }
        }
    }
}
