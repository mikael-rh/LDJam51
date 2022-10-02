using UnityEngine;
using System.Collections.Generic;

namespace RandomExtensions
{
    public static class RandomCollectionExtensions
    {
        public static T RandomElement<T>(this T[] arr) => arr[Random.Range(0, arr.Length)];
        public static T RandomElement<T>(this IList<T> list) => list[Random.Range(0, list.Count)];
    }
}