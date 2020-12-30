using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Button button0;
    public Button button2;
    public Button button4;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ButtonPressed(int id)
    {
        button0.image.color = Color.white;
        button2.image.color = Color.white;
        button4.image.color = Color.white;
        switch (id)
        {
            case 0:
                {
                    if (Time.timeScale == 0)
                        Time.timeScale = 1;
                    else
                    {
                        Time.timeScale = 0;
                        button0.image.color = Color.gray;
                    }
                }break;
            case 2:
                {
                    if (Time.timeScale == 2)
                        Time.timeScale = 1;
                    else
                    {
                        Time.timeScale = 2;
                        button2.image.color = Color.gray;
                    }
                }
                break;
            case 4:
                {
                    if (Time.timeScale == 4)
                        Time.timeScale = 1;
                    else
                    {
                        Time.timeScale = 4;
                        button4.image.color = Color.gray;
                    }
                }
                break;
        }
    }
}
