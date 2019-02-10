using System;
using System.Windows.Data;
using System.Windows.Media;

namespace QuizGame.Converter
{
    public class ColorConverter : IValueConverter
    {
        //in functie de valoarea proprietatii IsTrue a unui raspuns
        //returnez o culoare, rosu sau verde pentru false respectiv true
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                if ((bool)value == true)
                    return new SolidColorBrush(Colors.Green);
                else
                    return new SolidColorBrush(Colors.Red);
            }
            return Colors.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
