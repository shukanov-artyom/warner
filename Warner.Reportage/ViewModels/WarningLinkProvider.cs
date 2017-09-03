using System;

namespace Warner.Reportage.ViewModels
{
    public class WarningLinkProvider
    {
        private readonly string warningCode;

        public WarningLinkProvider(string warningCode)
        {
            this.warningCode = warningCode;
        }

        public string GetLink()
        {
            if (warningCode.ToUpper().StartsWith("CS"))
            {
                return $"https://www.google.by/search?q={warningCode}";
                //return $"https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/{warningCode}";
            }
            if (warningCode.ToUpper().StartsWith("SA"))
            {
                return $"http://stylecop.soyuz5.com/{warningCode}.html";
            }
            return $"https://www.google.by/search?q={warningCode}";
        }
    }
}
