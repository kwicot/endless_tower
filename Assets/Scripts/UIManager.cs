using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button Button0;
    public Button Button2;
    public Button Button4;
    public Image GreenProgressBar;
    public Image RedProgressBar;
    public Text CurrentWaveText;
    void Start()
    {
        
    }

    void Update()
    {
        GreenProgressBar.fillAmount = GameController.singleton.L_Enemy.Count / (float)GameController.singleton._Spawner.EnemyPerWave;
        RedProgressBar.fillAmount = GameController.singleton._Spawner.Timer / GameController.singleton._Spawner.PauseBetweenWave;
        CurrentWaveText.text = GameController.singleton._Spawner.CurrentWave.ToString();
    }

    public void ChangeTime(int id)
    {
        Button0.image.color = Color.white;
        Button2.image.color = Color.white;
        Button4.image.color = Color.white;
        switch (id)
        {
            case 0:
                {
                    if (Time.timeScale == 0)
                        Time.timeScale = 1;
                    else
                    {
                        Time.timeScale = 0;
                        Button0.image.color = Color.gray;
                    }
                }
                break;
            case 2:
                {
                    if (Time.timeScale == 2)
                        Time.timeScale = 1;
                    else
                    {
                        Time.timeScale = 2;
                        Button2.image.color = Color.gray;
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
                        Button4.image.color = Color.gray;
                    }
                }
                break;
        }
    }
}
