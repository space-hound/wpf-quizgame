using System;
using System.Windows.Data;

namespace QuizGame.Converter
{
    public class CheckBoxCheckConverter : IValueConverter
    {
        //transforma din boolean in litera "T" pentru true si "F" pentru fals
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "T";
                else
                    return "F";
            }
            return "F";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "yes":
                case "T":
                    return true;
                case "no":
                case "F":
                    return false;
            }
            return false;
        }
    }
}
