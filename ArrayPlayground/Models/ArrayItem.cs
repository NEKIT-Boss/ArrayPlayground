using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace ArrayPlayground.Models
{
    //The thing is, that, actually this model is bindable base, is it fine, no, but for now it will do,
    //later we will think about that
    public class ArrayItem : BindableBase
    {
        private int _value = 0;
        public int Value
        {
            get
            {
                return _value;
            }

            set
            {
                Set(ref _value, value);
            }
        }

        public ArrayItem(int value = 0)
        {
            Value = value;
        }
    }
}
