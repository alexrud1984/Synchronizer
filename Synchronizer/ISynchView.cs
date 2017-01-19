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


    public interface ISynchView
    {
        string Source { set; get; }

        string Target { set; get; }

        string[] FileType { set; }

        bool FileVersion { set; get; }

        bool LastChange { set; get; }

        bool AddMissedFile { set; get; }

        bool AutoSynch { set; get; }

        bool IncludeSubfolders { set; get; }

        string OperateSessionId { set; get; }

        List<FileInfo> SourceFilesList { set; }

        List<FileInfo> TargetFilesList { set; }



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

    }
}
