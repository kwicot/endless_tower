using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeElement : MonoBehaviour
{
    private GameController GC => GameController.singleton;
    
    public Text txParameter;            // Название параметра и текуще значение
    public Text txBonus;                // бонус улучшения
    public Text txUpdate;               // уровень улучшения
    public Button btUpgrader;           // кнопка улучшений
    public Text txCost;                 // стоимост ьулучшения
    public Image iResource;             // ресурс затрачиыаемый на улучшение
    
    // what
    private string nameParam = string.Empty;
    private UIParam _param;
    private ParameterVar _parameterVar;

    private void Awake()
    {
        //Init("Damage");
    }

    public void Init(string param, UIParam data)
    {
        nameParam = param;
        _param = data;

        if (GC.nameToValues.TryGetValue(nameParam, out ParameterVar value))
            _parameterVar = value;
        else Debug.LogError($"{GetType().Name} - Init: ERROR {param}");
        
        btUpgrader.onClick.RemoveAllListeners();
        btUpgrader.onClick.AddListener(Click);
        
        _param.SetA(nameParam, false, Refresh);

        Refresh();        
        iResource.color = Color.magenta;
    }

    void Refresh()
    {
        txParameter.text = $"{nameParam}: {Utils.Round(GameController.GameState.current.Get(nameParam))}";
        txBonus.text = $"({Utils.Round(_parameterVar.NextParameter)})";
        txUpdate.text = $"{Utils.Round(_param.Get(nameParam))}";
        // txCost.text = $"{GC.GetCost(nameParam)}";
        txCost.text =
            $"{((_param.TypeParam == TypeParam.Local) ? Utils.Round(_parameterVar.NextLocalCost) : Utils.Round(_parameterVar.NextCost))}";
    }

    void Click()
    {
        // проверка на купить
        if (!GC.CanBuy(_param.TypeParam == TypeParam.Local ? _parameterVar.NextLocalCost : _parameterVar.NextCost))
            return;

        // оплата
        GC.Buy(_param.TypeParam == TypeParam.Local ? _parameterVar.NextLocalCost : _parameterVar.NextCost);
        
        int value = _param.Get(nameParam);
        value += 1;
        // улучшить
        _param.Set(nameParam, value);
    }

    private void OnDisable()
    {
        _param.ClearA(nameParam, Refresh);
    }
}
