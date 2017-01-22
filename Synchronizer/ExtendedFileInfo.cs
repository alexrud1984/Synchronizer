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
        public FileInfo File { set; get; }
        public bool IsHighlighted { set; get; }
        public Color HlColor { set; get; }
        public bool IsSynchronized { set; get; }
        public bool IsExists { set; get; }
        public bool IsVersionHigh { set; get; }
        public bool IsLastChangeHigh { set; get; }
        public string Version { set; get; }
        public string Name { set; get; }
        public ExtendedFileInfo(string filePath)
        {
            File = new FileInfo(filePath);
            IsHighlighted = false;
            Version = FileVersionInfo.GetVersionInfo(filePath).FileVersion;
            Name = File.Name;
        }

        public int CompareTo(ExtendedFileInfo obj)
        {
            return(this.Name.CompareTo(obj.Name));
        }

    }
}
