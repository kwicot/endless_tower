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
        btnUpdate.onClick.AddListener(() =>
        {
            _factoryTimer.UpdateMaxTime();
        });

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
        Utilities.Timer.OnTick -= OnTick;
        Utilities.Timer.OnTick += OnTick;
    }

    void OnTick()
    {
        _factoryTimer.Seconds--;

        if (GameController.singleton.GameState == GameState.MainMenu)
        {
            //TODO: переделать на состояние игры а не "iProgress != null"
        }
        else if (GameController.singleton.GameState == GameState.Game)
        {
            //
        }

        if (_factoryTimer.Seconds > 0)
        {
            if (iProgress != null)
                iProgress.fillAmount = _factoryTimer.Seconds / _factoryTimer.MaxSeconds;
            if (txTime != null)
                txTime.text = _factoryTimer.GetTimeLeft();
        }
        else
        {
            Restart();
        }
    }

    void Restart()
    {
        FactoryScript.AddReward(_factoryTimer.Resource, _factoryTimer.Reward);
        _factoryTimer.Seconds = _factoryTimer.MaxSeconds;
        
        Debug.Log("Reward- " + GameController.singleton.GameMoney[_factoryTimer.Resource]);
    }

    private void OnDestroy()
    {
        // почемуто сюда не заходит :(
        
        Utilities.Timer.OnTick -= OnTick;
        btnUpdate.onClick.RemoveAllListeners();
    }
}
