using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobSearch.App.Utility.Converters
{
    public class TextToDoubleConverter
    {
        public static Double ToDouble(string value)
        {
            //Remover todas as letras.
            value = RemoveExtraText(value);

            //Conversão de String para Double
            return Double.Parse(value);
        }

        private static string RemoveExtraText(string value)
        {
            var allowedChars = "01234567890.,";
            return new string(value.Where(c => allowedChars.Contains(c)).ToArray());
        }
    }
}
