using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayPlayground.Models
{
    interface IArrayRandomizer
    {
        IArrayRandomizer Configure(int itemsRequired, bool uniqueOnly = false, bool sortAfter = false);
        IEnumerable<int> RollItems();
    }
}
