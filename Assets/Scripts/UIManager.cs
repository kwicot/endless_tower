using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button TimeButton0;
    public Button TimeButton2;
    public Button TimeButton4;
    public Image WaveGreenProgressBar;
    public Image WaveRedProgressBar;
    public Image TowerHealthProgressBar;
    public Text TowerHealthText;
    public Text TowerAtackText;
    public Text TowerDefenseText;
    public Text CurrentWaveText;
    void Start()
    {
        
    }

    void Update()
    {
        WaveGreenProgressBar.fillAmount = GameController.singleton.L_Enemy.Count / (float)GameController.singleton._Spawner.EnemyPerWave;
        WaveRedProgressBar.fillAmount = GameController.singleton._Spawner.Timer / GameController.singleton._Spawner.PauseBetweenWave;
        CurrentWaveText.text = GameController.singleton._Spawner.CurrentWave.ToString();
        TowerHealthProgressBar.fillAmount = GameController.singleton._Tower.tower.HP / GameController.singleton._Tower.tower.HPMax;
        TowerHealthText.text = GameController.singleton._Tower.tower.HP.ToString();
    }

    public void ChangeTime(int id)
    {
        TimeButton0.image.color = Color.white;
        TimeButton2.image.color = Color.white;
        TimeButton4.image.color = Color.white;
        switch (id)
        {
            case 0:
                {
                    if (Time.timeScale == 0)
                        Time.timeScale = 1;
                    else
                    {
                        Time.timeScale = 0;
                        TimeButton0.image.color = Color.gray;
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
                        TimeButton2.image.color = Color.gray;
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
                        TimeButton4.image.color = Color.gray;
                    }
                }
                break;
        }
    }
}
