using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Synchronizer
{
    public partial class SyncView : Form , ISynchView
    {
        ErrorProvider error;
        FolderBrowserDialog fbd = new FolderBrowserDialog();

        public string Source
        {
            get
            {
                return sourcePathTextBox.Text.Trim();
            }

            set
            {
                sourcePathTextBox.Text=value;
            }
        }

        public string Target
        {
            get
            {
                return targetPathTextBox.Text.Trim();
            }

            set
            {
                targetPathTextBox.Text = value;
            }
        }

        public string[] FileType
        {
             set
            {
                fileTypeComboBox.Items.Clear();
                fileTypeComboBox.Items.AddRange (value);
                fileTypeComboBox.SelectedItem = fileTypeComboBox.Items[0];
            }
        }

        public bool FileVersion
        {
            get
            {
                return fileVersionCheckBox.Checked;
            }

            set
            {
                fileVersionCheckBox.Checked = value;
            }
        }

        public bool LastChange
        {
            get
            {
                return lastChangeCheckBox.Checked;
            }

            set
            {
                lastChangeCheckBox.Checked = value;
            }
        }

        public bool AddMissedFile
        {
            get
            {
                return addMissedCheckBox.Checked;
            }

            set
            {
                addMissedCheckBox.Checked = value;
            }
        }

        public bool AutoSynch { set; get; }

        public bool IncludeSubfolders
        {
            get
            {
                return subfoldersCheckBox.Checked;
            }

            set
            {
                subfoldersCheckBox.Checked = value;
            }
        }

        public string OperateSessionId { set; get; }

        public List<FileInfo> SourceFilesList
        {
            set
            {
                listViewUpdate(value, sourceListView);
            }
        }

        public List<FileInfo> TargetFilesList
        {
            set
            {
                throw new NotImplementedException();
            }
        }

        public SyncView()
        {
            InitializeComponent();
            SyncPresenter syncPresenter = new SyncPresenter();
            syncPresenter.AttachView(this);
            syncPresenter.InitSyncView(this);
            error = new ErrorProvider();
        }

        public event SyncEventHandler SynchronizeFolders;
        public event AutoSyncEventOnHandler AutoSyncFoldersOn;
        public event OpenSessionEventHandler OpenSession;
        public event SaveSessionEventHandler SaveSession;
        public event DeleteSessionEventHandler DeleteSession;
        public event HistoryEventHandler ShowHistory;
        public event CompareEventHandler CompareFolders;
        public event AutoSyncEventOffHandler AutoSyncFoldersOff;
        public event SourcePathSelectedEventHandler SourcePathSelected;
        public event TargetPathSelectedEventHandler TargetPathSelected;

        private void sourceBrouseButton_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                sourcePathTextBox.Text = fbd.SelectedPath;
                if (!String.IsNullOrEmpty(error.GetError(sourceBrouseButton)))
                {
                    error.Clear();
                    IsDataValid();
                }
                //
                OnSourcePathSelected();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {

        }

        public void SynchSuccess()
        {
            MessageBox.Show("Folders synchronized!");
        }

        public bool IsDataValid()
        {
            bool isValid = true;
            error.Clear();

            if (String.IsNullOrEmpty(targetPathTextBox.Text))
            {
                error.SetError(targetPathTextBox, "Wrong target path");
                isValid = false;
            }

            if (String.IsNullOrEmpty(sourcePathTextBox.Text))
            {
                error.SetError(sourcePathTextBox, "Wrong source path");
                isValid = false;
            }
            return isValid;
        }

        private void OnSynchronizeFolders()
        {
            if (SynchronizeFolders != null)
            {
                SynchronizeFolders(this);
            }
        }

        private void OnAutoSyncFoldersOn()
        {
            if (AutoSyncFoldersOn != null)
            {
                AutoSyncFoldersOn(this);
            }

        }

        private void OnOpenSession()
        {
            if (OpenSession != null)
            {
                OpenSession(this);
            }
        }

        private void OnSaveSession()
        {
            if (SaveSession != null)
            {
                SaveSession(this);
            }
        }

        private void OnDeleteSession()
        {
            if (DeleteSession != null)
            {
                DeleteSession(this);
            }
        }

        private void OnShowHistory()
        {
            if (ShowHistory != null)
            {
                ShowHistory(this);
            }
        }

        private void OnCompareFolders()
        {
            if (CompareFolders != null)
            {
                CompareFolders(this);
            }
        }

        private void OnAutoSyncFoldersOff()
        {
            if (AutoSyncFoldersOff != null)
            {
                AutoSyncFoldersOff(this);
            }
        }

        private void OnSourcePathSelected()
        {
            if (SourcePathSelected != null)
            {
                SourcePathSelected(this);
            }
        }

        private void OnTargetPathSelected()
        {
            if (TargetPathSelected != null)
            {
                TargetPathSelected(this);
            }
        }


        // temp - to delete
        public void Messanger(string msg)
        {
            MessageBox.Show(msg);
        }

        private void compareButton_Click(object sender, EventArgs e)
        {
            if (IsDataValid())
            {
                OnCompareFolders();
            }
        }

        private void fileTypeComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void targetBrouseButton_Click_1(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                targetPathTextBox.Text = fbd.SelectedPath;
                if (!String.IsNullOrEmpty(error.GetError(targetBrouseButton)))
                {
                    error.Clear();
                    IsDataValid();
                }
                OnTargetPathSelected();
            }
        }

        private void synchButton_Click(object sender, EventArgs e)
        {
            if (IsDataValid())
            {
                OnSynchronizeFolders();
            }
        }

        private void openSessionToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OnOpenSession();
        }

        private void saveSessionToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OnSaveSession();
        }

        private void deleteSessionToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OnDeleteSession();
        }

        private void onToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (IsDataValid())
            {
                OnAutoSyncFoldersOn();
            }
        }

        private void offToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OnAutoSyncFoldersOff();
        }

        private void historyToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OnShowHistory();
        }

        private void listViewUpdate(List<FileInfo> filesList, ListView listToUpdate)
        {
            listToUpdate.Items.Clear();
            listToUpdate.SmallImageList.Images.Clear();
            foreach (FileInfo item in filesList)
            {
                listToUpdate.SmallImageList.Images.Add(item.Name, System.Drawing.Icon.ExtractAssociatedIcon(item.FullName));
                ListViewItem lvi = new ListViewItem(item.Name);
                lvi.SubItems.Add(item.Extension);
                lvi.SubItems.Add(FileVersionInfo.GetVersionInfo(item.FullName).FileVersion);
                lvi.SubItems.Add(item.LastWriteTime.ToString());
                listToUpdate.Items.Add(lvi);
                listToUpdate.Items[listToUpdate.Items.Count - 1].ImageKey = item.Name;
            }
        }
    }
}
