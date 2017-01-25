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
  
        public string Version { set; get; }
        public string Name { set; get; }
        public ExtendedFileInfo(string filePath)
        {
            File = new FileInfo(filePath);
            IsHighlighted = false;
            Version = FileVersionInfo.GetVersionInfo(filePath).FileVersion;
            Name = File.Name;
            IsExists = true;
        }

        public int CompareTo(ExtendedFileInfo obj)
        {
            return(this.Name.CompareTo(obj.Name));
        }

        public void SetDefaultValues()
        {
            IsExists = true;
            IsVersionHigh = false;
            IsLastChangeHigh = false;

        }
    }
}
