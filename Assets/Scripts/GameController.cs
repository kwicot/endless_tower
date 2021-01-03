using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController singleton { get; private set; }

    public TowerView _Tower;
    public List<EnemyView> L_Enemy = new List<EnemyView>();


    //Временное
    public float SpawnInterval;
    public float WaveInterval;
    public int EnemyPerWave;
    //Временное



    private void Awake()
    {
        if (!singleton)
        {
            singleton = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(gameObject);
    }


    public void StartGame(int level)
    {
        SceneManager.LoadScene(level);
    }

    public GameState GameState = GameState.Game;
    private float timeCalculate = 0.1f;
    private void Update()
    {
        if (GameState == GameState.Game)
        {
            timeCalculate -= Time.deltaTime;
            if (timeCalculate <= 0f)
            {
                timeCalculate = 0.1f;

                var count = L_Enemy.Count;
                if (count > 0)
                {
                    for (var i = count - 1; i >= 0; i--)
                    {
                        var distance = Vector3.Distance(L_Enemy[i].gameObject.transform.position, _Tower.transform.position);
                        if (distance < 2f)
                        {
                            //var enemyView = L_Enemy[i].GetComponent<EnemyView>();
                            if (L_Enemy[i] != null)
                            {
                                _Tower.Damage(L_Enemy[i].Damage);
                                Destroy(L_Enemy[i].gameObject);
                                L_Enemy.RemoveAt(i);
                            }
                        }
                    }
                }
            }

        }
    }
    
    
}

public enum GameState
{
    MainMenu,
    Game,
    Pause
}
