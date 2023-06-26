using System;

namespace OpenVTT.NetworkMessage
{
    public class FileInformation
    {
        public DateTime? LastWriteTime;

        public string fileLocation;
        public byte[] fileContent;
    }
}
