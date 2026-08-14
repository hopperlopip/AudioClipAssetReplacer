using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioClipAssetReplacer
{
    public class AudioClipAssetInfo : IComparable<AudioClipAssetInfo>
    {
        public readonly long pathId;
        public readonly string name = string.Empty;
        public readonly string resourceName = string.Empty;
        public readonly ulong audioOffset;
        private ulong _dataSize;
        private byte[] _newData = Array.Empty<byte>();
        public bool HasNewData => _newData.Length != 0;
        public FileStream? ResourceFileStream => GetResourceFileStream();
        public ulong DataSize
        {
            get
            {
                if (_newData.Length == 0)
                    return _dataSize;
                else
                    return (ulong)_newData.Length;
            }
        }

        public AudioClipAssetInfo(long pathId, string name, string resourceName, ulong audioOffset, ulong dataSize)
        {
            this.pathId = pathId;
            this.name = name;
            this.resourceName = resourceName;
            this.audioOffset = audioOffset;
            _dataSize = dataSize;
        }

        public byte[] GetFsbAudioData()
        {
            if (_newData.Length != 0)
                return _newData;
            if (ResourceFileStream == null)
                return Array.Empty<byte>();
            using BinaryReader reader = new BinaryReader(ResourceFileStream);
            reader.BaseStream.Position = (long)audioOffset;
            return reader.ReadBytes((int)_dataSize);
        }

        public void ReplaceFsbAudioData(byte[] data)
        {
            _newData = data;
        }

        public void CancelReplacingAudioData()
        {
            _newData = Array.Empty<byte>();
        }

        private FileStream? GetResourceFileStream()
        {
            FileStream? fileStream;
            if (!MainForm.resourceFileStreams.ContainsKey(resourceName))
                fileStream = null;
            else
                fileStream = MainForm.resourceFileStreams[resourceName];
            return fileStream;
        }

        public int CompareTo(AudioClipAssetInfo? other)
        {
            if (other == null) return 1;
            return audioOffset.CompareTo(other.audioOffset);
        }
    }
}
