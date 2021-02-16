using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class FabricTimer : MonoBehaviour
{
    [NonSerialized]  public System.Action actionEnable = null;
    [SerializeField] private Button btnUpdate;
    [SerializeField] private Image iProgress;
    [SerializeField] private Text txTime;
    [SerializeField] private Text txCost;

    private FactoryTimer _factoryTimer = null;
    private string nameElement;

    public void InitEmpty(string nameElement)
    {
        btnUpdate.onClick.RemoveAllListeners();
        btnUpdate.onClick.AddListener(BuyNew);

        this.nameElement = nameElement;
        
        if (txCost != null)
            txCost.text = $"{GetCost(true)}";
        
        
    }

    private void BuyNew()
    {
        int cost = GetCost(true);
        string inResources = "Orange";
        if (GameController.singleton.CanBuy(cost, inResources))
        {
            GameController.singleton.Buy(cost, inResources);
            GameController.singleton.UpTimerLevel(nameElement);
            
            // init new 
            actionEnable?.Invoke();
        }
    }

    public void Init(FactoryTimer factoryTimer)
    {
        _factoryTimer = factoryTimer;
        btnUpdate.onClick.RemoveAllListeners();
        btnUpdate.onClick.AddListener(Buy);

        switch (_factoryTimer.Resource)
        {
            case "Orange":
                iProgress.color = Color.yellow;
                break;

            case "Red":
                iProgress.color = Color.red;
                break;

            case "Green":
                iProgress.color = Color.green;
                break;

            case "Blue":
                iProgress.color = Color.blue;
                break;

        }

        // подписались на тики
        _factoryTimer.ActionTick -= Visualize;
        _factoryTimer.ActionTick += Visualize;
        Visualize();
    }

    private void Buy()
    {
        int cost = GetCost();
        string inResources = "Orange";
        if (GameController.singleton.CanBuy(cost, inResources))
        {
            GameController.singleton.Buy(cost, inResources);
            GameController.singleton.UpTimerLevel(_factoryTimer.Resource);
            _factoryTimer.UpdateMaxTime();
        }
    }

    void Visualize()
    {
        if (iProgress != null)
            iProgress.fillAmount = _factoryTimer.Seconds / _factoryTimer.MaxSeconds;
        if (txTime != null)
            txTime.text = _factoryTimer.GetTimeLeft();
        if (txCost != null)
            txCost.text = $"{GetCost()}";
    }

    private int GetCost(bool isNew = false)
    {
        int result = 0;

        result = (isNew == false)
            ? GameController.singleton.GetCostTimerLevel(_factoryTimer.Resource) 
            : GameController.singleton.GetCostNew(nameElement);

        return result;
    }

    private void OnDestroy()
    {
        // очищаем все при удалении элементов визуала
        
        if (_factoryTimer != null)
        {
            _factoryTimer.ActionTick -= Visualize;
            _factoryTimer = null;
        }

        btnUpdate.onClick.RemoveAllListeners();
        actionEnable = null;
    }
}
