using System.Text.RegularExpressions;

namespace Jago.CrossCutting.Helper
{
    public static class ValidDocumentExtension
    {
        public static bool IsValidDocument(this string document)
        {
            var patternRg = @"(^\d{1,2}).?(\d{3}).?(\d{3})-?(\d{1}|X|x$)";
            var patternCpf = @"(^\d{3}\.\d{3}\.\d{3}\-\d{2}$)";

            if (Regex.IsMatch(document, patternRg) || Regex.IsMatch(document, patternCpf))
                return true;

            return false;
        }
    }
}
