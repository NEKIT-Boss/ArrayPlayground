using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using ArrayPlayground.Models;

namespace ArrayPlayground.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<ArrayItem> _items = ArraySource.Instance.Items;
        public ObservableCollection<ArrayItem> Items
        {
            get
            {
                return _items;
            }

            set
            {
                Set(ref _items, value);
            }
        }

        private int _selectedIndex = -1;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }

            set
            {
                Set(ref _selectedIndex, value);
            }
        }

        public MainPageViewModel()
        {
            
        }

        public void Shuffle()
        {
            ArraySource.Instance.Shuffle();
            Items = ArraySource.Instance.Items;
        }

        public void Nexty()
        {
            if (SelectedIndex != (Items.Count - 1) )
            {
                SelectedIndex++;
            }

            else
            {
                Items.Add(new ArrayItem());
            }
        }

        public void Prevy()
        {
            if (SelectedIndex != 0)
            {
                SelectedIndex--;
            }
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            //if (suspensionState.Any())
            //{
            //    Value = suspensionState[nameof(Value)]?.ToString();
            //}
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            //if (suspending)
            //{
            //    suspensionState[nameof(Value)] = Value;
            //}
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        //public void GotoDetailsPage() =>
        //    NavigationService.Navigate(typeof(Views.DetailPage), Value);

        //public void GotoSettings() =>
        //    NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        //public void GotoPrivacy() =>
        //    NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        //public void GotoAbout() =>
        //    NavigationService.Navigate(typeof(Views.SettingsPage), 2);

    }
}

