using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static int GetRandomOnWeight(Dictionary<int,int> weights)
    {
        var summ = 0;
        foreach (var w in weights.Values) summ += w;
        var r = UnityEngine.Random.Range(0, summ);
        var keys = weights.Keys;
        foreach (var key in keys)
        {
            if (r < weights[key]) 
            {
                return key;
            }
            r -= weights[key];
        }

        return 0;
    }
    
    /// <summary>
    /// Удалить все дочерние gameObject элементы определенного Transform (родитель)
    /// </summary>
    /// <param name="tr">родитель</param>
    /// <param name="ignoreFirst">игнорировать первый</param>
    public static void DestroyChild(Transform tr, bool ignoreFirst = false)
    {
        var children = new List<GameObject>();
        foreach (Transform child in tr)
            children.Add(child.gameObject);

        if (ignoreFirst && children.Count > 0)
            children.RemoveAt(0);

        children.ForEach(child => UnityEngine.MonoBehaviour.DestroyImmediate(child));
    }

    /// <summary>
    /// Удалить все элементы GameObject по списку
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public static void DestroyListElement<T>(List<T> list) where T : Object
    {
        if (list != null)
        {
            list.ForEach(child => UnityEngine.MonoBehaviour.DestroyImmediate(child as GameObject));
            list.Clear();
        }
    }
}