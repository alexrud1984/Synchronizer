using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronizer
{
    public class ExtendedFileInfo:IComparable<ExtendedFileInfo>
    {
        //file itself
        public FileInfo File { set; get; }

        //view values
        public bool IsHighlighted { set; get; }
        public Color HlColor { set; get; }

        //model values
        public bool IsExists { set; get; }
        public bool IsVersionHigh { set; get; }
        public bool IsLastChangeHigh { set; get; }
        public bool IsSizeDiffer { set; get; }
  
        public string Version { set; get; }
        public string Name { set; get; }
        public string FullName { set; get; }
        public string ParentPath { set; get; }
        public string NoPathFullName { set; get; }
        public double Size { set; get; }

        public int OpponentIndex { set; get; }

        public ExtendedFileInfo(string filePath, string parentPath)
        {
            File = new FileInfo(filePath);
            IsHighlighted = false;
            Version = FileVersionInfo.GetVersionInfo(filePath).FileVersion;
            Name = File.Name;
            FullName = File.FullName;
            ParentPath = parentPath;
            NoPathFullName = FullName.Remove(0, parentPath.Length+1);
            IsExists = true;
            Size = this.File.Length;
        }

        public int CompareTo(ExtendedFileInfo obj)
        {
            return(this.NoPathFullName.CompareTo(obj.NoPathFullName));
        }

        public void SetDefaultValues()
        {
            IsExists = true;
            IsVersionHigh = false;
            IsLastChangeHigh = false;
            IsSizeDiffer = false;
        }
    }
}
