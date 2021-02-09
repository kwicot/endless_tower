using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

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
    public static void DestroyChild(Transform tr)
    {
        if (tr == null) return;
        while (tr.childCount > 0)
        {
            UnityEngine.MonoBehaviour.DestroyImmediate(tr.GetChild(0).gameObject);
        }
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

    public static float Round(float value, int places = 2)
    {
        var p = 1;
        for (int i = 0; i < places; i++)
            p *= 10;

        var result = (float) Math.Round(value * p) / p;  
        
        return result;
    }
}