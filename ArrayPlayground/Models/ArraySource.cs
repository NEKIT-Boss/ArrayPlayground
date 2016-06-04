using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace ArrayPlayground.Models
{
    class ArraySource : BindableBase
    {
        private Random Randomizer { get; set; }

        private static ArraySource _instance = new ArraySource();
        public static ArraySource Instance
        {
            get { return _instance; }
        }

        private ArraySource()
        {
            Randomizer = new Random();
            foreach (var item in Enumerable.Range(0, 2000))
            {
                Items.Add(new ArrayItem(item));
            }
        }

        private ObservableCollection<ArrayItem> _items = new ObservableCollection<ArrayItem>();
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

        public void Shuffle()
        {
            Items = new ObservableCollection<ArrayItem>(Items.OrderBy(i => Randomizer.NextDouble()));
        }

    }
}
