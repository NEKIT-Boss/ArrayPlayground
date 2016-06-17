using ArrayPlayground.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace ArrayPlayground.ViewModels
{
    class SearchViewModel : ViewModelBase
    {
        private ObservableCollection<ArrayItem> _items =
            new ObservableCollection<ArrayItem>();
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

        private int _foundIndex = -1;
        public int FoundIndex
        {
            get
            {
                return _foundIndex;
            }

            set
            {
                Set(ref _foundIndex, value);
                RaisePropertyChanged("FoundIndexReadable");
            }
        }

        public string FoundIndexReadable
            => (_foundIndex + 1).ToString();

        private int _searchTarget = 0;
        public int SearchTarget
        {
            get
            {
                return _searchTarget;
            }

            set
            {
                Set(ref _searchTarget, value);
            }
        }

        private SearchMethods _selectedSearchMethod = SearchMethods.Binary;
        public SearchMethods SelectedSearchMethod
        {
            get
            {
                return _selectedSearchMethod;
            }

            set
            {
                Set(ref _selectedSearchMethod, value);
            }
        }

        public void Reset()
        {
            SearchTarget = 0;
            FoundIndex = -1;
        }

        public SearchViewModel()
        {
            Items = ArraySource.Instance.Items;

            ArraySource.Instance.PropertyChanged += delegate
            {
                Items = ArraySource.Instance.Items;
            };
        }
    }
}
