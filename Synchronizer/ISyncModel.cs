using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronizer
{
    public delegate void FoldersFilteredEventHandler (ISyncModel sender);

    public delegate void FoldersComparedEventHandler(ISyncModel sender);

    public delegate void FoldersSynchronizedEventHandler(ISyncModel sender);

    public delegate void FolderUpdatedEventHandler(ISyncModel sender);

    public delegate void ListUpdatedEventHandler(ISyncModel sender);

    public delegate void ExceptionMessageEventHandler(string msg);

    
    public interface ISyncModel
    {
        string SourceFolder { set; get; }

        string TargetFolder { set; get; }

        string [] FileTypes { get; }

        int SourceFilesCount { get; }

        int TargetFilesCount { get; }

        string TypeSelected { set; }

        List<ExtendedFileInfo> FilteredSourceFileList { get; }

        List<ExtendedFileInfo> FilteredTargetFileList { get; }

        bool FileVersion { set; get; }

        bool LastChange { set; get; }

        bool Autorename { set; get; }

        bool AddMissedFile { set; get; }

        bool AutoSync { set; get; }

        bool Size { set; get; }

        bool IncludeSubfolders { set; }

        void CompareFolders();

        void SynchronizeFolders();

        event FoldersFilteredEventHandler FoldersFiltered;

        event FoldersComparedEventHandler FoldersCompared;

        event FoldersSynchronizedEventHandler FoldersSynchronized;

        event FolderUpdatedEventHandler FolderUpdated;

        event ListUpdatedEventHandler ListUpdated;

        event ExceptionMessageEventHandler ExceptionMessage;

    }
}
