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

    
    public interface ISyncModel
    {
        string SourceFolder { set; }

        string TargetFolder { set; }

        string [] FileTypes { get; }

        int SourceFilesCount { get; }

        int TargetFilesCount { get; }

        int TypeSelected { set; }

        List<ExtendedFileInfo> FilteredSourceFileList { get; }

        List<ExtendedFileInfo> FilteredTargetFileList { get; }

        bool FileVersion { set; get; }

        bool LastChange { set; get; }

        bool AddMissedFile { set; get; }

        void CompareFolders();

        void SynchronizeFolders();

        event FoldersFilteredEventHandler FoldersFiltered;

        event FoldersComparedEventHandler FoldersCompared;

        event FoldersSynchronizedEventHandler FoldersSynchronized;

        event FolderUpdatedEventHandler FolderUpdated;

    }
}
