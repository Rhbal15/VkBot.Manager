using System;

namespace VkBot.Manager.Controllers
{
    public static class StringHelper
    {
        public static bool EqualsIgnoreCase(this string first, string second)
        {
            return string.Equals(first, second, StringComparison.OrdinalIgnoreCase);
        }
    }
}