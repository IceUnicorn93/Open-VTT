using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenVTT.Scripting
{
    internal class VolatileClassForMetaData
    {
        public volatile System.Reflection.Metadata.BlobBuilder blobBuilder;
    }
}
