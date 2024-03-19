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
            audioGroupBox = new GroupBox();
            audioGridView = new DataGridView();
            splitContainer1 = new SplitContainer();
            replaceButton = new Button();
            linkButton = new Button();
            saveResourceDialog = new SaveFileDialog();
            menuStrip1.SuspendLayout();
            audioGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)audioGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(767, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(224, 26);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(224, 26);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(224, 26);
            saveAsToolStripMenuItem.Text = "Save As...";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(224, 26);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // openAssetsDialog
            // 
            openAssetsDialog.DefaultExt = "assets";
            openAssetsDialog.Filter = "Assets file|*.assets";
            // 
            // openFsbDialog
            // 
            openFsbDialog.DefaultExt = "fsb";
            openFsbDialog.Filter = "FSB file|*.fsb";
            // 
            // saveAssetsDialog
            // 
            saveAssetsDialog.DefaultExt = "assets";
            saveAssetsDialog.Filter = "Assets file|*.assets";
            // 
            // audioGroupBox
            // 
            audioGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            audioGroupBox.Controls.Add(audioGridView);
            audioGroupBox.Location = new Point(12, 31);
            audioGroupBox.Name = "audioGroupBox";
            audioGroupBox.Size = new Size(743, 352);
            audioGroupBox.TabIndex = 1;
            audioGroupBox.TabStop = false;
            audioGroupBox.Text = "Select your audio asset";
            // 
            // audioGridView
            // 
            audioGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            audioGridView.Dock = DockStyle.Fill;
            audioGridView.Location = new Point(3, 23);
            audioGridView.Name = "audioGridView";
            audioGridView.RowHeadersWidth = 51;
            audioGridView.RowTemplate.Height = 29;
            audioGridView.Size = new Size(737, 326);
            audioGridView.TabIndex = 2;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(12, 389);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(replaceButton);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(linkButton);
            splitContainer1.Size = new Size(743, 54);
            splitContainer1.SplitterDistance = 370;
            splitContainer1.TabIndex = 2;
            // 
            // replaceButton
            // 
            replaceButton.Dock = DockStyle.Fill;
            replaceButton.Location = new Point(0, 0);
            replaceButton.Name = "replaceButton";
            replaceButton.Size = new Size(370, 54);
            replaceButton.TabIndex = 0;
            replaceButton.Text = "Replace audio";
            replaceButton.UseVisualStyleBackColor = true;
            replaceButton.Click += replaceButton_Click;
            // 
            // linkButton
            // 
            linkButton.Dock = DockStyle.Fill;
            linkButton.Location = new Point(0, 0);
            linkButton.Name = "linkButton";
            linkButton.Size = new Size(369, 54);
            linkButton.TabIndex = 0;
            linkButton.Text = "Link audio";
            linkButton.UseVisualStyleBackColor = true;
            linkButton.Click += linkButton_Click;
            // 
            // saveResourceDialog
            // 
            saveResourceDialog.DefaultExt = "resource";
            saveResourceDialog.Filter = "Resource file|*.resource";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(767, 455);
            Controls.Add(splitContainer1);
            Controls.Add(audioGroupBox);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            ShowIcon = false;
            Text = "AudioClipAssetReplacer";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            audioGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)audioGridView).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
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
        private GroupBox audioGroupBox;
        private DataGridView audioGridView;
        private SplitContainer splitContainer1;
        private Button replaceButton;
        private Button linkButton;
        private SaveFileDialog saveResourceDialog;
    }
}