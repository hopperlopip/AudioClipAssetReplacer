using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioClipAssetReplacer
{
    public class AudioClipAssetInfo
    {
        public long pathId;
        public string name = string.Empty;
        public string resourceName = string.Empty;
        public ulong audioOffset;
        public ulong dataSize;
    }
}
