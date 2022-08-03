using System;

namespace SettlementBookingSystem.Application.Common
{
    public static class EnvironmentHelper
    {
        public static int GetInt(string variable, int defaultValue = 0)
        {
            var value = Environment.GetEnvironmentVariable(variable, EnvironmentVariableTarget.Process);
            return int.TryParse(value, out var configValue) ? configValue : defaultValue;
        }

        public static string GetString(string key, string defaultValue = "")
        {
            var value = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);
            return string.IsNullOrEmpty(value) ? defaultValue : value;
        }

        public static bool GetBoolean(string key, bool defaultValue = false)
        {
            var value = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);
            return bool.TryParse(value, out var result) ? result : defaultValue;
        }
    }
}
