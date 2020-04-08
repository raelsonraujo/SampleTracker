using System;
using System.Globalization;
using Xamarin.Forms;

namespace SampleTracker.Converters
{
    public class BoolTOString : IValueConverter
    {
        public string TrueString;
        public string FalseString;

        public BoolTOString(string trueString, string falseString)
        {
            TrueString = trueString;
            FalseString = falseString;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolean = (bool)value;

            if (boolean) return TrueString;
            else return FalseString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
