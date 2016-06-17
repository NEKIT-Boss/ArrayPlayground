using ArrayPlayground.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace ArrayPlayground.ViewModels
{
    class InputViewModel : ViewModelBase
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

        //Subscribe it to collection chanhged event!
        //Just check each time it is changed, actually a perfect idea,
        //But I'm not sure whether event fres as single value changes,
        //Not item added or deleted

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

        public int LastIndex
        {
            get
            {
                return Items.Count - 1;
            }
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
        #region AddingMethods
        public void AddBack()
        {
            DoSafeSelected(delegate
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
                DoSafeSelected(delegate
                {
                    Items.Insert(PrevSelected + 1, new ArrayItem());
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
        #region RemovingMethods

        private bool OneItemLeft()
        {
            return Items.Count == 1;
        }

        private void RemoveSelected()
        {
            if (Items.Count > 1)
            {
                DoSafeSelected(delegate
                {
                    Items.RemoveAt(PrevSelected);
                },

                (SelectedIndex == LastIndex) ? SelectedIndex - 1 : SelectedIndex);
            }
        }

        private DelegateCommand _removeSelectedCommand = null;
        public DelegateCommand RemoveSelectedCommand
            => _removeSelectedCommand ?? (_removeSelectedCommand = new DelegateCommand(delegate
            {
                RemoveSelected();
            }, () => !OneItemLeft()));


        public void ClearAll()
        {
            foreach (var item in Items)
            {
                item.Value = 0;
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

        private DelegateCommand _removeAllCommand = null;
        public DelegateCommand RemoveAllCommand
            => _removeAllCommand ?? (_removeAllCommand = new DelegateCommand(delegate
            {
                RemoveAll();
            }, () => !OneItemLeft()));

        #endregion

        //Subscribe method, to manage all subscriptions
        public InputViewModel()
        {
            PrevSelected = -1;
            Items.CollectionChanged += delegate
            {
                RemoveAllCommand.RaiseCanExecuteChanged();
                RemoveSelectedCommand.RaiseCanExecuteChanged();
            };

            ArraySource.Instance.PropertyChanged += delegate
            {
                Items = ArraySource.Instance.Items;
            };
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            Debug.WriteLine("Hello there!");
            return Task.CompletedTask;
        }
    }
}
