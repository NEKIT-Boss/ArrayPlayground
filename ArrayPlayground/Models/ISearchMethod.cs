using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayPlayground.Models
{
    interface ISearchMethod
    {
        ISearchMethod SetItems(IEnumerable<int> items);
        ISearchMethod SetTarget(int target);
        ISearchMethod Search();
        SearchResult Results { get; }
    }

    class BinarySearchMethod : SearchMethod, ISearchMethod
    {
        public SearchResult Results
            => LastSearch;

        public ISearchMethod Search()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            return this;
        }

        public ISearchMethod SetItems(IEnumerable<int> items)
        {
            Items = items;
            return this;
        }

        public ISearchMethod SetTarget(int target)
        {
            Target = target;
            return this;
        }
    }
}
