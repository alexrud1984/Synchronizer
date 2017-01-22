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

        public string[] FileTypes { get; private set; }

        public List<ExtendedFileInfo> SourceFilesList { get; private set; }
      
        public string SourceFolder
        {
            set
            {
                sourceFolder = value;
                SourceListInit();
                sourceDirectoryWatcher = new FileSystemWatcher(SourceFolder);
                sourceDirectoryWatcher.EnableRaisingEvents = true;
                sourceDirectoryWatcher.Changed += SourceDirectoryWatcher_Updated;
                sourceDirectoryWatcher.Deleted += SourceDirectoryWatcher_Updated;
                sourceDirectoryWatcher.Created += SourceDirectoryWatcher_Updated;
            }
            private get
            {
                return sourceFolder;
            }
        }

        private void SourceDirectoryWatcher_Updated(object sender, FileSystemEventArgs e)
        {
            CleanCompareData();
            SourceListInit();
            OnFolderUpdated();
        }

        public List<ExtendedFileInfo> TargetFilesList { get; private set; }
       

        public string TargetFolder
        {
            set
            {
                targetFolder = value;
                TargetListInit();
            }

            get
            {
                return targetFolder;
            }
        }

        public int TypeSelected
        {
            set
            {
                typeSelected = value;
                filterFileList(FileTypes[typeSelected], SourceFilesList, FilteredSourceFileList);
                filterFileList(FileTypes[typeSelected], TargetFilesList, FilteredTargetFileList);
                OnFoldersFiltered();
            }

            get
            {
                return typeSelected;
            }
        }

        public List<ExtendedFileInfo> FilteredSourceFileList { get; private set; }

        public List<ExtendedFileInfo> FilteredTargetFileList { get; private set; }

        public bool FileVersion { set; get; }

        public bool LastChange { set; get; }

        public bool AddMissedFile { set; get; }


        public SyncModel()
        {
            FileTypes = new string[] { "*" };
            SourceFilesList = new List<ExtendedFileInfo>();
            FilteredSourceFileList = new List<ExtendedFileInfo>();
            TargetFilesList = new List<ExtendedFileInfo>();
            FilteredTargetFileList = new List<ExtendedFileInfo>();
        }

        public event FoldersFilteredEventHandler FoldersFiltered;
        public event FoldersComparedEventHandler FoldersCompared;
        public event FoldersSynchronizedEventHandler FoldersSynchronized;
        public event FolderUpdatedEventHandler FolderUpdated;

        public void CompareFolders()
        {
            CompareAccorrdingSettings();
            filterFileList(FileTypes[typeSelected], SourceFilesList, FilteredSourceFileList);
            filterFileList(FileTypes[typeSelected], TargetFilesList, FilteredTargetFileList);
            OnFoldersCompared();
        }

        public void SynchronizeFolders()
        {
            CompareAccorrdingSettings();
            foreach (var itemS in FilteredSourceFileList)
            {
                if (AddMissedFile && !itemS.IsExists)
                {
                    itemS.File.CopyTo(Path.Combine(TargetFolder,itemS.File.Name));
                }
                else
                {
                    foreach (var itemT in FilteredTargetFileList)
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
            foreach (var item in SourceFilesList)
            {
                item.HlColor = System.Drawing.Color.Black;
                item.IsHighlighted = false;
                item.IsLastChangeHigh = false;
                item.IsVersionHigh = false;
            }
        }

        private void SourceListInit ()
        {
            List<string> types = new List<string>();
            SourceFilesList.Clear();
            FilteredSourceFileList.Clear();
            foreach (string item in Directory.GetFiles(sourceFolder))
            {
                ExtendedFileInfo efi = new ExtendedFileInfo(item);
                SourceFilesList.Add(efi);
                types.Add(efi.File.Extension);
            }
            SourceFilesList.Sort();
            types = types.Distinct().ToList();
            FileTypes = new string[types.Count + 1];
            FileTypes[0] = "*";

            for (int i = 1; i < FileTypes.Length; i++)
            {
                FileTypes[i] = types[i - 1];
            }
            filterFileList(FileTypes[typeSelected], SourceFilesList, FilteredSourceFileList);
            CleanCompareData();
        }

        private void TargetListInit()
        {
            TargetFilesList.Clear();
            FilteredTargetFileList.Clear();
            foreach (string item in Directory.GetFiles(targetFolder))
            {
                ExtendedFileInfo efi = new ExtendedFileInfo(item);
                TargetFilesList.Add(efi);
            }

            TargetFilesList.Sort();
            filterFileList(FileTypes[typeSelected], TargetFilesList, FilteredTargetFileList);
            CleanCompareData();
        }

        private void CompareAccorrdingSettings()
        {
            CleanCompareData();
            CheckExist(SourceFilesList, TargetFilesList);
            CheckExist(TargetFilesList, SourceFilesList);
            if (FileVersion)
            {
                CompareFileVersion(SourceFilesList, TargetFilesList);
                CompareFileVersion(TargetFilesList, SourceFilesList);
            }
            if (LastChange)
            {
                CompareLastChange(SourceFilesList, TargetFilesList);
                CompareLastChange(TargetFilesList, SourceFilesList);
            }
        }
    }
}
