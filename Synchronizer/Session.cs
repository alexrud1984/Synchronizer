using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronizer
{
    [Serializable]

    public class Session
    {
        public DateTime SessionTimestamp { set; get; }

        public string SourceFolder { set; get; }

        public string TargetFolder { set; get; }

        public string FileTypeSelected { set; get; }

        public bool FileVersion { set; get; }

        public bool LastChange { set; get; }

        public bool Autorename { set; get; }

        public bool AddMissedFile { set; get; }

        public bool AutoSync { set; get; }

        public bool Size { set; get; }

        public bool IncludeSubfolders { set; get; }


        public Session()
        {
            SessionTimestamp = DateTime.Now;
        }

    }
}
