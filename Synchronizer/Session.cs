using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronizer
{
    public class Session
    {
        static int count = 0;

        public int SessionId { private set; get; }

        public string Source { set; get; }

        public string Target { set; get; }

        public string FileTypeSelected { set; get; }

        public bool FileVersion { set; get; }

        public bool LastChange { set; get; }

        public bool AddMissedFile { set; get; }

        public bool AutoSynch { set; get; }

        public bool IncludeSubfolders { set; get; }

        public List<ExtendedFileInfo> SourceFiles { set; get; }

        public List<ExtendedFileInfo> TargetFiles { set; get; }

        public Session()
        {
            SessionId = count;
            count++;
        }

    }
}
