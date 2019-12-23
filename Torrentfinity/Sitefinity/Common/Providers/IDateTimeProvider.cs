namespace Torrentfinity.Sitefinity.Common.Providers
{
    using System;

    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}