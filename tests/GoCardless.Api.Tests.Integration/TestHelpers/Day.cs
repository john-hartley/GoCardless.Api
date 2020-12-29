using System;

namespace GoCardlessApi.Tests.Integration.TestHelpers
{
    internal static class Day
    {
        internal static int From(DateTime dateTime)
        {
            return dateTime.Day <= 28 ? dateTime.Day : -1;
        }
    }
}
