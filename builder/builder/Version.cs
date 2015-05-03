using System;

namespace builder
{
    public abstract class Version
    {
        public readonly int Major;

        public readonly int Minor;

        public readonly int MajorRevision;

        public abstract T Switch<T>(
            Func<StableVersion, T> stable, Func<UnstableVersion, T> unstable);

        protected Version(int major, int minor, int majorRevision)
        {
            Major = major;
            Minor = minor;
            MajorRevision = majorRevision;
        }

        public string BaseString
        {
            get
            {
                return Major + "." + Minor + "." + MajorRevision;
            }
        }

        public override string ToString()
        {
            return BaseString;
        }
    }

    public sealed class UnstableVersion : Version
    {
        public readonly string MinorRevision;

        public UnstableVersion(
            int major, int minor, int majorRevision, string minorRevision) : 
            base(major, minor, majorRevision)
        {
            MinorRevision = minorRevision;
        }

        public override T Switch<T>(Func<StableVersion, T> stable, Func<UnstableVersion, T> unstable)
        {
            return unstable(this);
        }

        public override string ToString()
        {
            return base.ToString() + "-" + MinorRevision;
        }
    }

    public sealed class StableVersion : Version
    {
        public readonly int MinorRevision;

        public StableVersion(
            int major, int minor, int majorRevision, int minorRevision) :
            base(major, minor, majorRevision)
        {
            MinorRevision = minorRevision;
        }

        public override T Switch<T>(
            Func<StableVersion, T> stable, Func<UnstableVersion, T> unstable)
        {
            return stable(this);
        }

        public override string ToString()
        {
            return base.ToString() + "." + MinorRevision;
        }
    }
}
