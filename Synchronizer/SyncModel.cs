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
        private string typeSelected;
        private bool autoSyncStatus;
        private FileSystemWatcher sourceDirectoryWatcher;
        private FileSystemWatcher targetDirectoryWatcher;
        private string[] fileTypes;
        private bool isSynchronizing;
        private bool includeSubfolders;
        private bool isCompared;


        private List<ExtendedFileInfo> sourceFilesList;

        private List<ExtendedFileInfo> targetFilesList;

        private List<ExtendedFileInfo> filteredSourceFileList;

        private List<ExtendedFileInfo> filteredTargetFileList;

        public bool AutoSync
        {
            set
            {
                if (value)
                {
                    autoSyncStatus = true;
                    SynchronizeFolders();
                }
                else
                {
                    autoSyncStatus = false;
                }
            }

            get
            {
                return autoSyncStatus;
            }
        }
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
                    //clean compare data if folder is new
                    if (!String.Equals(sourceFolder, value)) 
                    {
                        CleanCompareData();
                        isCompared = false;
                    }

                    SourceWindowsEventsDetach();
                    sourceFolder = value;
                    sourceFilesList.Clear();
                    SourceWindowsEventsAttach();
                    if (includeSubfolders)
                    {
                        IncludeSubfolders = includeSubfolders;
                    }
                    else
                    {
                        FilesListInit(sourceFilesList, filteredSourceFileList, sourceFolder);
                    }
                    OnListUpdated();
                }
                else
                {
                    sourceFolder = String.Empty;
                    sourceFilesList.Clear();
                    CleanCompareData();
                    isCompared = false;
                    filteredSourceFileList.Clear();
                    if (TargetFilesCount == 0)
                    {
                        fileTypes = new string[] { "*" };
                        TypeSelected = "*";
                    }
                    SourceWindowsEventsDetach();
                }
            }
            get
            {
                return sourceFolder;
            }
        }


        public string TargetFolder
        {
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    //clean compare data if folder is new
                    if (!String.Equals(targetFolder, value))
                    {
                        CleanCompareData();
                        isCompared = false;
                    }
                    TargetWindowsEventsDetach();
                    targetFolder = value;
                    targetFilesList.Clear();
                    TargetWindowsEventsAttach();
                    if (includeSubfolders)
                    {
                        IncludeSubfolders = includeSubfolders;
                    }
                    else
                    {
                        FilesListInit(targetFilesList, filteredTargetFileList, targetFolder);
                    }
                    OnListUpdated();
                }
                else
                {
                    targetFolder = String.Empty;
                    targetFilesList.Clear();
                    filteredTargetFileList.Clear();
                    CleanCompareData();
                    isCompared = false;
                    if (SourceFilesCount == 0)
                    {
                        fileTypes = new string[] { "*" };
                        typeSelected = "*";
                    }
                    TargetWindowsEventsDetach();
                }
            }

            get
            {
                return targetFolder;
            }
        }


        public string TypeSelected
        {
            set
            {
                typeSelected = value;
                FilterFileList(typeSelected, sourceFilesList, filteredSourceFileList);
                FilterFileList(typeSelected, targetFilesList, filteredTargetFileList);
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

        public bool Autorename { set; get; }

        public bool IncludeSubfolders
        {
            set
            {
                includeSubfolders = value;
                if (includeSubfolders)
                {
                    if (!String.IsNullOrEmpty(sourceFolder))
                    {
                        sourceFilesList.Clear();
                        FilesListInitTree(sourceFilesList, sourceFolder, sourceFolder);
                        sourceDirectoryWatcher.IncludeSubdirectories = true;
                    }

                    if (!String.IsNullOrEmpty(targetFolder))
                    {
                        targetFilesList.Clear();
                        FilesListInitTree(targetFilesList, targetFolder, targetFolder);
                        targetDirectoryWatcher.IncludeSubdirectories = true;
                    }

                    if (isCompared)
                    {
                        CompareAccorrdingToSettings();
                    }

                    FileTypesFilling();
                    FilterFileList(typeSelected, sourceFilesList, filteredSourceFileList);
                    FilterFileList(typeSelected, targetFilesList, filteredTargetFileList);
                    OnListUpdated();

                }
                else
                {
                    sourceDirectoryWatcher.IncludeSubdirectories = false;
                    targetDirectoryWatcher.IncludeSubdirectories = false;
                    SourceFolder = sourceFolder;
                    TargetFolder = targetFolder;
                }
            }
        }

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

        public bool Size { set; get; }

        public SyncModel()
        {
            fileTypes = new string[] { "*" };
            sourceFilesList = new List<ExtendedFileInfo>();
            filteredSourceFileList = new List<ExtendedFileInfo>();
            targetFilesList = new List<ExtendedFileInfo>();
            filteredTargetFileList = new List<ExtendedFileInfo>();
            sourceDirectoryWatcher = new FileSystemWatcher();
            targetDirectoryWatcher = new FileSystemWatcher();
        }

        public event FoldersFilteredEventHandler FoldersFiltered;
        public event FoldersComparedEventHandler FoldersCompared;
        public event FoldersSynchronizedEventHandler FoldersSynchronized;
        public event FolderUpdatedEventHandler FolderUpdated;
        public event ListUpdatedEventHandler ListUpdated;
        public event ExceptionMessageEventHandler ExceptionMessage;

        public void CompareFolders()
        {
            CleanCompareData();
            CompareAccorrdingToSettings();
            isCompared = true;
            OnFoldersCompared();
        }

        public void SynchronizeFolders()
        {
            targetDirectoryWatcher.EnableRaisingEvents = false;
            sourceDirectoryWatcher.EnableRaisingEvents = false;
            isSynchronizing = true;

            //subfolders synch
            if (includeSubfolders)
            {
                FoldersTreeAlign(sourceFolder, targetFolder);
            }
            CleanCompareData();
            CompareAccorrdingToSettings();
            foreach (var itemS in filteredSourceFileList)
            {
                if (AddMissedFile && !itemS.IsExists)
                {
                    itemS.File.CopyTo(Path.Combine(TargetFolder,itemS.NoPathFullName));
                }
                else
                {
                    if (itemS.IsLastChangeHigh || itemS.IsVersionHigh || itemS.IsSizeDiffer)
                    {
                        try
                        {
                            itemS.File.CopyTo(Path.Combine(targetFolder, itemS.NoPathFullName), true);
                        }
                        catch (AccessViolationException)
                        {
                            OnExceptionMessage("There is no access to the target folder");
                        }
                        catch (FileNotFoundException)
                        {
                            OnExceptionMessage("File probably been deleted during synchronization");
                        }
                        catch (Exception ex)
                        {
                            OnExceptionMessage(ex.Message);
                        }
                    }
                }
            }

            targetFilesList.Clear();
            CleanCompareData();

            if (includeSubfolders)
            {
                FilesListInitTree(targetFilesList, targetFolder, targetFolder);
            }
            else
            {
                FilesListInit(targetFilesList, filteredTargetFileList, targetFolder);  // init builds also filtered list
            }

            if (isCompared)
            {
                CompareAccorrdingToSettings();
            }
            FilterFileList(typeSelected, targetFilesList, filteredTargetFileList);

            targetDirectoryWatcher.EnableRaisingEvents = true;
            sourceDirectoryWatcher.EnableRaisingEvents = true;
            isSynchronizing = false;
            OnFoldersSynchronized();
        }

        private void FilterFileList(string filter, List<ExtendedFileInfo> toFilter, List<ExtendedFileInfo> filtered)
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
                    if (String.Equals(source[i].NoPathFullName, target[j].NoPathFullName))
                    {
                        source[i].IsExists = true;
                        source[i].OpponentIndex = j;
                    }
                }
            }
        }

        private void CompareFileVersion(List<ExtendedFileInfo> source, List<ExtendedFileInfo> target)
        {
            for (int i = 0; i<source.Count; i++)
            {
                if (source[i].IsExists)
                {
                    source[i].IsVersionHigh = false;
                    if (String.CompareOrdinal(source[i].Version, target[source[i].OpponentIndex].Version) > 0)
                    {
                        source[i].IsVersionHigh = true;
                    }
                }
            }
        }

        private void CompareLastChange(List<ExtendedFileInfo> source, List<ExtendedFileInfo> target)
        {
            for (int i = 0; i < source.Count; i++)
            {
                if (source[i].IsExists)
                {
                    if (source[i].File.LastWriteTime > target[source[i].OpponentIndex].File.LastWriteTime)
                    {
                        source[i].IsLastChangeHigh = true;
                    }
                }
            }
        }

        private void CompareSize(List<ExtendedFileInfo> source, List<ExtendedFileInfo> target)
        {
            for (int i = 0; i < source.Count; i++)
            {
                if (source[i].IsExists)
                {
                    if (source[i].Size != target[source[i].OpponentIndex].Size)
                    {
                        source[i].IsSizeDiffer = true;
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

        private void OnListUpdated()
        {
            if (ListUpdated != null)
            {
                ListUpdated(this);
            }
        }

        private void OnExceptionMessage(string msg)
        {
            if (ExceptionMessage != null)
            {
                ExceptionMessage(msg);
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

        private void FilesListInit ( List<ExtendedFileInfo>  initList, List<ExtendedFileInfo> filteredList, string path)
        {
            try
            {
                foreach (string item in Directory.GetFiles(path))
                {
                    if (File.Exists(item))
                    {
                        ExtendedFileInfo efi = new ExtendedFileInfo(item, path);
                        initList.Add(efi);
                    }
                }
                initList.Sort();
                FileTypesFilling();
                if (isCompared)
                {
                    CompareAccorrdingToSettings();
                }
                FilterFileList(typeSelected, initList, filteredList);
            }
            catch (FileNotFoundException)
            {
                initList.Clear();
                FilesListInit(initList, filteredList, path);
                return;
            }
            initList.Distinct();
        }

        private void CompareAccorrdingToSettings()
        {

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
            if (Size)
            {
                CompareSize(sourceFilesList, targetFilesList);
                CompareSize(targetFilesList, sourceFilesList);
            }
        }

        private void SourceWindowsEventsAttach()
        {
            sourceDirectoryWatcher.Path=sourceFolder;
            sourceDirectoryWatcher.EnableRaisingEvents = true;
            sourceDirectoryWatcher.Changed += SourceDirectoryWatcher_Updated;
            sourceDirectoryWatcher.Deleted += SourceDirectoryWatcher_Updated;
            sourceDirectoryWatcher.Created += SourceDirectoryWatcher_Updated;
            sourceDirectoryWatcher.Renamed += SourceDirectoryWatcher_Updated;
        }

        private void TargetWindowsEventsAttach()
        {
            targetDirectoryWatcher.Path=targetFolder;
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
 //               sourceDirectoryWatcher.Dispose();
            }
        }

        private void TargetWindowsEventsDetach()
        {
            if (targetDirectoryWatcher != null)
            {
            targetDirectoryWatcher.EnableRaisingEvents = false;
            targetDirectoryWatcher.Changed -= TargetDirectoryWatcher_Updated;
            targetDirectoryWatcher.Renamed -= TargetDirectoryWatcher_Updated;
            targetDirectoryWatcher.Created -= TargetDirectoryWatcher_Updated;
            targetDirectoryWatcher.Deleted -= TargetDirectoryWatcher_Updated;
  //          targetDirectoryWatcher.Dispose();

            }
        }

        private void FileTypesFilling()
        {
            List<string> types = new List<string>();

            foreach (var item in sourceFilesList)
            {
                types.Add(item.File.Extension);
            }
            foreach (var item in targetFilesList)
            {
                types.Add(item.File.Extension);
            }

            types = types.Distinct().ToList();
            types.Sort();

            fileTypes = new string[types.Count + 1];
            fileTypes[0] = "*";

            for (int i = 1; i < fileTypes.Length; i++)
            {
                fileTypes[i] = types[i - 1];
            }
        }

        private void SourceDirectoryWatcher_Updated(object sender, FileSystemEventArgs e)
        {
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Changed:
                    {
                        ExtendedFileInfo efi;
                        try
                        {
                            efi = new ExtendedFileInfo(e.FullPath, sourceFolder);
                        }
                        catch (FileNotFoundException)
                        {
                            return;
                        }
                        catch (Exception ex)
                        {
                            OnExceptionMessage(ex.Message);
                            return;
                        }
                        for (int i = 0; i<sourceFilesList.Count; i++)
                        {
                            if (String.Equals(sourceFilesList[i].FullName, efi.FullName))
                            {
                                if (String.Equals(sourceFilesList[i].Version, efi.Version) && String.Equals(sourceFilesList[i].File.LastWriteTime, sourceFilesList[i].File.LastWriteTime) && (sourceFilesList[i].Size == efi.Size))
                                {
                                    return;
                                }
                                else
                                {
                                    sourceFilesList[i] = efi;
                                }
                            }
                        }
                        break;
                    }

                case WatcherChangeTypes.Renamed:
                    {
                        try
                        {
                            ExtendedFileInfo efi = new ExtendedFileInfo(e.FullPath, sourceFolder);
                            string oldFullName = ((System.IO.RenamedEventArgs)e).OldFullPath;
                            for (int i = 0; i < sourceFilesList.Count; i++)
                            {
                                if (String.Equals(oldFullName, sourceFilesList[i].FullName))
                                {
                                    sourceFilesList[i] = efi;
                                    break;
                                }
                            }
                            if (Autorename)
                            {
                                if (Directory.Exists(Path.Combine(targetFolder, efi.File.Directory.FullName.Remove(0, sourceFolder.Length + 1))))
                                {
                                    string renamedFile = oldFullName.Remove(0, sourceFolder.Length + 1);
                                    for (int i = 0; i < targetFilesList.Count; i++)
                                    {
                                        if (String.Equals(renamedFile, targetFilesList[i].NoPathFullName))
                                        {
                                            File.Move(targetFilesList[i].FullName, Path.Combine(TargetFolder, efi.NoPathFullName));
                                            targetFilesList[i] = new ExtendedFileInfo(Path.Combine(TargetFolder, efi.NoPathFullName), targetFolder);
                                            targetFilesList.Sort();
                                            FilterFileList(typeSelected, targetFilesList, filteredTargetFileList);
                                            break;
                                        }
                                    }
                                }
                            }
                            sourceFilesList.Sort();
                            FilterFileList(typeSelected, sourceFilesList, filteredSourceFileList);
                            if (isCompared)
                            {
                                CompareAccorrdingToSettings();
                            }
                            OnListUpdated();
                        }
                        catch(AccessViolationException)
                        {
                            OnExceptionMessage("There is no access to the file");
                        }
                        catch(Exception ex)
                        {
                            OnExceptionMessage(ex.Message);
                        }
                        return;
                    }
                default:
                    {
                        if (SourceFilesCount == GetFilesCountInSub(sourceFolder, 0))
                        {
                            return;
                        }

                        bool isFolderStillUpdating = true;

                        //wait if files list in folder still updating
                        do
                        {
                            sourceFilesList.Clear();
                            if (includeSubfolders)
                            {
                                FilesListInitTree(sourceFilesList, sourceFolder, sourceFolder);
                            }
                            else
                            {
                                FilesListInit(sourceFilesList, filteredSourceFileList, sourceFolder);
                            }
                            Thread.Sleep(2000);
                            if (SourceFilesCount == GetFilesCountInSub(sourceFolder, 0))
                            {
                                isFolderStillUpdating = false;
                            }
                        }
                        while (isFolderStillUpdating);
                        break;
                    }
            }

            if (isCompared)
            {
                CompareAccorrdingToSettings();
            }

            FileTypesFilling();
            FilterFileList(typeSelected, sourceFilesList, filteredSourceFileList);

            //Autosynchronization if it is On
            if (AutoSync)
            {
                SynchronizeFolders();
                return;
            }
 
            OnFolderUpdated();
        }

        private void TargetDirectoryWatcher_Updated(object sender, FileSystemEventArgs e)
        {
            if (isSynchronizing)
            {
                return;
            }

            if (e.ChangeType == WatcherChangeTypes.Renamed)
            {
                try
                {
                    ExtendedFileInfo efi = new ExtendedFileInfo(e.FullPath, sourceFolder);
                    string oldFullName = ((System.IO.RenamedEventArgs)e).OldFullPath;
                    for (int i = 0; i < targetFilesList.Count; i++)
                    {
                        if (String.Equals(oldFullName, targetFilesList[i].FullName))
                        {
                            targetFilesList[i] = efi;
                            break;
                        }
                    }
                }
                catch (AccessViolationException)
                {
                    OnExceptionMessage("There is no access to the file");
                }
                catch (Exception ex)
                {
                    OnExceptionMessage(ex.Message);
                }

            }

            if (TargetFilesCount == GetFilesCountInSub(targetFolder, 0))
            {
                return;
            }
            bool isFolderStillUpdating = true;

            //wait if files list in folder still updating
            do
            {
                targetFilesList.Clear();
                if (includeSubfolders)
                {
                    FilesListInitTree(targetFilesList, targetFolder, targetFolder);
                }
                else
                {
                    FilesListInit(targetFilesList, filteredTargetFileList, targetFolder);
                }
                Thread.Sleep(2000);
                if (TargetFilesCount == GetFilesCountInSub(targetFolder, 0))
                {
                    isFolderStillUpdating = false;
                }
            }
            while (isFolderStillUpdating);

            if (isCompared)
            {
                CompareAccorrdingToSettings();
            }
            FilterFileList(typeSelected, targetFilesList, filteredTargetFileList);

            OnFolderUpdated();
        }

        private void FoldersTreeAlign(string source, string target)
        {
            List<DirectoryInfo> sourceDirList = new List<DirectoryInfo>();
            List<DirectoryInfo> targetDirList = new List<DirectoryInfo>();
            foreach (var item in Directory.GetDirectories(source))
            {
                sourceDirList.Add(new DirectoryInfo(item));
            }
            foreach (var item in Directory.GetDirectories(target))
            {
                targetDirList.Add(new DirectoryInfo(item));
            }

            foreach (var itemS in sourceDirList)
            {
                bool exists = false;
                foreach (var itemT in targetDirList)
                {
                    if (String.Equals(itemS.Name, itemT.Name))
                    {
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    try
                    {
                        Directory.CreateDirectory(Path.Combine(target, itemS.Name));
                    }
                    catch(UnauthorizedAccessException)
                    {
                        OnExceptionMessage("There is no access to the target folder");
                    }
                }

            }

            foreach (var item in sourceDirList)
            {
                FoldersTreeAlign(Path.Combine(source, item.Name), Path.Combine(target, item.Name));
            }
            return;
        }

        private void FilesListInitTree( List<ExtendedFileInfo> initList, string currentPath, string parentlPath)
        {
            //build file list from currnet folder
            try
            {
                foreach (string fileItem in Directory.GetFiles(currentPath))
                {
                    ExtendedFileInfo efi = new ExtendedFileInfo(fileItem, parentlPath);
                    initList.Add(efi);
                }
            }
            catch(FileNotFoundException)
            {
                initList.Clear();
                FilesListInitTree(initList, parentlPath, parentlPath);
            }

            //build folders list for current folder and go to each one to get files
            foreach (var dirItem in Directory.GetDirectories(currentPath))
            {
                FilesListInitTree(initList, dirItem, parentlPath);
            }

        }

        private int GetFilesCountInSub(string path, int count)
        {
            foreach (var file in Directory.GetFiles(path))
            {
                count++;
            }
            if (includeSubfolders)
            {
                foreach (var dir in Directory.GetDirectories(path))
                {
                    count = (GetFilesCountInSub(dir, count));
                }
            }

            return count;
        }

    }
}
