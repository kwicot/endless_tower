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
    private GameController GC => GameController.singleton;


    //TODO количество врагов зависит от волны
    public int EnemyPerWave => GC.EnemyPerWave;
    public int BossWave => GC.SettingWave.BossInWaves;
    public int CurrentWave { get; private set; }
    public float PauseBetweenWave => GC.WaveInterval;
    public float SpawnInterval => GC.SpawnInterval;
    public List<GameObject> L_Enemy = new List<GameObject>();


    private Dictionary<int, int> weight = new Dictionary<int, int>();

    List<Vector3> L_SpawnPoints = new List<Vector3>();


    public float Timer;
    public bool CanTime;

    void Start()
    {
        GC.spawner = this;
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
        L_Enemy.Clear();
        L_SpawnPoints.Clear();
        int child = parentPoints.childCount;
        for (int i = 0; i < child; i++)
        {
            // на самом деле можно брать непосредственно из parentPoints.GetChild
            L_SpawnPoints.Add(parentPoints.GetChild(i).position);
        }
        CanTime = true;


        
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
        Debug.Log(GC.GameDifficult);
        CurrentWave++;
        Timer = 0;
        CanTime = false;

        for (int i = 0; i < EnemyPerWave; i++)
        {

            var randomEnemy = _enemyList.Count == 1 ? _enemyList[0] : Utils.GetRandomOnWeight(weight);
            var randomPoint = GetRandomPoint();
            var obj = Instantiate<EnemyView>(EnemiesPrefab[randomEnemy], randomPoint, Quaternion.identity);
            obj.Init();
            GC.L_Enemy.Add(obj);




            //Debug.Log("Green progress bar fill= " + i / EnemyPerWave);
            yield return new WaitForSeconds(SpawnInterval);
        }

        // boss
        if (CurrentWave % BossWave == 0)
        {
            var randomPoint = GetRandomPoint();
            var obj = Instantiate<EnemyView>(EnemiesPrefab[_enemyList.Count - 1], randomPoint, Quaternion.identity);
            obj.Init();
            GC.L_Enemy.Add(obj);
        }
    }

    /// <summary>
    /// получить рандомную точку из списка возможных
    /// </summary>
    /// <returns></returns>
    private Vector3 GetRandomPoint()
    {
        var randomPoint = L_SpawnPoints[UnityEngine.Random.Range(0, L_SpawnPoints.Count)];
        randomPoint.x += UnityEngine.Random.Range(-1, 1);
        randomPoint.z += UnityEngine.Random.Range(-1, 1);

        return randomPoint;
    }

    public void SkipWave()
    {
        StartCoroutine(NewWave());
    }



}
