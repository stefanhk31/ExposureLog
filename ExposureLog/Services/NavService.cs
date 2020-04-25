using ExposureLog.Services;
using ExposureLog.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(NavService))]
namespace ExposureLog.Services
{
    public class NavService  : INavService
    {
        private readonly IDictionary<Type, Type> _map = new Dictionary<Type, Type>();

        public bool CanGoBack => Navigation.NavigationStack != null && Navigation.NavigationStack.Count > 0;
        public event PropertyChangedEventHandler CanGoBackChanged;
        public INavigation Navigation { get; set; }


        public void RegisterViewMapping(Type viewModel, Type view)
        {
            _map.Add(viewModel, view);
        }

        public void ClearBackStack()
        {
            if (Navigation.NavigationStack.Count < 2)
            { 
                return;
            }
            for (var i = 0; i < Navigation.NavigationStack.Count - 1; i++) 
            { 
                Navigation.RemovePage(Navigation.NavigationStack[i]); 
            }
        }

        public async Task GoBack()
        {
            if (CanGoBack)
            {
                await Navigation.PopAsync(true);
                OnCanGoBackChanged();
            }
        }

        public async Task NavigateTo<TVM>() where TVM : BaseViewModel
        {
            await NavigateToView(typeof(TVM)); 
            if (Navigation.NavigationStack.Last().BindingContext is BaseViewModel) 
            { 
                ((BaseViewModel)Navigation.NavigationStack.Last().BindingContext).Init();
            }
        }

        public async Task NavigateTo<TVM, TParameter>(TParameter parameter) where TVM : BaseViewModel
        {
            await NavigateToView(typeof(TVM));
            if (Navigation.NavigationStack.Last().BindingContext is BaseViewModel<TParameter>)
            {
                ((BaseViewModel<TParameter>)Navigation.NavigationStack.Last().BindingContext).Init(parameter);
            }
        }

        public async Task NavigateToUriAsync(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentException("Invalid URI");
            }
            await Launcher.OpenAsync(uri);
        }

        public void RemoveLastView()
        {
            if (Navigation.NavigationStack.Count < 2)
            { 
                return; 
            }
            var lastView = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]; Navigation.RemovePage(lastView);
        }

        private async Task NavigateToView(Type viewModelType)
        {
            if (!_map.TryGetValue(viewModelType, out Type viewType))
            {
                throw new ArgumentException("No view found in view mapping for " + viewModelType.FullName + ".");
            }
            // Use reflection to get the View's constructor and create an instance of the View
            var constructor = viewType.GetTypeInfo()
                                      .DeclaredConstructors
                                      .FirstOrDefault(dc => !dc.GetParameters().Any());
            var view = constructor.Invoke(null) as Page;
            await Navigation.PushAsync(view, true);
        }

        private void OnCanGoBackChanged() => CanGoBackChanged?.Invoke(this, new PropertyChangedEventArgs("CanGoBack"));
    }
}
