using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeElement : MonoBehaviour
{
    private GameController GC => GameController.singleton;
    
    public Text txParameter;
    public Text txBonus;
    public Button btUpgrader;
    public Text txCost;
    public Image iResource;
    
    // what
    private string nameParam = string.Empty;
    
    // How

    private void Awake()
    {
        Init("Damage");
    }

    public void Init(string param)
    {
        nameParam = param;
        
        btUpgrader.onClick.RemoveAllListeners();
        btUpgrader.onClick.AddListener(Click);

        txParameter.text = $"{10}";
        txBonus.text = "";
        txCost.text = $"{2}";
        iResource.color = Color.magenta;
    }

    void Click()
    {
        // проверка на купить
        
        
        // улучшить
        // local/global ? 
        //GameController.singleton.Damage.Invoke();

        // отобразить результат
        // новое значение
        GameController.current.Get(nameParam);
        // стоимость улучшения
        GameController.current.Next(nameParam,1);
    }
}


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
        param.TryGetValue("nameElement", out p);
        return p;
    }

    public float Next(string nameElement, int up)
    {
        return Get(nameElement);
    }

    public void Set(string nameElement, float _value, System.Action action = null)
    {
        if (!param.ContainsKey(nameElement))
            param[nameElement] = 0;

        param[nameElement] += _value;
        ActionChanged?.Invoke();
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
    [NonSerialized]
    Dictionary<string, System.Action> paramA = new Dictionary<string, System.Action>();

    public int Get(string nameElement)
    {
        int p = 0;
        param.TryGetValue("nameElement", out p);
        return p;
    }

    public void Set(string nameElement, int _value, System.Action action = null)
    {
        if (!param.ContainsKey(nameElement))
            param[nameElement] = 0;

        param[nameElement] += _value;
        ActionChanged?.Invoke();
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
