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
        }

        public void AutoSyncOff(ISynchView syncView)
        {
            syncModel.AutoSync = false;
        }

        public void AutoSyncOn(ISynchView syncView)
        {
            syncModel.AddMissedFile = syncView.AddMissedFile;
            syncModel.FileVersion = syncView.FileVersion;
            syncModel.LastChange = syncView.LastChange;
            syncModel.AutoSync = true;
        }

        public void CompareFolders(ISynchView syncView)
        {
            syncView.CompareButtonEnable = false;
            syncModel.FileVersion = syncView.FileVersion;
            syncModel.LastChange = syncView.LastChange;
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
        }

        public void FileTypeSelected(ISynchView syncView)
        {
            syncModel.TypeSelected=syncView.FileTypeSelected;
        }

        public void InitSyncView(ISynchView syncView)
        {
            syncView.FileVersion = true;
            syncView.LastChange = true;
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
            FilesListRefresh();
            syncView.FileTypes = syncModel.FileTypes;
            syncView.SourceFilesCount = syncModel.SourceFilesCount;
        }

        public void SyncFolder(ISynchView syncView)
        {
            syncView.SyncButtonEnable = false;
            syncModel.AddMissedFile = syncView.AddMissedFile;
            syncModel.FileVersion = syncView.FileVersion;
            syncModel.LastChange = syncView.LastChange;

            syncModel.SynchronizeFolders();
        }

        public void TargetPathSelected(ISynchView syncView)
        {
            syncModel.TargetFolder = syncView.Target;
            this.syncView = syncView;
            FilesListRefresh();
            syncView.TargetFilesCount = syncModel.TargetFilesCount;
        }

        public void FoldersSynchronized(ISyncModel syncModel)
        {
            HighlightFilesList(syncModel.FilteredSourceFileList);
            HighlightFilesList(syncModel.FilteredTargetFileList);
            FilesListRefresh();
            syncView.TargetFilesCount = syncModel.FilteredTargetFileList.Count;

            syncView.Messanger("Folders successfully synchronized");
            syncView.SyncButtonEnable = true;
        }

        public void FoldersCompared(ISyncModel syncModel)
        {
            HighlightFilesList(syncModel.FilteredSourceFileList);
            HighlightFilesList(syncModel.FilteredTargetFileList);

            FilesListRefresh();

            syncView.CompareButtonEnable = true;
            syncView.InfoLable=String.Empty;
        }

        public void FoldersFiltered(ISyncModel syncModel)
        {
            FilesListRefresh();

            syncView.TargetFilesCount = syncModel.TargetFilesCount;
            syncView.SourceFilesCount = syncModel.SourceFilesCount;

        }

        public void AttachModel(ISyncModel syncModel)
        {
            syncModel.FoldersCompared += FoldersCompared;
            syncModel.FoldersFiltered += FoldersFiltered;
            syncModel.FoldersSynchronized += FoldersSynchronized;
            syncModel.FolderUpdated += FolderUpdated;
        }

        public void DetachModel(ISyncModel syncModel)
        {
            syncModel.FoldersCompared -= FoldersCompared;
            syncModel.FoldersFiltered -= FoldersFiltered;
            syncModel.FoldersSynchronized -= FoldersSynchronized;
            syncModel.FolderUpdated -= FolderUpdated;
        }

        public void FolderUpdated(ISyncModel syncModel)
        {
            while (syncView.IsViewUpdating)
            {
                Thread.Sleep(2000);
            }
            FilesListRefresh();
            syncView.SourceFilesCount = syncModel.SourceFilesCount;
            syncView.TargetFilesCount = syncModel.TargetFilesCount;
            syncView.InfoLable = "Folders been updated!";
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
            }
        }

        private void ChangeFolders(ISynchView syncView)
        {
            string tempPath = syncView.Source;
            syncView.Source = syncModel.SourceFolder = syncView.Target;
            syncView.Target = syncModel.TargetFolder = tempPath;
            FilesListRefresh();
            syncView.SourceFilesCount = syncModel.SourceFilesCount;
            syncView.TargetFilesCount = syncModel.TargetFilesCount;
            syncModel.TypeSelected = syncView.FileTypeSelected;
        }

        private void FilesListRefresh()
        {
            List<ExtendedFileInfo> summarized = new List<ExtendedFileInfo>();
            sourceListView = syncModel.FilteredSourceFileList;
            targetListView = syncModel.FilteredTargetFileList;
            sourceListView.Sort();
            targetListView.Sort();

            foreach (var item in sourceListView)
            {
                summarized.Add(item);
            }

            foreach (var item in targetListView)
            {
                summarized.Add(item);
            }
            summarized.Sort();

            for (int i = 0; i < summarized.Count; i++)
            {
                if  (i >= sourceListView.Count)
                {
                    sourceListView.Add(null);
                }
                else if (!String.Equals(sourceListView[i].File.Name, summarized[i].File.Name))
                {
                    sourceListView.Insert(i, null);
                }

                if (i >= targetListView.Count)
                {
                    targetListView.Add(null);
                }
                else if (!String.Equals(targetListView[i].File.Name, summarized[i].File.Name))
                {
                    targetListView.Insert(i, null);
                }
            }
            for (int i = 0; i < sourceListView.Count || i<targetListView.Count ; i++)
            {
                if (sourceListView[i] == null && targetListView[i] == null)
                {
                    sourceListView.RemoveAt(i);
                    targetListView.RemoveAt(i);
                }
            }
            syncView.SourceFilesList = sourceListView;
            syncView.TargetFilesList = targetListView;
        }
    }
}
