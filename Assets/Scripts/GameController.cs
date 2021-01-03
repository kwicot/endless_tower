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
    private float timeCalculate2 = 0.1f;
    private void Update()
    {
        EnemyDistanceUpdate();
    }

    void EnemyDistanceUpdate()
    {
        if (GameState == GameState.Game)
        {
            timeCalculate -= Time.deltaTime;
            timeCalculate2 -= Time.deltaTime;
            if (timeCalculate <= 0f) //Enemy logic
            {
                timeCalculate = 0.1f;

                var count = L_Enemy.Count;
                if (count > 0)
                {
                    for (var i = count - 1; i >= 0; i--)
                    {
                        if (L_Enemy[i] != null)
                        {
                            var distance = Vector3.Distance(L_Enemy[i].gameObject.transform.position, _Tower.transform.position);
                            if (distance < 2f)
                            {
                                _Tower.TakeDamage(L_Enemy[i].Damage);
                                Destroy(L_Enemy[i].gameObject);
                                L_Enemy.RemoveAt(i);
                            }
                        }
                    }
                }
            } //Enemy logic end

            if (timeCalculate2 <= 0) //Tower logic
            {
                timeCalculate2 = _Tower.tower.AttackSpeed;
                var count = L_Enemy.Count;
                if (count > 0)
                {
                    var closets = L_Enemy[0];
                    if (count > 1)
                    {
                        for (int i = 1; i < L_Enemy.Count; i++)
                        {
                            if (L_Enemy[i] != null)
                            {
                                if (Vector3.Distance(_Tower.transform.position, L_Enemy[i].transform.position) <
                                    Vector3.Distance(closets.transform.position, _Tower.transform.position))
                                    closets = L_Enemy[i];
                            }
                            else
                            {
                                L_Enemy.RemoveAt(i);
                                i--;
                            }

                        }
                    }
                    var distance = Vector3.Distance(closets.gameObject.transform.position, _Tower.transform.position);
                    if (distance < _Tower.tower.AttackRange)
                    {
                        closets.TakeDamage(_Tower.tower.Damage);
                    }
                }
            } //Tower logic end
        }
    }
    
    public void EnemyKilled(Dictionary<int,int> reward)
    {
        for(int i = 0; i< L_Enemy.Count; i++)
        {
            if(L_Enemy[i] == null)
            {
                L_Enemy.RemoveAt(i);
                i--;
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
