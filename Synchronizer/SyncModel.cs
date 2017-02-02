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
        //fields
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


        //properties
        public bool AutoSync
        {
            set
            {
                if (value)
                {
                    autoSyncStatus = true;
                    SynchronizeFolders();
                    Logger.Write("Autosync On");
                }
                else
                {
                    autoSyncStatus = false;
                    Logger.Write("Off");
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
                    Logger.Write(String.Format("Sorce folder {0} seledted", sourceFolder));
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
                    Logger.Write(String.Format("Sorce folder {0} seledted", sourceFolder));
                    OnListUpdated();
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
                    Logger.Write(String.Format("Target folder {0} seledted", targetFolder));
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
                    Logger.Write(String.Format("Target folder {0} seledted", targetFolder));
                    OnListUpdated();
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
            get
            {
                return includeSubfolders;
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


        //constructor, creates all required objects
        public SyncModel()
        {
            fileTypes = new string[] { "*" };
            sourceFilesList = new List<ExtendedFileInfo>();
            filteredSourceFileList = new List<ExtendedFileInfo>();
            targetFilesList = new List<ExtendedFileInfo>();
            filteredTargetFileList = new List<ExtendedFileInfo>();
            sourceDirectoryWatcher = new FileSystemWatcher();
            targetDirectoryWatcher = new FileSystemWatcher();
            Logger.Write("Synchronizer started");
        }

        //events generated by model
        public event FoldersFilteredEventHandler FoldersFiltered;
        public event FoldersComparedEventHandler FoldersCompared;
        public event FoldersSynchronizedEventHandler FoldersSynchronized;
        public event FolderUpdatedEventHandler FolderUpdated;
        public event ListUpdatedEventHandler ListUpdated;
        public event ExceptionMessageEventHandler ExceptionMessage;

        //external methods
        public void CompareFolders()
        {
            CleanCompareData();
            CompareAccorrdingToSettings();
            isCompared = true;
            OnFoldersCompared();
        }

        public void SynchronizeFolders()
        {
            Logger.Write(String.Format("<<Synchronize started>>"));
            Logger.Write(String.Format("Parameters: FileVersion {0}, LastChange {1}, AddMissedFile {2}, Size {3}, Include subfolders {4}", FileVersion, LastChange, AddMissedFile, Size, includeSubfolders));
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
                try
                {

                    if (AddMissedFile && !itemS.IsExists)
                    {
                        itemS.File.CopyTo(Path.Combine(targetFolder, itemS.NoPathFullName));
                        Logger.Write(String.Format("File {0} been copied to {1}", itemS.FullName, Path.Combine(targetFolder, itemS.NoPathFullName)));
                    }
                    else
                    {
                        if (itemS.IsLastChangeHigh || itemS.IsVersionHigh || itemS.IsSizeDiffer)
                        {
                            itemS.File.CopyTo(Path.Combine(targetFolder, itemS.NoPathFullName), true);
                            Logger.Write(String.Format("File {0} been copied to {1}", itemS.FullName, Path.Combine(targetFolder, itemS.NoPathFullName)));
                        }
                    }
                }
                catch (AccessViolationException)
                {
                    OnExceptionMessage("There is no access to the target folder");
                    Logger.Write(String.Format("There is no access to the {0}", Path.Combine(targetFolder, itemS.NoPathFullName)));
                }
                catch (FileNotFoundException)
                {
                    OnExceptionMessage("File probably been deleted during synchronization");
                    Logger.Write(String.Format("File not found {0}", itemS.File.FullName));
                }
                catch (Exception ex)
                {
                    OnExceptionMessage(ex.Message);
                    Logger.Write(ex);
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
            Logger.Write(String.Format("<<Synchronize finished>>"));
        }

        //internal methods
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
                Logger.Write(String.Format("File not found during list initialization"));
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
                        FileChangedEventOperate(sourceFilesList, sourceFolder, e);
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
                                    sourceFilesList.Sort();
                                    break;
                                }
                            }
                            if (Autorename)
                            {
                                if (String.IsNullOrEmpty(targetFolder))
                                {
                                    break;
                                }
                                string dirToCheck = String.Empty;
                                try
                                {
                                    dirToCheck = efi.File.Directory.FullName.Remove(0, sourceFolder.Length + 1);
                                }
                                catch
                                {
                                }
                                if (Directory.Exists(Path.Combine(targetFolder, dirToCheck)))
                                {
                                    string renamedFile = oldFullName.Remove(0, sourceFolder.Length + 1);
                                    for (int i = 0; i < targetFilesList.Count; i++)
                                    {
                                        if (String.Equals(renamedFile, targetFilesList[i].NoPathFullName))
                                        {
                                            targetDirectoryWatcher.Renamed -= TargetDirectoryWatcher_Updated;
                                            File.Move(targetFilesList[i].FullName, Path.Combine(TargetFolder, efi.NoPathFullName));
                                            targetDirectoryWatcher.Renamed += TargetDirectoryWatcher_Updated;
                                            targetFilesList[i] = new ExtendedFileInfo(Path.Combine(TargetFolder, efi.NoPathFullName), targetFolder);
                                            targetFilesList.Sort();
                                            FilterFileList(typeSelected, targetFilesList, filteredTargetFileList);
                                            Logger.Write(String.Format("File {0} been renamed to {1}", targetFilesList[i].FullName, Path.Combine(TargetFolder, efi.NoPathFullName)));
                                            targetFilesList.Sort();
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        catch(AccessViolationException)
                        {
                            OnExceptionMessage("There is no access to the file");
                            Logger.Write(String.Format("There is no access to the file {0}", e.FullPath));
                        }
                        catch (Exception ex)
                        {
                            OnExceptionMessage(ex.Message);
                            Logger.Write(ex);
                        }
                        break;
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
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Renamed:
                    {
                        try
                        {
                            ExtendedFileInfo efi = new ExtendedFileInfo(e.FullPath, targetFolder);
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
                            Logger.Write(String.Format("There is no access to the file {0}", e.FullPath));
                        }
                        catch (Exception ex)
                        {
                            OnExceptionMessage(ex.Message);
                            Logger.Write(ex);
                        }
                        break;
                    }

                case WatcherChangeTypes.Changed:
                    {
                        FileChangedEventOperate(targetFilesList, targetFolder, e);
                        break;
                    }

                default:
                    {

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
                        break;
                    }
            }
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
                        Logger.Write(String.Format("There is no access to the folder {0}", Path.Combine(target, itemS.Name)));
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
            catch (FileNotFoundException)
            {
                initList.Clear();
                FilesListInitTree(initList, parentlPath, parentlPath);
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }

            //build folders list for current folder and go to each one to get files
            foreach (var dirItem in Directory.GetDirectories(currentPath))
            {
                try
                {
                    FilesListInitTree(initList, dirItem, parentlPath);
                }
                catch (Exception ex)
                {
                    Logger.Write(ex);
                }
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

        private void FileChangedEventOperate(List<ExtendedFileInfo> list, string folder, FileSystemEventArgs e)
        {
            ExtendedFileInfo efi;
            try
            {
                efi = new ExtendedFileInfo(e.FullPath, folder);
            }
            catch (FileNotFoundException)
            {
                return;
            }
            catch (Exception ex)
            {
                OnExceptionMessage(ex.Message);
                Logger.Write(ex);
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (String.Equals(list[i].FullName, efi.FullName))
                {
                    if (String.Equals(list[i].Version, efi.Version) && String.Equals(list[i].File.LastWriteTime, list[i].File.LastWriteTime) && (list[i].Size == efi.Size))
                    {
                        return;
                    }
                    else
                    {
                        list[i] = efi;
                    }
                }
            }
        }

        public void ShowHistory()
        {
            Logger.OpenLastLogfile();
        }

        //on events methods
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

    }
}
