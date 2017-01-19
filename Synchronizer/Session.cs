using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronizer
{
    public class Session
    {
        string Source { set; get; }

        string Target { set; get; }

        string FileType { set; get; }

        bool FileVersion { set; get; }

        bool LastChange { set; get; }

        bool AddMissedFile { set; get; }

        bool AutoSynch { set; get; }

        bool IncludeSubfolders { set; get; }

    }
}
