using ArrayPlayground.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ArrayPlayground.Converters
{
    class IntToSearchMethod : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is SearchMethods)
            {
                return (int)value;
            }
            else
            {
                return -1;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (int)value;
        }
    }
}
