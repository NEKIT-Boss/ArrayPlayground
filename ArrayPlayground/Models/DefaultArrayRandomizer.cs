using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayPlayground.Models
{
    class DefaultArrayRandomizer : IArrayRandomizer
    {
        const int MAX_VALUE = 100000;

        bool sortAfter;
        bool uniqueOnly;
        int itemsRequired;

        public IArrayRandomizer Configure(int itemsRequired, bool uniqueOnly = false, bool sortAfter = false)
        {
            this.itemsRequired = itemsRequired;
            this.uniqueOnly = uniqueOnly;
            this.sortAfter = sortAfter;
            return this;
        }

        public IEnumerable<int> RollItems()
        {
            int power = 10;

            while (power < MAX_VALUE)
            {
                if (power >= itemsRequired)
                {
                    break;
                }
                power *= 10;
            }

            int maxValue = 0;
            maxValue = (power == MAX_VALUE) ? power - 1 : power * 10;

            Random randomizer = new Random();
            int[] items = new int[itemsRequired];
            if (uniqueOnly)
            {
                items = Enumerable.Range(1, maxValue).OrderBy(x => randomizer.Next()).Take(itemsRequired).ToArray();
            }
            else
            {
                for (int i = 0; i < itemsRequired; i++)
                {
                    items[0] = randomizer.Next(1, maxValue);
                }
            }

            if (sortAfter)
            {
                items = items.OrderBy(x => x).ToArray();
            }

            return items;
        }
    }
}
