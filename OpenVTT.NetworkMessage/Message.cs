using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace OpenVTT.NetworkMessage
{
    public enum MessageType
    {
        Ok = 0, // Acknowledge Message send by Client to Server after each Message
        LengthDefinition = 1, // Size Definition of the next Package

        RequestFileStructure = 2, // Client sends this Message to Request the Server File Structure
        SendFileStructure = 3, // Server sends this Message to Send the Server File Structure

        RequestFileData = 4, // Client sends this Message to Request the Server File
        SendFileData = 5, // Server sends this Message to Send the Server File

        TextMessage = 2_147_483_647, // Spare Message for simple Text Messages Exchange
    }

    public class Message
    {
        public MessageType type;

        public int packageSize;

        public FileInformation fileInformation = new FileInformation();
        public List<(string Name, string Type, DateTime LastModified)> Definition;

        public string ToXML()
        {
            using (var stringwriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(stringwriter, this);
                return stringwriter.ToString();
            }
        }

        public static Message LoadFromXMLString(string xmlText)
        {
            using (var stringReader = new System.IO.StringReader(xmlText))
            {
                var serializer = new XmlSerializer(typeof(Message));
                return serializer.Deserialize(stringReader) as Message;
            }
        }
    }
}
