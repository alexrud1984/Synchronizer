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

    public delegate void HistoryEventHandler(ISynchView sender);

    public delegate void CompareEventHandler(ISynchView sender);

    public delegate void SourcePathSelectedEventHandler(ISynchView sender);

    public delegate void TargetPathSelectedEventHandler(ISynchView sender);

    public delegate void FileTypeSelectEventHandler(ISynchView sender);

    public delegate void ChangeFoldersEventHandler(ISynchView sender);

    public delegate void IncludeSubfoldersEventHandler(ISynchView sender);

    public delegate void ChangeBasParamEventHandler(ISynchView sender);




    public interface ISynchView
    {
        string InfoLable { set; get; }

        string Autosynch { set; get; }

        bool IsViewUpdating { get; }

        string SourceFolder { set; get; }

        string TargetFolder { set; get; }

        string[] FileTypes { set; }

        string FileTypeSelected { set; get; }

        bool FileVersion { set; get; }

        bool LastChange { set; get; }

        bool AddMissedFile { set; get; }

        bool IncludeSubfolders { set; get; }

        bool CompareButtonEnable { set; get; }

        bool SyncButtonEnable { set; get; }

        bool Size { set; get; }

        bool Autorename { set; get; }

        string SessionPath { set; get; }

        string SessionName { set; get; }

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

        event HistoryEventHandler ShowHistory;

        event CompareEventHandler CompareFolders;

        event SourcePathSelectedEventHandler SourcePathSelected;

        event TargetPathSelectedEventHandler TargetPathSelected;

        event FileTypeSelectEventHandler FileTypeSelect;

        event ChangeFoldersEventHandler ChangeFolders;

        event IncludeSubfoldersEventHandler IncludeSubfoldersEv;

        event ChangeBasParamEventHandler ChangeBasParam;

    }
}
