using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    float hp => GameController.singleton.tower.tower.HP;
    float regen => GameController.singleton.tower.tower.Regeneration;
    void Update()
    {
        WaveGreenProgressBar.fillAmount = GameController.singleton.L_Enemy.Count / (float)GameController.singleton.spawner.EnemyPerWave;
        WaveRedProgressBar.fillAmount = GameController.singleton.spawner.Timer / GameController.singleton.spawner.PauseBetweenWave;
        CurrentWaveText.text = GameController.singleton.spawner.CurrentWave.ToString();
        TowerHealthProgressBar.fillAmount = GameController.singleton.tower.tower.HP / GameController.singleton.tower.tower.HPMax;
        TowerHealthText.text = Mathf.Round(hp).ToString() + "( " + Mathf.Round(regen).ToString() + " )";
        TowerAtackText.text = GameController.singleton.tower.tower.Damage.ToString();
        TowerDefenseText.text = GameController.singleton.tower.tower.Defense.ToString();
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

    private void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        GameController.singleton.GameState = GameState.Game;
    }
}
