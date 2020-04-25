using ExposureLog.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;


namespace ExposureLog.Services
{
    public interface INavService
    {
        bool CanGoBack { get; }
        Task GoBack();
        Task NavigateTo<TVM>()
            where TVM : BaseViewModel;
        Task NavigateTo<TVM, TParameter>(TParameter parameter)
            where TVM : BaseViewModel;
        void RemoveLastView();
        void ClearBackStack();
        Task NavigateToUriAsync(Uri uri);
        event PropertyChangedEventHandler CanGoBackChanged;
    }
}
