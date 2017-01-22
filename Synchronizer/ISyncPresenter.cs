using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronizer
{

    interface ISyncPresenter
    {
        void AttachView(ISynchView syncView);

        void AttachModel(ISyncModel syncModel);


        // on view events methods

        void DetachView(ISynchView syncView);

        void InitSyncView(ISynchView syncView);

        void SaveSession(ISynchView syncView);

        void OpenSession(ISynchView syncView);

        void DeleteSession(ISynchView syncView);

        void SyncFolder(ISynchView syncView);

        void AutoSyncOn(ISynchView syncView);

        void AutoSyncOff(ISynchView syncView);

        void ShowHistory(ISynchView syncView);

        void CompareFolders(ISynchView syncView);

        void SourcePathSelected(ISynchView syncView);

        void TargetPathSelected(ISynchView syncView);

        void FileTypeSelected(ISynchView syncView);

        // on model events methods

        void FoldersSynchronized(ISyncModel syncModel);

        void FoldersCompared(ISyncModel syncModel);

        void FoldersFiltered(ISyncModel syncModel);

    }
}
