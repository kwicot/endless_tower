using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowDifficult : MonoBehaviour
{
    public Button[] buttons;

    private void OnEnable()
    {
        if (buttons == null) return;
        for (var i = 0; i < buttons.Length; i++)
        {
            var difficult = i;
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(() => { GameController.singleton.SetDifficeltAndStart(difficult);});
        }
    }
}
