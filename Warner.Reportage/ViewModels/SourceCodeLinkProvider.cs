using System;

namespace Warner.Reportage.ViewModels
{
    public class SourceCodeLinkProvider
    {
        private readonly string sourceFile;

        public SourceCodeLinkProvider(string sourceFile)
        {
            this.sourceFile = sourceFile;
        }

        public string Provide()
        {
            return String.Format(
                "https://lineage.beanstalkapp.com/prod-orthobullets-com/browse/dev-branches/merge-websites/{0}",
                sourceFile);
        }
    }
}
