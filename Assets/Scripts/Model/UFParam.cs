using System;
using System.Collections.Generic;

public class UFParam
{
    [NonSerialized]
    public System.Action ActionChanged = null;
    Dictionary<string, float> param = new Dictionary<string, float>();
    [NonSerialized]
    Dictionary<string, System.Action> paramA = new Dictionary<string, System.Action>();

    public float Get(string nameElement)
    {
        float p = 0;
        param.TryGetValue(nameElement, out p);
        return p;
    }

    public float Next(string nameElement, int up)
    {
        //
        return Get(nameElement);
    }

    public void Set(string nameElement, float _value, System.Action action = null)
    {
        param[nameElement] = _value;
        
        // на всех
        ActionChanged?.Invoke();
        // индивидуальный
        if (paramA.TryGetValue(nameElement, out var individualAction))
            individualAction?.Invoke();
    }

    public void SetA(string nameElement, System.Action action = null)
    {
        if (action != null)
        {
            if (!paramA.ContainsKey(nameElement))
                paramA[nameElement] = action;
            else paramA[nameElement] += action;
        }
    }

    public void ClearA(string nameElement, System.Action action)
    {
        if (paramA.ContainsKey(nameElement))
            paramA[nameElement] -= action;
    }
}

public class UIParam
{
    [NonSerialized]
    public System.Action ActionChanged = null;
    Dictionary<string, int> param = new Dictionary<string, int>();
    public Dictionary<string, int> P => param;
    
    [NonSerialized]
    Dictionary<string, System.Action> paramA = new Dictionary<string, System.Action>();

    public int Get(string nameElement)
    {
        int p = 0;
        param.TryGetValue(nameElement, out p);
        return p;
    }

    public void Set(string nameElement, int _value)
    {
        param[nameElement] = _value;
        
        // на всех
        ActionChanged?.Invoke();
        // индивидуальный
        if (paramA.TryGetValue(nameElement, out Action individualAction))
            individualAction?.Invoke();
    }

    public void SetA(string nameElement, bool _new = false , System.Action action = null)
    {
        if (action != null)
        {
            if (!paramA.ContainsKey(nameElement))
            {
                paramA[nameElement] = action;
                if (_new)
                {
                    // устанавливаем начальное значение
                    Set(nameElement, 0);
                }
            }
            else paramA[nameElement] += action;
        }
    }

    public void ClearA(string nameElement, System.Action action)
    {
        if (paramA.ContainsKey(nameElement))
            paramA[nameElement] -= action;
    }
}
