using System;

namespace Warner.Firefighter
{
    internal sealed class LineProcessorFactory
    {
        private readonly string warningType;

        public LineProcessorFactory(string warningType)
        {
            this.warningType = warningType;
        }

        public ILineProcessor Create(
            string filePathName,
            int codeLineNumber)
        {
            switch (warningType.ToUpper())
            {
                case "SA1508":
                    return new LineProcessorSa1058(filePathName, codeLineNumber);
                case "SA1500":
                    return new LineProcessorSa1500(filePathName, codeLineNumber);
                default:
                    return new StubLineProcessor();
            }
        }
    }
}
