using Fmod5Sharp;
using Fmod5Sharp.FmodTypes;
using FSBank.V1;

namespace AudioClipAssetReplacer.FSB
{
    public static class FsbHelper
    {
        private static readonly string _cacheFolderName = Path.Combine(Environment.CurrentDirectory, "Temp");

        public static byte[] ConvertFromFsb(byte[] fsbData, out string fileExtension)
        {
            FmodSoundBank bank = FsbLoader.LoadFsbFromByteArray(fsbData);
            List<FmodSample> samples = bank.Samples;
            var success = samples[0].RebuildAsStandardFileFormat(out byte[]? audioData, out string? fileExt);
            if (!success || audioData == null || fileExt == null)
            {
                fileExtension = string.Empty;
                return Array.Empty<byte>();
            }
            fileExtension = fileExt;
            return audioData;
        }

        public static byte[] ConvertToFsb(byte[] audioData)
        {
            if (Directory.Exists(_cacheFolderName))
                Directory.Delete(_cacheFolderName, true);
            Directory.CreateDirectory(_cacheFolderName);

            Methods.FSBank_Init(FSBankInitFlags.Normal, _cacheFolderName, 2);
            uint quality = 100;
            string buildFolder = Path.Combine(_cacheFolderName, "Build");
            Directory.CreateDirectory(buildFolder);
            string filePath = GetRandomFileNameInFolder(buildFolder);
            Methods.FSBank_Build(audioData, FSBANK_FORMAT.FSBANK_FORMAT_VORBIS, FSBankBuildFlags.DisableSyncPoints, quality, filePath);
            byte[] fsbData = File.ReadAllBytes(filePath);
            Directory.Delete(_cacheFolderName, true);
            Methods.FSBank_Release();
            return fsbData;
        }

        private static string GetRandomFileNameInFolder(string folderName)
        {
            string filePath;
            do
            {
                filePath = Path.Combine(folderName, Path.GetRandomFileName());
            } while (File.Exists(filePath));
            return filePath;
        }
    }
}
