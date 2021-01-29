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
    public Text txUpdate;
    public Button btUpgrader;
    public Text txCost;
    public Image iResource;
    
    // what
    private string nameParam = string.Empty;
    private UIParam _param;
    
    // How

    private void Awake()
    {
        //Init("Damage");
    }

    public void Init(string param, UIParam data)
    {
        nameParam = param;
        _param = data;
        
        btUpgrader.onClick.RemoveAllListeners();
        btUpgrader.onClick.AddListener(Click);
        
        _param.SetA(nameParam, false, Refresh);

        Refresh();        
        iResource.color = Color.magenta;
    }

    void Refresh()
    {
        txParameter.text = $"{nameParam}: {GameController.GameState.current.Get(nameParam)}";
        txBonus.text = "+";
        txUpdate.text = $"{_param.Get(nameParam)}";
        txCost.text = $"{2}";
    }

    void Click()
    {
        // проверка на купить
        //
        
        int value = _param.Get(nameParam);
        value += 1;
        
        // улучшить
        _param.Set(nameParam, value);
        // отобразить результат
        // новое значение
        
        
        // стоимость нового улучшения
    }

    private void OnDisable()
    {
        _param.ClearA(nameParam, Refresh);
    }
}
