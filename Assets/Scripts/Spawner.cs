using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public Image GreenProgressBar;
    public Image RedProgressBar;


    public int EnemyPerWave { get; set; }
    public int BossWave { get; set; }
    public int CurrentWave { get; }
    public float PauseBetweenWave { get; set; }
    public List<GameObject> L_Enemy { get; set; }



    List<Vector3> L_SpawnPoints = new List<Vector3>();


    float Timer;
    bool CanTime;

    void Start()
    {
        //EnemyPerWave = 100;
        //PauseBetweenWave = 10;


        L_Enemy = new List<GameObject>();
        GameObject[] points = GameObject.FindGameObjectsWithTag("SpawnPoint");
        for(int i = 0; i <points.Length -1; i++)
        {
            L_SpawnPoints.Add(points[i].transform.position);
        }
        CanTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanTime)
        {
            RedProgressBar.fillAmount = Timer / PauseBetweenWave;
            Timer += Time.deltaTime;
            if (Timer > PauseBetweenWave) NewWave();
        }
    }

    IEnumerator NewWave()
    {
        RedProgressBar.fillAmount = 0;
        Timer = 0;
        CanTime = false;
        
        for(int i = 0, x = 0; i< EnemyPerWave ;x++, i++)
        {
            if (x == L_SpawnPoints.Count) x = 0;
            GameObject obj = Instantiate(EnemyPrefab, L_SpawnPoints[x], Quaternion.identity);
            L_Enemy.Add(obj);
            GreenProgressBar.fillAmount = i / EnemyPerWave;
            yield return new WaitForSeconds(0.05f);
        }

    }
    public void SkipWave()
    {
        StartCoroutine(NewWave());
    }


}
