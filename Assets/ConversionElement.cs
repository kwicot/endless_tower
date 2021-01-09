using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class ConversionElement : MonoBehaviour
{
    private GameController GC => GameController.singleton;
    
    // что меняем
    public Text txInRes;
    // иконка что меняем
    public Image iInRes;
    // на что меняем
    public Text txOutRes;
    // иконка на что меняем
    public Image iOutRes;
    // кнопки 1, 10, 100, max, Convert
    public Button[] buttons;
    // title text
    public Text txTitle;
    //
    public InputField cInputField;
    // кол-во ресурса на который меняем
    public Text txProductResources;
    // иконка продукта
    public Image iProductResources;

    // 
    private string ResIn;
    private string ResOut;
    private int koef = 1;

    private void Awake()
    {
        Init("Green");
        GC.GameMoney["Orange"] = 10000;
    }

    public void Init(string resOut, string resIn = "Orange")
    {
        ResIn = resIn;
        ResOut = resOut;

        iInRes.color = GetColor(resIn);
        iOutRes.color = GetColor(resOut);
        iProductResources.color = GetColor(resOut);
        
        // koef
        koef = GC.koeffChange[resOut];
        txInRes.text = $"{koef}x";
        txOutRes.text = $"= {1}x";

        if (buttons != null)
        {
            // 1, 10, 100
            int[] listAmountOut = new[] { 1, 10, 100 };
            for (int i = 0; i < listAmountOut.Length; i++)
            {
                int iterator = i;
                buttons[i].onClick.AddListener(() =>
                {
                    int amountOut = listAmountOut[iterator];
                    int amountIn = amountOut * koef;
                    if (GC.GameMoney[resIn] >= amountIn)
                    {
                        GC.GameMoney[resIn] -= amountIn;
                        GC.GameMoney[resOut] += amountOut;
                        Debug.Log($"{resIn}={GC.GameMoney[resIn]} | {resOut}={GC.GameMoney[resOut]}");
                        RefreshProductResources();
                    }
                });
            }
            
            // max
            buttons[3].onClick.AddListener(() =>
            {
                if (GC.GameMoney[resIn] >= koef)
                {
                    int amountOut = (int) GC.GameMoney[resIn] / koef;
                    int amountIn = amountOut * koef;
                    GC.GameMoney[resIn] -= amountIn;
                    GC.GameMoney[resOut] += amountOut;
                    Debug.Log($"{resIn}={GC.GameMoney[resIn]} | {resOut}={GC.GameMoney[resOut]}");
                    RefreshProductResources();
                }
            });
            
            // convert
            // ...
        }
        
        // refresh txProductResources
        RefreshProductResources();
    }

    private void RefreshProductResources()
    {
        txProductResources.text = $"{GC.GameMoney[ResOut]}";
    }

    private Color GetColor(string element)
    {
        Color color = Color.white;
        switch (element)
        {
            case "Orange":
                color = Color.yellow;
                break;
            case "Red":
                color = Color.red;
                break;
            case "Green":
                color = Color.green;
                break;
            case "Blue":
                color = Color.blue;
                break;
        }

        return color;
    }
}
