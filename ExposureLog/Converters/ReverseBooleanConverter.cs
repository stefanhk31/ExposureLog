﻿using System;
using System.Globalization;
using Xamarin.Forms;


namespace ExposureLog.Converters
{
    class ReverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
            {
                return value;
            }
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
            {
                return value;
            }
            return !(bool)value;
        }
    }
}
