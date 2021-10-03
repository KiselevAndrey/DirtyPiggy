using System;
using System.Collections.Generic;

namespace KAP.Helper
{
    public static class KAPList
    {
        private static readonly Random _random = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T Random<T>(this IList<T> list) => list[_random.Next(list.Count)];

        #region List<T>.Ind
        /// <summary>
        /// Определяет элемент от любого числа без IndexOutRange
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T Ind<T>(this List<T> list, int index)
        {
            index = Math.Abs(index);
            return list[index % list.Count];
        }

        /// <summary>
        /// Определяет элемент от любого числа без IndexOutRange
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T Ind<T>(this List<T> list, ref int index, bool shuffle = false)
        {
            if (shuffle && index >= list.Count) list.Shuffle();

            index = Math.Abs(index) % list.Count;
            return list[index];
        }
        #endregion
    }
}
