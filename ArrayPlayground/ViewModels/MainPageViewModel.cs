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

        private int _selectedIndex = 0;
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

        public int LastIndex
        {
            get
            {
                return Items.Count - 1;
            }
        }

        private bool _isSorted = false;
        public bool IsSorted
        {
            get
            {
                return _isSorted;
            }

            set
            {
                Set(ref _isSorted, value);
            }
        }

        public MainPageViewModel()
        {
            PrevSelected = -1;
            Items.CollectionChanged += delegate
            {
                RemoveAllCommand.RaiseCanExecuteChanged();
                RemoveSelectedCommand.RaiseCanExecuteChanged();
            };
        }

        #region SelectionManipulation
        private int PrevSelected { get; set; }
        private void PushSelected()
        {
            PrevSelected = SelectedIndex;
            SelectedIndex = -1;
        }
        private int PopSelected(int? overrideSelection)
        {
            var prev = (overrideSelection.HasValue) ? overrideSelection.Value : PrevSelected;
            PrevSelected = -1;
            return prev;
        }

        //Need to know, whether it is possible to do that more elegant, bc 
        //It looks good with delegate, but the thing with losing SelectedIndex is rather 
        //Unpredictable

        /// <summary>
        /// Push SelectedIndex at the beginning and pop it when Action is done, ensures that index will coreectly be displayed in the UI
        /// Note: inside Action SelectedIndex is -1 always.
        /// 
        /// </summary>
        /// <param name="DoSet">Action to invoke</param>
        /// <param name="nextSelection">Next selection to set, if not specified, the pushed index will be used</param>
        private void DoSafeSelected(Action DoSet, int? nextSelection = null)
        {
            PushSelected();
            DoSet?.Invoke();
            SelectedIndex = PopSelected(nextSelection);
        }
        #endregion

        #region Adding
        public void AddBack()
        {
            DoSafeSelected( delegate
            {
                Items.Add(new ArrayItem());
            }, LastIndex + 1);
        }

        public void AddFront()
        {
            DoSafeSelected(delegate
            {
                Items.Insert(0, new ArrayItem());
            }, 0);
        }

        //I think we handle brim cases both
        public void AddAfter()
        {
            if (SelectedIndex == Items.Count - 1)
            {
                AddBack();
            }
            else
            {
                DoSafeSelected( delegate
                {
                    Items.Insert(PrevSelected+1, new ArrayItem());
                }, SelectedIndex + 1); 
            }
        }

        public void AddBefore()
        {
            if (SelectedIndex == 0)
            {
                AddFront();
            }
            else
            {
                DoSafeSelected(delegate
                {
                    Items.Insert(PrevSelected - 1, new ArrayItem());
                }, SelectedIndex);
            }
        }
        #endregion

        #region Removing
        private bool OneItemLeft()
        {
            return Items.Count == 1;
        }

        private DelegateCommand _removeSelectedCommand = null;
        public DelegateCommand RemoveSelectedCommand
        {
            get
            {
                return _removeSelectedCommand ??
                    (_removeSelectedCommand = new DelegateCommand(delegate
                        {
                            RemoveSelected();
                        },() => !OneItemLeft())
                    );
            }
        }
        private void RemoveSelected()
        {
            if (Items.Count > 1)
            {
                DoSafeSelected(delegate
                {
                    Items.RemoveAt(PrevSelected);
                }, 
                
                (SelectedIndex == LastIndex) ? SelectedIndex-1 : SelectedIndex);
            }
        }

        public void ClearAll()
        {
            foreach (var item in Items)
            {
                item.Value = 0;
            }
        }

        private DelegateCommand _removeAllCommand = null;
        public DelegateCommand RemoveAllCommand
        {
            get
            {
                return _removeAllCommand ??
                    (_removeAllCommand = new DelegateCommand(delegate
                        {
                            RemoveAll();
                        }, () => !OneItemLeft())
                    );
            }
        }

        public void RemoveAll()
        {
            DoSafeSelected(delegate
            {
                Items.Clear();
                AddBack();
            }, 0);
        }
        #endregion

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

