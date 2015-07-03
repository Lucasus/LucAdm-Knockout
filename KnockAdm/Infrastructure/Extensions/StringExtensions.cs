using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnockAdm
{
    public static class StringExtensions
    {
        public static string FirstLetterToUpper(this string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }

        public static bool IsNullOrEmpty(this string s)
        {
            return String.IsNullOrEmpty(s);
        }

        // Method based on this SO answer: http://stackoverflow.com/questions/272633/add-spaces-before-capital-letters
        public static string AddSpacesToSentence(this string text, bool preserveAcronyms, char delimiter = ' ')
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                    if ((text[i - 1] != delimiter && !char.IsUpper(text[i - 1])) ||
                        (preserveAcronyms && char.IsUpper(text[i - 1]) &&
                         i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                        newText.Append(delimiter);
                newText.Append(text[i]);
            }
            return newText.ToString();
        }
    }
}
