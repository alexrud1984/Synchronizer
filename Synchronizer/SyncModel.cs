using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Synchronizer
{
    public class SyncModel : ISyncModel
    {
        private string sourceFolder;
        private string targetFolder;
        private int typeSelected;
        private FileSystemWatcher sourceDirectoryWatcher;
        private FileSystemWatcher targetDirectoryWatcher;
        private string[] fileTypes;
        private bool isSynchronizing;


        private List<ExtendedFileInfo> sourceFilesList;

        private List<ExtendedFileInfo> targetFilesList;

        private List<ExtendedFileInfo> filteredSourceFileList;

        private List<ExtendedFileInfo> filteredTargetFileList;

        public string[] FileTypes
        {
            get
            {
                return (string[])fileTypes.Clone();
            }
        }

        public string SourceFolder
        {
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    SourceWindowsEventsDetach();
                    sourceFolder = value;
                    SourceListInit();
                    SourceWindowsEventsAttach();
                }
                else
                {
                    sourceFolder = String.Empty;
                    sourceFilesList.Clear();
                    filteredSourceFileList.Clear();
                    fileTypes = new string[] { "*" };
                    SourceWindowsEventsDetach();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
            private get
            {
                return sourceFolder;
            }
        }

        private void SourceDirectoryWatcher_Updated(object sender, FileSystemEventArgs e)
        {
            if (SourceFilesCount == Directory.GetFiles(sourceFolder).Count() || isSynchronizing)
            {
                return;
            }
            bool isFolderStillUpdating = true;

            //wait if files list in folder still updating
            do
            {
                TargetListInit();
                SourceListInit();
                Thread.Sleep(2000);
                if (SourceFilesCount == Directory.GetFiles(targetFolder).Count())
                {
                    isFolderStillUpdating = false;
                }
            }
            while (isFolderStillUpdating);

            OnFolderUpdated();
        }

        public string TargetFolder
        {
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    TargetwindowsEventsDetach();
                    targetFolder = value;
                    TargetListInit();
                    TargetwindowsEventsAttach();
                }
                else
                {
                    targetFolder = String.Empty;
                    targetFilesList.Clear();
                    filteredTargetFileList.Clear();
                    TargetwindowsEventsDetach();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }

            get
            {
                return targetFolder;
            }
        }

        private void TargetDirectoryWatcher_Updated(object sender, FileSystemEventArgs e)
        {
            if (TargetFilesCount == Directory.GetFiles(targetFolder).Count() || isSynchronizing)
            {
                return;
            }
            bool isFolderStillUpdating = true;

            //wait if files list in folder still updating
            do
            {
                TargetListInit();
                SourceListInit();
                Thread.Sleep(2000);
                if (TargetFilesCount == Directory.GetFiles(targetFolder).Count())
                {
                    isFolderStillUpdating = false;
                }
            }
            while (isFolderStillUpdating);

            OnFolderUpdated();
        }

        public int TypeSelected
        {
            set
            {
                typeSelected = value;
                filterFileList(fileTypes[typeSelected], sourceFilesList, filteredSourceFileList);
                filterFileList(fileTypes[typeSelected], targetFilesList, filteredTargetFileList);
                OnFoldersFiltered();
            }

            get
            {
                return typeSelected;
            }
        }

        public List<ExtendedFileInfo> FilteredSourceFileList
        {
            get
            {
                return new List<ExtendedFileInfo>(filteredSourceFileList);
            }
        }

        public List<ExtendedFileInfo> FilteredTargetFileList
        {
            get
            {
                return new List<ExtendedFileInfo>(filteredTargetFileList);
            }
        }

        public bool FileVersion { set; get; }

        public bool LastChange { set; get; }

        public bool AddMissedFile { set; get; }

        public int SourceFilesCount
        {
            get
            {
                return sourceFilesList.Count;
            }
        }

        public int TargetFilesCount
        {
            get
            {
                return targetFilesList.Count;
            }
        }

        public SyncModel()
        {
            fileTypes = new string[] { "*" };
            sourceFilesList = new List<ExtendedFileInfo>();
            filteredSourceFileList = new List<ExtendedFileInfo>();
            targetFilesList = new List<ExtendedFileInfo>();
            filteredTargetFileList = new List<ExtendedFileInfo>();
        }

        public event FoldersFilteredEventHandler FoldersFiltered;
        public event FoldersComparedEventHandler FoldersCompared;
        public event FoldersSynchronizedEventHandler FoldersSynchronized;
        public event FolderUpdatedEventHandler FolderUpdated;

        public void CompareFolders()
        {
            CompareAccorrdingSettings();
            filterFileList(fileTypes[typeSelected], sourceFilesList, filteredSourceFileList);
            filterFileList(fileTypes[typeSelected], targetFilesList, filteredTargetFileList);
            OnFoldersCompared();
        }

        public void SynchronizeFolders()
        {
            targetDirectoryWatcher.EnableRaisingEvents = false;
            sourceDirectoryWatcher.EnableRaisingEvents = false;
            isSynchronizing = true;

            CompareAccorrdingSettings();
            foreach (var itemS in filteredSourceFileList)
            {
                if (AddMissedFile && !itemS.IsExists)
                {
                    itemS.File.CopyTo(Path.Combine(TargetFolder,itemS.File.Name));
                }
                else
                {
                    foreach (var itemT in filteredTargetFileList)
                    {
                        if (itemS.File.Name == itemT.File.Name)
                        {
                            if (itemS.IsLastChangeHigh || itemS.IsVersionHigh)
                            {
                                itemS.File.CopyTo(Path.Combine(targetFolder, itemS.File.Name), true);
                            }
                        }
                    }
                }
            }
            TargetListInit();
            SourceListInit();
            targetDirectoryWatcher.EnableRaisingEvents = true;
            sourceDirectoryWatcher.EnableRaisingEvents = true;
            isSynchronizing = false;
            OnFoldersSynchronized();
        }


        private void filterFileList(string filter, List<ExtendedFileInfo> toFilter, List<ExtendedFileInfo> filtered)
        {
            filtered.Clear();
            foreach (ExtendedFileInfo item in toFilter)
            {
                if ((String.Equals(filter, item.File.Extension)) || (String.Equals(filter, "*")))
                {
                    filtered.Add(item);
                }
            }
        }

        private void CheckExist(List<ExtendedFileInfo> source, List<ExtendedFileInfo> target)
        {
            for (int i = 0; i < source.Count; i++)
            {
                source[i].IsExists = false;
                for (int j = 0; j < target.Count; j++)
                {
                    if (source[i].File.Name == target[j].File.Name)
                    {
                        source[i].IsExists = true;
                    }
                }
            }
        }

        private void CompareFileVersion(List<ExtendedFileInfo> source, List<ExtendedFileInfo> target)
        {
            for (int i = 0; i<source.Count; i++)
            {
                for (int j = 0; j < target.Count; j++)
                {
                    if (source[i].IsExists)
                    {
                        if (source[i].File.Name == target[j].File.Name)
                        {
                            if (String.CompareOrdinal (source[i].Version, target[j].Version)>0)
                            {
                                source[i].IsVersionHigh=true;
                            }
                            continue;
                        }
                    }
                }
            }
        }

        private void CompareLastChange(List<ExtendedFileInfo> source, List<ExtendedFileInfo> target)
        {
            for (int i = 0; i < source.Count; i++)
            {
                for (int j = 0; j < target.Count; j++)
                {
                    if (source[i].IsExists)
                    {
                        if (source[i].File.Name == target[j].File.Name)
                        {
                            if (source[i].File.LastWriteTime>target[j].File.LastWriteTime)
                            {
                                source[i].IsLastChangeHigh = true;
                            }
                            continue;
                        }
                    }
                }
            }
        }


        private void OnFoldersFiltered()
        {
            if (FoldersFiltered != null)
            {
                FoldersFiltered(this);
            }
        }

        private void OnFoldersCompared()
        {
            if (FoldersCompared != null)
            {
                FoldersCompared(this);
            }
        }

        private void OnFoldersSynchronized()
        {
            if (FoldersSynchronized != null)
            {
                FoldersSynchronized(this);
            }
        }

        private void OnFolderUpdated()
        {
            if (FolderUpdated != null)
            {
                FolderUpdated(this);
            }
        }
        private void CleanCompareData()
        {
            foreach (var item in sourceFilesList)
            {
                item.SetDefaultValues();
            }
            foreach (var item in targetFilesList)
            {
                item.SetDefaultValues();
            }
        }

        private void SourceListInit ()
        {
            List<string> types = new List<string>();
            sourceFilesList.Clear();
            filteredSourceFileList.Clear();
            foreach (string item in Directory.GetFiles(sourceFolder))
            {
                if (File.Exists(item))
                {
                    ExtendedFileInfo efi = new ExtendedFileInfo(item);
                    sourceFilesList.Add(efi);
                    types.Add(efi.File.Extension);
                }
            }
            sourceFilesList.Sort();

            types = types.Distinct().ToList();
            types.Sort();

            fileTypes = new string[types.Count + 1];
            fileTypes[0] = "*";

            for (int i = 1; i < fileTypes.Length; i++)
            {
                fileTypes[i] = types[i - 1];
            }
            filterFileList(fileTypes[typeSelected], sourceFilesList, filteredSourceFileList);
        }

        private void TargetListInit()
        {
            targetFilesList.Clear();
            filteredTargetFileList.Clear();
            foreach (string item in Directory.GetFiles(targetFolder))
            {
                ExtendedFileInfo efi = new ExtendedFileInfo(item);
                targetFilesList.Add(efi);
            }

            targetFilesList.Sort();
            filterFileList(fileTypes[typeSelected], targetFilesList, filteredTargetFileList);
        }

        private void CompareAccorrdingSettings()
        {
            CleanCompareData();
            CheckExist(sourceFilesList, targetFilesList);
            CheckExist(targetFilesList, sourceFilesList);
            if (FileVersion)
            {
                CompareFileVersion(sourceFilesList, targetFilesList);
                CompareFileVersion(targetFilesList, sourceFilesList);
            }
            if (LastChange)
            {
                CompareLastChange(sourceFilesList, targetFilesList);
                CompareLastChange(targetFilesList, sourceFilesList);
            }
        }

        private long GetDirctorySize(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            long size = 0;
            FileInfo [] fileList = di.GetFiles();
            foreach (FileInfo item in fileList)
            {
                try
                {
                    size += item.Length;
                }
                catch (UnauthorizedAccessException)
                {
                    //add to log in future
                }
            }
            return size;
        }

        private void SourceWindowsEventsAttach()
        {
            sourceDirectoryWatcher = new FileSystemWatcher(sourceFolder);
            sourceDirectoryWatcher.EnableRaisingEvents = true;
            sourceDirectoryWatcher.Changed += SourceDirectoryWatcher_Updated;
            sourceDirectoryWatcher.Deleted += SourceDirectoryWatcher_Updated;
            sourceDirectoryWatcher.Created += SourceDirectoryWatcher_Updated;
            sourceDirectoryWatcher.Renamed += SourceDirectoryWatcher_Updated;
        }

        private void TargetwindowsEventsAttach()
        {
            targetDirectoryWatcher = new FileSystemWatcher(targetFolder);
            targetDirectoryWatcher.EnableRaisingEvents = true;
            targetDirectoryWatcher.Changed += TargetDirectoryWatcher_Updated;
            targetDirectoryWatcher.Renamed += TargetDirectoryWatcher_Updated;
            targetDirectoryWatcher.Created += TargetDirectoryWatcher_Updated;
            targetDirectoryWatcher.Deleted += TargetDirectoryWatcher_Updated;
        }

        private void SourceWindowsEventsDetach()
        {
            if (sourceDirectoryWatcher != null)
            {
                sourceDirectoryWatcher.EnableRaisingEvents = false;
                sourceDirectoryWatcher.Changed -= SourceDirectoryWatcher_Updated;
                sourceDirectoryWatcher.Deleted -= SourceDirectoryWatcher_Updated;
                sourceDirectoryWatcher.Created -= SourceDirectoryWatcher_Updated;
                sourceDirectoryWatcher.Renamed -= SourceDirectoryWatcher_Updated;
                sourceDirectoryWatcher.Dispose();
            }
        }

        private void TargetwindowsEventsDetach()
        {
            if (targetDirectoryWatcher != null)
            {
            targetDirectoryWatcher.EnableRaisingEvents = false;
            targetDirectoryWatcher.Changed -= TargetDirectoryWatcher_Updated;
            targetDirectoryWatcher.Renamed -= TargetDirectoryWatcher_Updated;
            targetDirectoryWatcher.Created -= TargetDirectoryWatcher_Updated;
            targetDirectoryWatcher.Deleted -= TargetDirectoryWatcher_Updated;
            targetDirectoryWatcher.Dispose();

            }
        }

    }
}
