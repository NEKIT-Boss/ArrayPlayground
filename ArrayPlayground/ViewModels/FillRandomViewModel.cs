using ArrayPlayground.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Template10.Mvvm;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.Foundation;
using Template10.Common;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ArrayPlayground.ViewModels
{
    class FillRandomViewModel : ViewModelBase
    {
        private int _itemsRequired = 0;
        public int ItemsRequired
        {
            get
            {
                return _itemsRequired;
            }

            set
            {
                if (value < 0)
                {
                    value = -value;
                }

                Set(ref _itemsRequired, value);
                FillItemsCommandAsync.RaiseCanExecuteChanged();
            }
        }

        private bool _uniqueOnly = false;
        public bool UniqueOnly
        {
            get
            {
                return _uniqueOnly;
            }

            set
            {
                Set(ref _uniqueOnly, value);
            }
        }

        private bool _sortAfter = false;
        public bool SortAfter
        {
            get
            {
                return _sortAfter;
            }

            set
            {
                Set(ref _sortAfter, value);
            }
        }

        private IArrayRandomizer Randomizer { get; set; } = new DefaultArrayRandomizer();

        public FillRandomViewModel()
        {
            
        }

        public Task FillItemsAsync()
        {
            return Task.Run(async delegate
            {
                var dispatcher = DispatcherWrapper.Current();
                //Для потомков
                //await dispatcher.DispatchAsync(() => items.Clear());
                //foreach (var item in Randomizer
                //    .Configure(ItemsRequired, UniqueOnly, SortAfter)
                //    .RollItems()
                //    .Select(x => new ArrayItem(x)))
                //{
                //    await dispatcher.DispatchAsync(() => items.Add(item));
                //}

                var items = new ObservableCollection<ArrayItem>(Randomizer.Configure(ItemsRequired, UniqueOnly, SortAfter)
                        .RollItems()
                        .Select(x => new ArrayItem(x)));

                await dispatcher.DispatchAsync(delegate
                {
                    ArraySource.Instance.Items = items;
                });
            });
        }

        private DelegateCommand _fillItemsCommandAsync;
        public DelegateCommand FillItemsCommandAsync
            => _fillItemsCommandAsync ?? (_fillItemsCommandAsync = new DelegateCommand(async delegate
            {
                Views.Busy.SetBusy(true, "Заполнение элементов ...");
                await FillItemsAsync();
                Views.Busy.SetBusy(false);
            }, () => ItemsRequired > 0));
    }
}
