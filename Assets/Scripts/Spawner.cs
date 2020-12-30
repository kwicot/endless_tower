using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public int EnemyPerWave { get; set; }
    public int BossWave { get; set; }
    public int CurrentWave { get; }
    public float Timer { get; set; }
    public float PauseBetweenWave { get; set; }
    public List<Enemy> L_Enemy { get; set; }



    List<Vector3> L_SpawnPoints = new List<Vector3>();

    void Start()
    {
        L_Enemy = new List<Enemy>();
        GameObject[] points = GameObject.FindGameObjectsWithTag("SpawnPoint");
        for(int i = 0; i <points.Length -1; i++)
        {
            L_SpawnPoints.Add(points[i].transform.position);
            Destroy(points[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


}
