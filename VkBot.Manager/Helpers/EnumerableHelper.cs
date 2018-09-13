using System;
using System.Linq;

namespace VkBot.Manager.Helpers
{
    public static class EnumerableHelper
    {
        private static readonly Random R;

        static EnumerableHelper()
        {
            R = new Random();
        }

        public static T Random<T>(this IQueryable<T> input)
        {
            var count = input.Count();
            return input.ElementAt(R.Next(count));
        }
    }
}