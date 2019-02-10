using System;
using System.Windows.Data;

namespace QuizGame.Converter
{
    public class MultiConverter : IMultiValueConverter
    {
        //ia primele doua obiecte le transforma in int si verifica daca sunt zero
        //daca nu returneaza true
        //altfel false
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var question = (int)values[0];
            bool c1 = question > 0;
            var answers = (int)values[1];
            bool c2 = answers >= 2;

            //trebe sa fie ambele diferite de zero sa fie true
            return c1 && c2;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("BooleanAndConverter is a OneWay converter.");
        }
    }
}
