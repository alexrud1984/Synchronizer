using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Synchronizer
{
    class SyncPresenter : ISyncPresenter
    {
        private ISynchView syncView;
        private ISyncModel syncModel;
        List<ExtendedFileInfo> sourceListView;
        List<ExtendedFileInfo> targetListView;
        bool isRefreshing;

        public object Theread { get; private set; }

        public SyncPresenter(ISynchView syncView)
        {
            sourceListView = new List<ExtendedFileInfo>();
            targetListView = new List<ExtendedFileInfo>();
            this.syncView = syncView;
            syncModel = new SyncModel();
            this.AttachView(syncView);
            this.AttachModel(syncModel);

            this.InitSyncView(syncView);
        }

        public void AttachView(ISynchView syncView)
        {
            syncView.AutoSyncFoldersOn += AutoSyncOn;
            syncView.AutoSyncFoldersOff += AutoSyncOff;
            syncView.CompareFolders += CompareFolders;
            syncView.DeleteSession += DeleteSession;
            syncView.OpenSession += OpenSession;
            syncView.SaveSession += SaveSession;
            syncView.ShowHistory += ShowHistory;
            syncView.SynchronizeFolders += SyncFolder;
            syncView.SourcePathSelected += SourcePathSelected;
            syncView.TargetPathSelected += TargetPathSelected;
            syncView.FileTypeSelect += FileTypeSelected;
            syncView.ChangeFolders += ChangeFolders;
            syncView.IncludeSubfoldersEv += IncludeSubfolders;
            syncView.ChangeBasParam += ChangeBasParam;
        }

        public void AutoSyncOff(ISynchView syncView)
        {
            syncView.Autosynch = "Off";
            syncModel.AutoSync = false;
        }

        public void AutoSyncOn(ISynchView syncView)
        {
            syncView.Autosynch = "On";
            syncModel.AddMissedFile = syncView.AddMissedFile;
            syncModel.FileVersion = syncView.FileVersion;
            syncModel.LastChange = syncView.LastChange;
            syncModel.Size = syncView.Size;
            syncModel.AutoSync = true;
        }

        public void CompareFolders(ISynchView syncView)
        {
            syncView.CompareButtonEnable = false;
            syncModel.FileVersion = syncView.FileVersion;
            syncModel.LastChange = syncView.LastChange;
            syncModel.Size = syncView.Size;
            syncModel.CompareFolders();
        }

        public void DeleteSession(ISynchView syncView)
        {
            syncView.Messanger("Delete Session");
        }

        public void DetachView(ISynchView syncView)
        {
            syncView.AutoSyncFoldersOn -= AutoSyncOn;
            syncView.AutoSyncFoldersOff -= AutoSyncOff;
            syncView.CompareFolders -= CompareFolders;
            syncView.DeleteSession -= DeleteSession;
            syncView.OpenSession -= OpenSession;
            syncView.SaveSession -= SaveSession;
            syncView.ShowHistory -= ShowHistory;
            syncView.SynchronizeFolders -= SyncFolder;
            syncView.SourcePathSelected -= SourcePathSelected;
            syncView.TargetPathSelected -= TargetPathSelected;
            syncView.FileTypeSelect -= FileTypeSelected;
            syncView.ChangeFolders -= ChangeFolders;
            syncView.IncludeSubfoldersEv -= IncludeSubfolders;
            syncView.ChangeBasParam -= ChangeBasParam;
        }

        public void FileTypeSelected(ISynchView syncView)
        {
            syncModel.TypeSelected=syncView.FileTypeSelected;
        }

        public void IncludeSubfolders(ISynchView syncView)
        {
            syncModel.IncludeSubfolders = syncView.IncludeSubfolders;
        }

        public void InitSyncView(ISynchView syncView)
        {
            syncView.FileVersion = true;
            syncView.LastChange = true;
            syncView.Size = true;
            syncView.FileTypes = syncModel.FileTypes;
        }

        public void OpenSession(ISynchView syncView)
        {
            //throw new NotImplementedException();
        }

        public void SaveSession(ISynchView syncView)
        {
            //throw new NotImplementedException();
        }

        public void ShowHistory(ISynchView syncView)
        {
          //  throw new NotImplementedException();
        }

        public void SourcePathSelected(ISynchView syncView)
        {
            syncModel.SourceFolder = syncView.Source;
        }

        public void TargetPathSelected(ISynchView syncView)
        {
            syncModel.TargetFolder = syncView.Target;
        }

        public void SyncFolder(ISynchView syncView)
        {
            syncView.SyncButtonEnable = false;
            syncModel.AddMissedFile = syncView.AddMissedFile;
            syncModel.FileVersion = syncView.FileVersion;
            syncModel.LastChange = syncView.LastChange;
            syncModel.Size = syncView.Size;

            syncModel.SynchronizeFolders();
        }

        public void FoldersSynchronized(ISyncModel syncModel)
        {
            HighlightFilesList(syncModel.FilteredSourceFileList);
            HighlightFilesList(syncModel.FilteredTargetFileList);
            while (isRefreshing)
            {
                Thread.Sleep(2000);
            }
            FilesListRefresh();

            syncView.Messanger("Folders successfully synchronized");
            syncView.SyncButtonEnable = true;
        }

        public void FoldersCompared(ISyncModel syncModel)
        {
            HighlightFilesList(syncModel.FilteredSourceFileList);
            HighlightFilesList(syncModel.FilteredTargetFileList);

            while (isRefreshing)
            {
                Thread.Sleep(2000);
            }

            FilesListRefresh();

            syncView.CompareButtonEnable = true;
            syncView.InfoLable=String.Empty;
        }

        public void FoldersFiltered(ISyncModel syncModel)
        {
            while (isRefreshing)
            {
                Thread.Sleep(2000);
            }
            FilesListRefresh();
        }

        public void AttachModel(ISyncModel syncModel)
        {
            syncModel.FoldersCompared += FoldersCompared;
            syncModel.FoldersFiltered += FoldersFiltered;
            syncModel.FoldersSynchronized += FoldersSynchronized;
            syncModel.FolderUpdated += FolderUpdated;
            syncModel.ListUpdated += ListUpdated;
            syncModel.ExceptionMessage += ExceptionMessage;
        }

        public void DetachModel(ISyncModel syncModel)
        {
            syncModel.FoldersCompared -= FoldersCompared;
            syncModel.FoldersFiltered -= FoldersFiltered;
            syncModel.FoldersSynchronized -= FoldersSynchronized;
            syncModel.FolderUpdated -= FolderUpdated;
            syncModel.ListUpdated -= ListUpdated;
            syncModel.ExceptionMessage -= ExceptionMessage;
        }

        public void FolderUpdated(ISyncModel syncModel)
        {
            HighlightFilesList(syncModel.FilteredSourceFileList);
            HighlightFilesList(syncModel.FilteredTargetFileList);
            while (isRefreshing)
            {
                Thread.Sleep(2000);
            }
            FilesListRefresh();
            syncView.InfoLable = "Folders been updated!";
            syncView.FileTypes = syncModel.FileTypes;
        }

        public void ListUpdated(ISyncModel syncModel)
        {
            HighlightFilesList(syncModel.FilteredSourceFileList);
            HighlightFilesList(syncModel.FilteredTargetFileList);
            FilesListRefresh();
            syncView.FileTypes = syncModel.FileTypes;
        }

        private void ExceptionMessage(string msg)
        {
            syncView.Messanger(msg);
        }

        private void HighlightFilesList(List<ExtendedFileInfo> list)
        {
            foreach (ExtendedFileInfo item in list)
            {
                if (!item.IsExists)
                {
                    item.IsHighlighted = true;
                    item.HlColor = Color.Tomato;
                }
                else
                {
                    item.HlColor = Color.Black;
                }

                if (syncModel.FileVersion && item.IsVersionHigh)
                {
                    item.IsHighlighted = true;
                    item.HlColor = Color.SeaGreen;
                }

                if (syncModel.LastChange && item.IsLastChangeHigh)
                {
                    item.IsHighlighted = true;
                    item.HlColor = Color.SeaGreen;
                }

                if (syncModel.Size && item.IsSizeDiffer)
                {
                    item.IsHighlighted = true;
                    item.HlColor = Color.SeaGreen;
                }
            }
        }

        private void ChangeFolders(ISynchView syncView)
        {
            syncView.Autosynch = "Off";
            syncModel.AutoSync = false;
            string tempPath = syncView.Source;
            syncView.Source = syncModel.SourceFolder = syncView.Target;
            syncView.Target = syncModel.TargetFolder = tempPath;
            while (isRefreshing)
            {
                Thread.Sleep(2000);
            }
            FilesListRefresh();
            syncView.SourceFilesCount = syncModel.SourceFilesCount;
            syncView.TargetFilesCount = syncModel.TargetFilesCount;
            syncModel.TypeSelected = syncView.FileTypeSelected;
        }

        private void ChangeBasParam(ISynchView syncView)
        {
            syncModel.AddMissedFile = syncView.AddMissedFile;
            syncModel.Autorename = syncView.AutoRename;
            syncModel.FileVersion = syncView.FileVersion;
            syncModel.LastChange = syncView.LastChange;
            syncModel.Size = syncView.Size;
        }

        private void FilesListRefresh()
        {
            isRefreshing = true;
            List<String> summarized = new List<String>();
            sourceListView = syncModel.FilteredSourceFileList;
            targetListView = syncModel.FilteredTargetFileList;
            sourceListView.Sort();
            targetListView.Sort();

            foreach (var item in sourceListView)
            {
                summarized.Add(item.NoPathFullName);
            }

            foreach (var item in targetListView)
            {
                summarized.Add(item.NoPathFullName);
            }
            summarized.Sort();
            summarized.Distinct();


            for (int i = 0; i < summarized.Count; i++)
            {
                if  (i >= sourceListView.Count)
                {
                    sourceListView.Add(null);
                }
                else if (!String.Equals(sourceListView[i].NoPathFullName, summarized[i]))
                {
                    sourceListView.Insert(i, null);
                }

                if (i >= targetListView.Count)
                {
                    targetListView.Add(null);
                }
                else if (!String.Equals(targetListView[i].NoPathFullName, summarized[i]))
                {
                    targetListView.Insert(i, null);
                }
            }
            for (int i = 0; i < sourceListView.Count && i < targetListView.Count; i++)
            {
                if (sourceListView[i] == null && targetListView[i] == null)
                {
                    sourceListView.RemoveAt(i);
                    targetListView.RemoveAt(i);
                }
            }
            while (syncView.IsViewUpdating)
            {
                Thread.Sleep(2000);
            }

            syncView.TargetFilesCount = syncModel.FilteredTargetFileList.Count;
            syncView.SourceFilesCount = syncModel.FilteredSourceFileList.Count;

            syncView.SourceFilesList = sourceListView;
            syncView.TargetFilesList = targetListView;
            isRefreshing = false;
        }
    }
}
