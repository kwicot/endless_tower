using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowFail : MonoBehaviour
{
    public Text txTitle;
    public Text txPoint;
    public Button btTown;
    public Button btAgain;

    private void Awake()
    {
        btTown?.onClick.RemoveAllListeners();
        btTown?.onClick.AddListener(() =>
        {
            GameController.singleton.DestroyAndClearEnemyList();
            GameController.singleton.LoadLevel(0);
        });
        
        btAgain?.onClick.RemoveAllListeners();
        btAgain?.onClick.AddListener(() =>
        {
            GameController.singleton.DestroyAndClearEnemyList();
            GameController.singleton.LoadLevel(1);  
        });
    }

    private void OnEnable()
    {
        txPoint.text = $"Вы набрали {GameController.singleton.Points} очков";
    }
}
