using AssetsTools.NET;
using AssetsTools.NET.Extra;
using NAudio.Wave;
using NAudio.Extras;
using NAudio.Wasapi;
using System;
using System.Reflection;
using OggVorbisEncoder;
using NVorbis;
using NAudio.Vorbis;
using Timer = System.Windows.Forms.Timer;

namespace AudioClipAssetReplacer
{
    public partial class MainForm : Form
    {
        private bool _modified = false;
        private string _initFormTitle;

        private const string ERROR_TITLE = "Error";
        private const string QUESTION_TITLE = "Question";

        private AssetsManager _manager = new AssetsManager();
        private AssetsFileInstance? _fileInstance;
        private AssetsFile? _assetsFile;
        private string _assetsPath = string.Empty;

        private List<string> _resourceFileNames = new();
        private Dictionary<string, ResourceManager> _resourcesDictionary = new();

        private WasapiOut? _outputDevice;
        private float VolumeDevice
        {
            get
            {
                if (_outputDevice == null)
                    return 0f;
                return _outputDevice.Volume;
            }
            set
            {
                if (_outputDevice != null)
                    _outputDevice.Volume = value;
            }
        }
        private WaveStream? _audioReader;
        private Timer? _playbackTimer;

        private bool NoResources => _resourceFileNames.Count == 0;
        private string SelectedResourceFileName => _resourceFileNames[resourceComboBox.SelectedIndex];

        public MainForm()
        {
            InitializeComponent();
            _initFormTitle = Text;
            FormClosing += MainForm_FormClosing;
            volumeSlider.VolumeChanged += VolumeSlider_VolumeChanged;
            audioTrackBar.MouseDown += AudioTrackBar_MouseDown;
            audioTrackBar.MouseUp += AudioTrackBar_MouseUp;
            searchTextBox.KeyDown += SearchTextBox_KeyDown;
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
            DisposeAllResourceManagersFileStreams();
            audioGridView.Rows.Clear();
            _manager.LoadClassPackage("classdata.tpk");
            _fileInstance = _manager.LoadAssetsFile(assetsPath, loadDeps: true);
            _assetsFile = _fileInstance.file;
            _manager.LoadClassDatabaseFromPackage(_assetsFile.Metadata.UnityVersion);
            _resourcesDictionary = GetResourceManagerDictionary();
            resourceComboBox.Items.Clear();
            resourceComboBox.Items.AddRange(_resourceFileNames.ToArray());
            if (!NoResources)
            {
                resourceComboBox.SelectedIndex = 0;
                UpdateAudioGrid(SelectedResourceFileName);
            }
        }

        private static FileStream? OpenFileStream(string filePath)
        {
            try
            {
                FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Read);
                return fs;
            }
            catch { return null; }
        }

        private void DisposeAllResourceManagersFileStreams()
        {
            foreach (string resourceFileName in _resourcesDictionary.Keys)
            {
                ResourceManager resourceManager = _resourcesDictionary[resourceFileName];
                resourceManager.fileStream?.Dispose();
            }
        }

        private Dictionary<string, ResourceManager> GetResourceManagerDictionary()
        {
            if (_fileInstance == null || _assetsFile == null)
                throw new NullReferenceException("Assets file is not loaded.");
            _resourceFileNames.Clear();
            List<AssetFileInfo> assetFileInfos = _assetsFile.GetAssetsOfType(AssetClassID.AudioClip);
            Dictionary<string, ResourceManager> resourcesDictionary = new();
            foreach (AssetFileInfo assetFileInfo in assetFileInfos)
            {
                AssetTypeValueField baseField = _manager.GetBaseField(_fileInstance, assetFileInfo);
                AudioClipAssetInfo audioInfo = new AudioClipAssetInfo(
                    assetFileInfo.PathId,
                    baseField["m_Name"].AsString,
                    baseField["m_Resource.m_Source"].AsString,
                    baseField["m_Resource.m_Offset"].AsULong,
                    baseField["m_Resource.m_Size"].AsULong,
                    assetFileInfo,
                    baseField
                );
                if (!resourcesDictionary.ContainsKey(audioInfo.resourceName))
                {
                    string assetsFileDirectory = Path.GetDirectoryName(_fileInstance.path) ?? string.Empty;
                    string resourceFilePath = Path.Combine(assetsFileDirectory, audioInfo.resourceName);
                    ResourceManager resourceManager = new ResourceManager(audioInfo.resourceName, OpenFileStream(resourceFilePath), new());
                    if (resourceManager.fileStream == null)
                    {
                        MessageBox.Show($"The resource file ({resourceManager.resourceFileName}) can't be opened for this moment. You won't be able to save this resource file in future.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    resourcesDictionary.Add(audioInfo.resourceName, resourceManager);
                    _resourceFileNames.Add(audioInfo.resourceName);
                }
                audioInfo.ResourceManager = resourcesDictionary[audioInfo.resourceName];
                resourcesDictionary[audioInfo.resourceName].audioClipInfos.Add(audioInfo);
            }
            foreach (string resourceName in resourcesDictionary.Keys)
            {
                resourcesDictionary[resourceName].audioClipInfos.Sort();
            }
            return resourcesDictionary;
        }

        private void UpdateAudioGrid(string resourceName, Predicate<string>? searchPredicate = null)
        {
            audioGridView.Rows.Clear();
            List<AudioClipAssetInfo> audioInfos = _resourcesDictionary[resourceName].audioClipInfos;
            foreach (AudioClipAssetInfo audioInfo in audioInfos)
            {
                if (searchPredicate != null && !searchPredicate.Invoke(audioInfo.name))
                    continue;
                int rowIndex = audioGridView.Rows.Add();
                DataGridViewRow audioInfoRow = audioGridView.Rows[rowIndex];
                audioInfoRow.Cells[audioGridView.Columns["NameColumn"].Index].Value = audioInfo.name;
                audioInfoRow.Cells[audioGridView.Columns["PathIDColumn"].Index].Value = audioInfo.pathId;
                audioInfoRow.Cells[audioGridView.Columns["OffsetColumn"].Index].Value = audioInfo.audioOffset;
                audioInfoRow.Cells[audioGridView.Columns["SizeColumn"].Index].Value = audioInfo.DataSize;
                audioInfoRow.Cells[audioGridView.Columns["SourceColumn"].Index].Value = audioInfo.resourceName;
                audioInfoRow.Cells[audioGridView.Columns["ModifiedColumn"].Index].Value = audioInfo.HasNewData ? "*" : string.Empty;
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

        private void SaveAssetsFile(AssetsFile? assetsFile, string filePath)
        {
            if (assetsFile == null)
                return;
            string tmpAssetsFile = $"{filePath}.tmp";
            using (AssetsFileWriter writer = new AssetsFileWriter(tmpAssetsFile))
            {
                assetsFile.Write(writer);
            }
            assetsFile.Close();
            File.Move(tmpAssetsFile, filePath, true);
            assetsFile.Read(new AssetsFileReader(filePath));
        }

        private void SaveResourceFiles()
        {
            if (_fileInstance == null)
                return;
            foreach (string resourceFileName in _resourcesDictionary.Keys)
            {
                ResourceManager resourceManager = _resourcesDictionary[resourceFileName];
                if (!resourceManager.WasModified)
                    continue;
                if (resourceManager.fileStream == null)
                {
                    MessageBox.Show($"The resource file ({resourceManager.resourceFileName}) won't be saved because there were a problems with its opening.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
                string resourceFolder = Path.GetDirectoryName(_fileInstance.path) ?? string.Empty;
                string resourceFilePath = Path.Combine(resourceFolder, resourceManager.resourceFileName);
                string tempResourceFilePath = $"{resourceFilePath}.tmp";
                FileStream resourceFileStream = resourceManager.BuildNewResourceFile(tempResourceFilePath);
                resourceFileStream.Close();
                resourceManager.fileStream.Close();
                File.Move(tempResourceFilePath, resourceFilePath, true);
                resourceManager.fileStream = OpenFileStream(resourceFilePath);
            }
        }

        private void SaveAsResourceFiles()
        {
            foreach (string resourceFileName in _resourcesDictionary.Keys)
            {
                ResourceManager resourceManager = _resourcesDictionary[resourceFileName];
                if (!resourceManager.WasModified)
                    continue;
                if (resourceManager.fileStream == null)
                {
                    MessageBox.Show($"The resource file ({resourceManager.resourceFileName}) won't be saved because there were a problems with its opening.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
                saveResourceDialog.FileName = resourceFileName;
                if (saveResourceDialog.ShowDialog() == DialogResult.Cancel)
                    continue;
                string resourceFilePath = saveResourceDialog.FileName;
                FileStream resourceFileStream = resourceManager.BuildNewResourceFile(resourceFilePath);
                resourceFileStream.Close();
                resourceManager.fileStream.Close();
                resourceManager.fileStream = OpenFileStream(resourceFilePath);
            }
        }

        private void ExportAudio()
        {
            if (audioGridView.Rows.Count == 0)
                return;
            DataGridViewRow selectedRow = audioGridView.SelectedRows[0];
            AudioClipAssetInfo? audioClipInfo = selectedRow.Tag as AudioClipAssetInfo;
            if (audioClipInfo == null)
                return;
            if (audioClipInfo.FileStream == null)
            {
                MessageBox.Show($"Couldn't open the resource file ({audioClipInfo.resourceName}).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            byte[] audioData = audioClipInfo.GetAudioData(out string fileExtension);
            if (audioData.Length == 0)
            {
                MessageBox.Show("Couldn't convert FSB data to a standart audio format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            saveAudioDialog.Filter = $"{fileExtension} files|*.{fileExtension}|All files|*.*";
            saveAudioDialog.FileName = $"{audioClipInfo.name}.{fileExtension}";
            if (saveAudioDialog.ShowDialog() == DialogResult.Cancel)
                return;
            File.WriteAllBytes(saveAudioDialog.FileName, audioData);
        }

        private void ReplaceAudio()
        {
            if (audioGridView.Rows.Count == 0)
                return;
            DataGridViewRow selectedRow = audioGridView.SelectedRows[0];
            AudioClipAssetInfo? audioClipInfo = selectedRow.Tag as AudioClipAssetInfo;
            if (audioClipInfo == null)
                return;
            if (openAudioDialog.ShowDialog() == DialogResult.Cancel)
                return;
            byte[] audioData = File.ReadAllBytes(openAudioDialog.FileName);
            audioClipInfo.ReplaceAudioData(audioData);
            selectedRow.Cells[audioGridView.Columns["SizeColumn"].Index].Value = audioClipInfo.DataSize;
            selectedRow.Cells[audioGridView.Columns["ModifiedColumn"].Index].Value = audioClipInfo.HasNewData ? "*" : string.Empty;
            SetModifiedState(ModifiedState.Modified);
        }

        private void CancelChanges()
        {
            if (audioGridView.Rows.Count == 0)
                return;
            DataGridViewRow selectedRow = audioGridView.SelectedRows[0];
            AudioClipAssetInfo? audioClipInfo = selectedRow.Tag as AudioClipAssetInfo;
            if (audioClipInfo == null)
                return;
            audioClipInfo.CancelReplacingAudioData();
            selectedRow.Cells[audioGridView.Columns["SizeColumn"].Index].Value = audioClipInfo.DataSize;
            selectedRow.Cells[audioGridView.Columns["ModifiedColumn"].Index].Value = audioClipInfo.HasNewData ? "*" : string.Empty;
        }

        private void SearchAudioAssets()
        {
            if (_fileInstance == null || NoResources)
                return;
            Predicate<string>? searchPredicate = (audioClipName) =>
            {
                return audioClipName.Contains(searchTextBox.Text);
            };
            if (string.IsNullOrEmpty(searchTextBox.Text))
                searchPredicate = null;
            UpdateAudioGrid(SelectedResourceFileName, searchPredicate);
        }

        private void PlayAudio(bool sameAudio = false)
        {
            if (audioGridView.Rows.Count == 0)
                return;
            DataGridViewRow selectedRow = audioGridView.SelectedRows[0];
            AudioClipAssetInfo? audioClipInfo = selectedRow.Tag as AudioClipAssetInfo;
            if (audioClipInfo == null)
                return;
            if (_outputDevice != null)
            {
                _outputDevice.PlaybackStopped -= OnPlaybackStopped;
                _outputDevice.Stop();
                DisposeAudio(sameAudio);
            }
            _outputDevice = new WasapiOut();
            if (_audioReader == null)
            {
                MemoryStream audioStream = new MemoryStream(audioClipInfo.GetAudioData(out _));
                try
                {
                    _audioReader = new VorbisWaveReader(audioStream);
                }
                catch
                {
                    _audioReader = new StreamMediaFoundationReader(audioStream);
                }
            }
            _outputDevice.Init(_audioReader);
            _outputDevice.PlaybackStopped += OnPlaybackStopped;
            VolumeDevice = volumeSlider.Volume;
            if (_playbackTimer == null)
                _playbackTimer = new Timer() { Interval = 1 };
            _playbackTimer.Tick += _playbackTimer_Tick;
            _playbackTimer.Start();
            _outputDevice.Play();

            audioTrackBar.Maximum = (int)_audioReader.TotalTime.TotalMilliseconds;
        }

        private void OnPlaybackStopped(object? sender, StoppedEventArgs e)
        {
            if (loopCheckButton.Checked)
            {
                PlayAudio(true);
            }
        }

        private void _playbackTimer_Tick(object? sender, EventArgs e)
        {
            if (_audioReader == null)
                return;
            audioTrackBar.Value = (int)_audioReader.CurrentTime.TotalMilliseconds;
            if (_outputDevice?.PlaybackState == PlaybackState.Stopped)
            {
                DisposeAudio();
            }
        }

        private void PauseAudio()
        {
            if (_outputDevice?.PlaybackState == PlaybackState.Playing)
            {
                _outputDevice?.Pause();
                pauseAudioButton.Text = "Resume audio";
            }
            else if (_outputDevice?.PlaybackState == PlaybackState.Paused)
            {
                _outputDevice?.Play();
                pauseAudioButton.Text = "Pause audio";
            }
        }

        private void ChangeAudioPosition(int audioTimeMilliseconds)
        {
            if (_audioReader != null)
                _audioReader.CurrentTime = TimeSpan.FromMilliseconds((double)audioTimeMilliseconds);
        }

        private void DisposeAudio(bool keepAudio = false)
        {
            if (_playbackTimer != null)
            {
                _playbackTimer.Tick -= _playbackTimer_Tick;
            }
            _playbackTimer?.Stop();
            _playbackTimer?.Dispose();
            _playbackTimer = null;
            if (!keepAudio)
            {
                _audioReader?.Dispose();
                _audioReader = null;
            }
            else if (_audioReader != null)
                _audioReader.CurrentTime = TimeSpan.Zero;
            _outputDevice?.Dispose();
            _outputDevice = null;
            pauseAudioButton.Text = "Pause audio";
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_assetsFile == null)
            {
                MessageBox.Show("Assets file is not loaded.", ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SaveResourceFiles();
            SaveAssetsFile(_assetsFile, _assetsPath);
            SetModifiedState(ModifiedState.Saved);
            UpdateAudioGrid(SelectedResourceFileName);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_assetsFile == null)
            {
                MessageBox.Show("Assets file is not loaded.", ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            saveAssetsDialog.FileName = Path.GetFileName(_assetsPath);
            if (saveAssetsDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            SaveAsResourceFiles();
            SaveAssetsFile(_assetsFile, saveAssetsDialog.FileName);
            _assetsPath = saveAssetsDialog.FileName;
            SetModifiedState(ModifiedState.Saved);
            UpdateAudioGrid(SelectedResourceFileName);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void exportAudioButton_Click(object sender, EventArgs e)
        {
            ExportAudio();
        }

        private void replaceAudioButton_Click(object sender, EventArgs e)
        {
            ReplaceAudio();
        }

        private void cancelChangesButton_Click(object sender, EventArgs e)
        {
            CancelChanges();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            SearchAudioAssets();
        }

        private void SearchTextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchAudioAssets();
                e.SuppressKeyPress = true;
            }
        }

        private void playAudioButton_Click(object sender, EventArgs e)
        {
            PlayAudio();
        }

        private void pauseAudioButton_Click(object sender, EventArgs e)
        {
            PauseAudio();
        }

        private void VolumeSlider_VolumeChanged(object? sender, EventArgs e)
        {
            VolumeDevice = volumeSlider.Volume;
        }

        private void AudioTrackBar_MouseDown(object? sender, MouseEventArgs e)
        {
            _playbackTimer?.Stop();
        }

        private void audioTrackBar_Scroll(object sender, EventArgs e)
        {
            ChangeAudioPosition(audioTrackBar.Value);
        }

        private void AudioTrackBar_MouseUp(object? sender, MouseEventArgs e)
        {
            _playbackTimer?.Start();
        }
    }
}