using System;
using System.Globalization;
using System.Windows.Data;

namespace St.Common
{
    public class ReUploadEnableOrNotConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                bool isLive = (bool)value;
                return isLive;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
