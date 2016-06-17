using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayPlayground.Models
{
    public struct SearchResult
    {
        public bool Found { get; private set; } 
        public int FoundIndex { get; private set; }
        public TimeSpan TimeElapsed { get; private set; }
        public long TurnsMade { get; set; }

        public SearchResult(bool found, int foundIndex, TimeSpan timeElapsed, long turnsMade)
        {
            Found = found;
            FoundIndex = foundIndex;
            TimeElapsed = timeElapsed;
            TurnsMade = turnsMade;
        }
    }

    //We could later use T here, just to perform searches from within everything
    class SearchMethod
    {
        public SearchResult LastSearch { get; protected set; }
        protected IEnumerable<int> Items { get; set; }
        protected int Target { get; set; }

        public static ISearchMethod Create(SearchMethods method)
        {
            
        }
    }
}
