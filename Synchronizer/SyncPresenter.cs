﻿using System;
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

        public object Theread { get; private set; }

        public SyncPresenter(ISynchView syncView)
        {
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
            syncView.Messanger("AutosyncOff");
        }

        public void AutoSyncOn(ISynchView syncView)
        {
            syncView.Messanger("Autosync on");
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
            throw new NotImplementedException();
        }

        public void SaveSession(ISynchView syncView)
        {
            throw new NotImplementedException();
        }

        public void ShowHistory(ISynchView syncView)
        {
          //  throw new NotImplementedException();
        }

        public void SourcePathSelected(ISynchView syncView)
        {
            syncModel.SourceFolder = syncView.Source;
            syncView.SourceFilesList = syncModel.SourceFilesList;
            syncView.FileTypes = syncModel.FileTypes;
            syncView.SourceFilesCount = syncModel.SourceFilesList.Count;
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
            syncView.TargetFilesList = syncModel.TargetFilesList;
            syncView.TargetFilesCount = syncModel.TargetFilesList.Count;
        }

        public void FoldersSynchronized(ISyncModel syncModel)
        {
            syncView.SourceFilesList = syncModel.FilteredSourceFileList;
            syncView.TargetFilesList = syncModel.FilteredTargetFileList;
            syncView.TargetFilesCount = syncModel.FilteredTargetFileList.Count;

            syncView.Messanger("Folders successfully synchronized");
            syncView.SyncButtonEnable = true;
        }

        public void FoldersCompared(ISyncModel syncModel)
        {
            HighlightComparedFilesList(syncModel.FilteredSourceFileList);
            HighlightComparedFilesList(syncModel.FilteredTargetFileList);

            syncView.SourceFilesList = syncModel.FilteredSourceFileList;
            syncView.TargetFilesList = syncModel.FilteredTargetFileList;

            syncView.CompareButtonEnable = true;
            syncView.InfoLable=String.Empty;
        }

        public void FoldersFiltered(ISyncModel syncModel)
        {
            syncView.SourceFilesList = syncModel.FilteredSourceFileList;
            syncView.TargetFilesList = syncModel.FilteredTargetFileList;

            syncView.TargetFilesCount = syncModel.TargetFilesList.Count;
            syncView.SourceFilesCount = syncModel.SourceFilesList.Count;

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
/*            while (syncView.IsViewUpdating)
            {
                Thread.Sleep(2000);
            }
            syncView.SourceFilesList = syncModel.SourceFilesList;
            syncView.InfoLable = "Folders been updated!";*/
        }

        private void HighlightComparedFilesList(List<ExtendedFileInfo> list)
        {
            foreach (ExtendedFileInfo item in list)
            {
                if (!item.IsExists)
                {
                    item.IsHighlighted = true;
                    item.HlColor = Color.Green;
                }

                if (syncModel.FileVersion && item.IsVersionHigh)
                {
                    item.IsHighlighted = true;
                    item.HlColor = Color.Yellow;
                }

                if (syncModel.LastChange && item.IsLastChangeHigh)
                {
                    item.IsHighlighted = true;
                    item.HlColor = Color.Yellow;
                }
            }
        }

        private void ChangeFolders(ISynchView syncView)
        {
            string tempPath = syncView.Source;
            syncView.Source = syncModel.SourceFolder = syncView.Target;
            syncView.Target = syncModel.TargetFolder = tempPath;
            syncView.SourceFilesList = syncModel.SourceFilesList;
            syncView.TargetFilesList = syncModel.TargetFilesList;
            syncView.SourceFilesCount = syncModel.SourceFilesList.Count;
            syncView.TargetFilesCount = syncModel.TargetFilesList.Count;
        }
    }
}
