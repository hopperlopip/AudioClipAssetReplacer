using AssetsTools.NET.Extra;
using AssetsTools.NET;

namespace AudioClipAssetReplacer
{
    public partial class MainForm : Form
    {
        const string ERROR_TITLE = "Error";
        AssetsManager manager = new AssetsManager();
        AssetsFileInstance fileInstance;
        AssetsFile assetsFile;
        string assetsPath;
        byte[] fsbFileData;
        string resourcePath;
        byte[] resourceFile;

        public MainForm()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openAssetsDialog.ShowDialog() != DialogResult.Cancel)
            {
                assetsPath = openAssetsDialog.FileName;
                LoadAssetsFile(assetsPath);
            }
        }

        private void LoadAssetsFile(string assetsPath)
        {
            audioGridView.Rows.Clear();
            manager.LoadClassPackage("classdata.tpk");
            fileInstance = manager.LoadAssetsFile(assetsPath, loadDeps: true);
            assetsFile = fileInstance.file;
            manager.LoadClassDatabaseFromPackage(assetsFile.Metadata.UnityVersion);
            UpdateAssetsFileList();
        }

        private void UpdateAssetsFileList()
        {
            if (assetsFile != null)
            {
                int savedSelectedRowIndex = 0;
                if (audioGridView.SelectedRows.Count != 0)
                {
                    savedSelectedRowIndex = audioGridView.SelectedRows[0].Index;
                }
                audioGridView.Rows.Clear();
                List<AssetFileInfo> audioInfos = assetsFile.GetAssetsOfType(AssetClassID.AudioClip);
                for (int i = 0; i < audioInfos.Count; i++)
                {
                    AssetFileInfo audioInfo = audioInfos[i];
                    AssetTypeValueField audioBase = manager.GetBaseField(fileInstance, audioInfo);
                    audioGridView.Rows.Add();
                    audioGridView.Rows[i].Cells[0].Value = audioBase["m_Name"].AsString;
                    audioGridView.Rows[i].Cells[1].Value = audioInfo.PathId;
                    audioGridView.Rows[i].Cells[2].Value = audioBase["m_Resource.m_Offset"].AsULong;
                    audioGridView.Rows[i].Cells[3].Value = audioBase["m_Resource.m_Size"].AsULong;
                    audioGridView.Rows[i].Cells[4].Value = audioBase["m_Resource.m_Source"].AsString;
                }
                if (audioGridView.Rows.Count != 0)
                {
                    audioGridView.Rows[savedSelectedRowIndex].Selected = true;
                }
            }
        }

        private byte[] GetFsbData(long pathID)
        {
            AssetTypeValueField audioBase = manager.GetBaseField(fileInstance, pathID);
            string resourceName = audioBase["m_Resource.m_Source"].AsString;
            ulong audioOffset = audioBase["m_Resource.m_Offset"].AsULong;
            ulong dataSize = audioBase["m_Resource.m_Size"].AsULong;
            string currentDir = GetDirectory(assetsPath);
            string resourcePath = currentDir + resourceName;
            if (!File.Exists(resourcePath))
            {
                MessageBox.Show("The resource file (" + resourceName + ") doesn't exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return new byte[0];
            }
            byte[] resourceBytes = File.ReadAllBytes(resourcePath);
            MemoryStream memoryStream = new MemoryStream(resourceBytes);
            BinaryReader binaryReader = new BinaryReader(memoryStream);
            binaryReader.BaseStream.Position = Convert.ToInt64(audioOffset);
            return binaryReader.ReadBytes(Convert.ToInt32(dataSize));
        }

        private void LinkFsbData(long pathID, string fsbPath, long fsbSize)
        {
            fsbPath = fsbPath.Replace("\\", "/");
            AssetTypeValueField audioBase = manager.GetBaseField(fileInstance, pathID);
            AssetFileInfo audioInfo = assetsFile.GetAssetInfo(pathID);
            audioBase["m_Resource.m_Source"].AsString = fsbPath;
            audioBase["m_Resource.m_Offset"].AsULong = 0uL;
            audioBase["m_Resource.m_Size"].AsULong = Convert.ToUInt64(fsbSize);
            audioInfo.SetNewData(audioBase);
        }

        private void ReplaceFsbData(long pathID)
        {
            AssetTypeValueField audioBase = manager.GetBaseField(fileInstance, pathID);
            AssetFileInfo audioInfo = assetsFile.GetAssetInfo(pathID);
            string resourceName = audioBase["m_Resource.m_Source"].AsString;
            ulong audioOffset = audioBase["m_Resource.m_Offset"].AsULong;
            ulong dataSize = audioBase["m_Resource.m_Size"].AsULong;
            string currentDir = GetDirectory(assetsPath);
            resourcePath = currentDir + resourceName;
            if (!File.Exists(resourcePath))
            {
                MessageBox.Show($"The resource file ({resourceName}) doesn't exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            byte[] resourceBytes;
            if (Path.GetFileName(resourcePath) != resourceName || resourceFile == null)
            {
                resourceBytes = File.ReadAllBytes(resourcePath);
            }
            else
            {
                resourceBytes = resourceFile;
            }
            MemoryStream memoryStream = new MemoryStream();
            BinaryWriter binaryWriter = new BinaryWriter(memoryStream);

            //MessageBox.Show($"audioOffset: {audioOffset}\r\naudioOffset + dataSize: {audioOffset + dataSize}\r\ndataSize: {dataSize}\r\nresourceBytes.Length - Convert.ToInt32(audioOffset + dataSize): {resourceBytes.Length - Convert.ToInt32(audioOffset + dataSize)}");

            binaryWriter.Write(resourceBytes, 0, Convert.ToInt32(audioOffset));
            binaryWriter.Write(fsbFileData);
            binaryWriter.Write(resourceBytes, Convert.ToInt32(audioOffset + dataSize), resourceBytes.Length - Convert.ToInt32(audioOffset + dataSize));
            resourceFile = memoryStream.ToArray();
            audioBase["m_Resource.m_Size"].AsULong = Convert.ToUInt64(fsbFileData.LongLength);
            audioInfo.SetNewData(audioBase);
            for (int i = 0; i < audioGridView.Rows.Count; i++)
            {
                ulong currentAudioOffset = Convert.ToUInt64(audioGridView.Rows[i].Cells[2].Value);
                if (currentAudioOffset > audioOffset)
                {
                    long currentPathID = Convert.ToInt64(audioGridView.Rows[i].Cells[1].Value);
                    AssetTypeValueField currentAudioBase = manager.GetBaseField(fileInstance, currentPathID);
                    AssetFileInfo currentAudioInfo = assetsFile.GetAssetInfo(currentPathID);
                    if (currentAudioBase["m_Resource.m_Source"].AsString != resourceName)
                    {
                        break;
                    }
                    long offsetAfterWriting = fsbFileData.LongLength - Convert.ToInt64(dataSize);
                    if (offsetAfterWriting > 0)
                    {
                        currentAudioBase["m_Resource.m_Offset"].AsULong += Convert.ToUInt64(offsetAfterWriting);
                    }
                    else
                    {
                        offsetAfterWriting *= -1;
                        currentAudioBase["m_Resource.m_Offset"].AsULong -= Convert.ToUInt64(offsetAfterWriting);
                    }
                    currentAudioInfo.SetNewData(currentAudioBase);
                }
            }
        }

        private void SaveAssetsFile(AssetsFile assetsFile, string filePath)
        {
            MemoryStream memoryStream = new MemoryStream();
            using AssetsFileWriter writer = new AssetsFileWriter(memoryStream);
            assetsFile.Write(writer, 0L);
            assetsFile.Close();
            using (FileStream fs = File.OpenWrite(filePath))
            {
                memoryStream.WriteTo(fs);
            }
            assetsFile.Read(new AssetsFileReader(filePath));
        }

        private string GetDirectory(string filePath)
        {
            return filePath.Replace(Path.GetFileName(filePath), string.Empty);
        }

        private void replaceButton_Click(object sender, EventArgs e)
        {
            if (audioGridView.Rows.Count != 0 && openFsbDialog.ShowDialog() != DialogResult.Cancel)
            {
                fsbFileData = File.ReadAllBytes(openFsbDialog.FileName);
                long pathID = Convert.ToInt64(audioGridView.SelectedRows[0].Cells[1].Value);
                ReplaceFsbData(pathID);
                UpdateAssetsFileList();
            }
        }

        private void linkButton_Click(object sender, EventArgs e)
        {
            if (audioGridView.Rows.Count != 0 && openFsbDialog.ShowDialog() != DialogResult.Cancel)
            {
                string mainDir = GetDirectory(assetsPath);
                if (!openFsbDialog.FileName.StartsWith(mainDir))
                {
                    MessageBox.Show("Your file must be located inside a folder of the assets file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                string relFsbPath = openFsbDialog.FileName.Replace(mainDir, string.Empty);
                long pathID = Convert.ToInt64(audioGridView.SelectedRows[0].Cells[1].Value);
                long size = new FileInfo(openFsbDialog.FileName).Length;
                LinkFsbData(pathID, relFsbPath, size);
                UpdateAssetsFileList();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAssetsFile(assetsFile, assetsPath);
            if (resourceFile != null)
            {
                File.WriteAllBytes(resourcePath, resourceFile);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveAssetsDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            if (resourceFile != null)
            {
                if (saveResourceDialog.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                File.WriteAllBytes(saveResourceDialog.FileName, resourceFile);
            }
            SaveAssetsFile(assetsFile, saveAssetsDialog.FileName);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}