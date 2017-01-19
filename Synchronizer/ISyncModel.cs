using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronizer
{
    public interface ISyncModel
    {
        List<Session> GetSessionsList();

        void SessionSave(Session currentSession);

        void SessionLoad(int sessionID);

        void SessionDelete(int sessionID);

        void CompareFolders(Session currentSession);

        void SynchronizeFolders (Session currentSession);

        void GetHistory();

    }
}
