using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Domain.Utilities
{
    public static class TextHelper
    {
        //Slug Validation
        public static string ToSlug(this string Text)
        {
            if (Text == null)
                return null;

            return Text.Trim().ToLower()
                .Replace(" ", "-")
                .Replace("+", "")
                .Replace("_", "")
                .Replace(")", "")
                .Replace("(", "")
                .Replace("*", "")
                .Replace("&", "")
                .Replace("^", "")
                .Replace("%", "")
                .Replace("$", "")
                .Replace("#", "")
                .Replace("@", "");
        }
        //ConvertHtmlToText
        public static string ConvertHtmlToText(this string text)
        {
            return Regex.Replace(text, "<.*?>", " ")
                .Replace(":&nbsp;", " ");
        }

        public static bool IsUniCode(this string value)
        {
            return value.Any(v => v > 255);
        }
    }
}
