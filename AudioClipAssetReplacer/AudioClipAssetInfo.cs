using AssetsTools.NET;
using AudioClipAssetReplacer.FSB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioClipAssetReplacer
{
    public class AudioClipAssetInfo : IComparable<AudioClipAssetInfo>
    {
        private readonly AssetFileInfo _assetInfo;
        private readonly AssetTypeValueField _baseField;
        public readonly long pathId;
        public readonly string name = string.Empty;
        public readonly string resourceName = string.Empty;
        public ulong audioOffset;
        private ulong _dataSize;
        private ResourceManager? _resourceManager;
        private byte[] _newData = Array.Empty<byte>();
        public bool HasNewData => _newData.Length != 0;
        public ResourceManager ResourceManager
        {
            get
            {
                if (_resourceManager == null)
                    throw new NullReferenceException("The resource manager was null.");
                return _resourceManager;
            }
            set => _resourceManager = value;
        }
        public FileStream? FileStream => ResourceManager.fileStream;
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

        public AudioClipAssetInfo(long pathId, string name, string resourceName, ulong audioOffset, ulong dataSize, AssetFileInfo assetInfo, AssetTypeValueField baseField)
        {
            this.pathId = pathId;
            this.name = name;
            this.resourceName = resourceName;
            this.audioOffset = audioOffset;
            _dataSize = dataSize;
            _assetInfo = assetInfo;
            _baseField = baseField;
        }

        public void UpdateBaseFieldValues()
        {
            _baseField["m_Name"].AsString = name;
            _baseField["m_Resource.m_Source"].AsString = resourceName;
            _baseField["m_Resource.m_Offset"].AsULong = audioOffset;
            _baseField["m_Resource.m_Size"].AsULong = DataSize;
            _assetInfo.SetNewData(_baseField);
        }

        public byte[] GetFsbAudioData()
        {
            if (_newData.Length != 0)
                return _newData;
            if (FileStream == null)
                return Array.Empty<byte>();
            BinaryReader reader = new BinaryReader(FileStream);
            reader.BaseStream.Position = (long)audioOffset;
            return reader.ReadBytes((int)_dataSize);
        }

        public byte[] GetAudioData(out string fileExtension)
        {
            fileExtension = string.Empty;
            byte[] fsbData = GetFsbAudioData();
            if (fsbData.Length == 0)
                return Array.Empty<byte>();
            return FsbHelper.ConvertFromFsb(fsbData, out fileExtension);
        }

        public void ReplaceFsbAudioData(byte[] fsbData)
        {
            _newData = fsbData;
            ResourceManager.WasModified = true;
        }

        public void ReplaceAudioData(byte[] audioData)
        {
            ReplaceFsbAudioData(FsbHelper.ConvertToFsb(audioData));
        }

        public void CancelReplacingAudioData()
        {
            _newData = Array.Empty<byte>();
        }

        public int CompareTo(AudioClipAssetInfo? other)
        {
            if (other == null) return 1;
            return audioOffset.CompareTo(other.audioOffset);
        }
    }
}
