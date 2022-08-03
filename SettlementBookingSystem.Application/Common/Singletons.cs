using System;
using System.Collections.Concurrent;

namespace SettlementBookingSystem.Application.Common
{
    public static class Singletons<T>
    {
        private static readonly ConcurrentDictionary<string, T> Instances = new ConcurrentDictionary<string, T>(StringComparer.OrdinalIgnoreCase);

        public static T GetOrAdd(string key, Func<string, T> factory)
        {
            return Instances.GetOrAdd(key, factory);
        }
    }
}
