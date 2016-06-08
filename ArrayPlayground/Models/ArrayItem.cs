using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayPlayground.Models
{
    public class ArrayItem
    {
        public int Value { get; set; }

        public ArrayItem()
        {
            Value = 0;
        }

        public ArrayItem(int value)
        {
            Value = value;
        }
    }
}
