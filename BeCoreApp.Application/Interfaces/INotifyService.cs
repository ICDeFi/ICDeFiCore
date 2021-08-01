using BeCoreApp.Application.ViewModels.System;

namespace BeCoreApp.Application.Interfaces
{
    public interface INotifyService
    {

        NotifyViewModel Add(NotifyViewModel blog);

        void Update(NotifyViewModel blog);
        
        NotifyViewModel GetFirst();

        NotifyViewModel GetbyActive();

        void Save();
    }
}
