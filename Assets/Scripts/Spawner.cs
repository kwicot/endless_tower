using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Spawner : MonoBehaviour
{
    public bool IsNewSpawner = false;
    public EnemyView[] EnemiesPrefab;
    public Transform parentPoints;
    private List<int> _enemyList = new List<int>();
    


    public int EnemyPerWave { get; set; }
    public int BossWave { get; set; }
    public int CurrentWave { get; private set; }
    public float PauseBetweenWave { get; set; }
    public float SpawnInterval { get; set; }
    public List<GameObject> L_Enemy { get; set; }


    private Dictionary<int, int> weight = new Dictionary<int, int>();

    List<Vector3> L_SpawnPoints = new List<Vector3>();


    public float Timer;
    public bool CanTime;

    void Start()
    {
        GameController.singleton.spawner = this;
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (CanTime)
        {
            Timer += Time.deltaTime;
            if (Timer > PauseBetweenWave) StartCoroutine(NewWave());
        }
    }
    void Init()
    {
        CurrentWave = 0;
        L_Enemy = new List<GameObject>();
        int child = parentPoints.childCount;
        for (int i = 0; i < child; i++)
        {
            L_SpawnPoints.Add(parentPoints.GetChild(i).position);
        }
        CanTime = true;


        EnemyPerWave = GameController.singleton.EnemyPerWave;
        PauseBetweenWave = GameController.singleton.WaveInterval;
        SpawnInterval = GameController.singleton.SpawnInterval;
        
        // Генерируем список врагов и веса
        // веса
        switch (GameController.singleton.GameDifficult)
        {
            case GameDifficult.Easy:
                weight = new Dictionary<int, int>()
                {
                    {0, 100}
                };
                break;
            case GameDifficult.Normal:
                weight = new Dictionary<int, int>()
                {
                    {0, 85},
                    {1, 15}
                };
                break;
            case GameDifficult.Medium:
                weight = new Dictionary<int, int>()
                {
                    {0, 70},
                    {1, 20},
                    {2, 10},
                };
                break;
            case GameDifficult.Hard:
                weight = new Dictionary<int, int>()
                {
                    {0, 50},
                    {1, 20},
                    {2, 20},
                    {3, 10},
                };
                break;
            case GameDifficult.VeryHard:
                weight = new Dictionary<int, int>()
                {
                    {0, 35},
                    {1, 30},
                    {2, 30},
                    {3, 15},
                };
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        // враги
        foreach (var key in weight.Keys)
        {
            _enemyList.Add(key);
        }
    }

    IEnumerator NewWave()
    {
        CurrentWave++;
        Timer = 0;
        CanTime = false;
        
        for(int i = 0; i< EnemyPerWave ; i++)
        {
            // if (!IsNewSpawner)
            // { //Old spawner
            //     var randomPoint = UnityEngine.Random.Range(0, L_SpawnPoints.Count);
            //     var randomEnemy = UnityEngine.Random.Range(0, EnemiesPrefab.Length - 2); // -2 boss
            //     var obj = Instantiate<EnemyView>(EnemiesPrefab[randomEnemy], L_SpawnPoints[randomPoint], Quaternion.identity);
            //     obj.Init();
            //     GameController.singleton.L_Enemy.Add(obj);
            // }
            // else
            // {  //New spawner
                var randomEnemy = _enemyList.Count == 1 ? _enemyList[0] : Utils.GetRandomOnWeight(weight);
                var randomPoint = L_SpawnPoints[UnityEngine.Random.Range(0, L_SpawnPoints.Count)];
                randomPoint.x += UnityEngine.Random.Range(-1, 1);
                randomPoint.z += UnityEngine.Random.Range(-1, 1);
                var obj = Instantiate<EnemyView>(EnemiesPrefab[randomEnemy], randomPoint, Quaternion.identity);
                obj.Init();
                GameController.singleton.L_Enemy.Add(obj);
            // }


            
            //Debug.Log("Green progress bar fill= " + i / EnemyPerWave);
            yield return new WaitForSeconds(SpawnInterval);
        }

    }
    public void SkipWave()
    {
        StartCoroutine(NewWave());
    }



}
