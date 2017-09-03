using System;

namespace Warner.Domain.Surrogate
{
    public class Blame : IEquatable<Blame>
    {
        public Blame(
            string codeFileName,
            int codeLineNumber,
            string developer)
        {
            CodeFileName = codeFileName;
            CodeLineNumber = codeLineNumber;
            Developer = developer;
        }

        public string CodeFileName { get; }

        public int CodeLineNumber { get; }

        public string Developer { get; }

        public bool Equals(Blame other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return string.Equals(CodeFileName, other.CodeFileName)
                && CodeLineNumber == other.CodeLineNumber
                && string.Equals(Developer, other.Developer);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((Blame)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = CodeFileName != null ? CodeFileName.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ CodeLineNumber;
                hashCode = (hashCode * 397) ^
                    (Developer != null ? Developer.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
