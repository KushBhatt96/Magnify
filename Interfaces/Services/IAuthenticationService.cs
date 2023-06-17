using Magnify.ViewModel;
using System;

namespace Magnify.Interfaces.Services
{
    public interface IAuthenticationService
    {
        event Action<BaseViewModel>? RedirectionOccurred;

        void Login(string nu, string wp);

        void Logout();
    }
}
