namespace AudioClipAssetReplacer
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            openAssetsDialog = new OpenFileDialog();
            openFsbDialog = new OpenFileDialog();
            saveAssetsDialog = new SaveFileDialog();
            audioGridView = new DataGridView();
            splitContainer1 = new SplitContainer();
            exportAudioButton = new Button();
            replaceAudioButton = new Button();
            saveResourceDialog = new SaveFileDialog();
            resourceComboBox = new ComboBox();
            volumeSlider = new NAudio.Gui.VolumeSlider();
            playAudioButton = new Button();
            pauseAudioButton = new Button();
            saveAudioDialog = new SaveFileDialog();
            openAudioDialog = new OpenFileDialog();
            cancelChangesButton = new Button();
            resourceFileGroupBox = new GroupBox();
            searchTextBox = new TextBox();
            searchButton = new Button();
            audioTrackBar = new TrackBar();
            loopCheckButton = new CheckBox();
            NameColumn = new DataGridViewTextBoxColumn();
            PathIDColumn = new DataGridViewTextBoxColumn();
            OffsetColumn = new DataGridViewTextBoxColumn();
            SizeColumn = new DataGridViewTextBoxColumn();
            SourceColumn = new DataGridViewTextBoxColumn();
            ModifiedColumn = new DataGridViewTextBoxColumn();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)audioGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            resourceFileGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)audioTrackBar).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.Size = new Size(688, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(123, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(123, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(123, 22);
            saveAsToolStripMenuItem.Text = "Save As...";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(123, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // openAssetsDialog
            // 
            openAssetsDialog.DefaultExt = "assets";
            openAssetsDialog.Filter = "Assets file|*.assets|All files|*.*";
            // 
            // openFsbDialog
            // 
            openFsbDialog.DefaultExt = "fsb";
            openFsbDialog.Filter = "FSB file|*.fsb|All files|*.*";
            // 
            // saveAssetsDialog
            // 
            saveAssetsDialog.DefaultExt = "assets";
            saveAssetsDialog.Filter = "Assets file|*.assets|All files|*.*";
            // 
            // audioGridView
            // 
            audioGridView.AllowUserToAddRows = false;
            audioGridView.AllowUserToDeleteRows = false;
            audioGridView.AllowUserToResizeRows = false;
            audioGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            audioGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            audioGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            audioGridView.BackgroundColor = SystemColors.Control;
            audioGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            audioGridView.Columns.AddRange(new DataGridViewColumn[] { NameColumn, PathIDColumn, OffsetColumn, SizeColumn, SourceColumn, ModifiedColumn });
            audioGridView.Location = new Point(10, 108);
            audioGridView.Margin = new Padding(3, 2, 3, 2);
            audioGridView.MultiSelect = false;
            audioGridView.Name = "audioGridView";
            audioGridView.ReadOnly = true;
            audioGridView.RowHeadersVisible = false;
            audioGridView.RowHeadersWidth = 51;
            audioGridView.RowTemplate.Height = 29;
            audioGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            audioGridView.ShowCellToolTips = false;
            audioGridView.Size = new Size(667, 407);
            audioGridView.TabIndex = 2;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(10, 574);
            splitContainer1.Margin = new Padding(3, 2, 3, 2);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(exportAudioButton);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(replaceAudioButton);
            splitContainer1.Size = new Size(667, 40);
            splitContainer1.SplitterDistance = 331;
            splitContainer1.TabIndex = 2;
            // 
            // exportAudioButton
            // 
            exportAudioButton.Dock = DockStyle.Fill;
            exportAudioButton.Location = new Point(0, 0);
            exportAudioButton.Margin = new Padding(3, 2, 3, 2);
            exportAudioButton.Name = "exportAudioButton";
            exportAudioButton.Size = new Size(331, 40);
            exportAudioButton.TabIndex = 0;
            exportAudioButton.Text = "Export audio";
            exportAudioButton.UseVisualStyleBackColor = true;
            exportAudioButton.Click += exportAudioButton_Click;
            // 
            // replaceAudioButton
            // 
            replaceAudioButton.Dock = DockStyle.Fill;
            replaceAudioButton.Location = new Point(0, 0);
            replaceAudioButton.Margin = new Padding(3, 2, 3, 2);
            replaceAudioButton.Name = "replaceAudioButton";
            replaceAudioButton.Size = new Size(332, 40);
            replaceAudioButton.TabIndex = 0;
            replaceAudioButton.Text = "Replace audio";
            replaceAudioButton.UseVisualStyleBackColor = true;
            replaceAudioButton.Click += replaceAudioButton_Click;
            // 
            // saveResourceDialog
            // 
            saveResourceDialog.DefaultExt = "resource";
            saveResourceDialog.Filter = "Resource file|*.resource|All files|*.*";
            // 
            // resourceComboBox
            // 
            resourceComboBox.Dock = DockStyle.Fill;
            resourceComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            resourceComboBox.FormattingEnabled = true;
            resourceComboBox.Location = new Point(3, 19);
            resourceComboBox.Name = "resourceComboBox";
            resourceComboBox.Size = new Size(661, 23);
            resourceComboBox.TabIndex = 3;
            // 
            // volumeSlider
            // 
            volumeSlider.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            volumeSlider.Location = new Point(581, 549);
            volumeSlider.Name = "volumeSlider";
            volumeSlider.Size = new Size(96, 20);
            volumeSlider.TabIndex = 5;
            volumeSlider.Volume = 0.3F;
            // 
            // playAudioButton
            // 
            playAudioButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            playAudioButton.Location = new Point(10, 524);
            playAudioButton.Name = "playAudioButton";
            playAudioButton.Size = new Size(75, 40);
            playAudioButton.TabIndex = 6;
            playAudioButton.Text = "Play audio";
            playAudioButton.UseVisualStyleBackColor = true;
            playAudioButton.Click += playAudioButton_Click;
            // 
            // pauseAudioButton
            // 
            pauseAudioButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            pauseAudioButton.Location = new Point(91, 524);
            pauseAudioButton.Name = "pauseAudioButton";
            pauseAudioButton.Size = new Size(97, 40);
            pauseAudioButton.TabIndex = 7;
            pauseAudioButton.Text = "Pause audio";
            pauseAudioButton.UseVisualStyleBackColor = true;
            pauseAudioButton.Click += pauseAudioButton_Click;
            // 
            // openAudioDialog
            // 
            openAudioDialog.Filter = "Audio files (*.ogg; *.wav; *.mp3)|*.ogg;*.wav;*.mp3|All files|*.*";
            // 
            // cancelChangesButton
            // 
            cancelChangesButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cancelChangesButton.Location = new Point(10, 619);
            cancelChangesButton.Name = "cancelChangesButton";
            cancelChangesButton.Size = new Size(667, 40);
            cancelChangesButton.TabIndex = 8;
            cancelChangesButton.Text = "Cancel changes";
            cancelChangesButton.UseVisualStyleBackColor = true;
            cancelChangesButton.Click += cancelChangesButton_Click;
            // 
            // resourceFileGroupBox
            // 
            resourceFileGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            resourceFileGroupBox.Controls.Add(resourceComboBox);
            resourceFileGroupBox.Location = new Point(10, 27);
            resourceFileGroupBox.Name = "resourceFileGroupBox";
            resourceFileGroupBox.Size = new Size(667, 47);
            resourceFileGroupBox.TabIndex = 9;
            resourceFileGroupBox.TabStop = false;
            resourceFileGroupBox.Text = "Resource file";
            // 
            // searchTextBox
            // 
            searchTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            searchTextBox.Location = new Point(10, 80);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.PlaceholderText = "Search field";
            searchTextBox.Size = new Size(580, 23);
            searchTextBox.TabIndex = 10;
            // 
            // searchButton
            // 
            searchButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            searchButton.Location = new Point(596, 80);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(81, 23);
            searchButton.TabIndex = 11;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += searchButton_Click;
            // 
            // audioTrackBar
            // 
            audioTrackBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            audioTrackBar.Location = new Point(194, 524);
            audioTrackBar.Maximum = 100;
            audioTrackBar.Name = "audioTrackBar";
            audioTrackBar.Size = new Size(381, 45);
            audioTrackBar.TabIndex = 12;
            audioTrackBar.TickFrequency = 0;
            audioTrackBar.TickStyle = TickStyle.Both;
            audioTrackBar.Scroll += audioTrackBar_Scroll;
            // 
            // loopCheckButton
            // 
            loopCheckButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            loopCheckButton.Appearance = Appearance.Button;
            loopCheckButton.Location = new Point(581, 520);
            loopCheckButton.Name = "loopCheckButton";
            loopCheckButton.Size = new Size(96, 25);
            loopCheckButton.TabIndex = 13;
            loopCheckButton.Text = "Loop ⟳";
            loopCheckButton.TextAlign = ContentAlignment.MiddleCenter;
            loopCheckButton.UseVisualStyleBackColor = true;
            // 
            // NameColumn
            // 
            NameColumn.HeaderText = "Name";
            NameColumn.MinimumWidth = 6;
            NameColumn.Name = "NameColumn";
            NameColumn.ReadOnly = true;
            // 
            // PathIDColumn
            // 
            PathIDColumn.HeaderText = "PathID";
            PathIDColumn.MinimumWidth = 6;
            PathIDColumn.Name = "PathIDColumn";
            PathIDColumn.ReadOnly = true;
            // 
            // OffsetColumn
            // 
            OffsetColumn.HeaderText = "Offset";
            OffsetColumn.MinimumWidth = 6;
            OffsetColumn.Name = "OffsetColumn";
            OffsetColumn.ReadOnly = true;
            // 
            // SizeColumn
            // 
            SizeColumn.HeaderText = "Size";
            SizeColumn.MinimumWidth = 6;
            SizeColumn.Name = "SizeColumn";
            SizeColumn.ReadOnly = true;
            // 
            // SourceColumn
            // 
            SourceColumn.HeaderText = "Source";
            SourceColumn.MinimumWidth = 6;
            SourceColumn.Name = "SourceColumn";
            SourceColumn.ReadOnly = true;
            // 
            // ModifiedColumn
            // 
            ModifiedColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            ModifiedColumn.HeaderText = "";
            ModifiedColumn.Name = "ModifiedColumn";
            ModifiedColumn.ReadOnly = true;
            ModifiedColumn.Width = 19;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(688, 671);
            Controls.Add(loopCheckButton);
            Controls.Add(audioTrackBar);
            Controls.Add(searchButton);
            Controls.Add(searchTextBox);
            Controls.Add(resourceFileGroupBox);
            Controls.Add(cancelChangesButton);
            Controls.Add(pauseAudioButton);
            Controls.Add(playAudioButton);
            Controls.Add(volumeSlider);
            Controls.Add(audioGridView);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 2, 3, 2);
            Name = "MainForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AudioClipAssetReplacer";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)audioGridView).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            resourceFileGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)audioTrackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private OpenFileDialog openAssetsDialog;
        private OpenFileDialog openFsbDialog;
        private SaveFileDialog saveAssetsDialog;
        private DataGridView audioGridView;
        private SplitContainer splitContainer1;
        private SaveFileDialog saveResourceDialog;
        private Button exportAudioButton;
        private Button replaceAudioButton;
        private ComboBox resourceComboBox;
        private NAudio.Gui.VolumeSlider volumeSlider;
        private Button playAudioButton;
        private Button pauseAudioButton;
        private SaveFileDialog saveAudioDialog;
        private OpenFileDialog openAudioDialog;
        private Button cancelChangesButton;
        private GroupBox resourceFileGroupBox;
        private TextBox searchTextBox;
        private Button searchButton;
        private TrackBar audioTrackBar;
        private CheckBox loopCheckButton;
        private DataGridViewTextBoxColumn NameColumn;
        private DataGridViewTextBoxColumn PathIDColumn;
        private DataGridViewTextBoxColumn OffsetColumn;
        private DataGridViewTextBoxColumn SizeColumn;
        private DataGridViewTextBoxColumn SourceColumn;
        private DataGridViewTextBoxColumn ModifiedColumn;
    }
}