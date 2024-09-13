using System.Collections.Generic;

namespace Utils.ListExtension
{
    public static class ListExtenstion
    {
        public static void Shuffle<T>(this List<T> arr)
        {
            var random = new System.Random();
            var n = arr.Count;
            while (n > 1)
            {
                n--;
                var k = random.Next(n + 1);
                (arr[k], arr[n]) = (arr[n], arr[k]);
            }
        }
    }
}