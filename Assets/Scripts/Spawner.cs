using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool IsNewSpawner = false;
    public EnemyView[] EnemiesPrefab;


    


    public int EnemyPerWave { get; set; }
    public int BossWave { get; set; }
    public int CurrentWave { get; private set; }
    public float PauseBetweenWave { get; set; }
    public float SpawnInterval { get; set; }
    public List<GameObject> L_Enemy { get; set; }




    List<Vector3> L_SpawnPoints = new List<Vector3>();


    public float Timer;
    public bool CanTime;

    void Start()
    {
        GameController.singleton._Spawner = this;
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
        GameObject[] points = GameObject.FindGameObjectsWithTag("SpawnPoint");
        for (int i = 0; i < points.Length - 1; i++)
        {
            L_SpawnPoints.Add(points[i].transform.position);
        }
        CanTime = true;


        EnemyPerWave = GameController.singleton.EnemyPerWave;
        PauseBetweenWave = GameController.singleton.WaveInterval;
        SpawnInterval = GameController.singleton.SpawnInterval;

    }

    IEnumerator NewWave()
    {
        CurrentWave++;
        Timer = 0;
        CanTime = false;
        
        for(int i = 0; i< EnemyPerWave ; i++)
        {
            if (!IsNewSpawner)
            { //Old spawner
                var randomPoint = UnityEngine.Random.Range(0, L_SpawnPoints.Count);
                var randomEnemy = UnityEngine.Random.Range(0, EnemiesPrefab.Length - 2); // -2 boss
                var obj = Instantiate<EnemyView>(EnemiesPrefab[randomEnemy], L_SpawnPoints[randomPoint], Quaternion.identity);
                obj.Init();
                GameController.singleton.L_Enemy.Add(obj);
            }
            else
            {  //New spawner
                var randomEnemy = UnityEngine.Random.Range(0, EnemiesPrefab.Length - 2); // -2 boss
                var randomPoint = L_SpawnPoints[UnityEngine.Random.Range(0, L_SpawnPoints.Count)];
                randomPoint.x += UnityEngine.Random.Range(-1, 1);
                randomPoint.z += UnityEngine.Random.Range(-1, 1);
                var obj = Instantiate<EnemyView>(EnemiesPrefab[randomEnemy], randomPoint, Quaternion.identity);
                obj.Init();
                GameController.singleton.L_Enemy.Add(obj);
            }


            
            Debug.Log("Green progress bar fill= " + i / EnemyPerWave);
            yield return new WaitForSeconds(SpawnInterval);
        }

    }
    public void SkipWave()
    {
        StartCoroutine(NewWave());
    }



}
