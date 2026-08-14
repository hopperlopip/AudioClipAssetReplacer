using AssetsTools.NET.Extra;
using AssetsTools.NET;
using System.Reflection;

namespace AudioClipAssetReplacer
{
    public partial class MainForm : Form
    {
        private bool _modified = false;
        private string _initFormTitle;

        private const string ERROR_TITLE = "Error";
        private const string QUESTION_TITLE = "Question";

        private AssetsManager _manager = new AssetsManager();
        private AssetsFileInstance _fileInstance;
        private AssetsFile _assetsFile;
        private string _assetsPath;
        private List<string> _resourceFileNames = new();
        public static Dictionary<string, FileStream?> resourceFileStreams = new();
        private Dictionary<string, List<AudioClipAssetInfo>> _audioClipFileDictionary = new();

        public MainForm()
        {
            InitializeComponent();
            _initFormTitle = Text;
            FormClosing += MainForm_FormClosing;
        }

        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (_modified == true)
            {
                switch (MessageBox.Show("Would you like to save changes before exit?", QUESTION_TITLE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        SaveAssetsFile(_assetsFile, _assetsPath);
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        enum ModifiedState
        {
            Modified,
            Saved,
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openAssetsDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            _assetsPath = openAssetsDialog.FileName;
            if (_assetsFile != null)
            {
                _manager.UnloadAll();
            }
            LoadAssetsFile(_assetsPath);
            SetModifiedState(ModifiedState.Saved);
        }

        private void LoadAssetsFile(string assetsPath)
        {
            audioGridView.Rows.Clear();
            _manager.LoadClassPackage("classdata.tpk");
            _fileInstance = _manager.LoadAssetsFile(assetsPath, loadDeps: true);
            _assetsFile = _fileInstance.file;
            _manager.LoadClassDatabaseFromPackage(_assetsFile.Metadata.UnityVersion);
            _audioClipFileDictionary = GetAudioClipFileDictionary();
            resourceComboBox.Items.Clear();
            resourceComboBox.Items.AddRange(_resourceFileNames.ToArray());
            if (_resourceFileNames.Count != 0)
            {
                resourceComboBox.SelectedIndex = 0;
                UpdateAudioGrid(_resourceFileNames[resourceComboBox.SelectedIndex]);
            }
        }

        private static FileStream? OpenFileStream(string filePath)
        {
            try
            {
                FileStream fs = File.OpenWrite(filePath);
                return fs;
            }
            catch { return null; }
        }

        private Dictionary<string, List<AudioClipAssetInfo>> GetAudioClipFileDictionary()
        {
            _resourceFileNames.Clear();
            List<AssetFileInfo> assetFileInfos = _assetsFile.GetAssetsOfType(AssetClassID.AudioClip);
            Dictionary<string, List<AudioClipAssetInfo>> audioClipFileDictionary = new();
            foreach (AssetFileInfo assetFileInfo in assetFileInfos)
            {
                AssetTypeValueField baseField = _manager.GetBaseField(_fileInstance, assetFileInfo);
                AudioClipAssetInfo audioInfo = new AudioClipAssetInfo(
                    assetFileInfo.PathId,
                    baseField["m_Name"].AsString,
                    baseField["m_Resource.m_Source"].AsString,
                    baseField["m_Resource.m_Offset"].AsULong,
                    baseField["m_Resource.m_Size"].AsULong
                );
                if (!audioClipFileDictionary.ContainsKey(audioInfo.resourceName))
                {
                    audioClipFileDictionary.Add(audioInfo.resourceName, new());
                    _resourceFileNames.Add(audioInfo.resourceName);
                }
                audioClipFileDictionary[audioInfo.resourceName].Add(audioInfo);
            }
            foreach (string fileName in audioClipFileDictionary.Keys)
            {
                audioClipFileDictionary[fileName].Sort();
            }
            return audioClipFileDictionary;
        }

        private void UpdateAudioGrid(string resourceName)
        {
            audioGridView.Rows.Clear();
            List<AudioClipAssetInfo> audioInfos = _audioClipFileDictionary[resourceName];
            foreach (AudioClipAssetInfo audioInfo in audioInfos)
            {
                int rowIndex = audioGridView.Rows.Add();
                DataGridViewRow audioInfoRow = audioGridView.Rows[rowIndex];
                audioInfoRow.Cells[audioGridView.Columns["NameColumn"].Index].Value = audioInfo.name;
                audioInfoRow.Cells[audioGridView.Columns["PathIDColumn"].Index].Value = audioInfo.pathId;
                audioInfoRow.Cells[audioGridView.Columns["OffsetColumn"].Index].Value = audioInfo.audioOffset;
                audioInfoRow.Cells[audioGridView.Columns["SizeColumn"].Index].Value = audioInfo.DataSize;
                audioInfoRow.Cells[audioGridView.Columns["SourceColumn"].Index].Value = audioInfo.resourceName;
                audioInfoRow.Tag = audioInfo;
            }
        }

        private void SetModifiedState(ModifiedState state)
        {
            if (state == ModifiedState.Modified)
            {
                _modified = true;
                Text = _initFormTitle + $" - {Path.GetFileName(_assetsPath)} - Modified";
            }
            else if (state == ModifiedState.Saved)
            {
                _modified = false;
                Text = _initFormTitle + $" - {Path.GetFileName(_assetsPath)}";
            }
        }

        private void SaveAssetsFile(AssetsFile assetsFile, string filePath)
        {
            string tmpAssetsFile = $"{filePath}.tmp";
            using (AssetsFileWriter writer = new AssetsFileWriter(tmpAssetsFile))
            {
                assetsFile.Write(writer);
            }
            assetsFile.Close();
            File.Move(tmpAssetsFile, filePath, true);
            assetsFile.Read(new AssetsFileReader(filePath));
        }

        private void replaceButton_Click(object sender, EventArgs e)
        {
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_assetsFile == null)
            {
                MessageBox.Show("Assets file is not loaded.", ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SaveAssetsFile(_assetsFile, _assetsPath);
            SetModifiedState(ModifiedState.Saved);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_assetsFile == null)
            {
                MessageBox.Show("Assets file is not loaded.", ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (saveAssetsDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            SaveAssetsFile(_assetsFile, saveAssetsDialog.FileName);
            _assetsPath = saveAssetsDialog.FileName;
            SetModifiedState(ModifiedState.Saved);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}