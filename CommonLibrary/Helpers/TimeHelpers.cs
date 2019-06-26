using System;
using System.Threading;

namespace CommonLibrary.Helpers
{
    public static class TimeHelpers
    {
        public static long UtcNowTicks(ref long lastTimeStamp)
        {
            long original, newValue;
            do
            {
                original = lastTimeStamp;
                var now = DateTime.UtcNow.Ticks;
                newValue = Math.Max(now, original + 1);
            } while (Interlocked.CompareExchange(
                         ref lastTimeStamp,
                         newValue,
                         original) != original);

            return newValue;
        }
    }
}