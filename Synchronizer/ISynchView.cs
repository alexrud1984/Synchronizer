using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronizer
{
    public delegate void SyncEventHandler(ISynchView sender);

    public delegate void AutoSyncEventOnHandler(ISynchView sender);

    public delegate void AutoSyncEventOffHandler(ISynchView sender);

    public delegate void SaveSessionEventHandler(ISynchView sender);

    public delegate void OpenSessionEventHandler(ISynchView sender);

    public delegate void DeleteSessionEventHandler(ISynchView sender);

    public delegate void HistoryEventHandler(ISynchView sender);

    public delegate void CompareEventHandler(ISynchView sender);

    public delegate void SourcePathSelectedEventHandler(ISynchView sender);

    public delegate void TargetPathSelectedEventHandler(ISynchView sender);

    public delegate void FileTypeSelectEventHandler(ISynchView sender);

    public delegate void ChangeFoldersEventHandler(ISynchView sender);



    public interface ISynchView
    {
        string InfoLable { set; get; }

        bool IsViewUpdating { get; }

        string Source { set; get; }

        string Target { set; get; }

        string[] FileTypes { set; }

        int FileTypeSelected { set; get; }

        bool FileVersion { set; get; }

        bool LastChange { set; get; }

        bool AddMissedFile { set; get; }

        bool AutoSynch { set; get; }

        bool IncludeSubfolders { set; get; }

        bool CompareButtonEnable { set; get; }

        bool SyncButtonEnable { set; get; }

        string OperateSessionId { set; get; }

        List<ExtendedFileInfo> SourceFilesList { set; }

        List<ExtendedFileInfo> TargetFilesList { set; }

        int SourceFilesCount { set; get; }

        int TargetFilesCount { set; get; }



        void SynchSuccess();

        void Messanger(string msg);

        event SyncEventHandler SynchronizeFolders;

        event AutoSyncEventOnHandler AutoSyncFoldersOn;

        event AutoSyncEventOffHandler AutoSyncFoldersOff;

        event OpenSessionEventHandler OpenSession;

        event SaveSessionEventHandler SaveSession;

        event DeleteSessionEventHandler DeleteSession;

        event HistoryEventHandler ShowHistory;

        event CompareEventHandler CompareFolders;

        event SourcePathSelectedEventHandler SourcePathSelected;

        event TargetPathSelectedEventHandler TargetPathSelected;

        event FileTypeSelectEventHandler FileTypeSelect;

        event ChangeFoldersEventHandler ChangeFolders;
    }
}
