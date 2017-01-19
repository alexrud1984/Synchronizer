using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronizer
{
    class SyncPresenter : ISyncPresenter
    {
        private string[] FileTypes;
        private List<FileInfo> sourceFiles = new List<FileInfo>();
        private List<FileInfo> targetFiles = new List<FileInfo>();

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
            syncView.Messanger("Compare Folders");
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
        }

        public void InitSyncView(ISynchView syncView)
        {
            syncView.FileVersion=true;
            syncView.LastChange = true;
            FileTypes = new string [] { "*" };
            syncView.FileType = FileTypes;
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
            throw new NotImplementedException();
        }

        public void SourcePathSelected(ISynchView syncView)
        {
            sourceFiles.Clear();
            foreach (string item in Directory.GetFiles(syncView.Source))
            {
                sourceFiles.Add(new FileInfo(item));
            }
            syncView.SourceFilesList = sourceFiles;
        }

        public void SyncFolder(ISynchView syncView)
        {
            throw new NotImplementedException();
        }

        public void TargetPathSelected(ISynchView syncView)
        {
            throw new NotImplementedException();
        }
    }
}
