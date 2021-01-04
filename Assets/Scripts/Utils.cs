using System.Collections.Generic;

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
}