using System;

namespace Synchronizer
{
    partial class SyncView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncView));
            this.sourceListView = new System.Windows.Forms.ListView();
            this.fileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lastChange = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sizeFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sourceImageList = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.srcFilesCountLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.fileTypeComboBox = new System.Windows.Forms.ComboBox();
            this.source = new System.Windows.Forms.Label();
            this.sourceBrouseButton = new System.Windows.Forms.Button();
            this.sourcePathTextBox = new System.Windows.Forms.TextBox();
            this.changeFoldersButton = new System.Windows.Forms.Button();
            this.targetListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.targetImageList = new System.Windows.Forms.ImageList(this.components);
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.trgtCountAmountLabel = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Targetlabel = new System.Windows.Forms.Label();
            this.targetBrouseButton = new System.Windows.Forms.Button();
            this.targetPathTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.autosyncLabel = new System.Windows.Forms.Label();
            this.autosyncStateLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autosynchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.fileVersionCheckBox = new System.Windows.Forms.CheckBox();
            this.lastChangeCheckBox = new System.Windows.Forms.CheckBox();
            this.sizeCheckbox = new System.Windows.Forms.CheckBox();
            this.addMissedCheckBox = new System.Windows.Forms.CheckBox();
            this.autoRenameCheckBox = new System.Windows.Forms.CheckBox();
            this.subfoldersCheckBox = new System.Windows.Forms.CheckBox();
            this.compareButton = new System.Windows.Forms.Button();
            this.synchButton = new System.Windows.Forms.Button();
            this.infoLable = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // sourceListView
            // 
            this.sourceListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sourceListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.fileName,
            this.fileType,
            this.fileVersion,
            this.lastChange,
            this.sizeFile});
            this.sourceListView.Location = new System.Drawing.Point(6, 30);
            this.sourceListView.Name = "sourceListView";
            this.sourceListView.Size = new System.Drawing.Size(567, 355);
            this.sourceListView.SmallImageList = this.sourceImageList;
            this.sourceListView.TabIndex = 2;
            this.sourceListView.UseCompatibleStateImageBehavior = false;
            this.sourceListView.View = System.Windows.Forms.View.Details;
            this.sourceListView.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.sourceListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sourceListView_KeyDown);
            this.sourceListView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.sourceListView_KeyUp);
            // 
            // fileName
            // 
            this.fileName.Text = "File Name";
            this.fileName.Width = 270;
            // 
            // fileType
            // 
            this.fileType.Text = "File Type";
            this.fileType.Width = 57;
            // 
            // fileVersion
            // 
            this.fileVersion.Text = "File Version";
            this.fileVersion.Width = 67;
            // 
            // lastChange
            // 
            this.lastChange.Text = "Last Change";
            this.lastChange.Width = 108;
            // 
            // sizeFile
            // 
            this.sizeFile.Text = "Size";
            // 
            // sourceImageList
            // 
            this.sourceImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.sourceImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.sourceImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitContainer1.Location = new System.Drawing.Point(-3, 51);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel4);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1.Controls.Add(this.sourceListView);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitContainer1.Panel2.Controls.Add(this.changeFoldersButton);
            this.splitContainer1.Panel2.Controls.Add(this.targetListView);
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel3);
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(1152, 411);
            this.splitContainer1.SplitterDistance = 576;
            this.splitContainer1.TabIndex = 4;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flowLayoutPanel4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.flowLayoutPanel4.Controls.Add(this.label1);
            this.flowLayoutPanel4.Controls.Add(this.srcFilesCountLabel);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 388);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(572, 20);
            this.flowLayoutPanel4.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Files: ";
            // 
            // srcFilesCountLabel
            // 
            this.srcFilesCountLabel.AutoSize = true;
            this.srcFilesCountLabel.ForeColor = System.Drawing.Color.Black;
            this.srcFilesCountLabel.Location = new System.Drawing.Point(43, 3);
            this.srcFilesCountLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.srcFilesCountLabel.Name = "srcFilesCountLabel";
            this.srcFilesCountLabel.Size = new System.Drawing.Size(13, 13);
            this.srcFilesCountLabel.TabIndex = 0;
            this.srcFilesCountLabel.Text = "0";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.fileTypeComboBox);
            this.panel2.Controls.Add(this.source);
            this.panel2.Controls.Add(this.sourceBrouseButton);
            this.panel2.Controls.Add(this.sourcePathTextBox);
            this.panel2.Location = new System.Drawing.Point(0, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(588, 28);
            this.panel2.TabIndex = 3;
            // 
            // fileTypeComboBox
            // 
            this.fileTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fileTypeComboBox.FormattingEnabled = true;
            this.fileTypeComboBox.Location = new System.Drawing.Point(524, 2);
            this.fileTypeComboBox.Name = "fileTypeComboBox";
            this.fileTypeComboBox.Size = new System.Drawing.Size(50, 21);
            this.fileTypeComboBox.TabIndex = 5;
            this.fileTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.fileTypeComboBox_SelectedIndexChanged_1);
            // 
            // source
            // 
            this.source.AutoSize = true;
            this.source.Location = new System.Drawing.Point(3, 6);
            this.source.Margin = new System.Windows.Forms.Padding(5, 0, 3, 0);
            this.source.Name = "source";
            this.source.Size = new System.Drawing.Size(44, 13);
            this.source.TabIndex = 3;
            this.source.Text = "Source:";
            this.source.Click += new System.EventHandler(this.label1_Click);
            // 
            // sourceBrouseButton
            // 
            this.sourceBrouseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sourceBrouseButton.Location = new System.Drawing.Point(489, 1);
            this.sourceBrouseButton.Name = "sourceBrouseButton";
            this.sourceBrouseButton.Size = new System.Drawing.Size(32, 22);
            this.sourceBrouseButton.TabIndex = 1;
            this.sourceBrouseButton.Text = "...";
            this.sourceBrouseButton.UseVisualStyleBackColor = true;
            this.sourceBrouseButton.Click += new System.EventHandler(this.sourceBrouseButton_Click);
            // 
            // sourcePathTextBox
            // 
            this.sourcePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sourcePathTextBox.Location = new System.Drawing.Point(53, 2);
            this.sourcePathTextBox.Name = "sourcePathTextBox";
            this.sourcePathTextBox.ReadOnly = true;
            this.sourcePathTextBox.Size = new System.Drawing.Size(430, 20);
            this.sourcePathTextBox.TabIndex = 4;
            // 
            // changeFoldersButton
            // 
            this.changeFoldersButton.Location = new System.Drawing.Point(6, 3);
            this.changeFoldersButton.Name = "changeFoldersButton";
            this.changeFoldersButton.Size = new System.Drawing.Size(37, 22);
            this.changeFoldersButton.TabIndex = 9;
            this.changeFoldersButton.Text = "<=>";
            this.changeFoldersButton.UseVisualStyleBackColor = true;
            this.changeFoldersButton.Click += new System.EventHandler(this.changeFoldersButton_Click);
            // 
            // targetListView
            // 
            this.targetListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.targetListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.targetListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.targetListView.Location = new System.Drawing.Point(3, 30);
            this.targetListView.Name = "targetListView";
            this.targetListView.Size = new System.Drawing.Size(565, 355);
            this.targetListView.SmallImageList = this.targetImageList;
            this.targetListView.TabIndex = 6;
            this.targetListView.UseCompatibleStateImageBehavior = false;
            this.targetListView.View = System.Windows.Forms.View.Details;
            this.targetListView.SelectedIndexChanged += new System.EventHandler(this.targetListView_SelectedIndexChanged);
            this.targetListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.targetListView_KeyDown);
            this.targetListView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.targetListView_KeyUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File Name";
            this.columnHeader1.Width = 286;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "File Type";
            this.columnHeader2.Width = 56;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "File Version";
            this.columnHeader3.Width = 68;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Last Change";
            this.columnHeader4.Width = 97;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Size";
            // 
            // targetImageList
            // 
            this.targetImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.targetImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.targetImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flowLayoutPanel3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.flowLayoutPanel3.Controls.Add(this.label2);
            this.flowLayoutPanel3.Controls.Add(this.trgtCountAmountLabel);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(6, 388);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(570, 20);
            this.flowLayoutPanel3.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Files: ";
            // 
            // trgtCountAmountLabel
            // 
            this.trgtCountAmountLabel.AutoSize = true;
            this.trgtCountAmountLabel.ForeColor = System.Drawing.Color.Black;
            this.trgtCountAmountLabel.Location = new System.Drawing.Point(43, 3);
            this.trgtCountAmountLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.trgtCountAmountLabel.Name = "trgtCountAmountLabel";
            this.trgtCountAmountLabel.Size = new System.Drawing.Size(13, 13);
            this.trgtCountAmountLabel.TabIndex = 0;
            this.trgtCountAmountLabel.Text = "0";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.Targetlabel);
            this.panel3.Controls.Add(this.targetBrouseButton);
            this.panel3.Controls.Add(this.targetPathTextBox);
            this.panel3.Location = new System.Drawing.Point(46, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(518, 28);
            this.panel3.TabIndex = 5;
            // 
            // Targetlabel
            // 
            this.Targetlabel.AutoSize = true;
            this.Targetlabel.Location = new System.Drawing.Point(3, 6);
            this.Targetlabel.Name = "Targetlabel";
            this.Targetlabel.Size = new System.Drawing.Size(41, 13);
            this.Targetlabel.TabIndex = 3;
            this.Targetlabel.Text = "Target:";
            // 
            // targetBrouseButton
            // 
            this.targetBrouseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.targetBrouseButton.Location = new System.Drawing.Point(482, 1);
            this.targetBrouseButton.Name = "targetBrouseButton";
            this.targetBrouseButton.Size = new System.Drawing.Size(32, 21);
            this.targetBrouseButton.TabIndex = 1;
            this.targetBrouseButton.Text = "...";
            this.targetBrouseButton.UseVisualStyleBackColor = true;
            this.targetBrouseButton.Click += new System.EventHandler(this.targetBrouseButton_Click_1);
            // 
            // targetPathTextBox
            // 
            this.targetPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.targetPathTextBox.Location = new System.Drawing.Point(50, 2);
            this.targetPathTextBox.Name = "targetPathTextBox";
            this.targetPathTextBox.ReadOnly = true;
            this.targetPathTextBox.Size = new System.Drawing.Size(430, 20);
            this.targetPathTextBox.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1149, 24);
            this.panel1.TabIndex = 5;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.flowLayoutPanel1.Controls.Add(this.autosyncLabel);
            this.flowLayoutPanel1.Controls.Add(this.autosyncStateLabel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1056, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(93, 24);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // autosyncLabel
            // 
            this.autosyncLabel.AutoSize = true;
            this.autosyncLabel.Location = new System.Drawing.Point(3, 6);
            this.autosyncLabel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.autosyncLabel.Name = "autosyncLabel";
            this.autosyncLabel.Size = new System.Drawing.Size(54, 13);
            this.autosyncLabel.TabIndex = 0;
            this.autosyncLabel.Text = "Autosync:";
            // 
            // autosyncStateLabel
            // 
            this.autosyncStateLabel.AutoSize = true;
            this.autosyncStateLabel.Location = new System.Drawing.Point(63, 6);
            this.autosyncStateLabel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.autosyncStateLabel.Name = "autosyncStateLabel";
            this.autosyncStateLabel.Size = new System.Drawing.Size(21, 13);
            this.autosyncStateLabel.TabIndex = 1;
            this.autosyncStateLabel.Text = "Off";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.autosynchToolStripMenuItem,
            this.historyToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1149, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSessionToolStripMenuItem,
            this.saveSessionToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openSessionToolStripMenuItem
            // 
            this.openSessionToolStripMenuItem.Name = "openSessionToolStripMenuItem";
            this.openSessionToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.openSessionToolStripMenuItem.Text = "Open session";
            this.openSessionToolStripMenuItem.Click += new System.EventHandler(this.openSessionToolStripMenuItem_Click_1);
            // 
            // saveSessionToolStripMenuItem
            // 
            this.saveSessionToolStripMenuItem.Name = "saveSessionToolStripMenuItem";
            this.saveSessionToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.saveSessionToolStripMenuItem.Text = "Save session";
            this.saveSessionToolStripMenuItem.Click += new System.EventHandler(this.saveSessionToolStripMenuItem_Click_1);
            // 
            // autosynchToolStripMenuItem
            // 
            this.autosynchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onToolStripMenuItem,
            this.offToolStripMenuItem});
            this.autosynchToolStripMenuItem.Name = "autosynchToolStripMenuItem";
            this.autosynchToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.autosynchToolStripMenuItem.Text = "Autosynch";
            // 
            // onToolStripMenuItem
            // 
            this.onToolStripMenuItem.Name = "onToolStripMenuItem";
            this.onToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.onToolStripMenuItem.Text = "On";
            this.onToolStripMenuItem.Click += new System.EventHandler(this.onToolStripMenuItem_Click_1);
            // 
            // offToolStripMenuItem
            // 
            this.offToolStripMenuItem.Name = "offToolStripMenuItem";
            this.offToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.offToolStripMenuItem.Text = "Off";
            this.offToolStripMenuItem.Click += new System.EventHandler(this.offToolStripMenuItem_Click_1);
            // 
            // historyToolStripMenuItem
            // 
            this.historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            this.historyToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.historyToolStripMenuItem.Text = "History";
            this.historyToolStripMenuItem.Click += new System.EventHandler(this.historyToolStripMenuItem_Click_1);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.flowLayoutPanel2.Controls.Add(this.fileVersionCheckBox);
            this.flowLayoutPanel2.Controls.Add(this.lastChangeCheckBox);
            this.flowLayoutPanel2.Controls.Add(this.sizeCheckbox);
            this.flowLayoutPanel2.Controls.Add(this.addMissedCheckBox);
            this.flowLayoutPanel2.Controls.Add(this.autoRenameCheckBox);
            this.flowLayoutPanel2.Controls.Add(this.subfoldersCheckBox);
            this.flowLayoutPanel2.Controls.Add(this.compareButton);
            this.flowLayoutPanel2.Controls.Add(this.synchButton);
            this.flowLayoutPanel2.Controls.Add(this.infoLable);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 25);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(1149, 26);
            this.flowLayoutPanel2.TabIndex = 6;
            // 
            // fileVersionCheckBox
            // 
            this.fileVersionCheckBox.AutoSize = true;
            this.fileVersionCheckBox.Location = new System.Drawing.Point(3, 5);
            this.fileVersionCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.fileVersionCheckBox.Name = "fileVersionCheckBox";
            this.fileVersionCheckBox.Size = new System.Drawing.Size(80, 17);
            this.fileVersionCheckBox.TabIndex = 0;
            this.fileVersionCheckBox.Text = "File Version";
            this.fileVersionCheckBox.UseVisualStyleBackColor = true;
            this.fileVersionCheckBox.CheckedChanged += new System.EventHandler(this.fileVersionCheckBox_CheckedChanged);
            // 
            // lastChangeCheckBox
            // 
            this.lastChangeCheckBox.AutoSize = true;
            this.lastChangeCheckBox.Location = new System.Drawing.Point(89, 5);
            this.lastChangeCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.lastChangeCheckBox.Name = "lastChangeCheckBox";
            this.lastChangeCheckBox.Size = new System.Drawing.Size(86, 17);
            this.lastChangeCheckBox.TabIndex = 1;
            this.lastChangeCheckBox.Text = "Last Change";
            this.lastChangeCheckBox.UseVisualStyleBackColor = true;
            this.lastChangeCheckBox.CheckedChanged += new System.EventHandler(this.lastChangeCheckBox_CheckedChanged);
            // 
            // sizeCheckbox
            // 
            this.sizeCheckbox.AutoSize = true;
            this.sizeCheckbox.Location = new System.Drawing.Point(181, 5);
            this.sizeCheckbox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.sizeCheckbox.Name = "sizeCheckbox";
            this.sizeCheckbox.Size = new System.Drawing.Size(46, 17);
            this.sizeCheckbox.TabIndex = 7;
            this.sizeCheckbox.Text = "Size";
            this.sizeCheckbox.UseVisualStyleBackColor = true;
            this.sizeCheckbox.CheckedChanged += new System.EventHandler(this.sizeCheckbox_CheckedChanged);
            // 
            // addMissedCheckBox
            // 
            this.addMissedCheckBox.AutoSize = true;
            this.addMissedCheckBox.Location = new System.Drawing.Point(233, 5);
            this.addMissedCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.addMissedCheckBox.Name = "addMissedCheckBox";
            this.addMissedCheckBox.Size = new System.Drawing.Size(105, 17);
            this.addMissedCheckBox.TabIndex = 2;
            this.addMissedCheckBox.Text = "Add Missed Files";
            this.addMissedCheckBox.UseVisualStyleBackColor = true;
            this.addMissedCheckBox.CheckedChanged += new System.EventHandler(this.addMissedCheckBox_CheckedChanged);
            // 
            // autoRenameCheckBox
            // 
            this.autoRenameCheckBox.AutoSize = true;
            this.autoRenameCheckBox.Location = new System.Drawing.Point(344, 5);
            this.autoRenameCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.autoRenameCheckBox.Name = "autoRenameCheckBox";
            this.autoRenameCheckBox.Size = new System.Drawing.Size(83, 17);
            this.autoRenameCheckBox.TabIndex = 8;
            this.autoRenameCheckBox.Text = "Autorename";
            this.autoRenameCheckBox.UseVisualStyleBackColor = true;
            this.autoRenameCheckBox.CheckedChanged += new System.EventHandler(this.autoRenameCheckBox_CheckedChanged);
            // 
            // subfoldersCheckBox
            // 
            this.subfoldersCheckBox.AutoSize = true;
            this.subfoldersCheckBox.Location = new System.Drawing.Point(433, 5);
            this.subfoldersCheckBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.subfoldersCheckBox.Name = "subfoldersCheckBox";
            this.subfoldersCheckBox.Size = new System.Drawing.Size(114, 17);
            this.subfoldersCheckBox.TabIndex = 4;
            this.subfoldersCheckBox.Text = "Include Subfolders";
            this.subfoldersCheckBox.UseVisualStyleBackColor = true;
            this.subfoldersCheckBox.CheckedChanged += new System.EventHandler(this.subfoldersCheckBox_CheckedChanged);
            // 
            // compareButton
            // 
            this.compareButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.compareButton.Location = new System.Drawing.Point(553, 0);
            this.compareButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.compareButton.Name = "compareButton";
            this.compareButton.Size = new System.Drawing.Size(89, 26);
            this.compareButton.TabIndex = 5;
            this.compareButton.Text = "Compare";
            this.compareButton.UseVisualStyleBackColor = true;
            this.compareButton.Click += new System.EventHandler(this.compareButton_Click);
            // 
            // synchButton
            // 
            this.synchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.synchButton.Location = new System.Drawing.Point(648, 0);
            this.synchButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.synchButton.Name = "synchButton";
            this.synchButton.Size = new System.Drawing.Size(89, 26);
            this.synchButton.TabIndex = 3;
            this.synchButton.Text = "Synchronize";
            this.synchButton.UseVisualStyleBackColor = true;
            this.synchButton.Click += new System.EventHandler(this.synchButton_Click);
            // 
            // infoLable
            // 
            this.infoLable.AutoSize = true;
            this.infoLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.infoLable.ForeColor = System.Drawing.Color.Red;
            this.infoLable.Location = new System.Drawing.Point(743, 6);
            this.infoLable.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.infoLable.Name = "infoLable";
            this.infoLable.Size = new System.Drawing.Size(0, 13);
            this.infoLable.TabIndex = 6;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Synchronizer";
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // SyncView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.ClientSize = new System.Drawing.Size(1149, 462);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitContainer1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SyncView";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Synchronizer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.SyncView_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView sourceListView;
        private System.Windows.Forms.ColumnHeader fileName;
        private System.Windows.Forms.ColumnHeader fileType;
        private System.Windows.Forms.ColumnHeader fileVersion;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ColumnHeader lastChange;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label source;
        private System.Windows.Forms.Button sourceBrouseButton;
        private System.Windows.Forms.TextBox sourcePathTextBox;
        private System.Windows.Forms.ComboBox fileTypeComboBox;
        private System.Windows.Forms.ListView targetListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label Targetlabel;
        private System.Windows.Forms.Button targetBrouseButton;
        private System.Windows.Forms.TextBox targetPathTextBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label autosyncLabel;
        private System.Windows.Forms.Label autosyncStateLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox fileVersionCheckBox;
        private System.Windows.Forms.CheckBox lastChangeCheckBox;
        private System.Windows.Forms.CheckBox addMissedCheckBox;
        private System.Windows.Forms.Button synchButton;
        private System.Windows.Forms.CheckBox subfoldersCheckBox;
        private System.Windows.Forms.Button compareButton;
        private EventHandler fileTypeComboBox_SelectedIndexChanged;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autosynchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem historyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ImageList sourceImageList;
        private System.Windows.Forms.ImageList targetImageList;
        private System.Windows.Forms.Label infoLable;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Label srcFilesCountLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label trgtCountAmountLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button changeFoldersButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.CheckBox sizeCheckbox;
        private System.Windows.Forms.CheckBox autoRenameCheckBox;
        private System.Windows.Forms.ColumnHeader sizeFile;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

