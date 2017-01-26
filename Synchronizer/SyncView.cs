using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

        public string[] FileTypes
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

        public List<ExtendedFileInfo> SourceFilesList
        {
            set
            {
                IsViewUpdating = true;
                if (!sourceListView.InvokeRequired)
                {
                    listViewUpdate(value, sourceListView);
                }
                else
                {
                    srcFilesCountLabel.Invoke((Action)delegate
                    {
                        listViewUpdate(value, sourceListView);
                    });
                }
                IsViewUpdating = false;
            }
        }

        public List<ExtendedFileInfo> TargetFilesList
        {
            set
            {
                IsViewUpdating = true;
                if (!targetListView.InvokeRequired)
                {
                    listViewUpdate(value, targetListView);
                }
                else
                {
                    srcFilesCountLabel.Invoke((Action)delegate
                    {
                        listViewUpdate(value, targetListView);
                    });
                }
                IsViewUpdating = false;
            }
        }

        public string FileTypeSelected
        {
            get
            {
                return fileTypeComboBox.Text;
            }

            set
            {
                fileTypeComboBox.Text=value;
            }
        }

        public bool CompareButtonEnable
        {
            get
            {
                return compareButton.IsAccessible;
            }

            set
            {
                compareButton.Enabled = value;
            }
        }

        public bool SyncButtonEnable
        {
            get
            {
                return synchButton.IsAccessible;
            }

            set
            {
                synchButton.Enabled = value;
            }
        }

        public string InfoLable
        {
            get
            {
                return infoLable.Text;
            }

            set
            {
                infoLable.Invoke((Action)delegate 
                {
                    infoLable.Text = value;
                });
            }
        }

        public bool IsViewUpdating { private set; get; }

        public int SourceFilesCount
        {
            get
            {
                int result;
                Int32.TryParse(srcFilesCountLabel.Text, out result);
                return result;
            }

            set
            {
                if (!srcFilesCountLabel.InvokeRequired)
                {
                    srcFilesCountLabel.Text = value.ToString();
                }
                else
                {
                    srcFilesCountLabel.Invoke((Action)delegate
                    {
                        srcFilesCountLabel.Text = value.ToString();
                    });
                }

            }
        }

        public int TargetFilesCount
        {
            get
            {
                int result;
                Int32.TryParse(trgtCountAmountLabel.Text, out result);
                return result;
            }

            set
            {
                if (!trgtCountAmountLabel.InvokeRequired)
                {
                    trgtCountAmountLabel.Text = value.ToString();
                }
                else
                {
                    trgtCountAmountLabel.Invoke((Action)delegate
                   {
                       trgtCountAmountLabel.Text = value.ToString();
                   });
                }
            }
        }

        public SyncView()
        {
            InitializeComponent();
            SyncPresenter syncPresenter = new SyncPresenter(this);
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
        public event FileTypeSelectEventHandler FileTypeSelect;
        public event ChangeFoldersEventHandler ChangeFolders;

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

        private void OnFileTypeSelect()
        {
            if (FileTypeSelect != null)
            {
                FileTypeSelect(this);
            }
        }

        private void OnChangeFolders()
        {
            if (ChangeFolders != null)
            {
                ChangeFolders(this);
            }
        }

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
            OnFileTypeSelect();
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
                autosyncStateLabel.Text = "On";
                OnAutoSyncFoldersOn();
            }
        }

        private void offToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            autosyncStateLabel.Text = "Off";
            OnAutoSyncFoldersOff();
        }

    private void historyToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OnShowHistory();
        }

        private void listViewUpdate(List<ExtendedFileInfo> filesList, ListView listToUpdate)
        {
            try
            {
                listToUpdate.Items.Clear();
                listToUpdate.SmallImageList.Images.Clear();

                foreach (ExtendedFileInfo item in filesList)
                {
                    if (item != null)
                    {
                        ListViewItem lvi = new ListViewItem(item.File.Name);
                        listToUpdate.SmallImageList.Images.Add(item.File.Name, System.Drawing.Icon.ExtractAssociatedIcon(item.File.FullName));
                        lvi.SubItems.Add(item.File.Extension);
                        lvi.SubItems.Add(FileVersionInfo.GetVersionInfo(item.File.FullName).FileVersion);
                        lvi.SubItems.Add(item.File.LastWriteTime.ToString());
                        if (item.IsHighlighted)
                        {
                            lvi.ForeColor = item.HlColor;
                        }
                        listToUpdate.Items.Add(lvi);
                        listToUpdate.Items[listToUpdate.Items.Count - 1].ImageKey = item.File.Name;
                    }
                    else
                    {
                        ListViewItem lvi = new ListViewItem(" ");
                        listToUpdate.Items.Add(lvi);
                    }
                }
            }
            catch (Exception)
            {
                Messanger("Someshing went wong! Press refresh.");
            }

        }

        private void changeFoldersButton_Click(object sender, EventArgs e)
        {
            OnChangeFolders();
        }
    }
}
