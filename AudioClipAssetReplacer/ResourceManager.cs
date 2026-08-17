using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioClipAssetReplacer
{
    public class ResourceManager
    {
        public readonly string resourceFileName;
        public FileStream? fileStream;
        public List<AudioClipAssetInfo> audioClipInfos = new();
        public bool WasModified { get; set; } = false;

        public ResourceManager(string resourceFileName, FileStream? fileStream, List<AudioClipAssetInfo> audioClipInfos)
        {
            this.resourceFileName = resourceFileName;
            this.fileStream = fileStream;
            this.audioClipInfos = audioClipInfos;
        }

        public FileStream BuildNewResourceFile(string filePath)
        {
            FileStream fileStream = File.Create(filePath);
            BinaryWriter writer = new BinaryWriter(fileStream);
            foreach (var audioClipInfo in audioClipInfos)
            {
                byte[] fsbData = audioClipInfo.GetFsbAudioData();
                audioClipInfo.audioOffset = (ulong)writer.BaseStream.Position;
                audioClipInfo.UpdateBaseFieldValues();
                writer.Write(fsbData);
            }
            return fileStream;
        }
    }
}
