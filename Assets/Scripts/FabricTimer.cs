using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class FabricTimer : MonoBehaviour
{
    [SerializeField] private Button btnUpdate;
    [SerializeField] private Image iProgress;
    [SerializeField] private Text txTime;

    private FactoryTimer _factoryTimer = null;

    public void Init(FactoryTimer factoryTimer)
    {
        _factoryTimer = factoryTimer;
        btnUpdate.onClick.RemoveAllListeners();
        btnUpdate.onClick.AddListener(() => { _factoryTimer.UpdateMaxTime(); });

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
    }

    void Visualize()
    {
        if (iProgress != null)
            iProgress.fillAmount = _factoryTimer.Seconds / _factoryTimer.MaxSeconds;
        if (txTime != null)
            txTime.text = _factoryTimer.GetTimeLeft();
    }

    private void OnDestroy()
    {
        // очищаем все при удалении элементов визуала
        _factoryTimer.ActionTick -= Visualize;
        _factoryTimer = null;
        btnUpdate.onClick.RemoveAllListeners();
    }
}
