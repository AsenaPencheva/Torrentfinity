namespace Torrentfinity.Sitefinity.Common.Providers
{
    using System;

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow =>  DateTime.UtcNow;
    }
}