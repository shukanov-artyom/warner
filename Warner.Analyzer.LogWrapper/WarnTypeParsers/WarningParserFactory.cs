using System;

namespace Warner.Analyzer.LogWrapper.WarnTypeParsers
{
    internal class WarningParserFactory
    {
        private readonly string line;

        public WarningParserFactory(string line)
        {
            if (String.IsNullOrEmpty(line))
            {
                throw new ArgumentNullException(nameof(line));
            }
            this.line = line.ToLower();
        }

        public WarningParserBase Create()
        {
            WarningType type = CodeWarningFootprints.Classify(line);
            switch (type)
            {
                case WarningType.CA:
                    return new WarningParserCa(line);
                case WarningType.CS:
                    return new WarningParserCs(line);
                case WarningType.SA:
                    return new WarningParserSa(line);
                default:
                    throw new NotSupportedException(
                        $"This type of Warning is not supporrted: {type}.");
            }
        }
    }
}
