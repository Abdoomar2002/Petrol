using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrol.Utils
{
    public class Helper
    {
        public static string Normalize(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            StringBuilder normalizedText = new StringBuilder(input);

            // Normalize variations of "ا"
            normalizedText.Replace("أ", "ا")
                         .Replace("إ", "ا")
                         .Replace("آ", "ا")
                         .Replace("ى", "ي")
                         .Replace("ئ", "ي")
                         .Replace("ؤ", "و")
                         .Replace("ة", "ه");

            // Normalize variations of "ي"
            normalizedText.Replace("ى", "ي")
                         .Replace("ئ", "ي");

            // Normalize variations of "و"
            normalizedText.Replace("ؤ", "و");

            // Normalize variations of "ه"
            normalizedText.Replace("ة", "ه");

            // Add more replacements as needed
            // Example: Normalize variations of "ك" and "ک" (if needed)
            normalizedText.Replace("ک", "ك");
            
            return normalizedText.ToString().ToLower();
        }

    }
}
