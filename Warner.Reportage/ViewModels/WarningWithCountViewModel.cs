using System;

namespace Warner.Reportage.ViewModels
{
    public class WarningWithCountViewModel
    {
        private readonly string warning;
        private readonly int countInBuild;

        public WarningWithCountViewModel(
            string warning,
            int countInBuild)
        {
            this.warning = warning;
            this.countInBuild = countInBuild;
        }

        public string WarningType
        {
            get
            {
                return warning;
            }
        }

        public int WarningsCountInBuild
        {
            get
            {
                return countInBuild;
            }
        }

        public string WarningDetailsLink
        {
            get
            {
                return GetWarningDetailsLink(WarningType);
            }
        }

        private string GetWarningDetailsLink(string warningCode)
        {
            if (warningCode.ToUpper().StartsWith("CS"))
            {
                return $"https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/{warningCode}";
            }
            if (warningCode.ToUpper().StartsWith("SA"))
            {
                return $"http://stylecop.soyuz5.com/{warningCode}.html";
            }
            return $"https://www.google.by/search?q={warningCode}";
        }
    }
}
